using Cloud_Vibe.Data.Models;
using Cloud_Vibe.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data
{
    public interface ICloudVibeData
    {
        IRepository<Album> Albums { get; }
        IRepository<AppUser> AppUsers { get; }
        IRepository<Artist> Artists { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Message> Messages { get; }
        IRepository<SocialNetwork> SocialNetworks { get; }
        IRepository<SocialAccountLink> SocialAccountLink { get; }
        IRepository<Song> Songs { get; }
        IRepository<Thanx> Thanxies { get; }

        int SaveChanges();
    }
}