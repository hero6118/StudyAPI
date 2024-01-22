using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeMoCRUDController : ControllerBase
    {
        [HttpGet("[Action]")]
        public ActionResult GetListProduct()
        {
            return Ok(new {ms = "haha",data = "123123"});
        }
        [HttpPost("[Action]")]
        public ActionResult PostProduct()
        {
            return Ok(new { ms = "haha", data = "123123" });
        }
        [HttpPut("[Action]")]
        public ActionResult UpdateProduct()
        {
            return Ok(new { ms = "haha", data = "123123" });
        }
        [HttpDelete("[Action]")]
        public ActionResult DeleteProduct()
        {
            return Ok(new { ms = "haha", data = "123123" });
        }
        [HttpPatch("[Action]")]
        public ActionResult PatchUpProduct()
        {
            return Ok(new { ms = "haha", data = "123123" });
        }
        

    }
}
