using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroupProject.Models;

namespace GroupProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteFoodsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoriteFoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteFoods
        [HttpGet]
        public ActionResult<IEnumerable<FavoriteFoods>> GetFavoriteFoods()
        {
            return _context.FavoriteFoods.ToList();
        }

        // GET: api/FavoriteFoods/5
        [HttpGet("{id}")]
        public ActionResult<FavoriteFoods> GetFavoriteFood(int id)
        {
            var favoriteFood = _context.FavoriteFoods.Find(id);

            if (favoriteFood == null)
            {
                return NotFound();
            }

            return favoriteFood;
        }

        // POST: api/FavoriteFoods
        [HttpPost]
        public IActionResult PostFavoriteFood(FavoriteFoods favoriteFood)
        {
            _context.FavoriteFoods.Add(favoriteFood);
            _context.SaveChanges();

            return CreatedAtAction("GetFavoriteFood", new { id = favoriteFood.FoodId }, favoriteFood);
        }

        // PUT: api/FavoriteFoods/5
        [HttpPut("{id}")]
        public IActionResult PutFavoriteFood(int id, FavoriteFoods favoriteFood)
        {
            if (id != favoriteFood.FoodId)
            {
                return BadRequest();
            }

            _context.Entry(favoriteFood).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteFoodExists(id))
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

        // DELETE: api/FavoriteFoods/5
        [HttpDelete("{id}")]
        public IActionResult DeleteFavoriteFood(int id)
        {
            var favoriteFood = _context.FavoriteFoods.Find(id);

            if (favoriteFood == null)
            {
                return NotFound();
            }

            _context.FavoriteFoods.Remove(favoriteFood);
            _context.SaveChanges();

            return NoContent();
        }

        private bool FavoriteFoodExists(int id)
        {
            return _context.FavoriteFoods.Any(e => e.FoodId == id);
        }
    }
}
