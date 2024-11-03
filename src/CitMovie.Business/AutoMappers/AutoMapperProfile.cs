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

        // Search History
        CreateMap<SearchHistory, SearchHistoryResult>()
            .ForMember(dest => dest.SearchText, opt => opt.MapFrom(src => src.Query));

        // Person
        CreateMap<Person, PersonResult>()
          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PersonId));
        CreateMap<Media, MediaResult>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name)))
            .ForMember(dest => dest.MediaId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Plot))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.FirstOrDefault().Name));
        CreateMap<CoActor, CoActorResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CoActorImdbId))
            .ForMember(dest => dest.ActorName, opt => opt.MapFrom(src => src.CoActorName));
    }
}