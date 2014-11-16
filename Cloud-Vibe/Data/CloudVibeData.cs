using Cloud_Vibe.Data.Models;
using Cloud_Vibe.Data.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data
{
    public class CloudVibeData : ICloudVibeData 
    {
        private readonly ICloudDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public CloudVibeData(ICloudDbContext context)
        {
            this.context = context;
        }

        public ICloudDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IDeletableEntityRepository<Album> Albums
        {
            get { return this.GetDeletableEntityRepository<Album>(); }
        }

        public IRepository<Artist> Artists
        {
            get { return this.GetRepository<Artist>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Message> Messages
        {
            get { return this.GetRepository<Message>(); }
        }

        public IRepository<SocialNetwork> SocialNetworks
        {
            get { return this.GetRepository<SocialNetwork>(); }
        }

        public IRepository<SocialAccountLink> SocialAccountLinks
        {
            get { return this.GetRepository<SocialAccountLink>(); }
        }

        public IDeletableEntityRepository<Song> Songs
        {
            get { return this.GetDeletableEntityRepository<Song>(); }
        }

        public IRepository<Thanx> Thanxies
        {
            get { return this.GetRepository<Thanx>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EFRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
        
    }
}