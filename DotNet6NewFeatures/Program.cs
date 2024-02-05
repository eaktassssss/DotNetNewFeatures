using DotNet6NewFeatures.Entities;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

static async Task Main(string[] args)
{
    using (var httpClient = new HttpClient())
    {

        #region DeserializeAsync
        var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
        var content = await response.Content.ReadAsStreamAsync();
        var datas = await JsonSerializer.DeserializeAsync<IEnumerable<Posts>>(content);
        foreach (var item in datas)
        {
            Console.WriteLine($"id={item.id}-- title={item.title}");
        }
        #endregion


        #region DeserializeAsyncEnumerable
        await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<Posts>(content))
        {
            Console.WriteLine($"id={item.id}-- title={item.title}");
        }
        #endregion
    }
}
Console.ReadLine();














