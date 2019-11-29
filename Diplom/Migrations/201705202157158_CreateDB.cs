namespace Diplom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BeginningDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        OrderNum = c.Int(nullable: false),
                        Position = c.String(),
                        WorkerId = c.Int(nullable: false),
                        ArtistCategoryId = c.Int(nullable: false),
                        ContractTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ArtistCategories", t => t.ArtistCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ContractTypes", t => t.ContractTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerId, cascadeDelete: true)
                .Index(t => t.WorkerId)
                .Index(t => t.ArtistCategoryId)
                .Index(t => t.ContractTypeId);
            
            CreateTable(
                "dbo.ContractTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Patronymic = c.String(),
                        RegistrationPlace = c.String(),
                        BirthPlace = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Nationality = c.String(),
                        Education = c.String(),
                        Sex = c.Boolean(nullable: false),
                        MaritalStatus = c.Boolean(nullable: false),
                        PhoneNum = c.String(),
                        Email = c.String(),
                        DepartamentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departaments", t => t.DepartamentID, cascadeDelete: true)
                .Index(t => t.DepartamentID);
            
            CreateTable(
                "dbo.ConcertMarks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumOfMarks = c.Int(nullable: false),
                        ConcertEventID = c.Int(nullable: false),
                        WorkerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ConcertEvents", t => t.ConcertEventID, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerID, cascadeDelete: true)
                .Index(t => t.ConcertEventID)
                .Index(t => t.WorkerID);
            
            CreateTable(
                "dbo.ConcertEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BeginningDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Country = c.String(),
                        City = c.String(),
                        Address = c.String(),
                        Description = c.String(),
                        ConcertPlaceTypeId = c.Int(nullable: false),
                        ConcertTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ConcertPlaceTypes", t => t.ConcertPlaceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ConcertTypes", t => t.ConcertTypeId, cascadeDelete: true)
                .Index(t => t.ConcertPlaceTypeId)
                .Index(t => t.ConcertTypeId);
            
            CreateTable(
                "dbo.ConcertPlaceTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ConcertTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Departaments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LessonMarks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsVisited = c.Boolean(nullable: false),
                        LessonEventID = c.Int(nullable: false),
                        WorkerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LessonEvents", t => t.LessonEventID, cascadeDelete: true)
                .ForeignKey("dbo.Workers", t => t.WorkerID, cascadeDelete: true)
                .Index(t => t.LessonEventID)
                .Index(t => t.WorkerID);
            
            CreateTable(
                "dbo.LessonEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Workers", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ID", "dbo.Workers");
            DropForeignKey("dbo.LessonMarks", "WorkerID", "dbo.Workers");
            DropForeignKey("dbo.LessonMarks", "LessonEventID", "dbo.LessonEvents");
            DropForeignKey("dbo.Workers", "DepartamentID", "dbo.Departaments");
            DropForeignKey("dbo.Contracts", "WorkerId", "dbo.Workers");
            DropForeignKey("dbo.ConcertMarks", "WorkerID", "dbo.Workers");
            DropForeignKey("dbo.ConcertEvents", "ConcertTypeId", "dbo.ConcertTypes");
            DropForeignKey("dbo.ConcertEvents", "ConcertPlaceTypeId", "dbo.ConcertPlaceTypes");
            DropForeignKey("dbo.ConcertMarks", "ConcertEventID", "dbo.ConcertEvents");
            DropForeignKey("dbo.Contracts", "ContractTypeId", "dbo.ContractTypes");
            DropForeignKey("dbo.Contracts", "ArtistCategoryId", "dbo.ArtistCategories");
            DropIndex("dbo.Users", new[] { "ID" });
            DropIndex("dbo.LessonMarks", new[] { "WorkerID" });
            DropIndex("dbo.LessonMarks", new[] { "LessonEventID" });
            DropIndex("dbo.ConcertEvents", new[] { "ConcertTypeId" });
            DropIndex("dbo.ConcertEvents", new[] { "ConcertPlaceTypeId" });
            DropIndex("dbo.ConcertMarks", new[] { "WorkerID" });
            DropIndex("dbo.ConcertMarks", new[] { "ConcertEventID" });
            DropIndex("dbo.Workers", new[] { "DepartamentID" });
            DropIndex("dbo.Contracts", new[] { "ContractTypeId" });
            DropIndex("dbo.Contracts", new[] { "ArtistCategoryId" });
            DropIndex("dbo.Contracts", new[] { "WorkerId" });
            DropTable("dbo.Users");
            DropTable("dbo.LessonEvents");
            DropTable("dbo.LessonMarks");
            DropTable("dbo.Departaments");
            DropTable("dbo.ConcertTypes");
            DropTable("dbo.ConcertPlaceTypes");
            DropTable("dbo.ConcertEvents");
            DropTable("dbo.ConcertMarks");
            DropTable("dbo.Workers");
            DropTable("dbo.ContractTypes");
            DropTable("dbo.Contracts");
            DropTable("dbo.ArtistCategories");
        }
    }
}
