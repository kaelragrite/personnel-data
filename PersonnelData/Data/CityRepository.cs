using PersonnelData.Models;

namespace PersonnelData.Data;

public interface ICityRepository
{
    Task<City?> GetAsync(int id);
}

public class CityRepository : ICityRepository
{
    private readonly ApplicationDbContext _context;

    public CityRepository(ApplicationDbContext context) => _context = context;

    public async Task<City?> GetAsync(int id) => await _context.Cities.FindAsync(id);
}