using System.Net.Http.Json;

var http = new HttpClient
{
    BaseAddress = new Uri("http://localhost:5000")
};

Console.WriteLine("Klient systemu zarządzania zasobami firmy");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("1. Pokaż zasoby");
    Console.WriteLine("2. Dodaj zasób");
    Console.WriteLine("3. Zarezerwuj zasób");
    Console.WriteLine("4. Zwolnij zasób");
    Console.WriteLine("5. Usuń zasób");
    Console.WriteLine("0. Wyjście");
    Console.Write("Wybór: ");

    var option = Console.ReadLine();

    try
    {
        switch (option)
        {
            case "1":
                await ShowResources();
                break;

            case "2":
                await AddResource();
                break;

            case "3":
                await ReserveResource();
                break;

            case "4":
                await ReleaseResource();
                break;

            case "5":
                await DeleteResource();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Nieprawidłowa opcja.");
                break;
        }
    }
    catch (HttpRequestException)
    {
        Console.WriteLine("Błąd połączenia. Najpierw uruchom projekt ZasobyApi.");
    }
}

async Task ShowResources()
{
    var resources = await http.GetFromJsonAsync<List<Resource>>("/resources") ?? new List<Resource>();

    Console.WriteLine();
    Console.WriteLine("Lista zasobów:");

    foreach (var resource in resources)
    {
        var status = resource.IsReserved
            ? "Zarezerwowany przez: " + resource.ReservedBy
            : "Dostępny";

        Console.WriteLine($"{resource.Id}. {resource.Name} | {resource.Type} | {status}");
    }
}

async Task AddResource()
{
    Console.Write("Nazwa zasobu: ");
    var name = Console.ReadLine() ?? "";

    Console.Write("Typ zasobu: ");
    var type = Console.ReadLine() ?? "";

    var response = await http.PostAsJsonAsync("/resources", new
    {
        Name = name,
        Type = type
    });

    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

async Task ReserveResource()
{
    Console.Write("ID zasobu: ");
    var id = Console.ReadLine();

    Console.Write("Kto rezerwuje: ");
    var user = Console.ReadLine() ?? "";

    var response = await http.PutAsJsonAsync($"/resources/{id}/reserve", new
    {
        User = user
    });

    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

async Task ReleaseResource()
{
    Console.Write("ID zasobu: ");
    var id = Console.ReadLine();

    var response = await http.PutAsync($"/resources/{id}/release", null);

    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

async Task DeleteResource()
{
    Console.Write("ID zasobu: ");
    var id = Console.ReadLine();

    var response = await http.DeleteAsync($"/resources/{id}");

    Console.WriteLine(await response.Content.ReadAsStringAsync());
}

class Resource
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public bool IsReserved { get; set; }
    public string ReservedBy { get; set; } = "";
}