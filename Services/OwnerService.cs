using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Helpers;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAdminPortal.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly GenerateRandomNumberHelper _randomNumberHelper;
        private readonly PasswordHashingHelper _passwordHashingHelper;
        private readonly ILogger<OwnerService> _logger;

        public OwnerService(ApplicationDbContext dbContext, GenerateRandomNumberHelper randomNumberHelper, PasswordHashingHelper passwordHashingHelper, ILogger<OwnerService> logger) 
        {
            _dbContext = dbContext;
            _randomNumberHelper = randomNumberHelper;
            _passwordHashingHelper = passwordHashingHelper;
            _logger = logger;
        }

        public async Task<List<OwnerResponseDto>> GetAllOwner()
        {
           var owners = await _dbContext.Owners.ToListAsync();

            var ownerResponseDtos = owners.Select(owner => new OwnerResponseDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                BusinessName = owner.BusinessName,
                Phone = owner.Phone,
                Email = owner.Email,
                MembershipNumber = owner.MembershipNumber,

            }).ToList();

            _logger.LogInformation("The owner data fetched successfully");
            

            return ownerResponseDtos;
        }

        public async Task<OwnerResponseDto?> GetOwnerById(Guid ownerId)
        {
            Owner? owner = await _dbContext.Owners.FirstOrDefaultAsync(e => e.Id == ownerId);

            if (owner == null) 
            {
                return null;
            }

            var ownerResponseDtos = new OwnerResponseDto
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                BusinessName = owner.BusinessName,
                Phone = owner.Phone,
                Email = owner.Email,
                MembershipNumber = owner.MembershipNumber,
            };

            return ownerResponseDtos;
        }

        public async Task<Owner?> AddOwner(AddOwnerDto addOwnerDto)
        {
            var hashedPassword = "";

            if (addOwnerDto.Password != null) 
            {
                // Hash the password
                hashedPassword = _passwordHashingHelper.HashPassword(addOwnerDto.Password);
            }
           

            Owner owner = new Owner()
            {
                FirstName = addOwnerDto.FirstName,
                LastName = addOwnerDto.LastName,
                Phone = addOwnerDto.Phone,
                Email = addOwnerDto.Email,
                Password = hashedPassword,
                ConfirmPassword = hashedPassword,
                BusinessName = addOwnerDto.BusinessName,
                MembershipNumber = GenerateUniqueMembershipNumber()
            };

            await _dbContext.Owners.AddAsync(owner);
           await  _dbContext.SaveChangesAsync();

            return owner;
        }
       
        public async Task<Owner?> UpdateOwner(Guid ownerId, UpdateOwnerDto updateOwnerDto)
        {
            Owner? owner = await _dbContext.Owners.FirstOrDefaultAsync(e => e.Id == ownerId);

            if (owner == null)
            {
                return null;
            }

            owner.FirstName = updateOwnerDto.FirstName;
            owner.LastName = updateOwnerDto.LastName;
            owner.Phone = updateOwnerDto.Phone;
            owner.Email = updateOwnerDto.Email;
            owner.BusinessName = updateOwnerDto.BusinessName;
            owner.IsActive = updateOwnerDto.IsActive;

            await _dbContext.SaveChangesAsync();

            return owner;
        }

        public async Task<Owner?> DeleteOwner(Guid ownerId)
        {
           var owner = await _dbContext.Owners.FindAsync(ownerId);

            if (owner is null)
            {
                return null;
            }

             _dbContext.Owners.Remove(owner);

            await _dbContext.SaveChangesAsync();

            return owner;
        }


        public string GenerateUniqueMembershipNumber()
        {
            string membershipNumber;
            bool exists;

            do
            {
                membershipNumber = _randomNumberHelper.GenerateMembershipNumber();
                exists = _dbContext.Owners.Any(o => o.MembershipNumber == membershipNumber);

            } while (exists);

            return membershipNumber;
        }


    }
}
