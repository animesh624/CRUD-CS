using CURD_APP.Data;
using CURD_APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
//namespace keyword is used to declare a scope that contains a set of related objects. 
namespace CURD_APP.Controllers
{
    [Route("api/v1")]
    //[ApiController]  // this tells that this class is an API controller
    public class APIController : ControllerBase  // manage operations related to controller
    {
        private readonly ApplicationDbContext _db;

        public APIController(ApplicationDbContext db)
        {
            _db = db;
        }
        //[Authorize]
        [HttpGet]
        public ActionResult<List<Model1>> getItems()
        {
            List<Model1> items = _db.Dish.ToList();
            return Ok(new Response<List<Model1>>("Items retrieved successfully.", items));
        }
        //[Authorize]
        [HttpGet("{id:int}")]
        public ActionResult<Model1> getItem(int? id)
        {
            if (id == null || !ModelState.IsValid)
            {
                return BadRequest("Please Provide Valid ID.");
            }
            Model1 item = _db.Dish.FirstOrDefault(u => u.Id == id);

            if (item == null)
            {
                return NotFound("Given ID is not present in Database");
            }

            return Ok(new Response<Model1>("Item retrieved successfully.", item));
        }
        //[Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteItem(int? id)
        {
            if(id==null)
            {
                return BadRequest("Please Provide Valid ID.");
            }
            Model1 item = _db.Dish.FirstOrDefault(u => u.Id == id);

            if (item==null)
            {
                return NotFound("Given ID is not present in Database");
            }
            _db.Dish.Remove(item);
            _db.SaveChanges();

            return Ok(new Response<Model1>("Item deleted successfully.", item));
        }
        //[Authorize]
        [HttpPost]
        public IActionResult CreateItem([FromBody]Model1 obj)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please Provide Valid Item values");
            }
            if (obj.Id>0)
            {
                return BadRequest("Please do not Give ID");
            }
            _db.Dish.Add(obj);
            _db.SaveChanges();

            return Ok(new Response<Model1>("Item created successfully.", obj));
        }
        //[Authorize]
        [HttpPut("update/{id:int}")]
        public IActionResult UpdateItem(int? id, [FromBody] Model1 obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please Provide Valid set of values to Update");
            }
            if (id == null)
            {
                return BadRequest("ID is Inappropriate");
            }

            Model1 item = _db.Dish.FirstOrDefault(u => u.Id == id);

            if (item == null)
            {
                return NotFound("Given Item is not present to get Updated");
            }
            _db.Entry(item).State = EntityState.Detached;
            _db.Dish.Update(obj);
            if (!ModelState.IsValid)
            {
                return BadRequest("Please Provide Valid set of values to Update");
            }

            _db.SaveChanges();

            return Ok(new Response<Model1>("Item updated successfully.",obj));
        }
    }
    public class Response<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        
        public Response(string message, T data)
        {
            
            Message = message;
            Data = data;
        }
    }
}
