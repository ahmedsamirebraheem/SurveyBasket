using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SurveyBasket.Api.Entities;
using SurveyBasket.Api.Extensions;
using System.Reflection;
using System.Security.Claims;

namespace SurveyBasket.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : 
    IdentityDbContext<ApplicationUser>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public DbSet<Poll> Polls { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<VoteAnswer> VoteAnswers { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);

        //// 1. تجاهل جداول الـ Passkey عشان مشكلة الـ EF9
        //modelBuilder.Ignore<Microsoft.AspNetCore.Identity.IdentityPasskeyData>();

        //// 2. تشفير كلمة السر
        //var hasher = new PasswordHasher<ApplicationUser>();

        //// 3. بيانات أحمد سمير (قيم ثابتة 100%)
        //var adminUser = new ApplicationUser
        //{
        //    Id = "D765FB08-3390-445C-BF80-07BECDB1F816",
        //    FirstName = "Ahmed",
        //    LastName = "Samir",
        //    UserName = "ahmed.samir@test.com",
        //    NormalizedUserName = "AHMED.SAMIR@TEST.COM",
        //    Email = "ahmed.samir@test.com",
        //    NormalizedEmail = "AHMED.SAMIR@TEST.COM",
        //    EmailConfirmed = true,
        //    // Stamps ثابتة عشان ميعملش Conflict في الـ Migration
        //    SecurityStamp = "C3688126-B817-4523-9D07-160B8808A613",
        //    ConcurrencyStamp = "88995738-D0E0-469F-83C3-E9D2849E7C05"
        //};

        //// 4. وضع الهاش السليم للباسورد
        //adminUser.PasswordHash = hasher.HashPassword(adminUser, "P@ssw0rd123");

        //// 5. حفظ البيانات كـ Seed
        //modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
    }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    // تجاهل تحذير التغييرات المعلقة في الموديل
    //    optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    //}
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entityEntry in entries)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId()!;

            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(x => x.CreatedById).CurrentValue = currentUserId;
            }
            else if(entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                entityEntry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}