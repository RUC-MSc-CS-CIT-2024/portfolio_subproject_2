### **Movie Application API Endpoints**

All paths are prefixed with `/api/` and support pagination in the form of `page=<int>` and `count=<int>` as query parameters.

#### **General Data Endpoints**

| Method | Path                        | Status code                          | Description                        |
| ------ | --------------------------- | ------------------------------------ | ---------------------------------- |
| `GET`  | `/api/genres`               | 200 (empty if no genre)              | Retrieve all genres.               |
| `GET`  | `/api/production_companies` | 200 (empty if no production company) | Retrieve all production companies. |
| `GET`  | `/api/job_categories`       | 200 (empty if no job category)       | Retrieve all job categories.       |
| `GET`  | `/api/countries`            | 200 (empty if no country)            | Retrieve all countries.            |
| `GET`  | `/api/languages`            | 200 (empty if no language)           | Retrieve all languages.            |
| `GET`  | `/api/title_attributes`     | 200 (empty if no title attributes)   | Retrieve all title attributes.     |
| `GET`  | `/api/title_types`          | 200 (empty if no title types)        | Retrieve all title types.          |

#### **Authentication & Authorization**

|Method|Path|Status code|Description|
|---|---|---|---|
|`POST`|`/api/auth/login`|200 or 4xx|Authenticate a user and return a token.|
|`POST`|`/api/auth/revoke`|200 or 4xx|Invalidate auth token.|
|`POST`|`/api/auth/refresh_token`|200 or 401|Refresh an expired token.|

> **Note**: All endpoints involving creation, update, or deletion should require proper authorization, with status codes like `401 Unauthorized` or `403 Forbidden` when access is denied.

#### **Media Endpoints**

|Method|Path|Status code|Description|
|---|---|---|---|
|`GET`|`/api/media`|200 (empty if no media)|Retrieve all media. Query parameters: keywords, type, personName, countries, genreName, plot, title. Includes primary poster, title, release info, etc.|
|`GET`|`/api/media/{id}`|200 or 404|Retrieve detailed media information (includes genres, scores, production country, etc.).|
|`GET`|`/api/media/{mediaId}/releases`|200 (empty if no release)|Retrieve all releases for a specific media. Includes title attributes and types.|
|`POST`|`/api/media/{mediaId}/releases`|201 or 4xx/5xx|Create a new release for a media.|
|`GET`|`/api/media/{mediaId}/releases/{releaseId}`|200 or 404|Retrieve specific release for a media.|
|`PATCH`|`/api/media/{mediaId}/releases/{releaseId}`|200 or 404|Update part of a specific release.|
|`DELETE`|`/api/media/{mediaId}/releases/{releaseId}`|200 or 404|Delete a specific release.|
|`GET`|`/api/media/{mediaId}/promotional_media`|200 (empty if no promotional media)|Retrieve all promotional media for a media.|
|`POST`|`/api/media/{mediaId}/release/{releaseId}/promotional_media`|201 or 4xx|Add new promotional media for a release.|
|`DELETE`|`/api/media/{mediaId}/release/{releaseId}/promotional_media/{id}`|200 or 401|Delete specific promotional media for a release.|
|`GET`|`/api/media/{mediaId}/titles`|200 (empty if no titles)|Retrieve all titles for a media.|
|`POST`|`/api/media/{mediaId}/titles`|201|Create a new title for a media.|
|`DELETE`|`/api/media/{mediaId}/titles/{titleId}`|200 or 404|Delete a specific title for a media.|
|`GET`|`/api/media/{id}/crew`|200 (empty if no crew)|Retrieve all crew members for a media.|
|`GET`|`/api/media/{id}/cast`|200 (empty if no cast)|Retrieve all cast members for a media.|
|`GET`|`/api/media/{id}/similar_media`|200 (empty if no related media)|Retrieve all similar media.|
|`GET`|`/api/media/{id}/related_media`|200 (empty if no related media)|Retrieve all related media.|

#### **User Media Reviews & Recommendations**

| Method   | Path                                 | Status code                       | Description                                            |
| -------- | ------------------------------------ | --------------------------------- | ------------------------------------------------------ |
| `GET`    | `/api/users/{id}/reviews`            | 200 (empty if no reviews)         | Retrieve all reviews written by a user.                |
| `POST`   | `/api/users/{id}/reviews`            | 201 or 4xx                        | Create a new review for media.                         |
| `PATCH`  | `/api/users/{id}/reviews/{reviewId}` | 200 or 404                        | Update an existing review.                             |
| `DELETE` | `/api/users/{id}/reviews/{reviewId}` | 200 or 404                        | Delete a specific review.                              |
| `GET`    | `/api/users/{id}/recommendations`    | 200 (empty if no recommendations) | Retrieve media recommendations based on user activity. |

#### **Person Endpoints**

|Method|Path|Status code|Description|
|---|---|---|---|
|`GET`|`/api/persons`|200 (empty if no persons)|Retrieve all people. Supports query parameters.|
|`GET`|`/api/persons/{id}`|200 or 404|Retrieve details for a specific person.|
|`GET`|`/api/person/{id}/media`|200 (empty if no media)|Retrieve all media that a person has worked on.|

#### **User Endpoints**

|Method|Path|Status code|Description|
|---|---|---|---|
|`GET`|`/api/users`|200 (empty if no user)|Retrieve all users.|
|`POST`|`/api/users`|201 or 4xx|Create a new user.|
|`GET`|`/api/users/{id}`|200 or 404|Retrieve specific user details.|
|`PATCH`|`/api/users/{id}`|200 or 404|Update user details.|
|`DELETE`|`/api/users/{id}`|200 or 404|Delete a specific user.|
|`GET`, `POST`|`/api/users/{id}/bookmarks`|200 (empty if no bookmarks)|Retrieve or add bookmarks for a user.|
|`DELETE`|`/api/users/{id}/bookmarks/{bookmarkId}`|200 or 404|Delete a specific bookmark for a user.|
|`GET`|`/api/users/{id}/completed`|200 (empty if no completed media)|Retrieve all completed media for a user.|
|`POST`|`/api/users/{id}/completed`|201 or 4xx|Mark media as completed for a user.|
|`DELETE`|`/api/users/{id}/completed/{completedId}`|200 or 404|Delete a specific completed media entry.|
|`GET`|`/api/users/{id}/following`|200 (empty if no following)|Retrieve all people a user is following.|
|`POST`|`/api/users/{id}/following`|201 or 4xx|Add a person to a user's following list.|
|`DELETE`|`/api/users/{id}/following/{followingId}`|200 or 404|Remove a follow from a user's list.|
|`GET`|`/api/users/{id}/search_history`|200 (empty result if no history)|Retrieve search history for a user. Supports pagination.|
|`DELETE`|`/api/users/{id}/search_history`|200 (empty result if no history)|Clear search history for a user. Optionally supports a duration parameter.|
|`GET`|`/api/users/{id}/scores`|200 (empty list if no scores)|Retrieve all scores given by a user. Query parameters: mediaType, mediaId, mediaName.|
|`POST`|`/api/users/{id}/scores`|201 or 4xx|Add a new score for media.|

#### **Filtering, Sorting & Search**

|Method|Path|Status code|Description|
|---|---|---|---|
|`GET`|`/api/search`|200 (empty if no results)|Advanced search for media, with query parameters like keywords, media type, title, genre, and more.|
|`GET`|`/api/media`|200|Supports filtering and sorting by parameters such as date range, popularity, and score.|
