namespace Wordless.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Wordless.Models.WordlessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Wordless.Models.WordlessContext context)
        {
            #region Users    
            var user1 = new User
            {
                Username = "Patrik",
                Password = "123",
                Name = "Patrik Nilsson",
                Email = "patrik@gmail.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            var temp = from x in context.Users
                       where x.Username == user1.Username
                       select x;
            if (temp.Count() == 0)
            {
                context.Users.Add(user1);
            }
            var user2 = new User
            {
                Username = "Nitin",
                Password = "123",
                Name = "Nitin Anand",
                Email = "nitin@anand.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            temp = from x in context.Users
                   where x.Username == "Nitin"
                   select x;
            if (temp.Count() == 0)
            {
                context.Users.Add(user2);
            }

            var user3 = new User
            {
                Username = "Alex",
                Password = "123",
                Name = "Alexander Litos",
                Email = "ales@litos.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            temp = from x in context.Users
                   where x.Username == "Alex"
                   select x;
            if (temp.Count() == 0)
            {
                context.Users.Add(user3);
            }
            var user4 = new User
            {
                Username = "Christian",
                Password = "123",
                Name = "Christian Jarenfors",
                Email = "Christian@Jarenfors.com",
                Author = true,
                Admin = true,
                Funds = 200
            };
            temp = from x in context.Users
                   where x.Username == "Christian"
                   select x;
            if (temp.Count() == 0)
            {
                context.Users.Add(user4);
            }
            #endregion
        }
    }
}
