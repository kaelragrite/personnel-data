using Microsoft.EntityFrameworkCore;
using PersonnelData.Data.QueryObjects;
using PersonnelData.Data.ResultObjects;
using PersonnelData.Models;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Data;

public interface IPersonRepository
{
    Task<Person?> GetAsync(int id);

    Task<Person?> GetIncludedAsync(int id);

    Task<List<Person>> FilterAsync(FilterPersonQueryObject queryObject);

    Task<RelationGroupReportResult> RelationGroupReportAsync();

    Task AddAsync(Person person);

    void Update(Person person);

    void Delete(Person person);
    
    Task<PersonRelation?> GetRelationAsync(RelationType relationType, int personId, int relatedPersonId);

    Task AddRelationAsync(PersonRelation relation);

    void DeleteRelation(PersonRelation relation);
}

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context) => _context = context;

    public async Task<Person?> GetAsync(int id) => await _context.Persons
        .Include(x => x.City)
        .Include(x => x.Phones)
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<Person?> GetIncludedAsync(int id) => await _context.Persons
        .Include(x => x.City)
        .Include(x => x.Phones)
        .Include(x => x.Relations)
        .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Person>> FilterAsync(FilterPersonQueryObject queryObject)
    {
        var persons = await _context.Persons
            .Where(x => queryObject.Gender == null || x.Gender == queryObject.Gender)
            .Where(x => queryObject.BirthDate == null || x.BirthDate == queryObject.BirthDate)
            .Where(x => queryObject.CityId == null || x.CityId == queryObject.CityId)

            .Where(x => string.IsNullOrWhiteSpace(queryObject.Name) || x.Name.Contains(queryObject.Name))
            .Where(x => string.IsNullOrWhiteSpace(queryObject.Surname) || x.Name.Contains(queryObject.Surname))
            .Where(x => string.IsNullOrWhiteSpace(queryObject.IdentificationNumber) || x.Name.Contains(queryObject.IdentificationNumber))

            .Skip((queryObject.Page - 1) * queryObject.PageCount)
            .Take(queryObject.PageCount)
            .ToListAsync();
        
        return persons;
    }

    public async Task<RelationGroupReportResult> RelationGroupReportAsync()
    {
        var groupObjects = await _context.PersonRelations
            .GroupBy(x => x.RelationType)
            .Select(x => new RelationGroupReportResult.GroupReportObject(x.Key, x.Count()))
            .ToListAsync();

        return new RelationGroupReportResult(groupObjects);
    }

    public async Task AddAsync(Person person) => await _context.Persons.AddAsync(person);

    public void Update(Person person) => _context.Persons.Update(person);

    public void Delete(Person person) => _context.Persons.Remove(person);

    public async Task<PersonRelation?> GetRelationAsync(RelationType relationType, int personId, int relatedPersonId) =>
        await _context.PersonRelations.FirstOrDefaultAsync(x => x.RelationType == relationType && x.PersonId == personId && x.RelatedPersonId == relatedPersonId);

    public async Task AddRelationAsync(PersonRelation relation) => await _context.PersonRelations.AddAsync(relation);

    public void DeleteRelation(PersonRelation relation) => _context.PersonRelations.Remove(relation);
}