using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
        context.Authors.AddRange(
            new Author
            {
                Id = 1,
                Name = "Kutay",
                Surname = "Yıldırım",
                Birthdate = new DateTime(1998, 11, 25).Date
            },
            new Author
            {
                Id = 2,
                Name = "Çağan",
                Surname = "Bıçakçı",
                Birthdate = new DateTime(1998, 09, 12).Date
            },
            new Author
            {
                Id = 3,
                Name = "Hasan",
                Surname = "Taşkın",
                Birthdate = new DateTime(1998, 1, 13).Date
            }
        );
    }
}
