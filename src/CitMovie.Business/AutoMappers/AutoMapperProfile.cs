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
        
        //Promotional Media
        CreateMap<PromotionalMediaCreateRequest, PromotionalMedia>();
        CreateMap<PromotionalMedia, PromotionalMediaResult>()
            .ForMember(dest => dest.MediaId, 
                opt => opt.MapFrom(src => src.Release.MediaId));

        // Search History
        CreateMap<SearchHistory, SearchHistoryResult>()
            .ForMember(dest => dest.SearchText, opt => opt.MapFrom(src => src.Query));

    }
}