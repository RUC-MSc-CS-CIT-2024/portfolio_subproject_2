namespace CitMovie.Business;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User
        CreateMap<UserCreateRequest, User>();
        CreateMap<UserUpdateRequest, User>();
        CreateMap<User, UserResult>();

        // Follow
        CreateMap<Follow, FollowResult>();

        // Country
        CreateMap<Country, CountryResult>();

        // Language
        CreateMap<Language, LanguageResult>();

        // Genre
        CreateMap<Genre, GenreResult>();
        
        // TitleType
        CreateMap<TitleType, TitleTypeResult>();

        // Job Category
        CreateMap<JobCategory, JobCategoryResult>();
        
        //Release
        CreateMap<Release, ReleaseResult>()
            .ForMember(m => m.Title, opt => opt.MapFrom(src => src.Title.Name.Where(src.Title.Name == "Primary title")));
    }
}