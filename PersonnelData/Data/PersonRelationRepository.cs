using Microsoft.EntityFrameworkCore;
using PersonnelData.Data.ResultObjects;
using PersonnelData.Models;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Data;

public interface IPersonRelationRepository
{
    Task<PersonRelation?> GetAsync(RelationType relationType, int personId, int relatedPersonId);

    Task AddAsync(PersonRelation relation);

    void Delete(PersonRelation relation);
    
    Task<RelationGroupReportResult> TypeGroupReportAsync();
}

public class PersonRelationRepository : IPersonRelationRepository
{
    private readonly ApplicationDbContext _context;
    
    public PersonRelationRepository(ApplicationDbContext context) => _context = context;
    
    public async Task<PersonRelation?> GetAsync(RelationType relationType, int personId, int relatedPersonId) =>
        await _context.PersonRelations.FirstOrDefaultAsync(x => x.RelationType == relationType && x.PersonId == personId && x.RelatedPersonId == relatedPersonId);

    public async Task AddAsync(PersonRelation relation) => await _context.PersonRelations.AddAsync(relation);

    public void Delete(PersonRelation relation) => _context.PersonRelations.Remove(relation);

    public async Task<RelationGroupReportResult> TypeGroupReportAsync()
    {
        var groupObjects = await _context.PersonRelations
            .GroupBy(x => x.RelationType)
            .Select(x => new RelationGroupReportResult.GroupReportObject(x.Key, x.Count()))
            .ToListAsync();

        return new RelationGroupReportResult(groupObjects);
    }
}