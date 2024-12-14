namespace CitMovie.Api;
public static class IUrlHelperExtensions
{
    public static List<Link> AddMediaLinks(this IUrlHelper source, int mediaId)
        => [
            new Link {
                Href = source.Link(nameof(MediaController.GetMedia), new { id = mediaId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetSimilar), new { id = mediaId }) ?? string.Empty,
                Rel = "similar_media",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetRelated), new { id = mediaId }) ?? string.Empty,
                Rel = "related_media",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetCrew), new { id = mediaId }) ?? string.Empty,
                Rel = "crew",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetCast), new { id = mediaId }) ?? string.Empty,
                Rel = "cast",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(PromotionalMediaController.GetPromotionalMediaofMedia), new { mediaId }) ?? string.Empty,
                Rel = "promotional_media",
                Method = "GET"
            }
        ];

    public static List<Link> AddCrewAndCastLinks(this IUrlHelper source, int mediaId, int personId)
        => [
            new Link {
                Href = source.Link(nameof(MediaController.GetMedia), new { id = mediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(PersonController.GetPersonById), new { id = personId }) ?? string.Empty,
                Rel = "person",
                Method = "GET"
            },
        ];
    
    public static List<Link> AddFollowLinks(this IUrlHelper source, int personId, int userId, int followId)
        => [ 
            new Link {
                Href = source.Link(nameof(FollowController.CreateFollow), new { userId }) ?? string.Empty,
                Rel = "create_follow",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(FollowController.RemoveFollowing), new { userId, followingId = followId }) ?? string.Empty,
                Rel = "remove_follow",
                Method = "DELETE"
            },
            new Link {
                Href = source.Link(nameof(PersonController.GetPersonById), new { id = personId }) ?? string.Empty,
                Rel = "person",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(UserController.GetUser), new { userId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            },
        ];

    public static List<Link> AddCompletedLinks(this IUrlHelper source, int completedId, int mediaId, int userId)
        => [
            new Link {
                Href = source.Link(nameof(CompletedController.GetCompleted), new { userId, id = completedId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(CompletedController.MoveBookmarkToCompleted), new { userId }) ?? string.Empty,
                Rel = "move_to_completed",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(CompletedController.UpdateCompleted), new { userId, id = completedId }) ?? string.Empty,
                Rel = "update_completed",
                Method = "PUT"
            },
            new Link {
                Href = source.Link(nameof(CompletedController.DeleteCompleted), new { userId, id = completedId }) ?? string.Empty,
                Rel = "remove_completed",
                Method = "DELETE"
            },
            new Link {
                Href = source.Link(nameof(UserController.GetUser), new { userId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetMedia), new { id = mediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            }
        ];
    
    public static List<Link> AddBookmarkLinks(this IUrlHelper source, int bookmarkId, int mediaId, int userId)
        => [
            new Link {
                Href = source.Link(nameof(BookmarkController.GetBookmark), new { userId, id = bookmarkId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(BookmarkController.CreateBookmark), new { userId }) ?? string.Empty,
                Rel = "create_bookmark",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(BookmarkController.MoveBookmarkToCompleted), new { userId, id = bookmarkId }) ?? string.Empty,
                Rel = "move_to_completed",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(BookmarkController.UpdateBookmark), new { userId, id = bookmarkId }) ?? string.Empty,
                Rel = "update_bookmark",
                Method = "PATCH"
            },
            new Link {
                Href = source.Link(nameof(BookmarkController.DeleteBookmark), new { userId, id = bookmarkId }) ?? string.Empty,
                Rel = "delete_bookmark",
                Method = "DELETE"
            },
            new Link {
                Href = source.Link(nameof(UserController.GetUser), new { userId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetMedia), new { id = mediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            }
        ];

    public static List<Link> AddPersonLinks(this IUrlHelper source, int personId)
        => [ 
            new Link {
                Href = source.Link(nameof(PersonController.GetPersonById), new { id = personId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(PersonController.GetFrequentCoActors), new { id = personId }) ?? string.Empty,
                Rel = "coactors",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(PersonController.GetMediaByPersonId), new { id = personId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            }
        ];

    public static List<Link> AddPromotionalMediaLinks(this IUrlHelper source, int promotionalMediaId, int mediaId, int releaseId)
        => [ 
            new Link {
                Href = source.Link(nameof(PromotionalMediaController.GetPromotionalMediaById), new { id = promotionalMediaId, mediaId, releaseId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(PromotionalMediaController.CreatePromotionalMedia), new { mediaId, releaseId }) ?? string.Empty,
                Rel = "create_promotional_media",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(PromotionalMediaController.DeletePromotionalMedia), new { mediaId, releaseId, id = promotionalMediaId }) ?? string.Empty,
                Rel = "delete_promotional_media",
                Method = "DELETE"
            },
            new Link {
                Href = source.Link(nameof(ReleaseController.GetReleaseOfMediaById), new { mediaId, id = releaseId }) ?? string.Empty,
                Rel = "release",
                Method = "GET"
            },
        ];
    
    public static List<Link> AddReleaseLinks(this IUrlHelper source, int mediaId, int releaseId)
        => [ 
            new Link {
                Href = source.Link(nameof(ReleaseController.GetReleaseOfMediaById), new { mediaId, id = releaseId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(ReleaseController.CreateReleaseForMediaAsync), new { mediaId }) ?? string.Empty,
                Rel = "create_release",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(ReleaseController.DeleteReleaseOfMediaAsync), new { mediaId, id = releaseId }) ?? string.Empty,
                Rel = "delete_release",
                Method = "DELETE"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetMedia), new { id = mediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(PromotionalMediaController.GetPromotionalMediaOfRelease), new { mediaId, releaseId }) ?? string.Empty,
                Rel = "promotional_media",
                Method = "GET"
            }
        ];

    public static List<Link> AddSearchHistoryUserLink(this IUrlHelper source, int searchHistoryId, int userId)
        => [
            new Link {
                Href = source.Link(nameof(SearchHistoryController.DeleteUserSearchHistories), new { userId, searchHistoryId }) ?? string.Empty,
                Rel = "delete_search_history",
                Method = "DELETE"
            },
            new Link
            {
                Href = source.Link(nameof(UserController.GetUser), new { userId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            }
        ];
    
    public static List<Link> AddTitleLinks(this IUrlHelper source, int titleId, int mediaId)
        => [
            new Link {
                Href = source.Link(nameof(TitleController.GetTitle), new { mediaId, titleId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(TitleController.CreateTitle), new { mediaId }) ?? string.Empty,
                Rel = "create_title",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(TitleController.DeleteTitle), new { mediaId, titleId }) ?? string.Empty,
                Rel = "delete_title",
                Method = "DELETE"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetMedia), new { mediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            }
        ];
    
    public static List<Link> AddUserLinks(this IUrlHelper source, int userId)
        => [
            new Link {
                Href = source.Link(nameof(UserController.GetUser), new { userId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(UserController.UpdateUser), new { userId }) ?? string.Empty,
                Rel = "update_user",
                Method = "PATCH"
            },
            new Link {
                Href = source.Link(nameof(UserController.DeleteUser), new { userId }) ?? string.Empty,
                Rel = "delete_user",
                Method = "DELETE"
            },
            new Link {
                Href = source.Link(nameof(UserScoreController.GetUserScores), new { userId }) ?? string.Empty,
                Rel = "user_scores",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(CompletedController.GetUserCompleted), new { userId }) ?? string.Empty,
                Rel = "completed",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(BookmarkController.GetUserBookmarks), new { userId }) ?? string.Empty,
                Rel = "bookmarks",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(FollowController.GetFollowings), new { userId }) ?? string.Empty,
                Rel = "followers",
                Method = "GET"
            },
        ];
    
    public static List<Link> AddUserScoreLinks(this IUrlHelper source, int userId, int mediaId)
        => [
            new Link {
                Href = source.Link(nameof(UserScoreController.GetUserScores), new { userId }) ?? string.Empty,
                Rel = "self",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(UserScoreController.CreateUserScore), new { userId }) ?? string.Empty,
                Rel = "create_user_score",
                Method = "POST"
            },
            new Link {
                Href = source.Link(nameof(UserScoreController.GetUserScores), new { userId }) ?? string.Empty,
                Rel = "user",
                Method = "GET"
            },
            new Link {
                Href = source.Link(nameof(MediaController.GetMedia), new { id = mediaId }) ?? string.Empty,
                Rel = "media",
                Method = "GET"
            }
        ];
}
