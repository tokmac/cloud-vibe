using Cloud_Vibe.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Cloud_Vibe.Data.Migrations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Cloud_Vibe.Data.Contracts.CodeFirstConventions;

namespace Cloud_Vibe.Data
{
    public class CloudVibeDbContex : IdentityDbContext<User>, ICloudDbContext
    {
        public CloudVibeDbContex()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }
        public CloudVibeDbContex(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CloudVibeDbContex, Configuration>());
        }


        public virtual IDbSet<Album> Albums { get; set; }
        public virtual IDbSet<Artist> Artists { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Message> Messages { get; set; }
        public virtual IDbSet<SocialNetwork> SocialNetworks { get; set; }
        public virtual IDbSet<SocialAccountLink> SocialAccountLinks { get; set; }
        public virtual IDbSet<Song> Songs { get; set; }
        public virtual IDbSet<Thanx> Thanxies { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }


        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static CloudVibeDbContex Create()
        {
            return new CloudVibeDbContex();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder); // Without this call EntityFramework won't be able to configure the identity model
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}