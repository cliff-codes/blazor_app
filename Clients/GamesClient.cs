using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
     private readonly List<GameSummary> games = 
    [
        new(){
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            Price = 19.23M,
            ReleaseDate = new DateOnly(1992, 7, 15)
        },
        new(){
            Id = 2,
            Name = "Final Fantasy IV",
            Genre = "Roleplaying",
            Price = 34.83M,
            ReleaseDate = new DateOnly(2010, 1, 05)
        },
        new(){
            Id = 3,
            Name = "FIFA 24",
            Genre = "Sports",
            Price = 10.23M,
            ReleaseDate = new DateOnly(2024, 2, 01)
        },
    ];

    private readonly Genre[] genres = new GenresClient().GetGenres();

    public GameSummary[] GetGames() => [.. games];

    //method to add game
    public void AddGame(GameDetails game)
    {
        Genre genre = GetGenreById(game.GenreId);

        var gameSummary = new GameSummary
        {
            Id = games.Count + 1,
            Name = game.Name,
            Genre = genre.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
        games.Add(gameSummary);
    }

    //update game
    public void UpdateGame(GameDetails updatedGame){
        var genre = GetGenreById(updatedGame.GenreId);
        GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

        existingGame.Name = updatedGame.Name;
        existingGame.Genre = genre.Name;
        existingGame.Price = updatedGame.Price;
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    public GameDetails GetGame(int id)
    {
        GameSummary? game = GetGameSummaryById(id);

        var genre = genres.Single(genre => string.Equals(genre.Name, game.Genre, StringComparison.OrdinalIgnoreCase));

        return new GameDetails
        {
            Id = game.Id,
            Name = game.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate,
            GenreId = genre.Id.ToString()
        };
    }

    private GameSummary GetGameSummaryById(int id)
    {
        GameSummary? game = games.Find(game => game.Id == id);
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }

    private Genre GetGenreById(string? id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        return  genres.Single(genre => genre.Id == int.Parse(id));
        
    }

}
