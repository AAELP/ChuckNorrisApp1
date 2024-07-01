using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ChuckNorrisJokeDatabase
{
    readonly SQLiteAsyncConnection _database;

    public ChuckNorrisJokeDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<ChuckNorrisJoke>().Wait();
    }

    public Task<List<ChuckNorrisJoke>> GetJokesAsync()
    {
        return _database.Table<ChuckNorrisJoke>().ToListAsync();
    }

    public Task<int> SaveJokeAsync(ChuckNorrisJoke joke)
    {
        if (joke.ID != 0)
        {
            return _database.UpdateAsync(joke);
        }
        else
        {
            return _database.InsertAsync(joke);
        }
    }

    public Task<int> DeleteJokeAsync(ChuckNorrisJoke joke)
    {
        return _database.DeleteAsync(joke);
    }
}
