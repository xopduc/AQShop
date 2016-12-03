namespace AQShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AQShop.Data.AQShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AQShop.Data.AQShopDbContext context)
        {
            //UpdateProductCategory(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AQShopDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AQShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "aqadmin",
                Email = "aq.international@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "aq shop"

            };

            manager.Create(user, "123654$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("aq.international@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

            context.SaveChanges();
        }

        private void UpdateProductCategory(AQShop.Data.AQShopDbContext context)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>()
            {
                new ProductCategory() {Name= "Điện lạnh", Alias= "dien_lanh", Status = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now  },
                new ProductCategory() {Name= "Viễn thông", Alias= "vien-thong", CreateDate = DateTime.Now, UpdateDate = DateTime.Now  },
                new ProductCategory() {Name= "Gia dụng", Alias= "gia_dung", CreateDate = DateTime.Now, UpdateDate = DateTime.Now  },
                new ProductCategory() {Name= "Nội thất", Alias= "noi_that", CreateDate = DateTime.Now, UpdateDate = DateTime.Now  },
            };
            context.ProductCategories.AddRange(productCategories);
            context.SaveChanges();
        }
    }
}
