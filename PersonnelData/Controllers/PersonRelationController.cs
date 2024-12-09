using System.Resources;
using Microsoft.AspNetCore.Mvc;
using PersonnelData.Data;
using PersonnelData.Messages;
using PersonnelData.Models;

namespace PersonnelData.Controllers;

[Route("person-relation")]
[ApiController]
public class PersonRelationController : ControllerBase
{
    private readonly ResourceManager _resourceManager;
    private readonly IUnitOfWork _uow;

    public PersonRelationController(ResourceManager resourceManager, IUnitOfWork uow)
    {
        _resourceManager = resourceManager;
        _uow = uow;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonRelationRequest createPersonRelationRequest)
    {
        var person = await _uow.PersonRepository.GetAsync(createPersonRelationRequest.PersonId);
        if (person is null)
        {
            var errorMessage = _resourceManager.GetString("PersonNotFound");
            return NotFound(errorMessage);
        }

        var relatedPerson = await _uow.PersonRepository.GetAsync(createPersonRelationRequest.RelatedPersonId);
        if (relatedPerson is null)
        {
            var errorMessage = _resourceManager.GetString("RelatedPersonNotFound");
            return NotFound(errorMessage);
        }

        var relation = new PersonRelation
        {
            RelationType = createPersonRelationRequest.RelationType,
            PersonId = person.Id,
            RelatedPersonId = relatedPerson.Id
        };

        await _uow.RelationRepository.AddAsync(relation);
        await _uow.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeletePersonRelationRequest deletePersonRelationRequest)
    {
        var relation = await _uow.RelationRepository
            .GetAsync(deletePersonRelationRequest.RelationType, deletePersonRelationRequest.PersonId, deletePersonRelationRequest.RelatedPersonId);
        if (relation is null)
        {
            var errorMessage = _resourceManager.GetString("RelationNotFound");
            return NotFound(errorMessage);
        }

        _uow.RelationRepository.Delete(relation);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("type-group-report")]
    public async Task<IActionResult> TypeGroupReport()
    {
        var report = await _uow.RelationRepository.TypeGroupReportAsync();
        
        return Ok(new PersonRelationGroupReportResponse(report));
    }
}