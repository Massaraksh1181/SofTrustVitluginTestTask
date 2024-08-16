using MailClientTestDomain;
using MailClientTestDomain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MailClientTestApi.Controllers
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
