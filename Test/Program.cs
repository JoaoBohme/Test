using Test.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Timers;

HttpClient client = new HttpClient();
System.Timers.Timer aTimer;
aTimer = new System.Timers.Timer();
System.Timers.Timer bTimer;
bTimer = new System.Timers.Timer();
Example Example = new();

string? _path = "";

do
{
    Console.Write("ENTER A PATH: ");
    _path = Console.ReadLine();
    if (!File.Exists(_path))
    {
        Console.WriteLine("NOT FOUND");
    }
}
while (!File.Exists(_path));

//C:\Users\Admin\source\repos\Test\Test\Data\example.txt

string[] content = File.ReadAllLines(_path);

for (int i = 0; i < content.Count(); i++)
{
    content[i] = content[i].Replace(" ", "");
    int semicolom = content[i].IndexOf(';');

    Example.IdExample = i + 1;
    Example.Category = content[i].Substring(0, semicolom);
    Example.Year = content[i].Substring(semicolom + 1);

#pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
    Example.ExampleList.Add(Example);
#pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

    aTimer.AutoReset = false;
    aTimer.AutoReset = false;
    if (i == 0)
    {
        aTimer.Enabled = false;
        bTimer.Enabled = false;
    }

    do
    {
        if (!aTimer.Enabled && !bTimer.Enabled)
        {


#pragma warning disable CS8622 // A nulidade de tipos de referência no tipo de parâmetro não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
#pragma warning restore CS8622 // A nulidade de tipos de referência no tipo de parâmetro não corresponde ao delegado de destino (possivelmente devido a atributos de nulidade).

            Console.WriteLine(Example.ExampleList[i].IdExample + "|" + Example.ExampleList[i].Category + "|" + Example.ExampleList[i].Year);

            using (var response = await client.GetAsync("https://api.nobelprize.org/v1/prize.json?category=" + Example.Category[i] + "&year=" + Example.Year[i]))
            {
                if (response.IsSuccessStatusCode)
                {

                    string result = await response.Content.ReadAsStringAsync();
                    File.WriteAllText("C:/Users/Admin/source/repos/Test/Test/Data/log.txt", result);

                    Console.WriteLine("   FOUND");
                    if (i == content.Count() - 1)
                    {
                        bTimer = new System.Timers.Timer(60000);
                        bTimer.Start();
                    }
                    else
                    {
                        CancellationTokenSource source = new CancellationTokenSource();
                        await Task.Delay(15000, source.Token);
                    }
                }
                else
                {
                    Console.WriteLine("   NOT FOUND");
                }
            }
        }
    }
    while (aTimer.Enabled && bTimer.Enabled);
}

static void OnTimedEvent(object source, ElapsedEventArgs e)
{
    Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
}