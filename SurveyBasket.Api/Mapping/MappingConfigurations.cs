using SurveyBasket.Api.Contracts.Authentication;
using SurveyBasket.Api.Contracts.Questions;
using SurveyBasket.Api.Contracts.Users;
using SurveyBasket.Api.Entities;
using SurveyBasket.Api.Persistence.EntitiesConfigurations;

namespace SurveyBasket.Api.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<QuestionRequest, Question>()
            .Map(dest=>dest.Answers, src=>src.Answers.Select(answer => new Answer { Content = answer }));

        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName,src=>src.Email);

        config.NewConfig<(ApplicationUser user, IList<string> Roles), UserResponse>()
            .Map(dest => dest, src => src.user)
            .Map(dest => dest.Roles, src => src.Roles);

        config.NewConfig<CreateUserRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email)
            .Map(dest => dest.EmailConfirmed, src => true);

        config.NewConfig<UpdateUserRequest, ApplicationUser>()
           .Map(dest => dest.UserName, src => src.Email)
           .Map(dest => dest.NormalizedUserName, src => src.Email.ToUpper());


        config.NewConfig<ApplicationUser, UserProfileResponse>()
        .Map(dest => dest.Email, src => src.Email!) 
        .Map(dest => dest.UserName, src => src.UserName!)
        .Map(dest => dest.FirstName, src => src.FirstName)
        .Map(dest => dest.LastName, src => src.LastName)
        .MapToConstructor(true);
    }
}