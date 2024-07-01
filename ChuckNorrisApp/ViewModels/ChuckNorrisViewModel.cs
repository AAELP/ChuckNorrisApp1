using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ChuckNorrisApp.Helpers;


namespace ChuckNorrisApp.ViewModels
{
    public class ChuckNorrisViewModel : BaseViewModel
{
    public ObservableCollection<ChuckNorrisJoke> Jokes { get; }
    public Command LoadJokesCommand { get; }
    public Command AddJokeCommand { get; }

    private readonly ChuckNorrisApiService _apiService;
    private readonly ChuckNorrisJokeDatabase _database;

    public ChuckNorrisViewModel()
    {
        Title = "Chuck Norris Jokes";
        Jokes = new ObservableCollection<ChuckNorrisJoke>();
        LoadJokesCommand = new Command(async () => await ExecuteLoadJokesCommand());
        AddJokeCommand = new Command(async () => await AddJoke());

        _apiService = new ChuckNorrisApiService();
        _database = new ChuckNorrisJokeDatabase(Constants.DatabasePath);

        LoadJokesCommand.Execute(null);
    }

    async Task ExecuteLoadJokesCommand()
    {
        IsBusy = true;

        try
        {
            Jokes.Clear();
            var jokes = await _database.GetJokesAsync();
            foreach (var joke in jokes)
            {
                Jokes.Add(joke);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    async Task AddJoke()
    {
        var jokeText = await _apiService.GetRandomJoke();
        var random = new Random();
        var code = $"PR{random.Next(1000, 2000)}";

        var joke = new ChuckNorrisJoke
        {
            Joke = jokeText,
            Code = code
        };

        await _database.SaveJokeAsync(joke);
        Jokes.Add(joke);
    }

    public async Task DeleteJoke(ChuckNorrisJoke joke)
    {
        await _database.DeleteJokeAsync(joke);
        Jokes.Remove(joke);
    }

    public async Task UpdateJoke(ChuckNorrisJoke joke)
    {
        await _database.SaveJokeAsync(joke);
        // Refresh list or specific item if necessary
    }
}
}

