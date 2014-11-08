namespace Cloud_Vibe.Data.Migrations
{
    using Cloud_Vibe.Data;
    using Cloud_Vibe.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CloudVibeDbContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CloudVibeDbContex context)
        {
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

            //Add Users
            if (context.Users.Count() == 0)
            {
                context.Users.Add(new AppUser { FirstName = "Georgi", LastName = "Georgiev", UserName = "gosho", Email = "gosho@goshov.bg" });
                context.Users.Add(new AppUser { FirstName = "Pesho", LastName = "Peshov", UserName = "peshkata", Email = "pesho@peshov.bg" });
                context.Users.Add(new AppUser { FirstName = "Radka", LastName = "Piratka", UserName = "radka", Email = "radka@piratka.bg" });
                context.SaveChanges();
            }
            
            //Add Artists
            if (context.Artists.Count() == 0)
            {
                context.Artists.Add(new Artist { Name = "Madonna", Picture = new byte[] { 0 }, Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc suscipit ultricies ligula, et malesuada libero elementum vel. Nulla sed rhoncus sapien. Duis sed faucibus metus. Etiam tincidunt vitae velit vitae dignissim. Fusce commodo eleifend nibh, at lobortis odio venenatis vel. Praesent vehicula luctus quam sit amet mollis. Nulla a ex est. Vivamus sapien libero, volutpat eu maximus eget, euismod ac nunc. Mauris finibus sem eget orci vulputate tempor. Praesent vel urna efficitur, lacinia risus et, condimentum justo. In aliquam congue varius. Mauris sollicitudin dignissim metus, vitae accumsan est blandit at. Sed pretium vehicula tortor. Fusce elementum ornare diam, eget pulvinar est consectetur luctus.", WebSite = "www.madonna.com", IsFamous = true });
                context.Artists.Add(new Artist { Name = "AC/DC", Picture = new byte[] { 0 }, Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc suscipit ultricies ligula, et malesuada libero elementum vel. Nulla sed rhoncus sapien. Duis sed faucibus metus. Etiam tincidunt vitae velit vitae dignissim. Fusce commodo eleifend nibh, at lobortis odio venenatis vel. Praesent vehicula luctus quam sit amet mollis. Nulla a ex est. Vivamus sapien libero, volutpat eu maximus eget, euismod ac nunc. Mauris finibus sem eget orci vulputate tempor. Praesent vel urna efficitur, lacinia risus et, condimentum justo. In aliquam congue varius. Mauris sollicitudin dignissim metus, vitae accumsan est blandit at. Sed pretium vehicula tortor. Fusce elementum ornare diam, eget pulvinar est consectetur luctus.", WebSite = "www.acdc.com", IsFamous = true });
                context.Artists.Add(new Artist { Name = "Pesho", Picture = new byte[] { 0 }, WebSite = "www.juicyj.com", IsFamous = false });
                context.SaveChanges();
            }
            
            ////Add Albums
            if (context.Albums.Count() == 0)
            {
                context.Albums.Add(new Album { Title = "Bedtime Stories", Year = 1994, Artist = (context.Artists.Where(a => a.Name == "Madonna").ToArray())[0], Torrent = new byte[] { 0, 1 }, SharedOn = DateTime.Now });
                context.Albums.Add(new Album { Title = "Like a Virgin", Year = 2001, Artist = (context.Artists.Where(a => a.Name == "Madonna").ToArray())[0], Torrent = new byte[] { 0, 1 }, SharedOn = DateTime.Now });
                context.Albums.Add(new Album { Title = "Something to Remember", Year = 1995, Artist = (context.Artists.Where(a => a.Name == "Madonna").ToArray())[0], Torrent = new byte[] { 0, 1 }, SharedOn = DateTime.Now });
                context.Albums.Add(new Album { Title = "Black Ice", Year = 2009, Artist = (context.Artists.Where(a => a.Name == "AC/DC").ToArray())[0], Torrent = new byte[] { 0, 1 }, SharedOn = DateTime.Now, UserShared = (context.Users.Where(u => u.UserName == "radka").ToArray())[0] });
                context.SaveChanges();
            }
            
            ////Add Songs
            if (context.Songs.Count() == 0)
            {
                context.Songs.Add(new Song { Artist = (context.Artists.Where(a => a.Name == "Pesho").ToArray())[0], Title = "Tigre Tigre", Year = 1996, SharedOn = DateTime.Now, Torrent = new byte[] { 0 }, VideoLink = "https://www.youtube.com/watch?v=teA8TE092ss" });
                context.Songs.Add(new Song { Artist = (context.Artists.Where(a => a.Name == "Pesho").ToArray())[0], Title = "Tuhla v stenata", Year = 1996, SharedOn = DateTime.Now, Torrent = new byte[] { 0 }, VideoLink = "https://www.youtube.com/watch?v=fGvbz50yo0s" });
                context.Songs.Add(new Song { Artist = (context.Artists.Where(a => a.Name == "Pesho").ToArray())[0], Title = "Katadjiiska", Year = 1998, SharedOn = DateTime.Now, Torrent = new byte[] { 0 }, VideoLink = "https://www.youtube.com/watch?v=_sFOsI4m2fk" });
                context.SaveChanges();
            }
            
            ////Add Comments
            if (context.Comments.Count() == 0)
            {
                context.Comments.Add(new Comment { User = (context.Users.Where(a => a.UserName == "radka").ToArray())[0], Text = "Aide tigreeeeeee", Song = (context.Songs.Where(s => s.Title == "Tigre Tigre").ToArray())[0] });
                context.Comments.Add(new Comment { User = (context.Users.Where(a => a.UserName == "radka").ToArray())[0], Text = "glory glory Lepa Brena!!!!", Song = (context.Songs.Where(s => s.Title == "Katadjiiska").ToArray())[0] });
                context.SaveChanges();
            }
           
        }
    }
}
