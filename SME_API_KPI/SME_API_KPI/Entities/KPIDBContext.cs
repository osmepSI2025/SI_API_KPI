using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SME_API_KPI.Entities;

public partial class KPIDBContext : DbContext
{
    public KPIDBContext()
    {
    }

    public KPIDBContext(DbContextOptions<KPIDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MApiInformation> MApiInformations { get; set; }

    public virtual DbSet<MDimensionSystem> MDimensionSystems { get; set; }

    public virtual DbSet<MDivision> MDivisions { get; set; }

    public virtual DbSet<MInputFormate> MInputFormates { get; set; }

    public virtual DbSet<MKpiType> MKpiTypes { get; set; }

    public virtual DbSet<MMeasure> MMeasures { get; set; }

    public virtual DbSet<MPlanBudgetYear> MPlanBudgetYears { get; set; }

    public virtual DbSet<MPlanKpi> MPlanKpis { get; set; }

    public virtual DbSet<MPlanKpiAssign> MPlanKpiAssigns { get; set; }

    public virtual DbSet<MPlanKpiDescription> MPlanKpiDescriptions { get; set; }

    public virtual DbSet<MPlanKpiList> MPlanKpiLists { get; set; }

    public virtual DbSet<MPlanKpiTarget> MPlanKpiTargets { get; set; }

    public virtual DbSet<MPlanName> MPlanNames { get; set; }

    public virtual DbSet<MPlanPeriod> MPlanPeriods { get; set; }

    public virtual DbSet<MPlanResult> MPlanResults { get; set; }

    public virtual DbSet<MPlanTargetDescription> MPlanTargetDescriptions { get; set; }

    public virtual DbSet<MPlanweight> MPlanweights { get; set; }

    public virtual DbSet<MScheduledJob> MScheduledJobs { get; set; }

    public virtual DbSet<MStatus> MStatuses { get; set; }

    public virtual DbSet<TKpiTarget> TKpiTargets { get; set; }

    public virtual DbSet<TPlanKpidivision> TPlanKpidivisions { get; set; }

    public virtual DbSet<TPlanKpilist> TPlanKpilists { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=27.254.173.62;Database=bluecarg_SME_API_KPI;User Id=SME_KPI;Password=1Uf@e9g60;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SME_KPI");

        modelBuilder.Entity<MApiInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MApiInformation");

            entity.ToTable("M_ApiInformation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessToken).HasColumnName("accessToken");
            entity.Property(e => e.ApiKey).HasMaxLength(150);
            entity.Property(e => e.AuthorizationType).HasMaxLength(50);
            entity.Property(e => e.ContentType).HasMaxLength(150);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MethodType).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.ServiceNameCode).HasMaxLength(250);
            entity.Property(e => e.ServiceNameTh).HasMaxLength(250);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Urldevelopment).HasColumnName("URLDevelopment");
            entity.Property(e => e.Urlproduction).HasColumnName("URLProduction");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<MDimensionSystem>(entity =>
        {
            entity.HasKey(e => e.Dimensionid);

            entity.ToTable("M_DimensionSystem");

            entity.Property(e => e.Dimensionid)
                .ValueGeneratedNever()
                .HasColumnName("dimensionid");
            entity.Property(e => e.Dimensionname).HasColumnName("dimensionname");
        });

        modelBuilder.Entity<MDivision>(entity =>
        {
            entity.HasKey(e => e.Divisionid);

            entity.ToTable("M_Division");

            entity.Property(e => e.Divisionid)
                .ValueGeneratedNever()
                .HasColumnName("divisionid");
            entity.Property(e => e.Divisioncode)
                .HasMaxLength(255)
                .HasColumnName("divisioncode");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(255)
                .HasColumnName("divisionname");
        });

        modelBuilder.Entity<MInputFormate>(entity =>
        {
            entity.HasKey(e => e.Masterid);

            entity.ToTable("M_InputFormate");

            entity.Property(e => e.Masterid)
                .ValueGeneratedNever()
                .HasColumnName("masterid");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<MKpiType>(entity =>
        {
            entity.HasKey(e => e.Masterid);

            entity.ToTable("M_KpiType");

            entity.Property(e => e.Masterid)
                .ValueGeneratedNever()
                .HasColumnName("masterid");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<MMeasure>(entity =>
        {
            entity.ToTable("M_Measure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Masterid).HasColumnName("masterid");
        });

        modelBuilder.Entity<MPlanBudgetYear>(entity =>
        {
            entity.HasKey(e => e.Year).HasName("PK_M_BudgetYear");

            entity.ToTable("M_PlanBudgetYear");

            entity.Property(e => e.Year)
                .ValueGeneratedNever()
                .HasColumnName("year");
        });

        modelBuilder.Entity<MPlanKpi>(entity =>
        {
            entity.HasKey(e => new { e.PlanId, e.Kpiid }).HasName("PK__M_PlanKP__72724B9D34509BDB");

            entity.ToTable("M_PlanKPI");

            entity.Property(e => e.Kpiid).HasColumnName("KPIId");
        });

        modelBuilder.Entity<MPlanKpiAssign>(entity =>
        {
            entity.ToTable("M_PlanKpiAssign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Kpiid).HasColumnName("kpiid");
            entity.Property(e => e.Planid).HasColumnName("planid");
        });

        modelBuilder.Entity<MPlanKpiDescription>(entity =>
        {
            entity.ToTable("M_PlanKpiDescription");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Kpidescription).HasColumnName("kpidescription");
            entity.Property(e => e.Kpiid).HasColumnName("kpiid");
            entity.Property(e => e.Planid).HasColumnName("planid");
        });

        modelBuilder.Entity<MPlanKpiList>(entity =>
        {
            entity.ToTable("M_PlanKpiList");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Effectivedate)
                .HasMaxLength(50)
                .HasColumnName("effectivedate");
            entity.Property(e => e.Enddate)
                .HasMaxLength(50)
                .HasColumnName("enddate");
            entity.Property(e => e.PlanTypeid)
                .HasMaxLength(50)
                .HasColumnName("planTypeid");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Planremark).HasColumnName("planremark");
            entity.Property(e => e.Plantitle)
                .HasMaxLength(255)
                .HasColumnName("plantitle");
            entity.Property(e => e.Planyear).HasColumnName("planyear");
        });

        modelBuilder.Entity<MPlanKpiTarget>(entity =>
        {
            entity.HasKey(e => new { e.PlanId, e.Kpiid }).HasName("PK__M_PlanKp__72724B9DBF373EBD");

            entity.ToTable("M_PlanKpiTarget");

            entity.Property(e => e.Kpiid).HasColumnName("KPIId");
        });

        modelBuilder.Entity<MPlanName>(entity =>
        {
            entity.ToTable("M_PlanName");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlanTypeId).HasMaxLength(50);
        });

        modelBuilder.Entity<MPlanPeriod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_M_Period");

            entity.ToTable("M_PlanPeriod");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Enddate)
                .HasMaxLength(50)
                .HasColumnName("enddate");
            entity.Property(e => e.Fullname).HasColumnName("fullname");
            entity.Property(e => e.Period)
                .HasMaxLength(50)
                .HasColumnName("period");
            entity.Property(e => e.PeriodId).HasColumnName("periodID");
            entity.Property(e => e.PlanTypeId)
                .HasMaxLength(50)
                .HasColumnName("planTypeId");
            entity.Property(e => e.PlanYear).HasColumnName("planYear");
            entity.Property(e => e.Planid)
                .HasMaxLength(50)
                .HasColumnName("planid");
            entity.Property(e => e.Startdate)
                .HasMaxLength(50)
                .HasColumnName("startdate");
            entity.Property(e => e.Userid)
                .HasMaxLength(50)
                .HasColumnName("userid");
        });

        modelBuilder.Entity<MPlanResult>(entity =>
        {
            entity.ToTable("M_PlanResult");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Assignid)
                .HasMaxLength(255)
                .HasColumnName("assignid");
            entity.Property(e => e.Kpiid).HasColumnName("kpiid");
            entity.Property(e => e.Periodid)
                .HasMaxLength(255)
                .HasColumnName("periodid");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Point).HasColumnName("point");
            entity.Property(e => e.Result)
                .HasMaxLength(255)
                .HasColumnName("result");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
        });

        modelBuilder.Entity<MPlanTargetDescription>(entity =>
        {
            entity.ToTable("M_PlanTargetDescription");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Kpiid).HasColumnName("kpiid");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Target)
                .HasMaxLength(255)
                .HasColumnName("target");
        });

        modelBuilder.Entity<MPlanweight>(entity =>
        {
            entity.ToTable("M_Planweight");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Kpiid).HasColumnName("kpiid");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("weight");
        });

        modelBuilder.Entity<MScheduledJob>(entity =>
        {
            entity.ToTable("M_ScheduledJobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.JobName).HasMaxLength(150);
        });

        modelBuilder.Entity<MStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_M_StatusData");

            entity.ToTable("M_Status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Masterid).HasColumnName("masterid");
        });

        modelBuilder.Entity<TKpiTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_KPI_Ta__3214EC073C46451A");

            entity.ToTable("T_KPI_Target");

            entity.Property(e => e.Kpiid).HasColumnName("KPIId");
            entity.Property(e => e.LabelStr).HasMaxLength(100);
            entity.Property(e => e.LevelDesc).HasMaxLength(255);
            entity.Property(e => e.PeriodDetail).HasMaxLength(100);
            entity.Property(e => e.PeriodId).HasColumnName("PeriodID");

            entity.HasOne(d => d.MPlanKpiTarget).WithMany(p => p.TKpiTargets)
                .HasForeignKey(d => new { d.PlanId, d.Kpiid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_KPI_Target__5BE2A6F2");
        });

        modelBuilder.Entity<TPlanKpidivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_PlanKP__3214EC07640AE7B0");

            entity.ToTable("T_PlanKPIDivision");

            entity.Property(e => e.DivisionName).HasMaxLength(255);
            entity.Property(e => e.KpilistId).HasColumnName("KPIListId");

            entity.HasOne(d => d.Kpilist).WithMany(p => p.TPlanKpidivisions)
                .HasForeignKey(d => d.KpilistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_PlanKPI__KPILi__6383C8BA");
        });

        modelBuilder.Entity<TPlanKpilist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_PlanKP__3214EC072B0EECDE");

            entity.ToTable("T_PlanKPIList");

            entity.Property(e => e.Kpiid).HasColumnName("KPIId");
            entity.Property(e => e.Kpiindex).HasColumnName("KPIIndex");
            entity.Property(e => e.Kpiname)
                .HasMaxLength(255)
                .HasColumnName("KPIName");
            entity.Property(e => e.Target).HasMaxLength(255);
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.MPlanKpi).WithMany(p => p.TPlanKpilists)
                .HasForeignKey(d => new { d.PlanId, d.Kpiid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_PlanKPIList__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
