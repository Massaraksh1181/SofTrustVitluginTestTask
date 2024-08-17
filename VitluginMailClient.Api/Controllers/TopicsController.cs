using VitluginMailClientDomain;
using VitluginMailClientDomain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VitluginMailClientApi.Controllers
{
    public class TopicsController : ControllerBaseApi
    {
        public TopicsController(AppDbContext context) : base(context)
        {
        }

        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            var topics = Context.Topics.ToList();
            return Ok(topics);
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] Topic newTopic)
        {
            if (newTopic == null || string.IsNullOrEmpty(newTopic.Name))
            {
                return BadRequest("Invalid topic data.");
            }

            Context.Topics.Add(newTopic);
            Context.SaveChanges();

            return Ok(newTopic);
        }
    }
}
