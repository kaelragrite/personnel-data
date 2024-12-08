using System.Resources;
using Microsoft.AspNetCore.Mvc;
using PersonnelData.Data;
using PersonnelData.Data.QueryObjects;
using PersonnelData.Messages;
using PersonnelData.Models;

namespace PersonnelData.Controllers;

[Route("person")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;
    private readonly ResourceManager _resourceManager;
    private readonly IUnitOfWork _uow;

    public PersonController(IWebHostEnvironment environment, ResourceManager resourceManager, IUnitOfWork uow)
    {
        _environment = environment;
        _resourceManager = resourceManager;
        _uow = uow;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var person = await _uow.PersonRepository.GetIncludedAsync(id);
        if (person is null)
        {
            var errorMessage = _resourceManager.GetString("PersonNotFound");
            return NotFound(errorMessage);
        }

        var fullPath = Path.Combine(_environment.ContentRootPath, "uploads", "images", person.ImageName ?? "NOT_EXISTS");
        if (!System.IO.File.Exists(fullPath))
            return Ok(new GetPersonResponse(person, null));

        var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);

        return Ok(new GetPersonResponse(person, fileBytes));
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterPersonQuery query)
    {
        var persons = await _uow.PersonRepository.FilterAsync(new FilterPersonQueryObject
        {
            Name = query.Name,
            Surname = query.Surname,
            Gender = query.Gender,
            IdentificationNumber = query.IdentificationNumber,
            BirthDate = query.BirthDate,
            CityId = query.CityId,
            Page = query.Page,
            PageCount = query.PageCount
        });
        
        return Ok(new FilterPersonResponse(persons));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
    {
        var city = await _uow.CityRepository.GetAsync(request.CityId);
        if (city is null)
        {
            var errorMessage = _resourceManager.GetString("CityNotFound");
            return NotFound(errorMessage);
        }

        var person = new Person
        {
            Name = request.Name,
            Surname = request.Surname,
            Gender = request.Gender,
            IdentificationNumber = request.IdentificationNumber,
            BirthDate = request.BirthDate,
            City = city,
            Phones = new List<Phone>(request.Phones.Select(x => new Phone
            {
                PhoneType = x.PhoneType,
                Number = x.Number
            }))
        };
        
        await _uow.PersonRepository.AddAsync(person);
        await _uow.SaveChangesAsync();

        return Created(string.Empty, person.Id);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdatePersonRequest request)
    {
        var person = await _uow.PersonRepository.GetAsync(id);
        if (person is null)
        {
            var errorMessage = _resourceManager.GetString("PersonNotFound");
            return NotFound(errorMessage);
        }

        City? city = null;
        if (request.CityId is not null)
        {
            city = await _uow.CityRepository.GetAsync(request.CityId.Value);
            if (city is null)
            {
                var errorMessage = _resourceManager.GetString("CityNotFound");
                return NotFound(errorMessage);
            }   
        }

        // Update Fields
        person.Name = request.Name ?? person.Name;
        person.Surname = request.Surname ?? person.Surname;
        person.Gender = request.Gender ?? person.Gender;
        person.IdentificationNumber = request.IdentificationNumber ?? person.IdentificationNumber;
        person.BirthDate = request.BirthDate ?? person.BirthDate;
        person.City = city ?? person.City;

        // Update/Create/Delete Phones
        if (request.Phones is not null)
        {
            foreach (var personPhone in person.Phones)
            {
                var phoneUpdate = request.Phones.FirstOrDefault(x => x.PhoneType == personPhone.PhoneType);
                if (phoneUpdate is null)
                    _uow.PhoneRepository.Delete(personPhone);
                else
                    personPhone.Number = phoneUpdate.Number;
            }
            foreach (var phoneUpdate in request.Phones)
            {
                if (person.Phones.FirstOrDefault(x => x.PhoneType == phoneUpdate.PhoneType) is null)
                {
                    var phone = new Phone
                    {
                        PhoneType = phoneUpdate.PhoneType,
                        Number = phoneUpdate.Number,
                        PersonId = person.Id
                    };
                    person.Phones.Add(phone);

                    await _uow.PhoneRepository.AddAsync(phone);
                }
            }
        }

        _uow.PersonRepository.Update(person);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var person = await _uow.PersonRepository.GetAsync(id);
        if (person is null)
        {
            var errorMessage = _resourceManager.GetString("PersonNotFound");
            return NotFound(errorMessage);
        }

        _uow.PersonRepository.Delete(person);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet]
    [Route("person-relation/relation-group-report")]
    public async Task<IActionResult> PersonRelationGroupReport()
    {
        var report = await _uow.PersonRepository.RelationGroupReportAsync();
        
        return Ok(new PersonRelationGroupReportResponse(report));
    }

    [HttpPost]
    [Route("person-relation")]
    public async Task<IActionResult> CreatePersonRelation(CreatePersonRelationRequest createPersonRelationRequest)
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

        await _uow.PersonRepository.AddRelationAsync(relation);
        await _uow.SaveChangesAsync();

        return Created();
    }

    [HttpDelete]
    [Route("person-relation")]
    public async Task<IActionResult> DeletePersonRelation(DeletePersonRelationRequest deletePersonRelationRequest)
    {
        var relation = await _uow.PersonRepository
            .GetRelationAsync(deletePersonRelationRequest.RelationType, deletePersonRelationRequest.PersonId, deletePersonRelationRequest.RelatedPersonId);
        if (relation is null)
        {
            var errorMessage = _resourceManager.GetString("RelationNotFound");
            return NotFound(errorMessage);
        }

        _uow.PersonRepository.DeleteRelation(relation);
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}