using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Entities
{
    public class CrExtContext : IdentityDbContext<Users>
    {

        public CrExtContext(DbContextOptions<CrExtContext> options) : base(options)
        {
            Database.Migrate();
        }
        public new DbSet<Users> Users { get; set; }
        //public new DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<TeamMembers> TeamMembers { get; set; }
        public new DbSet<Roles> Roles { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<PRole> PRoles { get; set; }
        public DbSet<PackagePRoles> PackagePRoles { get; set; }

        public DbSet<Organisations> Organisations { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Customers> Customers { get; set; }

        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<ReviewActions> ReviewActions { get; set; }
        public DbSet<ReviewNotifications> ReviewNotifications { get; set; }
        public DbSet<ReviewKinds> ReviewKinds { get; set; }
        public DbSet<Replies> Replies { get; set; }


        public DbSet<AssignedProjects> AssignedProjects { get; set; }
        public DbSet<UserOrganisation> UserOrganisations { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           

            modelBuilder.Entity<Packages>().HasMany(a => a.PackagePRoles);
         
            modelBuilder.Entity<PackagePRoles>().HasKey(a => new { a.PackageId, a.PRoleId });
            modelBuilder.Entity<PackagePRoles>().HasOne(a => a.Package).WithMany(b => b.PackagePRoles)
                                                .HasForeignKey(b => b.PackageId);

            modelBuilder.Entity<PackagePRoles>().HasOne(a => a.PRole)
                                                .WithMany(c => c.PackagePRoles)
                                                .HasForeignKey(c => c.PRoleId);

            modelBuilder.Entity<Projects>().HasOne(a => a.Organisation);
            modelBuilder.Entity<Projects>().HasMany(a => a.Teams);
         //   modelBuilder.Entity<Projects>().HasOne(a => a.ReviewNotifications);
            modelBuilder.Entity<Organisations>().HasOne(a => a.Package).WithMany(a => a.Organisations);
            modelBuilder.Entity<Organisations>().HasMany(a => a.Projects).WithOne(a=>a.Organisation);
            modelBuilder.Entity<Organisations>().HasMany(a => a.Teams).WithOne(a => a.organisation);


            modelBuilder.Entity<Teams>().HasOne(a => a.Project);
            modelBuilder.Entity<Teams>().HasOne(a => a.organisation);
            modelBuilder.Entity<Teams>().HasMany(a => a.TeamMembers);


            //modelBuilder.Entity<Teams>();

            modelBuilder.Entity<TeamMembers>().HasOne(a => a.Projects);
            modelBuilder.Entity<TeamMembers>().HasOne(a => a.Teams).WithMany(a=>a.TeamMembers);
                
            modelBuilder.Entity<TeamMembers>().HasOne(a => a.User);
         


            modelBuilder.Entity<Customers>().HasOne(a => a.Team);
            modelBuilder.Entity<Customers>().HasMany(a => a.Reviews).WithOne(a => a.Customer);

           // modelBuilder.Entity<Reviews>().HasMany(a => a.ReviewNotifications).WithOne(a => a.Review);
            modelBuilder.Entity<ReviewActions>().HasMany(a => a.ReviewNotifications).WithOne(a => a.ReviewAction);
            modelBuilder.Entity<ReviewKinds>().HasMany(a => a.ReviewNotifications).WithOne(a => a.ReviewKind);
            modelBuilder.Entity<ReviewNotifications>().HasOne(a => a.Review);
            modelBuilder.Entity<AssignedProjects>().HasOne(a => a.Project);
            modelBuilder.Entity<AssignedProjects>().HasOne(a => a.AssignedToUser);
            modelBuilder.Entity<AssignedProjects>().HasOne(a => a.AssignedbyUser);

            modelBuilder.Entity<UserOrganisation>().HasOne(a => a.Organisations);
            modelBuilder.Entity<UserOrganisation>().HasOne(a => a.User);

            modelBuilder.Entity<Reviews>().HasOne(a => a.Customer)
                .WithMany(a => a.Reviews)
                .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Replies>().HasOne(a => a.Review).WithMany(a=>a.Replies);

            modelBuilder.Entity<Replies>().HasOne(a => a.Users);

            // .OnDelete(DeleteBehavior.ClientSetNull)
           //modelBuilder.Entity<CustomerFeedBacks>().HasOne(a => a.Customers);
          //modelBuilder.Entity<CustomerFeedBacks>().HasOne(a => a.FeedBackType);

        }

    }
}
//add-migration InitialMigrations -verbose