using Microsoft.AspNetCore.Mvc;
using GroupProject.Models;

namespace GroupProject.Controllers;

[ApiController]
[Route("[controller]")]
public class DreamJobController : ControllerBase
{
    private readonly ILogger<DreamJobController> _logger;
    private readonly ApplicationDbContext _context;

    public DreamJobController(ILogger<DreamJobController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("BySalary")]
    [ProducesResponseType(typeof(DreamJob), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetBySalary(int? Salary)
    {
        if (Salary == null || Salary == 0) return Ok(_context.dreamJobs?.ToList().Take(5));
        try
        {
            var job = _context.dreamJobs.Find(Salary);
            if (job == null)
            {
                return NotFound("The requested resource was not found");
            }
            return Ok(job);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(typeof(List<DreamJob>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        try
        {
            if (_context.dreamJobs == null || !_context.dreamJobs.Any())
                return NotFound("No DreamJobs found in the database");
            return Ok(_context.dreamJobs?.ToList());
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
    public IActionResult Delete(int Salary)
    {
        try
        {
            var job = _context.dreamJobs?.Find(Salary);
            if (job == null)
            {
                return NotFound($"DreamJobs with Salary {Salary} was not found");
            }

            _context.dreamJobs?.Remove(job);
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
    public IActionResult Add(DreamJob newDreamJob)
    {
        if (newDreamJob.Salary != 0)
        {
            return BadRequest("Salary was provided but not needed");
        }
        try
        {
            _context.dreamJobs?.Add(newDreamJob);
            var result = _context.SaveChanges();
            if (result >= 1)
            {
                return Ok($"DreamJob {newDreamJob.JobTitle} added successfully");
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
    public IActionResult Put(DreamJob jobToEdit)
    {
        if (jobToEdit.Salary < 1)
        {
            return BadRequest("Please provide a valid Salary");
        }

        try
        {
            var job = _context.dreamJobs?.Find(jobToEdit.Salary);
            if (job == null)
                return NotFound("The DreamJob was not found");

            job.JobTitle = jobToEdit.JobTitle;
            job.JobDescription = jobToEdit.JobDescription;
            job.Benefits = jobToEdit.Benefits;
            job.Rating = jobToEdit.Rating;

            _context.dreamJobs?.Update(job);
            var result = _context.SaveChanges();
            if (result >= 1)
            {
                return Ok("job edited successfully");
            }
            return Problem("Edit was not successful. Please try again");
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}
