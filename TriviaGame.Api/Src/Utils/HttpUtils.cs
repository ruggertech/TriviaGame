using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using TriviaGame.Api.Dtos.Response;

namespace TriviaGame.Api.Utils;

public class HttpUtils
{
    public static async Task<OpentdbResponse> GetAsync(string url)
    {
        OpentdbResponse opentdbResponse;
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Anything");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();

        try
        {
            opentdbResponse = JsonSerializer.Deserialize<OpentdbResponse>(json);
        }
        catch (Exception)
        {
            TextWriter errorWriter = Console.Error;
            await errorWriter.WriteLineAsync("Failed deserializing trivia questions from url. url = " + url);
            throw;
        }

        return opentdbResponse;
    }
}