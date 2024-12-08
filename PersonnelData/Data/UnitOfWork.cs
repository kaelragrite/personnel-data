namespace PersonnelData.Data;

public interface IUnitOfWork : IDisposable
{
    public IPersonRepository PersonRepository { get; }
    public ICityRepository CityRepository { get; }
    public IPhoneRepository PhoneRepository { get; }
    public IPersonRelationRepository RelationRepository { get; }

    Task SaveChangesAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IPersonRepository _personRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IPhoneRepository _phoneRepository;
    private readonly IPersonRelationRepository _relationRepository;

    public UnitOfWork(
        ApplicationDbContext context,
        IPersonRepository personRepository,
        ICityRepository cityRepository,
        IPhoneRepository phoneRepository,
        IPersonRelationRepository relationRepository
        )
    {
        _context = context;
        
        _personRepository = personRepository;
        _cityRepository = cityRepository;
        _phoneRepository = phoneRepository;
        _relationRepository = relationRepository;
    }

    public IPersonRepository PersonRepository => _personRepository;
    public ICityRepository CityRepository => _cityRepository;
    public IPhoneRepository PhoneRepository => _phoneRepository;
    public IPersonRelationRepository RelationRepository => _relationRepository;
    
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}