using PersonnelData.Models;
using PersonnelData.Shared.Enums;

namespace PersonnelData.Data.DdSeed;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Cities.Any()) return;

        var city1 = new City { Name = "თბილისი" };
        var city2 = new City { Name = "ქუთაისი" };
        var city3 = new City { Name = "ბათუმი" };

        context.Cities.AddRange(city1, city2, city3);
        context.SaveChanges();

        var person1 = new Person
        {
            Name = "პერსონა 1",
            Surname = "გვარი 1",
            Gender = Gender.Man,
            IdentificationNumber = "01011112345",
            BirthDate = new DateTime(1995, 12, 31),
            ImageName = "123.jpg",
            City = city1,
            Phones = new List<Phone>
            {
                new() { PhoneType = PhoneType.Home, Number = "0322111721" },
                new() { PhoneType = PhoneType.Mobile, Number = "558111721" }
            }
        };
        var person2 = new Person
        {
            Name = "პერსონა 2",
            Surname = "გვარი 2",
            Gender = Gender.Woman,
            IdentificationNumber = "01011112346",
            BirthDate = new DateTime(1995, 12, 25),
            City = city2,
            Phones = new List<Phone>
            {
                new() { PhoneType = PhoneType.Home, Number = "0322111722" }
            }
        };
        var person3 = new Person
        {
            Name = "პერსონა 3",
            Surname = "გვარი 3",
            Gender = Gender.Man,
            IdentificationNumber = "01011112347",
            BirthDate = new DateTime(1995, 1, 1),
            City = city3,
            Phones = new List<Phone>
            {
                new() { PhoneType = PhoneType.Home, Number = "0322111723" },
                new() { PhoneType = PhoneType.Mobile, Number = "558111723" },
                new() { PhoneType = PhoneType.Office, Number = "0322111723" }
            }
        };

        context.Persons.AddRange(person1, person2, person3);
        context.SaveChanges();


        var relation1 = new PersonRelation { RelationType = RelationType.Familiar, Person = person1, RelatedPerson = person2 };
        var relation2 = new PersonRelation { RelationType = RelationType.Relative, Person = person1, RelatedPerson = person3 };
        var relation3 = new PersonRelation { RelationType = RelationType.Familiar, Person = person2, RelatedPerson = person1 };
        
        context.PersonRelations.AddRange(relation1, relation2, relation3);
        context.SaveChanges();
    }
}