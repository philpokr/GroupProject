using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupProject.Models; 

[Route("api/[controller]")]
[ApiController]
public class TeamMembersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TeamMembersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/TeamMembers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMember>>> GetTeamMembers()
    {
        return await _context.TeamMembers.ToListAsync();
    }

    // GET: api/TeamMembers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TeamMember>> GetTeamMember(int id)
    {
        var teamMember = await _context.TeamMembers.FindAsync(id);

        if (teamMember == null)
        {
            return NotFound();
        }

        return teamMember;
    }

    // POST: api/TeamMembers
    [HttpPost]
    public async Task<ActionResult<TeamMember>> PostTeamMember(TeamMember teamMember)
    {
        _context.TeamMembers.Add(teamMember);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTeamMember", new { id = teamMember.Id }, teamMember);
    }

    // PUT: api/TeamMembers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTeamMember(int id, TeamMember teamMember)
    {
        if (id != teamMember.Id)
        {
            return BadRequest();
        }

        _context.Entry(teamMember).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TeamMemberExists(id))
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

    // DELETE: api/TeamMembers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeamMember(int id)
    {
        var teamMember = await _context.TeamMembers.FindAsync(id);
        if (teamMember == null)
        {
            return NotFound();
        }

        _context.TeamMembers.Remove(teamMember);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TeamMemberExists(int id)
    {
        return _context.TeamMembers.Any(e => e.Id == id);
    }
}

