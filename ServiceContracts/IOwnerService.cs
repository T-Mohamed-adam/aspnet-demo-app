using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.Models.Entities;

namespace EmployeeAdminPortal.ServiceContracts
{
    public interface IOwnerService
    {
        public Task<List<OwnerResponseDto>> GetAllOwner();
        public Task<OwnerResponseDto?> GetOwnerById(Guid ownerId);

   
        public Task<Owner?> AddOwner(AddOwnerDto addOwnerDto);

        public Task<Owner?> UpdateOwner(Guid ownerId, UpdateOwnerDto updateOwnerDto);

        public Task<Owner?> DeleteOwner(Guid ownerId);
    }
}
