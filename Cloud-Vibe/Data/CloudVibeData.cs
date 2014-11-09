using Cloud_Vibe.Data.Models;
using Cloud_Vibe.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data
{
    public class CloudVibeData : ICloudVibeData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public CloudVibeData(CloudVibeDbContex context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Album> Albums
        {
            get { return this.GetRepository<Album>(); }
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

        public IRepository<Song> Songs
        {
            get { return this.GetRepository<Song>(); }
        }

        public IRepository<Thanx> Thanxies
        {
            get { return this.GetRepository<Thanx>(); }
        }
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}