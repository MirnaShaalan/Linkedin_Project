using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LastLinkedin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("firstName", this.firstName));
            userIdentity.AddClaim(new Claim("lastName", this.lastName));
            userIdentity.AddClaim(new Claim("Email", this.Email));
            //userIdentity.AddClaim(new Claim("Connections", this.Connections.Count.ToString()));
            await manager.AddClaimAsync(this.Id, new Claim("firstName", this.firstName));
            await manager.AddClaimAsync(this.Id, new Claim("lastName", this.lastName));
            await manager.AddClaimAsync(this.Id, new Claim("Email", this.Email));
            return userIdentity;
        }


        /////relations of memeber
        /// <summary>
        /// for messaging
        /// </summary>
        //[Required]
        //public string ConnectionCode { get; set; }   //connection id
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }

        public string UserPhoto { get; set; }
        public string BackgroundPhoto { get; set; }

        ////////////////////////


        public virtual Address Address { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual ICollection<Connection> Connections { get; set; }
        public virtual ICollection<Connection> Connections1 { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
        public virtual ICollection<Recommendation> Recommendations1 { get; set; }
        public virtual ICollection<People_Being_Followed> People_Being_Followeds { get; set; }
        public virtual ICollection<People_Being_Followed> People_Being_Followeds1 { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<UserJob> UserJobs { get; set; }
        public virtual ICollection<UserEducation> UserEducations { get; set; }
        public virtual ICollection<UserWorkExperience> UserWorkExperiences { get; set; }



    }



    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
               .HasMany(e => e.Connections)
               .WithOptional(e => e.User)
               .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Connections1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.connection_user_id);

            modelBuilder.Entity<ApplicationUser>()
           .HasMany(e => e.People_Being_Followeds)
           .WithRequired(e => e.User)
           .HasForeignKey(e => e.UserId)
           .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.People_Being_Followeds1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.User_being_followed_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Recommendations)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_recommending_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Recommendations1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.User_being_recommended_id)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Member> Members { get; set; }
        // public virtual ICollection<ApplicationUser> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<People_Being_Followed> People_Being_Followeds { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<GMessage> GMessages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<UserJob> UserJobs { get; set; }
        public virtual DbSet<OrganizationJob> OrganizationJobs { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
        public virtual DbSet<UserWorkExperience> UserWorkExperiences { get; set; }
        public virtual DbSet<UserEducation> UserEducations { get; set; }










    }
}