using SurveyBasket.Api.Entities;

namespace SurveyBasket.Api.Persistence.EntitiesConfigurations;

public class QuestionConfigurations : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasIndex(x => new
        {
            x.Content,
            x.PollId    
        }).IsUnique();
        builder.Property(q => q.Content)
            .HasMaxLength(1000);
    }
}
