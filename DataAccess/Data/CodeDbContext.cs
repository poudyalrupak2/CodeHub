using Microsoft.AspNet.Identity.EntityFramework;
using Models;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]

    public class CodeDbContext:IdentityDbContext<ApplicationUser>
    {

        public CodeDbContext():base("defaultconnection")
        {
            this.Configuration.ValidateOnSaveEnabled = false;

        }
        public static CodeDbContext Create()
        {
            return new CodeDbContext();
        }
        public DbSet<InpCodes> InpCodes { get; set; }
        public DbSet<InpCodesImages> InpCodesImages { get; set; }
        public DbSet<InpWorks> InpWorks { get; set; }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }


        private void AddTimestamps()
        {
            try
            {
                var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

                IEnumerable<Claim> claims = identity.Claims;


                var currentUsername = claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();

                // = HttpContext.Current.Session["userEmail"].ToString();

                foreach (var entity in entities)
                {
                    if (entity.State == EntityState.Added)
                    {
                        ((BaseEntity)entity.Entity).DateCreated = DateTime.UtcNow;
                        ((BaseEntity)entity.Entity).UserCreated = currentUsername;
                    }
                    else
                    {

                        ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
                        ((BaseEntity)entity.Entity).UserModified = currentUsername;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
