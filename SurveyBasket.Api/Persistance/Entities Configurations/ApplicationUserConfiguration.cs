namespace SurveyBasket.Api.Persistance.Entities_Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        

        builder.Property(u=>u.FirstName)
            .HasMaxLength(100);
        builder.Property(u => u.LastName)
            .HasMaxLength(100);
    }
}
