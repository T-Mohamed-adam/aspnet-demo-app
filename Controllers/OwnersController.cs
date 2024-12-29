using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Models.Dtos;
using EmployeeAdminPortal.ServiceContracts;
using Rotativa.AspNetCore;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/owners
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OwnersController : Controller
    {
        private readonly IOwnerService _ownerService;

        // Inject owner service
        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet("pdf")]
        public async Task<IActionResult> OwnerPDF()
        {
            // Get list of persons
            var owners = await _ownerService.GetAllOwner(); // Fetch the data
            return new ViewAsPdf("OwnerPDF", owners, ViewData) 
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins() 
                {
                    Top = 20, Right = 20, Left = 20, Bottom = 20
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
            }; // Pass the data to the view as pdf
        }

        // Get all owners
        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _ownerService.GetAllOwner();

            return Ok(new {Success = "Ok", Message = "Owner data fetch successfully", Data = owners});
        }

        // Get owner by its id

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult?> GetOwnerById(Guid id)
        {
            var owner = await _ownerService.GetOwnerById(id);

            if (owner is null)
            {
                return NotFound("Owner not found");
            }

            return Ok(owner);

        }

        // Add new owner
        [HttpPost]
        public async Task<IActionResult> AddOwner([FromForm] AddOwnerDto addOwnerDto)
        {
            try
            {
                Owner? owner = await _ownerService.AddOwner(addOwnerDto);
                return Ok(owner);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // Update owner by its id
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateOwner(Guid id, [FromForm] UpdateOwnerDto updateOwnerDto) 
        {
            try
            {
                Owner? owner = await _ownerService.UpdateOwner(id, updateOwnerDto);

                return Ok(owner);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete specific owner data
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult?> DeleteEmployee(Guid id)
        {
            Owner? owner = await _ownerService.DeleteOwner(id);

            if (owner is null)
            {
                return NotFound("Owner not found");
            }

            return Ok("Owner deleted successfully");
        }
    }
}
