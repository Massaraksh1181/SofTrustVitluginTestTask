using MailClientTestDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MailClientTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControllerBaseApi : ControllerBase
    {
        protected AppDbContext Context { get; private set; }

        protected ControllerBaseApi(AppDbContext context)
        {
            Context = context;
        }
    }
}
