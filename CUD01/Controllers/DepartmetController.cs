using CUD01.Filters;
using Microsoft.AspNetCore.Mvc;
using University.Core.DTO;
using University.Core.Interfaces;
using University.Core.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CUD01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAll());
        }
        [HttpGet("GetAllWithStudent")]
        public async Task<IActionResult> GetAllWithStudent()
        {
            return Ok(await _departmentService.GetAllWithStudent());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _departmentService.GetDepartmentById(id));
        }

        [HttpPost]
        //[ValidationLocation]
        public async Task<IActionResult> Post([FromBody] DepartmentDTO department)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.CreateAsync(department);
                string url = Url.Link("GetDepartmentById", new { id = department.DepartmentId });
                return Created(url, department);
            }
            else
            {
                return BadRequest(new { msg = "error!!", Errors = ModelState });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DepartmentDTO department)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.UpdateAsync(department);
                string url = Url.Link("GetDepartmentById", new { id = department.DepartmentId });
                return Ok();
            }
            else
            {
                return BadRequest(new { msg = "error!!", Errors = ModelState });
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _departmentService.DeleteAsync(id);
        }
    }
}
