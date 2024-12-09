using Microsoft.AspNetCore.Mvc;
using PersonnelData.Data;

namespace PersonnelData.Controllers;

[Route("image")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly IUnitOfWork _uow;

    public ImageController(IWebHostEnvironment environment, IUnitOfWork uow)
    {
        _environment = environment;
        _uow = uow;
    }

    [HttpPost("upload/{id}")]
    public async Task<IActionResult> UploadImage([FromRoute] int id, IFormFile file)
    {
        var person = await _uow.PersonRepository.GetAsync(id);
        if (person is null) return NotFound("person");
    
        if (file.Length == 0) return BadRequest("No file uploaded.");
    
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
            return BadRequest("Invalid file type.");
    
        var fileName = person.ImageName;
        if (fileName is null)
        {
            fileName = Guid.NewGuid() + extension;
            person.ImageName = fileName;
        }
    
        var uploadsFolder = Path.Combine(_environment.ContentRootPath, "uploads", "images");
        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder); // sheidzleba programshi gatana
    
        var filePath = Path.Combine(uploadsFolder, fileName);
    
        // Save file and update person
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    
        _uow.PersonRepository.Update(person);
        await _uow.SaveChangesAsync();
    
        return NoContent();
    }
}