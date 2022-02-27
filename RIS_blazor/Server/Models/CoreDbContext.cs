using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RIS_blazor.Shared.Models.VWModels;
using RIS_blazor.Shared.Models;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
#nullable disable

namespace RIS_blazor.Server.Models
{
    public partial class CoreDbContext : DbContext
    {

        public CoreDbContext()
        {
            this.Database.SetCommandTimeout(5000);
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {

        }

        public String ConnectionString { get; set; }

        public virtual DbSet<AllowedModality> AllowedModalities { get; set; }
        public virtual DbSet<DatabaseDataset> DatabaseDatasets { get; set; }
        public virtual DbSet<HismodalityProcedureMapping> HismodalityProcedureMappings { get; set; }
        public virtual DbSet<Hisprocedure> Hisprocedures { get; set; }
        public virtual DbSet<MasterTemplate> MasterTemplates { get; set; }
        public virtual DbSet<MenuPermission> MenuPermissions { get; set; }
        public virtual DbSet<Modality> Modalities { get; set; }
        public virtual DbSet<ProcedureLog> ProcedureLogs { get; set; }
        public virtual DbSet<ProcedureRadiologistTemplate> ProcedureRadiologistTemplates { get; set; }
        public virtual DbSet<ProcedureStatus> ProcedureStatuses { get; set; }
        public virtual DbSet<ProjectMenu> ProjectMenus { get; set; }
        public virtual DbSet<RadiologistOpinionOne> RadiologistOpinionOnes { get; set; }
        public virtual DbSet<RadiologistOpinionTwo> RadiologistOpinionTwoes { get; set; }
        public virtual DbSet<RemoteDicomNode> RemoteDicomNodes { get; set; }
        public virtual DbSet<ReportConsultant> ReportConsultants { get; set; }
        public virtual DbSet<RISWorkList> RISWorkLists { get; set; }
        public virtual DbSet<NextCloudUser> NextCloudUserLists { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShortCutKey> ShortCutKeys { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }
        public virtual DbSet<TenantDefaultConsultantMapping> TenantDefaultConsultantMappings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<MainMenuItem> MenuItems { get; set; }

        public virtual DbSet<PrintPageSetup> PrintPageSetups { get; set; }

        public virtual DbSet<HtmlTempleForReport> HtmlTempleForReports { get; set; }

        public virtual DbSet<ConsultantOpinionOnStudy> ConsultantOpinionOnStudies { get; set; } //Report generated from Html editor will be saved here.

        public virtual DbSet<ReferralPhysician> ReferralPhysicians { get; set; }

        public virtual DbSet<VMRISWorklist> VMRISWorklists { get; set; }
        public virtual DbSet<VMHISModalityProcedureMapping> VMHISModalityProcedureMappings { get; set; }
        public virtual DbSet<VMRadProcTemplate> VMRadProcTemplates { get; set; }
        public virtual DbSet<VMRemoteDicomNode> VMRemoteDicomNodes { get; set; }
        public virtual DbSet<VMReportObj> VMReportObjs { get; set; }
        public virtual DbSet<VMTenantRadiologistMapping> VMTenantRadiologistMappings { get; set; }
        public virtual DbSet<VMUserDetail> VMUserDetails { get; set; }


        public virtual DbSet<VwradReportOpinion> VwradReportOpinions { get; set; }
        public virtual DbSet<Vwworklist> Vwworklists { get; set; }

        public virtual DbSet<VMReportConsultant> VMReportConsultants { get; set; }
        public virtual DbSet<SelectedProcedureForAssign> SelectedProcedureForAssigns { get; set; }
        public IEnumerable<object> NextCloudUserList { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //ConnectionString = ("Data Source=EMSL; Initial Catalog=TestDicomDb;Persist Security Info=False;User ID=sa;Password=EmslDicomServer@2021;Connection Timeout=1200; multipleactiveresultsets=true;");

            ConnectionString = ("Data Source=localhost; Initial Catalog=TestDicomDb;Persist Security Info=False;Trusted_Connection=True;Connection Timeout=1200; multipleactiveresultsets=true;");


            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseDataset>(entity =>
            {
                entity.HasKey(e => e.DatasetId)
                    .HasName("PK__DatabaseDatasets__0000000000000030");
            });

            modelBuilder.Entity<ProjectMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK_Menu");
            });

            modelBuilder.Entity<RadiologistOpinionTwo>(entity =>
            {
                entity.HasKey(e => e.OpId)
                    .HasName("PK_RadiologistOpinionTwos");
            });

            modelBuilder.Entity<VMHISModalityProcedureMapping>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<VMRISWorklist>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<SelectedProcedureForAssign>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<VMRadProcTemplate>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<VMRemoteDicomNode>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<VMReportObj>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<VMTenantRadiologistMapping>(entity =>
            {
                entity.HasNoKey();
            });

            modelBuilder.Entity<VMUserDetail>(entity =>
            {
                entity.HasNoKey();
            });


            modelBuilder.Entity<RemoteDicomNode>(entity =>
            {
                entity.Property(e => e.NodeGuid).ValueGeneratedNever();
            });

            modelBuilder.Entity<VwradReportOpinion>(entity =>
            {
                entity.ToView("VWRadReportOpinions");
            });

            modelBuilder.Entity<Vwworklist>(entity =>
            {
                entity.ToView("VWWorklist");
            });

            modelBuilder.Entity<VMReportConsultant>(entity =>
            {
                entity.HasNoKey();
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }

}

