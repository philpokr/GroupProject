using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroupProject.Models;

namespace GroupProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HobbiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HobbiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Hobbies
        [HttpGet]
        public ActionResult<IEnumerable<Hobby>> GetHobbies()
        {
            return _context.Hobbies.ToList();
        }

        // GET: api/Hobbies/5
        [HttpGet("{id}")]
        public ActionResult<Hobby> GetHobby(int id)
        {
            var hobby = _context.Hobbies.Find(id);

            if (hobby == null)
            {
                return NotFound();
            }

            return hobby;
        }

        // POST: api/Hobbies
        [HttpPost]
        public IActionResult PostHobby(Hobby hobby)
        {
            _context.Hobbies.Add(hobby);
            _context.SaveChanges();

            return CreatedAtAction("GetHobby", new { id = hobby.HobbyId }, hobby);
        }

        // PUT: api/Hobbies/5
        [HttpPut("{id}")]
        public IActionResult PutHobby(int id, Hobby hobby)
        {
            if (id != hobby.HobbyId)
            {
                return BadRequest();
            }

            _context.Entry(hobby).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HobbyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Hobbies/5
        [HttpDelete("{id}")]
        public IActionResult DeleteHobby(int id)
        {
            var hobby = _context.Hobbies.Find(id);

            if (hobby == null)
            {
                return NotFound();
            }

            _context.Hobbies.Remove(hobby);
            _context.SaveChanges();

            return NoContent();
        }

        private bool HobbyExists(int id)
        {
            return _context.Hobbies.Any(e => e.HobbyId == id);
        }
    }
}

