using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyAPI.Models;
using StudyAPI.Models.data;
using StudyAPI.Models.Request;

namespace StudyAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeMoCRUDController : ControllerBase
    {
        // gọi db
        private readonly AppDbContext de;

        public DeMoCRUDController(AppDbContext context)
        {
            de = context;
        }
        // gọi db
        [HttpGet("[Action]")]
        public ActionResult GetListProduct()
        {
            
            var listp = de.products.AsNoTracking().ToList();
            foreach (var product in listp)
            {
               product.Category = de.Categories.FirstOrDefault(p=>p.Id == product.CategoryId);
            }
            return Ok(new {check= true,ms = "haha",data = listp });
        }
        [HttpPost("[Action]")]
        public ActionResult PostProduct([FromForm] ProductRequest model)
        {
            if(string.IsNullOrEmpty(model.Name))
                return Ok(new { check = false, ms = "Enter Name!" });
            var p = new Product()
            {
                ProductId = Guid.NewGuid(),
                Name = model.Name,
                CategoryId = model.CategoryId,
                Img = model.Img,
                Quantity = model.Quantity,  
                Description = model.Description,    
            };

            de.Add(p);
            de.SaveChanges();

            return Ok(new { check = true, ms = "Create product success!", data = p });
        }
        [HttpPut("[Action]")]
        public ActionResult UpdateProduct(ProductRequest model)
        {
            try
            {
                if (model.ProductId == null || model.ProductId == Guid.Empty)
                    return Ok(new { check = false, ms = "Invalid Id" });

                var pro = de.products.FirstOrDefault(p => p.ProductId == model.ProductId);
                if (pro == null)
                    return Ok(new { check = false, ms = "Invalid product or Invalid Id" });

                pro.Name = model.Name;
                if(model.CategoryId == null || model.CategoryId == Guid.Empty)
                    return Ok(new { check = false, ms = "Invalid category Id" });
                var Cate = de.Categories.FirstOrDefault(p => p.Id == model.CategoryId);
               /* if (Cate == null)
                    return Ok(new { check = false, ms = "Invalid Cate or Invalid CateId" });*/
                if(Cate!= null)
                    pro.CategoryId = model.CategoryId;

                pro.Img = model.Img;
                pro.Quantity = model.Quantity;
                pro.Description = model.Description;

                de.SaveChanges();
                return Ok(new { check = true, ms = "Update product success!", data = pro });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException?.Message);
            }

        }
        [HttpDelete("[Action]")]
        public ActionResult DeleteProduct(ProductRequest model)
        {
            try
            {
                if (model.ProductId == null || model.ProductId == Guid.Empty)
                    return Ok(new { check = false, ms = "Invalid Id" });

                var pro = de.products.FirstOrDefault(p => p.ProductId == model.ProductId);
                if (pro == null)
                    return Ok(new { check = false, ms = "Invalid product or Invalid Id" });

                de.Remove(pro);

                de.SaveChanges();
                return Ok(new { check = true, ms = "Delete product success!"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException?.Message);
            }
        }
        [HttpPatch("[Action]")]
        public ActionResult PatchUpProduct()
        {
            return Ok(new { ms = "haha", data = "123123" });
        }
        

    }
}
