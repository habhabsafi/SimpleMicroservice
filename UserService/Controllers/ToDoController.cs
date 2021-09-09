using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Database;
using UserService.Database.Entities;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        DatabaseContext db;

        public ToDoController()
        {
            db = new DatabaseContext();
        }
        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return db.ToDos.ToList();

        }

        // GET api/<ToDo>/5
        [HttpGet("{id}")]
        public ToDo Get(int id)
        {
            return db.ToDos.FirstOrDefault(c=>c.ID==id);
        }

        // POST api/<ToDo>
        [HttpPost]
        public IActionResult Create([FromBody] ToDo model)
        {
            try
            {
                db.ToDos.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] ToDo model)
        {
            try
            {
                //db.ToDos.Find(model);
                db.ToDos.Update(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingToDo = db.ToDos.FirstOrDefault(c => c.ID == id); 
                db.ToDos.Remove(existingToDo);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
