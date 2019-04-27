using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualitySouvenirs.Models;

namespace QualitySouvenirs.Data
{
    public class DbInitializer
    {
        public static void Initialize(SouvenirContext context)
        {
            context.Database.EnsureCreated();

            if (context.Souvenirs.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
                new Customer {FirstMidName = "Customer1", LastName="Test", HomePhoneNumber = "658265632", WorkPhoneNumber = "482975272658",
                MobilePhoneNumber = "8795827591", Address = "Address1", Email = "customer1@gmail.com", Password="@Test1234"}
            };

            foreach(Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var categories = new Category[]
            {
                new Category {Name = "Maori Gifts"},
                new Category {Name = "T-Shirts"},
                new Category {Name = "Mugs"}
            };


            foreach (Category c in categories)
            {
               context.Categories.Add(c);
            }
            context.SaveChanges();

            var suppliers = new Supplier[]
            {
                new Supplier{Name = "Mountain Jade", HomePhoneNumber = "12345678", WorkPhoneNumber = "87654321", MobilePhoneNumber = "321654987", Email = "info@mountainjade.co.nz"},
                new Supplier{Name = "Mr Vintage", HomePhoneNumber = "12345678", WorkPhoneNumber = "87654321", MobilePhoneNumber = "321654987", Email = "info@mrvintage.co.nz"},
                new Supplier{Name = "Simply New Zealand", HomePhoneNumber = "12345678", WorkPhoneNumber = "87654321", MobilePhoneNumber = "321654987", Email="info@simplynz.co.nz"}
            };


            foreach (Supplier s in suppliers)
            {
                context.Suppliers.Add(s);
            }
            context.SaveChanges();

            var souvenirs = new Souvenir[]
            {
                new Souvenir {Name = "Koru Necklace", Description="Greenstone, hand carved",SupplierID = 1, CategoryID = 1, Price = 150,
                    ImagePath = "images/KoruNecklace.jpg"},
                new Souvenir {Name = "Toki Blade Necklace", Description="Greenstone, hand carved", SupplierID=1, CategoryID = 1, Price=76,
                    ImagePath= "images/TokiBlade.jpg"},
                new Souvenir {Name = "Pounamu Necklace", Description="Greenstone, hand carved", SupplierID=1, CategoryID = 1, Price=228,
                    ImagePath= "images/Pounamu.jpg"},
                new Souvenir {Name = "Silver Disc Necklace", Description="Greenstone, hand carved", SupplierID=1, CategoryID = 1, Price=149,
                    ImagePath= "images/SilverDisc.jpg"},
                new Souvenir {Name = "Canadian Jade Necklace", Description="Single Twist Necklace", SupplierID=1, CategoryID = 1, Price=138,
                    ImagePath= "images/CanadianJade.jpg"},
                new Souvenir {Name = "NZ Jade Necklace", Description="Large Twist Pendant", SupplierID=1, CategoryID = 1, Price=398,
                    ImagePath= "images/NZJadeLargeTwist.jpg"},

               
                new Souvenir{Name = "NZShirt", Description="Gender Neutral T-shirt", SupplierID = 2, CategoryID = 2, Price = 30,
                    ImagePath = "images/NZShirt.jpg"},
                new Souvenir{Name = "All Good T-shirt", Description="Gender Neutral T-shirt", SupplierID = 2, CategoryID = 2, Price = 30,
                    ImagePath = "images/AllGoodShirt.jpg"},
                new Souvenir{Name = "Hauraki Bhuja Hawk T-Shirt", Description="Mens's T-shirt", SupplierID = 2, CategoryID = 2, Price = 30,
                    ImagePath = "images/BhujaHawk.jpg"},
                new Souvenir{Name = "Bros T-Shirt", Description="Mens's T-shirt", SupplierID = 2, CategoryID = 2, Price = 30,
                    ImagePath = "images/BrosShirt.jpg"},
                new Souvenir{Name = "Double Brown T-Shirt", Description="Gender Neutral T-shirt", SupplierID = 2, CategoryID = 2, Price = 30,
                    ImagePath = "images/DoubleBrownShirt.png"},
                new Souvenir{Name = "Kiwi Slang 90's T-Shirt", Description="Gender Neutral T-shirt", SupplierID = 2, CategoryID = 2, Price = 30,
                    ImagePath = "images/KiwiSlangShirt.png"},


                new Souvenir{Name = "NZ Mug", Description="Silver Fern Mug", SupplierID = 3, CategoryID = 3, Price = 15,
                    ImagePath = "images/NZMug.jpg"},
                new Souvenir{Name = "All Blacks Mug", Description="Heat Activated Mug", SupplierID = 3, CategoryID = 3, Price = 15,
                    ImagePath = "images/NZMug.jpg"},
                new Souvenir{Name = "All Blacks Beer Handle", Description="Frosted", SupplierID = 3, CategoryID = 3, Price = 15,
                    ImagePath = "images/AllBlacksBeer.jpg"},
                new Souvenir{Name = "Fantail Mug", Description="Red", SupplierID = 3, CategoryID = 3, Price = 15,
                    ImagePath = "images/FantailMugRed.jpg"},
                new Souvenir{Name = "Kiwi Mug", Description="Yellow", SupplierID = 3, CategoryID = 3, Price = 15,
                    ImagePath = "images/KiwiMugYellow.jpg"},
                new Souvenir{Name = "Pukeko Mug", Description="Blue", SupplierID = 3, CategoryID = 3, Price = 15,
                    ImagePath = "images/PukekoMugBlue.png"},

            };

            foreach (Souvenir s in souvenirs)
            {
                context.Souvenirs.Add(s);
            }
            context.SaveChanges();

            var administrators = new Administrator[]
            {
                new Administrator{AdministratorID=1, AdminLoginName="Admin", AdminPassword="@Test1234"}
            };

            foreach(Administrator a in administrators)
            {
                context.Administrators.Add(a);
            }
            context.SaveChanges();
        }


    }
}
