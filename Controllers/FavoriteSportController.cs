using Microsoft.AspNetCore.Mvc;
using GroupProject.Models;

namespace GroupProject.Controllers;

[ApiController]
[Route("[controller]")]
public class FavoriteSportController : ControllerBase
{
    private readonly ILogger<FavoriteSportController> _logger;
    private readonly ApplicationDbContext _context;

    public FavoriteSportController(ILogger<FavoriteSportController> logger, ApplicationDbContext context)

    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("ByNumOfPlayers")]
    [ProducesResponseType(typeof(FavoriteSport), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetByNumPlay(int? numPlay)
    {
        if (numPlay == null || numPlay == 0) return Ok(_context.FavoriteSports?.ToList().Take(5));
        try
        {
            var sport = _context.FavoriteSports.Find(numPlay);
            if (sport == null)
            {
                return NotFound("The requested resource was not found");
            }
            return Ok(sport);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(typeof(List<FavoriteSport>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        try
        {
            if (_context.FavoriteSports == null || !_context.FavoriteSports.Any())
                return NotFound("No FavoriteSports found in the database");
            return Ok(_context.FavoriteSports?.ToList());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int numPlay)
    {
        try
        {
            var sport = _context.FavoriteSports?.Find(numPlay);
            if (sport == null)
            {
                return NotFound($"FavoriteSports with {numPlay} Players was not found");
            }

            _context.FavoriteSports?.Remove(sport);
            var result = _context.SaveChanges();
            if (result >= 1)
            {
                return Ok("Delete operation was successful");
            }
            return Problem("Delete was not successful. Please try again");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Add(FavoriteSport newSport)
    {
        if (newSport.NumOfPlayers != 0)
        {
            return BadRequest("The Number of players was provided but not needed");
        }
        try
        {
            _context.FavoriteSports?.Add(newSport);
            var result = _context.SaveChanges();
            if (result >= 1)
            {
                return Ok($"Sport {newSport.SportTitle} added successfully");
            }
            return Problem("Add was not successful. Please try again");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Put(FavoriteSport sportToEdit)
    {
        if (sportToEdit.NumOfPlayers < 1)
        {
            return BadRequest("Please provide a valid Salary");
        }

        try
        {
            var sport = _context.FavoriteSports?.Find(sportToEdit.NumOfPlayers);
            if (sport == null)
                return NotFound("The SPort was not found");

            sport.SportTitle = sportToEdit.SportTitle;
            sport.FieldsOfPlay = sportToEdit.FieldsOfPlay;
            sport.NumOfPlayers = sportToEdit.NumOfPlayers;
            sport.LastTimeWatchedOrPlayed = sportToEdit.LastTimeWatchedOrPlayed;

            _context.FavoriteSports?.Update(sport);
            var result = _context.SaveChanges();
            if (result >= 1)
            {
                return Ok("Sport edited successfully");
            }
            return Problem("Edit was not successful. Please try again");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
