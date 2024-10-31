
namespace Survey.API.Controllers
{
    [Route("Errors/{StatusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        public ActionResult Index(int StatusCode)
        {
            var res = new CustomeErrorResponse()
            {
                StatusCode = StatusCode,
            };

            return NotFound(res);
        }
    }
}
