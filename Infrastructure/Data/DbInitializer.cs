using Domain.Models;
using Infrastructure;

public static class DbInitializer
{
    public static void Seed(AhlDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    Username = "ucyildirim",
                    Email = "ucyildirim@gmail.com",
                    PasswordHash = "806fe3f853a940ce2bbb2d1b98cae9fc083e8022fffceebd1384991a710c0fb6",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new User
                {
                    Username = "ncyildirim",
                    Email = "ncyildirim@gmail.com",
                    PasswordHash = "806fe3f853a940ce2bbb2d1b98cae9fc083e8022fffceebd1384991a710c0fb6",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
            context.SaveChanges();
        }

        if (!context.Accounts.Any())
        {
            var ugur = context.Users.FirstOrDefault(u => u.Username == "ucyildirim");
            var nazli = context.Users.FirstOrDefault(u => u.Username == "ncyildirim");

            context.Accounts.AddRange(
                new Account
                {
                    Balance = 1000.00m,
                    UserId = ugur.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Account
                {
                    Balance = 2000.00m,
                    UserId = nazli.Id,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
            context.SaveChanges();
        }

        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category
                {
                     Name = "Maas",
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now
                },
                new Category
                {
                    Name = "Mutfak",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Category
                {
                    Name = "Eglence",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
            context.SaveChanges();
        }

        if (!context.Transactions.Any())
        {
            var ugurAccount = context.Accounts.FirstOrDefault(a => a.User.Username == "ucyildirim");
            var nazliAccount = context.Accounts.FirstOrDefault(a => a.User.Username == "ncyildirim");
            var maasCategory = context.Categories.FirstOrDefault(c => c.Name == "Maas");
            var kitchenCategory = context.Categories.FirstOrDefault(c => c.Name == "Mutfak");
            var entertainmentCategory = context.Categories.FirstOrDefault(c => c.Name == "Eglence");

            context.Transactions.AddRange(
                new Transaction
                {
                    Amount = 10000m,
                    TransactionDate = DateTime.Now,
                    Description = "Ekim maas",
                    AccountId = ugurAccount.Id,
                    CategoryId = maasCategory.Id,
                    CreatedAt = DateTime.Now
                }, 
                new Transaction
                {
                    Amount = 10000m,
                    TransactionDate = DateTime.Now,
                    Description = "Ekim maas",
                    AccountId = nazliAccount.Id,
                    CategoryId = maasCategory.Id,
                    CreatedAt = DateTime.Now
                },

                new Transaction
                {
                    Amount = -100.50m,
                    TransactionDate = DateTime.Now,
                    Description = "Mutfak alisverisi",
                    AccountId = ugurAccount.Id,
                    CategoryId = kitchenCategory.Id,
                    CreatedAt = DateTime.Now
                },
                new Transaction
                {
                    Amount = -250.75m,
                    TransactionDate = DateTime.Now,
                    Description = "Tiyatro bileti",
                    AccountId = nazliAccount.Id,
                    CategoryId = entertainmentCategory.Id,
                    CreatedAt = DateTime.Now
                }
            );
            ugurAccount.Balance += - 100.50m;
            nazliAccount.Balance += - 250.75m;
            context.SaveChanges();
        }
    }
}
