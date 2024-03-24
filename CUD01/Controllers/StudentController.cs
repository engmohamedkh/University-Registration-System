using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.Core.DTO;
using University.Core.Interfaces;
using University.Core.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CUD01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IStudentService studentService;
        IDepartmentService departmentService;


        public StudentController(IStudentService _studentService,IDepartmentService _departmentService)
        {
            studentService = _studentService;
            departmentService=_departmentService;

        }
        [HttpGet("GetAll")]
        public  async Task<IActionResult> GetAll()
        {
            return Ok( await studentService.GetAll());
        }


        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await studentService.GetStudentById(id));
        }

            // POST api/<StudentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentDTO std)
        {
            if (ModelState.IsValid)
            {
                await studentService.CreateAsync(std);
                string url = Url.Link("GetStudentById", new { id = std.StudentId });
                return Created(url, std);

            }
            else
            {
                return BadRequest(new { msg = "error!!", Errors = ModelState });
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] StudentDTO std)
        {

            if (ModelState.IsValid)
            {
                await studentService.UpdateAsync(std);
                string url = Url.Link("GetStudentById", new { id = std.StudentId });
                return Ok();

            }
            else
            {
                return BadRequest(new { msg = "error!!", Errors = ModelState });
            }
        }


        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await studentService.DeleteAsync(id);
        }
    }
}
