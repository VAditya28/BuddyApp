using Buddy.Models.Models;

using Microsoft.EntityFrameworkCore;

namespace Buddy.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { category_Id = 1, Name = "Action", DisplayOrder = 1 },
            new Category { category_Id = 2, Name = "Sci-fi", DisplayOrder = 2 },
            new Category { category_Id = 3, Name = "History", DisplayOrder = 3 },
            new Category { category_Id = 4, Name = "Rom-Com", DisplayOrder = 4 }

            );

        modelBuilder.Entity<Product>().HasData(
            new Product {
                Id = 1,
                Name = "Rich Dad poor Dad",
                Description = "Rich Dad Poor Dad is mainly about achieving financial literacy and independence by making money work for you, not the other way around. ",
                ISBN = "3838971919",
                Author = "Robert T.",
                ListPrice = 10,
                Price = 500,
                Price50 = 400,
                price100 = 200,
                CategoryId = 1,
                Imageurl=""

            },
            new Product {
                Id = 2,
                Name = "Half Girlfriend",
                Description = "Half Girlfriend is a 2014 novel by Chetan Bhagat about a Bihari boy named Madhav who falls in love with a wealthy Delhi girl, Riya. ",
                ISBN = "3832151919",
                Author = "Chethan Bhagath",
                ListPrice = 12.00,
                Price = 600,
                Price50 = 500,
                price100 = 300,
                CategoryId = 2,
                Imageurl = ""
            },
            new Product {
                Id = 3,
                Name = "Baahubali The Legend",
                Description = "Baahubali is a  Indian epic action series about a legendary warrior who rises from humble beginnings to reclaim his rightful throne in the kingdom of Mahishmati. ",
                ISBN = "3838999919",
                Author = "S.S. Rajamouli",
                ListPrice = 5.00,
                Price = 400,
                Price50 = 200,
                price100 = 100,
                CategoryId = 3,
                Imageurl = ""

            }
            


            );
    }




   

   
}
