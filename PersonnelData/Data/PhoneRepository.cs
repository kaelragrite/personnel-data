using Microsoft.EntityFrameworkCore;
using PersonnelData.Models;

namespace PersonnelData.Data;

public interface IPhoneRepository
{
    Task<List<Phone>> GetAllByPersonAsync(int personId);

    Task AddAsync(Phone phone);

    Task UpdateAsync(int personId, Phone phone);

    void Delete(Phone phone);
}

public class PhoneRepository : IPhoneRepository
{
    private readonly ApplicationDbContext _context;

    public PhoneRepository(ApplicationDbContext context) => _context = context;

    
    public async Task<List<Phone>> GetAllByPersonAsync(int personId) =>
        await _context.Phones.Where(x => x.Person.Id == personId).ToListAsync();

    public async Task AddAsync(Phone phone) => await _context.Phones.AddAsync(phone);

    public Task UpdateAsync(int personId, Phone phone)
    {
        throw new NotImplementedException();
    }

    public void Delete(Phone phone) => _context.Phones.Remove(phone);
}