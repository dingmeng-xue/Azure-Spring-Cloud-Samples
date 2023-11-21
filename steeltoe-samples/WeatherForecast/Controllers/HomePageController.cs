using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Azure.SpringApps.Sample.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomePageController : ControllerBase
    {

        [HttpGet]
        public ContentResult Get()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<h1>Hello World!</h1>");

            sb.Append("<h1>HTTP Request</h1>");
            sb.Append("<ul>");
            foreach (var header in Request.Headers)
            {
                sb.Append($"<li>{header.Key}={header.Value}</li>");
            }
            sb.Append("</ul>");

            return Content(sb.ToString(), "text/html");
        }
    }
}
