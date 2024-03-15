using KYC360.WebAPI.Interfaces;
using KYC360.WebAPI.Model.DTOs.Entity;
using Microsoft.AspNetCore.Mvc;

namespace KYC360.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IEntityServices _entityServices;
        public EntityController(IEntityServices entityServices)
        {
            _entityServices = entityServices;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEntities([FromQuery] String? search=null)
        {
            try
            {
                var entities = await _entityServices.GetAllEntities(search);
                return Ok(entities);
            }
            catch
            {
                return BadRequest("Could not process request");
            }

        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetEntityById(string id)
        {
            try
            {
                var entity = await _entityServices.GetEntityById(id);
                return Ok(entity);
            }
            catch
            {
                return BadRequest("Could not process request");
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddCourse([FromBody] EntityRequestDTO entityRequest)
        {

            try
            {
                var entity = await _entityServices.CreateEntity(entityRequest);
                if (entity == false)
                {
                    return BadRequest("Course could not be created");
                }
                return Ok("Course Created Successfully!!!");
            }
            catch
            {
                return BadRequest("Could not process request");
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse([FromBody] EntityUpdateDTO entityUpdateDTO)
        {
            try
            {
                var entity = await _entityServices.UpdateEntity(entityUpdateDTO);
                if (entity == false)
                {
                    return BadRequest("Could not update course");
                }
                return Ok("Updated Successfully");
            }
            catch
            {
                return BadRequest("Could not process request");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(string id)
        {
            try
            {
                var entity = await _entityServices.DeleteEntity(id);
                if (entity == false)
                {
                    return BadRequest("Could not delete course");
                }
                return Ok("Successfully Deleted");
            }
            catch
            {
                return BadRequest("Something went wrong, could not delete");
            }
        }

        [HttpGet("filtered")]
        public async Task<ActionResult> GetAdvancedFilteredEntities([FromQuery] string search = null,
            [FromQuery] string gender = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] List<string> countries = null)
        {
            try
            {
                var entities = await _entityServices.GetAdvancedEntities(search, gender, startDate, endDate, countries);
                return Ok(entities);
            }
            catch
            {
                return BadRequest("Could not process request");
            }


        }
    }
}
