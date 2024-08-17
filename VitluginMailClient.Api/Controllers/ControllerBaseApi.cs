using VitluginMailClientDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VitluginMailClientApi.Controllers
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
