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

        // Media
        CreateMap<Media, MediaBasicResult>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PrimaryInformation!.Title.Name))
            .ForMember(dest => dest.ReleaseDate, opt => {
                opt.AllowNull();
                opt.MapFrom(src => src.PrimaryInformation!.Release!.ReleaseDate);
            })
            .ForMember(dest => dest.PosterUri, opt => {
                opt.AllowNull();
                opt.MapFrom(src => src.PrimaryInformation!.PromotionalMedia!.Uri);
            });


        CreateMap<Media, MediaResult>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.PrimaryInformation!.Title.Name))
            .ForMember(dest => dest.ReleaseDate, opt => {
                opt.AllowNull();
                opt.MapFrom(src => src.PrimaryInformation!.Release!.ReleaseDate);
            })
            .ForMember(dest => dest.PosterUri, opt => {
                opt.AllowNull();
                opt.MapFrom(src => src.PrimaryInformation!.PromotionalMedia!.Uri);
            })
            .ForMember(dest => dest.RuntimeMinutes, opt => {
                opt.AllowNull();
                opt.MapFrom(src => src.Runtime);
            })
            .ForMember(dest => dest.AwardText, opt => {
                opt.AllowNull();
                opt.MapFrom(src => src.Awards);
            });
        
        CreateMap<Episode, MediaResult>()
            .IncludeBase<Media, MediaResult>();
            
        CreateMap<Season, MediaResult>()
            .IncludeBase<Media, MediaResult>();

        CreateMap<Score, MediaResult.ScoreResult>();
        CreateMap<ProductionCompany, MediaResult.MediaProductionCompanyResult>();
        
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
            
        // Person
        CreateMap<Person, PersonResult>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId));
        CreateMap<CoActor, CoActorResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CoActorImdbId))
            .ForMember(dest => dest.ActorName, opt => opt.MapFrom(src => src.CoActorName));
        
        // TitleAttribute
        CreateMap<TitleAttribute, TitleAttributeResult>();

        // User Score
        CreateMap<UserScore, UserScoreResult>()
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.ScoreValue));

        // Title
        CreateMap<Title, TitleResult>()
            .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.TitleTypes))
            .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.TitleAttributes));
        CreateMap<TitleCreateRequest, Title>();

        // Crew
        CreateMap<CrewMember, CrewResult>()
            .ForMember(dest => dest.JobCategory, opt => opt.MapFrom(src => src.JobCategory.Name));
        CreateMap<CastMember, CrewResult>()
            .ForMember(dest => dest.JobCategory, opt => opt.MapFrom(src => "Actor"));
    }
}
