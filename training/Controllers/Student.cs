using Microsoft.AspNetCore.Mvc;
using training.Model;

namespace training.Controllers;

[ApiController]
[Route("[controller]")]
public class Studentcontroller : ControllerBase
{

    private readonly studentDbContext _studentDbContext;

    private readonly ILogger<Studentcontroller> _logger;

    public Studentcontroller(ILogger<Studentcontroller> logger, studentDbContext studentDbContext)
    {
        _logger = logger;
        _studentDbContext = studentDbContext;
    }

    [HttpGet]
    public ActionResult getStudents()
    {
        _logger.LogWarning("Entering Get Student");
        return Ok(_studentDbContext?.Students.ToList());
    }

    [HttpPost("add")]
    public async Task<ActionResult> addStudent(Student student)
    {
        _logger.LogWarning("Entering Add Student");
        _studentDbContext.Students.Add(student);
        await _studentDbContext.SaveChangesAsync();
        return Ok(student);
    }

    [HttpGet("delete/{id}")]
    public async Task<ActionResult> deleteStudent(int id)
    {
        _logger.LogWarning("Entering Delete Student");
        var studentToDelete = await _studentDbContext.Students.FindAsync(id);
        if (studentToDelete == null)
        {
            return NotFound();
        }
        _studentDbContext.Students.Remove(studentToDelete);
        await _studentDbContext.SaveChangesAsync();
        return Ok(true);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> getStudent(int id)
    {
        _logger.LogWarning("Entering Get Student");
        var student = await _studentDbContext.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost("update")]
    public async Task<ActionResult> update(Student student)
    {
        _logger.LogWarning("Entering Get Student");
        try
        {
            _studentDbContext.Students.Update(student);
            await _studentDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return BadRequest("Id not found "+e);
        }
       
        return Ok(student);
    }
}

