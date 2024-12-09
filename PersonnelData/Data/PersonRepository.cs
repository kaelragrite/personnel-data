using Microsoft.EntityFrameworkCore;
using PersonnelData.Data.QueryObjects;
using PersonnelData.Models;

namespace PersonnelData.Data;

public interface IPersonRepository
{
    Task<Person?> GetAsync(int id);

    Task<Person?> GetIncludedAsync(int id);

    Task<List<Person>> FilterAsync(FilterPersonQueryObject queryObject);

    Task AddAsync(Person person);

    void Update(Person person);

    void Delete(Person person);
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

    public async Task AddAsync(Person person) => await _context.Persons.AddAsync(person);

    public void Update(Person person) => _context.Persons.Update(person);

    public void Delete(Person person) => _context.Persons.Remove(person);
}