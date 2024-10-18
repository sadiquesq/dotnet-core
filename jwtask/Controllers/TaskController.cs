using jwtask.models;
using jwtask.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwtask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;

        public TaskController(ITaskServices taskServices)
        {
            _taskServices = taskServices;
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromBody] TaskItem task)
        {
            _taskServices.CreateTask(task);
            return NoContent();
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskServices.GetAllTasks());
        }

        [Authorize(Roles = "admin,user")]
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var t = _taskServices.GetTaskById(id);
            if (t == null)
            {
                return NotFound();
            }
            return Ok(t);
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult UpdateTask([FromBody] TaskItem task)
        {
            _taskServices.UpdateTask(task);
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _taskServices.DeleteTask(id);
            return NoContent();
        }
    }
}
