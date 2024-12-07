using Microsoft.EntityFrameworkCore;
using PersonnelData.Models;

namespace PersonnelData.Data;

public interface ICityRepository
{
    Task<List<City>> GetAllAsync();

    Task<City?> GetAsync(int id);
}

public class CityRepository : ICityRepository
{
    private readonly ApplicationDbContext _context;

    public CityRepository(ApplicationDbContext context) => _context = context;


    public async Task<List<City>> GetAllAsync() => await _context.Cities.ToListAsync();

    public async Task<City?> GetAsync(int id) => await _context.Cities.FindAsync(id);

    public async Task AddAsync(City city) => await _context.Cities.AddAsync(city);
}