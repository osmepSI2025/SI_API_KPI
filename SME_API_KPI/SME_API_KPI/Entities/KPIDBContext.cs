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

    public virtual DbSet<MExportEval> MExportEvals { get; set; }

    public virtual DbSet<MInputFormate> MInputFormates { get; set; }

    public virtual DbSet<MKpiSystemAssign> MKpiSystemAssigns { get; set; }

    public virtual DbSet<MKpiSystemKpiTarget> MKpiSystemKpiTargets { get; set; }

    public virtual DbSet<MKpiSystemWeight> MKpiSystemWeights { get; set; }

    public virtual DbSet<MKpiType> MKpiTypes { get; set; }

    public virtual DbSet<MMeasure> MMeasures { get; set; }

    public virtual DbSet<MPlanBudgetYear> MPlanBudgetYears { get; set; }

    public virtual DbSet<MPlanKpi> MPlanKpis { get; set; }

    public virtual DbSet<MPlanKpiDescription> MPlanKpiDescriptions { get; set; }

    public virtual DbSet<MPlanKpiList> MPlanKpiLists { get; set; }

    public virtual DbSet<MPlanName> MPlanNames { get; set; }

    public virtual DbSet<MPlanPeriod> MPlanPeriods { get; set; }

    public virtual DbSet<MPlanResult> MPlanResults { get; set; }

    public virtual DbSet<MPlanTargetDescription> MPlanTargetDescriptions { get; set; }

    public virtual DbSet<MScheduledJob> MScheduledJobs { get; set; }

    public virtual DbSet<MStatus> MStatuses { get; set; }

    public virtual DbSet<TKpiSystemAssignDivision> TKpiSystemAssignDivisions { get; set; }

    public virtual DbSet<TKpiSystemKpiTarget> TKpiSystemKpiTargets { get; set; }

    public virtual DbSet<TKpiSystemKpiTargetLevel> TKpiSystemKpiTargetLevels { get; set; }

    public virtual DbSet<TKpiSystemWeight> TKpiSystemWeights { get; set; }

    public virtual DbSet<TKpiTarget> TKpiTargets { get; set; }

    public virtual DbSet<TPlanKpilist> TPlanKpilists { get; set; }

    public virtual DbSet<TPlanPeriodDetail> TPlanPeriodDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=192.168.9.155;Database=bluecarg_SME_API_KPI;User Id=sa;Password=Osmep@2025;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<MApiInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MApiInformation");

            entity.ToTable("M_ApiInformation", "SME_KPI");

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

            entity.ToTable("M_DimensionSystem", "SME_KPI");

            entity.Property(e => e.Dimensionid)
                .ValueGeneratedNever()
                .HasColumnName("dimensionid");
            entity.Property(e => e.Dimensionname).HasColumnName("dimensionname");
            entity.Property(e => e.Plantypeid)
                .HasMaxLength(50)
                .HasColumnName("plantypeid");
        });

        modelBuilder.Entity<MDivision>(entity =>
        {
            entity.ToTable("M_Division", "SME_KPI");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Divisioncode)
                .HasMaxLength(255)
                .HasColumnName("divisioncode");
            entity.Property(e => e.Divisionid)
                .HasMaxLength(255)
                .HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(255)
                .HasColumnName("divisionname");
        });

        modelBuilder.Entity<MExportEval>(entity =>
        {
            entity.ToTable("M_ExportEval");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoreWeight).HasColumnName("core_weight");
            entity.Property(e => e.Division)
                .HasMaxLength(150)
                .HasColumnName("division");
            entity.Property(e => e.EmpCode)
                .HasMaxLength(50)
                .HasColumnName("empCode");
            entity.Property(e => e.Fullname)
                .HasMaxLength(150)
                .HasColumnName("fullname");
            entity.Property(e => e.IndvWeight).HasColumnName("indv_weight");
            entity.Property(e => e.KpiPlanInvidualApproveId1)
                .HasMaxLength(150)
                .HasColumnName("kpiPlanInvidualApproveId1");
            entity.Property(e => e.KpiPlanInvidualApproveId2)
                .HasMaxLength(150)
                .HasColumnName("kpiPlanInvidualApproveId2");
            entity.Property(e => e.ManagerialWeight)
                .HasMaxLength(150)
                .HasColumnName("managerial_weight");
            entity.Property(e => e.PlanId)
                .HasMaxLength(150)
                .HasColumnName("planID");
            entity.Property(e => e.Position)
                .HasMaxLength(150)
                .HasColumnName("position");
            entity.Property(e => e.Segment)
                .HasMaxLength(150)
                .HasColumnName("segment");
            entity.Property(e => e.Seq).HasColumnName("seq");
            entity.Property(e => e.UserApprove1)
                .HasMaxLength(150)
                .HasColumnName("userApprove1");
            entity.Property(e => e.UserApprove2)
                .HasMaxLength(150)
                .HasColumnName("userApprove2");
        });

        modelBuilder.Entity<MInputFormate>(entity =>
        {
            entity.HasKey(e => e.Masterid);

            entity.ToTable("M_InputFormate", "SME_KPI");

            entity.Property(e => e.Masterid)
                .ValueGeneratedNever()
                .HasColumnName("masterid");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<MKpiSystemAssign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_KpiSys__3214EC079EFB0E43");

            entity.ToTable("M_KpiSystemAssign");

            entity.HasIndex(e => e.KpiId, "UQ__M_KpiSys__8C69D5BF97AD85F7").IsUnique();

            entity.Property(e => e.KpiId).HasMaxLength(50);
            entity.Property(e => e.KpiName).HasMaxLength(1000);
            entity.Property(e => e.PlanId).HasMaxLength(50);
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<MKpiSystemKpiTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_KpiSys__3214EC0772DE58EA");

            entity.ToTable("M_KpiSystemKpiTarget");

            entity.HasIndex(e => e.KpiId, "UQ_M_KpiSystemKpiTarget_KpiId").IsUnique();

            entity.Property(e => e.KpiId).HasMaxLength(50);
            entity.Property(e => e.PlanId).HasMaxLength(50);
        });

        modelBuilder.Entity<MKpiSystemWeight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_KpiSys__3214EC07BD9053DB");

            entity.ToTable("M_KpiSystemWeight");

            entity.HasIndex(e => e.KpiId, "UQ__M_KpiSys__8C69D5BFF0BEAFEC").IsUnique();

            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.KpiId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Planid)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MKpiType>(entity =>
        {
            entity.HasKey(e => e.Masterid);

            entity.ToTable("M_KpiType", "SME_KPI");

            entity.Property(e => e.Masterid)
                .ValueGeneratedNever()
                .HasColumnName("masterid");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<MMeasure>(entity =>
        {
            entity.ToTable("M_Measure", "SME_KPI");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Masterid).HasColumnName("masterid");
        });

        modelBuilder.Entity<MPlanBudgetYear>(entity =>
        {
            entity.HasKey(e => e.Year).HasName("PK_M_BudgetYear");

            entity.ToTable("M_PlanBudgetYear", "SME_KPI");

            entity.Property(e => e.Year)
                .ValueGeneratedNever()
                .HasColumnName("year");
        });

        modelBuilder.Entity<MPlanKpi>(entity =>
        {
            entity.HasKey(e => new { e.PlanId, e.Kpiid }).HasName("PK__M_PlanKP__72724B9D34509BDB");

            entity.ToTable("M_PlanKPI", "SME_KPI");

            entity.Property(e => e.Kpiid).HasColumnName("KPIId");
        });

        modelBuilder.Entity<MPlanKpiDescription>(entity =>
        {
            entity.ToTable("M_PlanKpiDescription", "SME_KPI");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Kpidescription).HasColumnName("kpidescription");
            entity.Property(e => e.Kpiid)
                .HasMaxLength(50)
                .HasColumnName("kpiid");
            entity.Property(e => e.Planid)
                .HasMaxLength(50)
                .HasColumnName("planid");
        });

        modelBuilder.Entity<MPlanKpiList>(entity =>
        {
            entity.ToTable("M_PlanKpiList", "SME_KPI");

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
            entity.Property(e => e.Planid)
                .HasMaxLength(50)
                .HasColumnName("planid");
            entity.Property(e => e.Planremark).HasColumnName("planremark");
            entity.Property(e => e.Plantitle)
                .HasMaxLength(255)
                .HasColumnName("plantitle");
            entity.Property(e => e.Planyear).HasColumnName("planyear");
        });

        modelBuilder.Entity<MPlanName>(entity =>
        {
            entity.ToTable("M_PlanName", "SME_KPI");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PlanTypeId).HasMaxLength(50);
        });

        modelBuilder.Entity<MPlanPeriod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__M_PlanPe__3214EC07A8A3328C");

            entity.ToTable("M_PlanPeriod");

            entity.HasIndex(e => e.PlanId, "UQ__M_PlanPe__755C22B6D2A5A568").IsUnique();

            entity.Property(e => e.EffectiveDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.PlanId).HasMaxLength(50);
            entity.Property(e => e.PlanTypeId).HasMaxLength(20);
        });

        modelBuilder.Entity<MPlanResult>(entity =>
        {
            entity.ToTable("M_PlanResult", "SME_KPI");

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
            entity.HasKey(e => e.Id).HasName("PK_Table_1");

            entity.ToTable("M_PlanTargetDescription");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Kpiid)
                .HasMaxLength(50)
                .HasColumnName("kpiid");
            entity.Property(e => e.Planid)
                .HasMaxLength(50)
                .HasColumnName("planid");
            entity.Property(e => e.Target).HasColumnName("target");
        });

        modelBuilder.Entity<MScheduledJob>(entity =>
        {
            entity.ToTable("M_ScheduledJobs", "SME_KPI");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.JobName).HasMaxLength(150);
        });

        modelBuilder.Entity<MStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_M_StatusData");

            entity.ToTable("M_Status", "SME_KPI");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Masterid).HasColumnName("masterid");
        });

        modelBuilder.Entity<TKpiSystemAssignDivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_KpiSys__3214EC075D0CDDE3");

            entity.ToTable("T_KpiSystemAssignDivision");

            entity.Property(e => e.DivisionName).HasMaxLength(255);
            entity.Property(e => e.KpiId).HasMaxLength(50);

            entity.HasOne(d => d.Kpi).WithMany(p => p.TKpiSystemAssignDivisions)
                .HasPrincipalKey(p => p.KpiId)
                .HasForeignKey(d => d.KpiId)
                .HasConstraintName("FK_KpiSystemAssign_Division");
        });

        modelBuilder.Entity<TKpiSystemKpiTarget>(entity =>
        {
            entity.HasKey(e => e.TargetId).HasName("PK__T_KpiSys__2B1F0F96387D8B49");

            entity.ToTable("T_KpiSystemKpiTarget");

            entity.Property(e => e.KpiId).HasMaxLength(50);
            entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Kpi).WithMany(p => p.TKpiSystemKpiTargets)
                .HasPrincipalKey(p => p.KpiId)
                .HasForeignKey(d => d.KpiId)
                .HasConstraintName("FK_T_KpiSystemKpiTarget_KpiId");
        });

        modelBuilder.Entity<TKpiSystemKpiTargetLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__T_KpiSys__09F03C26DDF28AC4");

            entity.ToTable("T_KpiSystemKpiTargetLevel");

            entity.Property(e => e.LabelStr).HasMaxLength(255);

            entity.HasOne(d => d.Target).WithMany(p => p.TKpiSystemKpiTargetLevels)
                .HasForeignKey(d => d.TargetId)
                .HasConstraintName("FK_T_KpiSystemKpiTargetLevel_TargetId");
        });

        modelBuilder.Entity<TKpiSystemWeight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_KpiSys__3214EC077B9E678C");

            entity.ToTable("T_KpiSystemWeight");

            entity.HasIndex(e => new { e.KpiId, e.PeriodId }, "UQ_KpiSystemWeight").IsUnique();

            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.KpiId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TargetValue).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Weight)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Kpi).WithMany(p => p.TKpiSystemWeights)
                .HasPrincipalKey(p => p.KpiId)
                .HasForeignKey(d => d.KpiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_KpiSystemWeight_M_KpiSystemWeight");
        });

        modelBuilder.Entity<TKpiTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_KPI_Ta__3214EC073C46451A");

            entity.ToTable("T_KPI_Target", "SME_KPI");

            entity.Property(e => e.Kpiid)
                .HasMaxLength(50)
                .HasColumnName("KPIId");
            entity.Property(e => e.LabelStr).HasMaxLength(100);
            entity.Property(e => e.LevelDesc).HasMaxLength(255);
            entity.Property(e => e.PeriodDetail).HasMaxLength(100);
            entity.Property(e => e.PeriodId).HasColumnName("PeriodID");
            entity.Property(e => e.PlanId).HasMaxLength(50);
        });

        modelBuilder.Entity<TPlanKpilist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_PlanKP__3214EC072B0EECDE");

            entity.ToTable("T_PlanKPIList", "SME_KPI");

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

        modelBuilder.Entity<TPlanPeriodDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_PlanPe__3214EC0750217AC7");

            entity.ToTable("T_PlanPeriodDetail");

            entity.Property(e => e.EffectiveDate).HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.PlanId).HasMaxLength(50);

            entity.HasOne(d => d.Plan).WithMany(p => p.TPlanPeriodDetails)
                .HasPrincipalKey(p => p.PlanId)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK_T_PlanPeriodDetail_PlanId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
