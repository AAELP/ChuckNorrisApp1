using SQLite;

public class ChuckNorrisJoke
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Joke { get; set; }
    public string Code { get; set; }
}
