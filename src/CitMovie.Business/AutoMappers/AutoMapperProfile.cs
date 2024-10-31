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
    }
}