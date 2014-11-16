using Cloud_Vibe.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Cloud_Vibe.Data.Migrations;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Cloud_Vibe.Data
{
    public class CloudVibeDbContex : IdentityDbContext<User>
    {
        public CloudVibeDbContex()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CloudVibeDbContex, Configuration>());
        }

        public static CloudVibeDbContex Create()
        {
            return new CloudVibeDbContex();
        }

        public IDbSet<Album> Albums { get; set; }
        public IDbSet<Artist> Artists { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<SocialNetwork> SocialNetworks { get; set; }
        public IDbSet<SocialAccountLink> SocialAccountLinks { get; set; }
        public IDbSet<Song> Songs { get; set; }
        public IDbSet<Thanx> Thanxies { get; set; }

        public override int SaveChanges()
        {
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
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