# Movie-System API
#### Creating my very first RESTful API using Entity Framework Core, C# and SSMS. 
#### In this project an external api is also used and the source of it is TMDB(The Movie Database).

## API Calls

URL path| Does...
:-----:|:-----:
`/Person`|Get all people
`/Genres`|Get all genres
`/Person/Genre`|Get all genres connected to a specific person
`/Person/Movie`|Get all movies connected to a specific person
`/Person/Ratings`|Get movieratings connected to a person
`/Person/AddRating`|Add movierating connected to a person
`/Person/AddGenre`|Connect a person to a genre
`/Person/AddMovie`|Add a new movie for a specific person and genre
`/Recommendations`|Get movie related to genre
