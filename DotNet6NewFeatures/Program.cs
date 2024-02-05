using DotNet6NewFeatures.Entities;
using System.Diagnostics;
using System.Text;
using System.Text.Json;


using (var httpClient = new HttpClient())
{

    #region DeserializeAsync
    var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
    var content = await response.Content.ReadAsStreamAsync();
    var datas = await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(content);
    foreach (var item in datas)
    {
        Console.WriteLine($"id={item.id}-- title={item.title}");
    }
    #endregion
    #region DeserializeAsyncEnumerable
    //await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<Posts>(content))
    //{
    //    Console.WriteLine($"id={item.id}-- title={item.title}");
    //}
    #endregion
    #region UnionBy/DistinctBy/MaxBy

    var firstProducts = new List<Produt>
    {
        new Produt()
        {
            Id = 1,Name="Product 1",CategoryId = 1
        },

        new Produt()
        {
             Id = 2,Name="Product 2",CategoryId=2
        },
        new Produt()
        {
             Id = 3,Name="Product 3",CategoryId=3
        }
    };

    var secondProducts = new List<Produt>
    {
        new Produt()
        {
            Id = 4,Name="Product 4",CategoryId = 3
        },

        new Produt()
        {
             Id = 5,Name="Product 5",CategoryId=2
        },
        new Produt()
        {
             Id = 6,Name="Product 6",CategoryId=4
        }
    };

    int i = 1;
    var allProducts = firstProducts.UnionBy(secondProducts, p => p.CategoryId);
    foreach (var item in allProducts)
    {
        Console.WriteLine($"Product:{i}");
        Console.WriteLine($"Id:{item.Id}-Name:{item.Name}-CategoryId={item.CategoryId}");
        i++;
    }
    var categoryIdMaxProduct = allProducts.MaxBy(x => x.CategoryId);

    var products = firstProducts.Union(secondProducts).DistinctBy(x => x.CategoryId);
    #endregion
    #region FirstOrDefault/LastOrDefault/SingleOrDefault 


    var cars = new Dictionary<string, int>();
    cars.Add("Volvo", 100);
    cars.Add("Audi", 101);
    cars.Add("BMW", 102);

    var car = cars.Keys.FirstOrDefault(x => x == "Mercedes", "data_not_found");

    var product = products.FirstOrDefault(x => x.CategoryId == 7, new Produt() { Name = "default", Id = 0, CategoryId = 0 });
    #endregion



    #region Zip

    List<string> names = new List<string> { "Alice", "Bob", "Charlie" };

    List<int> ages = new List<int> { 25, 30, 35 };


    var nameAgePairs = names.Zip(ages, (name, age) =>
    {
        return $"name:{name},age:{age}";
    });

    foreach (var item in nameAgePairs)
    {
        Console.WriteLine(item);
    }

    #endregion

}




Console.ReadLine();














