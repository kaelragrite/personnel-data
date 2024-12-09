using PersonnelData.Models;

namespace PersonnelData.Data;

public interface IPhoneRepository
{
    Task AddAsync(Phone phone);

    void Delete(Phone phone);
}

public class PhoneRepository : IPhoneRepository
{
    private readonly ApplicationDbContext _context;

    public PhoneRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Phone phone) => await _context.Phones.AddAsync(phone);

    public void Delete(Phone phone) => _context.Phones.Remove(phone);
}