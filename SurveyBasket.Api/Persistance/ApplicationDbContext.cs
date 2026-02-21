using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // تأكد من وجود دي
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Api.Entities; // تأكد من مسار الـ ApplicationUser

namespace SurveyBasket.Api.Persistance;

// 1. لازم تورث من IdentityDbContext عشان جداول الـ Identity تظهر
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Poll> Polls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        
    }
}