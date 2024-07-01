using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class ChuckNorrisApiService
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> GetRandomJoke()
    {
        var response = await client.GetStringAsync("https://api.chucknorris.io/jokes/random");
        var joke = JObject.Parse(response)["value"].ToString();
        return joke;
    }
}

