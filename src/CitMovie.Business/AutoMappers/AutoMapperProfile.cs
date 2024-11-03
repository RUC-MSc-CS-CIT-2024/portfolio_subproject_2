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
        CreateMap<ReleaseUpdateRequest, Release>()
            .ForMember(dest => dest.SpokenLanguages, opt => opt.Ignore());
        CreateMap<ReleaseCreateRequest, Release>()
            .ForMember(dest => dest.SpokenLanguages, opt => opt.Ignore());
        CreateMap<Release, ReleaseResult>()
            .ForMember(m => m.Title, 
                opt => opt.MapFrom(src => src.Title.Name))
            .ForMember(m => m.Country, 
                opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(m => m.SpokenLanguages, 
                opt => opt.MapFrom(src => src.SpokenLanguages.Select(sl => sl.Name)));
                
        //Promotional Media
        CreateMap<PromotionalMediaCreateRequest, PromotionalMedia>();
        CreateMap<PromotionalMedia, PromotionalMediaResult>()
            .ForMember(dest => dest.MediaId, 
                opt => opt.MapFrom(src => src.Release.MediaId));

        // Search History
        CreateMap<SearchHistory, SearchHistoryResult>()
            .ForMember(dest => dest.SearchText, opt => opt.MapFrom(src => src.Query));
        
        // TitleAttribute
        CreateMap<TitleAttribute, TitleAttributeResult>();


        // User Score
        CreateMap<UserScore, UserScoreResult>()
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.ScoreValue));
    }
}