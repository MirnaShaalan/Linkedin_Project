namespace LastLinkedin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Line_1 = c.String(),
                        Line_2 = c.String(),
                        Line_3 = c.String(),
                        city = c.String(),
                        State_County_Province = c.String(),
                        Zip_or_Postcode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        UserPhoto = c.String(),
                        BackgroundPhoto = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Address_ID = c.Int(),
                        Organization_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_ID)
                .ForeignKey("dbo.Organizations", t => t.Organization_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Address_ID)
                .Index(t => t.Organization_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Connections",
                c => new
                    {
                        Connectionid = c.Int(nullable: false, identity: true),
                        connection_user_id = c.String(maxLength: 128),
                        user_id = c.String(maxLength: 128),
                        Accepted = c.Boolean(),
                        date_connection_made = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Connectionid)
                .ForeignKey("dbo.AspNetUsers", t => t.user_id)
                .ForeignKey("dbo.AspNetUsers", t => t.connection_user_id)
                .Index(t => t.connection_user_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ReceiverId = c.String(nullable: false, maxLength: 128),
                        Msg = c.String(nullable: false),
                        Date = c.String(),
                        Read = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Organization_Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrganizationJobs",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        OrganizationID = c.Int(nullable: false),
                        Job_ID = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserID, t.OrganizationID })
                .ForeignKey("dbo.Jobs", t => t.Job_ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID)
                .Index(t => t.Job_ID);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserJobs",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.JobID })
                .ForeignKey("dbo.Jobs", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.People_Being_Followed",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        User_being_followed_id = c.String(nullable: false, maxLength: 128),
                        date_start_following = c.DateTime(nullable: false, storeType: "date"),
                        date_stopped_following = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => new { t.UserId, t.date_start_following })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_being_followed_id)
                .Index(t => t.UserId)
                .Index(t => t.User_being_followed_id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        PublishedPostDate = c.DateTime(nullable: false),
                        ReadPost = c.Int(nullable: false),
                        Liked = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PublishedID = c.String(maxLength: 128),
                        Description = c.String(nullable: false),
                        Post_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID)
                .Index(t => t.Post_ID);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PublishedID = c.String(maxLength: 128),
                        Post_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.Post_ID)
                .Index(t => t.Post_ID);
            
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        User_recommending_id = c.String(nullable: false, maxLength: 128),
                        User_being_recommended_id = c.String(nullable: false, maxLength: 128),
                        date_of_recommendation = c.DateTime(storeType: "date"),
                        other_details = c.String(),
                    })
                .PrimaryKey(t => new { t.User_recommending_id, t.User_being_recommended_id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_recommending_id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_being_recommended_id)
                .Index(t => t.User_recommending_id)
                .Index(t => t.User_being_recommended_id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserEducations",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        EducationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.EducationID })
                .ForeignKey("dbo.Educations", t => t.EducationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.EducationID);
            
            CreateTable(
                "dbo.Educations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        GroupID = c.Int(nullable: false),
                        DateJoin = c.DateTime(),
                        DateLeft = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.UserID, t.GroupID })
                .ForeignKey("dbo.Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Admin_Id = c.String(maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        lastActivity = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        ReceiverId = c.String(maxLength: 128),
                        Msg = c.String(nullable: false),
                        Date = c.String(),
                        Read = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.UserWorkExperiences",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        WorkExperienceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.WorkExperienceID })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.WorkExperiences", t => t.WorkExperienceID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.WorkExperienceID);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        date_created = c.DateTime(nullable: false),
                        date_updated = c.DateTime(nullable: false),
                        cvPath = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CVs", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserWorkExperiences", "WorkExperienceID", "dbo.WorkExperiences");
            DropForeignKey("dbo.UserWorkExperiences", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroups", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroups", "GroupID", "dbo.Groups");
            DropForeignKey("dbo.GMessages", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.UserEducations", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserEducations", "EducationID", "dbo.Educations");
            DropForeignKey("dbo.Skills", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Recommendations", "User_being_recommended_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Recommendations", "User_recommending_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "Post_ID", "dbo.Posts");
            DropForeignKey("dbo.People_Being_Followed", "User_being_followed_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.People_Being_Followed", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Organization_ID", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationJobs", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.UserJobs", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserJobs", "JobID", "dbo.Jobs");
            DropForeignKey("dbo.OrganizationJobs", "Job_ID", "dbo.Jobs");
            DropForeignKey("dbo.Messages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Connections", "connection_user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Connections", "user_id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Address_ID", "dbo.Addresses");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CVs", new[] { "User_Id" });
            DropIndex("dbo.UserWorkExperiences", new[] { "WorkExperienceID" });
            DropIndex("dbo.UserWorkExperiences", new[] { "UserID" });
            DropIndex("dbo.GMessages", new[] { "GroupId" });
            DropIndex("dbo.UserGroups", new[] { "GroupID" });
            DropIndex("dbo.UserGroups", new[] { "UserID" });
            DropIndex("dbo.UserEducations", new[] { "EducationID" });
            DropIndex("dbo.UserEducations", new[] { "UserID" });
            DropIndex("dbo.Skills", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Recommendations", new[] { "User_being_recommended_id" });
            DropIndex("dbo.Recommendations", new[] { "User_recommending_id" });
            DropIndex("dbo.Likes", new[] { "Post_ID" });
            DropIndex("dbo.Comments", new[] { "Post_ID" });
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.People_Being_Followed", new[] { "User_being_followed_id" });
            DropIndex("dbo.People_Being_Followed", new[] { "UserId" });
            DropIndex("dbo.UserJobs", new[] { "JobID" });
            DropIndex("dbo.UserJobs", new[] { "UserID" });
            DropIndex("dbo.OrganizationJobs", new[] { "Job_ID" });
            DropIndex("dbo.OrganizationJobs", new[] { "OrganizationID" });
            DropIndex("dbo.Messages", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Connections", new[] { "user_id" });
            DropIndex("dbo.Connections", new[] { "connection_user_id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Organization_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Address_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CVs");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.UserWorkExperiences");
            DropTable("dbo.GMessages");
            DropTable("dbo.Groups");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Educations");
            DropTable("dbo.UserEducations");
            DropTable("dbo.Skills");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Recommendations");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.Posts");
            DropTable("dbo.People_Being_Followed");
            DropTable("dbo.UserJobs");
            DropTable("dbo.Jobs");
            DropTable("dbo.OrganizationJobs");
            DropTable("dbo.Organizations");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Connections");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
