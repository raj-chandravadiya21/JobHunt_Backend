using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobHunt.Domain.Entities;

public partial class DefaultdbContext : DbContext
{
    public DefaultdbContext()
    {
    }

    public DefaultdbContext(DbContextOptions<DefaultdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public virtual DbSet<ApplicationStatusLog> ApplicationStatusLogs { get; set; }

    public virtual DbSet<Aspnetuser> Aspnetusers { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<DegreeType> DegreeTypes { get; set; }

    public virtual DbSet<EmailLog> EmailLogs { get; set; }

    public virtual DbSet<InterviewDetail> InterviewDetails { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobSkill> JobSkills { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<OtpRecord> OtpRecords { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Roleset> Rolesets { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserEducation> UserEducations { get; set; }

    public virtual DbSet<UserEducationType> UserEducationTypes { get; set; }

    public virtual DbSet<UserLanguage> UserLanguages { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    public virtual DbSet<UserSocialProfile> UserSocialProfiles { get; set; }

    public virtual DbSet<WorkExperience> WorkExperiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=test-pateldemo979-2f5a.f.aivencloud.com;Port=24996;Database=defaultdb;Username=avnadmin;Password=AVNS_f8q4IBOurtCLjsuwOsq");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("admin_pkey");

            entity.Property(e => e.AdminId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Aspnetuser).WithMany(p => p.Admins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("admin_aspnetuser_id_fkey");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("application_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_job_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_status_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_user_id_fkey");
        });

        modelBuilder.Entity<ApplicationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("application_status_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<ApplicationStatusLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("application_status_logs_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationStatusLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_status_logs_application_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.ApplicationStatusLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("application_status_logs_status_id_fkey");
        });

        modelBuilder.Entity<Aspnetuser>(entity =>
        {
            entity.HasKey(e => e.AspnetuserId).HasName("AspNetUser_pkey");

            entity.Property(e => e.AspnetuserId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetusers).HasConstraintName("aspnetuser_role_id_fkey");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("company_pkey");

            entity.Property(e => e.CompanyId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Aspnetuser).WithMany(p => p.Companies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("company_aspnetuser_id_fkey");
        });

        modelBuilder.Entity<DegreeType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("degree_type_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<EmailLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("email_log			_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<InterviewDetail>(entity =>
        {
            entity.HasKey(e => e.InterviewId).HasName("interview_details_pkey");

            entity.Property(e => e.InterviewId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Application).WithMany(p => p.InterviewDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("interview_details_application_id_fkey");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("job_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Company).WithMany(p => p.Jobs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_company_id_fkey");
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasOne(d => d.Job).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_skills_job_id_fkey");

            entity.HasOne(d => d.Skill).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("job_skills_skill_id_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("language_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<OtpRecord>(entity =>
        {
            entity.HasKey(e => e.OtpId).HasName("otp_record_pkey");

            entity.Property(e => e.OtpId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Aspnetuser).WithMany(p => p.OtpRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("otp_record_aspnetuser_id_fkey");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("projects_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.User).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("projects_user_id_fkey");
        });

        modelBuilder.Entity<Roleset>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("RoleSet_pkey");

            entity.Property(e => e.RoleId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("skill_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user			_pkey");

            entity.Property(e => e.UserId).UseIdentityAlwaysColumn();
            entity.Property(e => e.IsVerified).HasDefaultValue(false);

            entity.HasOne(d => d.Aspnetuser).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user			_aspnetuser_id_fkey");
        });

        modelBuilder.Entity<UserEducation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_education_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Degree).WithMany(p => p.UserEducations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_education_degree_id_fkey");

            entity.HasOne(d => d.UserEducationType).WithMany(p => p.UserEducations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_education_user_education_type_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserEducations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_education_user_id_fkey");
        });

        modelBuilder.Entity<UserEducationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_education_type_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<UserLanguage>(entity =>
        {
            entity.HasOne(d => d.Language).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_languages_language_id_fkey");

            entity.HasOne(d => d.User).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_languages_user_id_fkey");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_log	_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Aspnetuser).WithMany(p => p.UserLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_log	_aspnetuser_id_fkey");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_skill_pkey");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn();

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkills)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_skill_skill_id_fkey");

            entity.HasOne(d => d.User).WithOne(p => p.UserSkill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_skill_user_id_fkey");
        });

        modelBuilder.Entity<UserSocialProfile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("user_social_profile			_pkey");

            entity.Property(e => e.UserProfileId).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.User).WithMany(p => p.UserSocialProfiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_social_profile			_user_id_fkey");
        });

        modelBuilder.Entity<WorkExperience>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("work_experience_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.User).WithMany(p => p.WorkExperiences)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("work_experience_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
