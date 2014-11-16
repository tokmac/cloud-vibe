using Cloud_Vibe.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Vibe.Data
{
    public interface ICloudDbContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Album> Albums { get; set; }
        IDbSet<Artist> Artists { get; set; }
        IDbSet<Comment> Comments { get; set; }
        IDbSet<Message> Messages { get; set; }
        IDbSet<SocialNetwork> SocialNetworks { get; set; }
        IDbSet<SocialAccountLink> SocialAccountLinks { get; set; }
        IDbSet<Song> Songs { get; set; }
        IDbSet<Thanx> Thanxies { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
