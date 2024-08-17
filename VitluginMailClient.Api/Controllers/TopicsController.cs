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
    }
}
