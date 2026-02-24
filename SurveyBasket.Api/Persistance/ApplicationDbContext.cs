using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // تأكد من وجود دي
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Api.Entities;
using System.Security.Claims; // تأكد من مسار الـ ApplicationUser

namespace SurveyBasket.Api.Persistance;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IHttpContextAccessor httpContextAccessor)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Poll> Polls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    // 1. تجاهل جداول الـ Passkey عشان مشكلة الـ Primary Key
    //    modelBuilder.Ignore<Microsoft.AspNetCore.Identity.IdentityPasskeyData>();

    //    // 2. إعداد الـ Hasher لتشفير كلمة السر
    //    var hasher = new PasswordHasher<ApplicationUser>();

    //    // 3. تعريف المستخدم (أحمد سمير) بقيم ثابتة تماماً
    //    var adminUser = new ApplicationUser
    //    {
    //        Id = "D765FB08-3390-445C-BF80-07BECDB1F816", // ثابت
    //        FirstName = "Ahmed",
    //        LastName = "Samir",
    //        UserName = "ahmed.samir@test.com",
    //        NormalizedUserName = "AHMED.SAMIR@TEST.COM",
    //        Email = "ahmed.samir@test.com",
    //        NormalizedEmail = "AHMED.SAMIR@TEST.COM",
    //        EmailConfirmed = true,
    //        // القيم دي "سر" النجاح.. لازم تكون نصوص ثابتة كدة
    //        SecurityStamp = "C3688126-B817-4523-9D07-160B8808A613",
    //        ConcurrencyStamp = "88995738-D0E0-469F-83C3-E9D2849E7C05"
    //    };

    //    // 4. تشفير الباسورد
    //    adminUser.PasswordHash = hasher.HashPassword(adminUser, "P@ssw0rd123");

    //    // 5. إضافة البيانات (Seed)
    //    modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

    //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    //}
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);
    //    // ده السطر اللي هيمنع الـ EF إنه يمسح أو يرفض الـ Migration بسبب الـ Pending Changes
    //    optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    //}

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();
        foreach (var entityRntry in entries)
        {
            var currentUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (entityRntry.State == EntityState.Added)
            {
                entityRntry.Property(x=>x.CreatedById).CurrentValue = currentUserId;
            }
            else if(entityRntry.State == EntityState.Modified)
            {
                entityRntry.Property(x=>x.UpdatedById).CurrentValue = currentUserId;
                entityRntry.Property(x=>x.UpdatedOn).CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}