# Endpoints

📃 Supports pagination in the form of page=<int> and count=<int> as query parameters

| Method | Path | Status code | Description |
| --- | --- | --- | --- |
| `GET` | genres | 200 (empty if no genre) | All genres 📃 |
| `GET` | production_companies | 200 (empty if no production company) | All production companies 📃 |
| `GET` | job_categories | 200 (empty if no job category) | All job categories 📃 |
| `GET` | countries | 200 (empty if no country) | All countries 📃 |
| `GET` | languages | 200 (empty if no language) | All languages 📃 |
| `GET`  | title_attributes | 200 (empty if no title attributes) | All title attributes 📃 |
| `GET`  | title_types | 200 (empty if no title types) | All title types 📃 |
| `GET` | media/{mediaId}/releases | 200 (empty if no release) | All releases for a specific media 📃 Include title attributes and types |
| `POST` | media/{mediaId}/releases | 201 (body contains new release) or 4xx or 5xx | Create new release for media |
| `GET`  | media/{mediaId}/releases/{releaseId} | 200 () or 404 | Get release |
| `PATCH` | media/{mediaId}/releases/{releaseId} | 200 (response to access changed) or 404 | Update part of release |
| `DELETE` | media/{mediaId}/releases/{releaseId} | 200 or 404 | Delete release for media |
| `GET` | media/{mediaId}/release/{releaseId}/promotional_media | 200 (empty if no promotional media) | All promotional media for a specific release |
| `POST` | media/{mediaId}/release/{releaseId}/promotional_media | 201 or 4xx | Add new promotional media for release |
| `GET` | media/{mediaId}/release/{releaseId}/promotional_media/{id} | 200 or 401 | Get promotional media for relesae |
| `DELETE` | media/{mediaId}/release/{releaseId}/promotional_media/{id} | 200 or 401 | Deletes promotional media for release |
| `GET` | media/{mediaId}/promotional_media | 200 (empty if no promotional media) | All promotional media regardless of release |
| `GET` | media/{mediaId}/titles | 200 (empty if no titles) | All titles for a media 📃 |
| `POST`  | media/{mediaId}/titles | 201 | Create new title for a media |
| `GET`  | media/{mediaId}/titles/{titleId} | 200 or 404 | Get title for media |
| `DELETE`  | media/{mediaId}/titles/{titleId} | 200 or 404 | Delete title for a media |
| `GET` | media | 200 (empty if no media) | All media  📃, query parameteres: keywords, type, personName, countries, genreName, plot, title. <br/><br/>Should return additional information if episode or season etc.Also include primary poster, primary title, primary release |
| `GET` | media/{id} | 200 or 404 | Should return additional information if episode or season etc. <br/>Also return genres, scores, production country,  |
| `GET` | media/{id}/crew | 200 (empty if no crew) | All crew members and cast members  📃 |
| `GET` | media/{id}/similar_media | 200 (empty if no related media) | returns ids for all similar media  📃 |
| `GET` | persons | 200 (empty if no persons) | All people. Supported query parameters  📃 |
| `GET` | persons/{id} | 200 or 404 |  Get details for a person|
| `GET` | person/{id}/media | 200 (empty if person is in no media) | List all media a person has worked on📃 |
| `GET` | users | 200 (empty if no user) | List all users 📃 |
| `POST` | users | 200 or 4xx | Create new user |
| `GET` | users/{id} | 200 or 404 | Get a specific user |
| `DELETE` | users/{id} | 200 or 404 | Deletes user |
| `PATCH` | users/{id} | 200 or 404 | Updates part of a user |
| `GET`, `POST` | users/{id}/bookmarks | 200 (empty if no bookmarks) | List all bookmarks for a user 📃 |
| `POST` | users/{id}/bookmarks | 200 or 4xx | Add new bookmark for user |
| `GET`  | users/{id}/bookmarks/{bookmarkId} | 200 or 404 | Get bookmark for user |
| `DELETE` | users/{id}/bookmarks/{bookmarksId} | 200 or 404 | Delete bookmark |
| `PATCH` | users/{id}/bookmarks/{bookmarksId} | 200 or 404 | Update parts of a bookmark like note |
| `GET` | users/{id}/completed | 200 (empty if no completed) | List all completed media for a user 📃 |
| `POST` | users/{id}/completed | 200 or 4xx | Mark media as completed  |
| `GET`  | users/{id}/complated/{compatedId} | 200 or 404 | Get completed media |
| `DELETE` | users/{id}/completed/{completedId} | 200 or 404 | Delete completed field |
| `PATCH` | users/{id}/completed/{completedId} | 200 or 404 or 400 | Update parts of a completed media like note |
| `GET` | users/{id}/following | 200 (empty if no following) | List all people a user follows 📃 |
| `POST`  | users/{id}/following | 200 or 4xx | Add person to follow-list for user |
| `DELETE` | users/{id}/following/{followingId} | 200 or 404 | Remove a follow |
| `GET` | users/{id}/search_history | 200 (empty result if no history) | Get search history for user in chronologically order (latest to oldest) 📃 |
| `DELETE` | users/{id}/search_history | 200 (empty result if no history) | clears search history. Body can contain a duration for how long back the search history should be cleared. If no duration all history should be cleared  |
| `GET` | users/{id}/scores | 200 (empty list if nothing matches query) | Get all scores a user have given. Only return the latest score for a given media. Support query parameter: mediaType, mediaId, mediaName  📃 |
| `GET`  | users/{id}/scores/{id} | 200 or 404 | Get user score for a media |
| `POST` | users/{id}/scores | 201 with reference to newly created object or 4xx | Add a new score for a media. |
