using Microsoft.AspNetCore.Mvc;

namespace Smiosoft.PASS.Examples.AspNetCore.Controllers.V1
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class V1ControllerBase : ControllerBase
    {
        protected IPass Pass { get; }

        public V1ControllerBase(IPass pass)
        {
            Pass = pass;
        }
    }
}
