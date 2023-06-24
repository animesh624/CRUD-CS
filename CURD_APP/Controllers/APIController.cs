using CURD_APP.Data;
using CURD_APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        [HttpGet]
        public ActionResult<List<Model1>> getItems()
        {
            List<Model1> items = _db.Dish.ToList();
            return Ok(items);
        }
        [HttpGet("{id:int}")]
        public ActionResult<Model1> getItem(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Model1 item = _db.Dish.FirstOrDefault(u => u.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteItem(int? id)
        {
            if(id==null)
            {
                return BadRequest();
            }
            Model1 item = _db.Dish.FirstOrDefault(u => u.Id == id);

            if (item==null)
            {
                return NotFound();
            }
            _db.Dish.Remove(item);
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody]Model1 obj)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (obj.Id>0)
            {
                return BadRequest();
            }
            //List<Model1> list = _db.Dish.ToList();
            //if (list.Count == 0)
            //{
            //    obj.Id = 1;
            //}
            //else
            //{
            //    int largestId = list.OrderByDescending(item => item.Id).Select(item => item.Id).FirstOrDefault();
            //    obj.Id = largestId + 1; 
            //}
            _db.Dish.Add(obj);
            _db.SaveChanges();

            return Ok(obj);
        }

        [HttpPut]
        public IActionResult UpdateItem(int? id,[FromBody] Model1 obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id==null)
            {
                return BadRequest();
            }
      
            Model1 item=_db.Dish.FirstOrDefault(u=>u.Id == id);
            if(item==null)
            {
                return NotFound();
            }

            item=

            return Ok(obj);
        }

    }
}
