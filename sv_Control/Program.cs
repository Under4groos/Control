using sv_Control;
using System.Net;



ThreadUdpClient threadUdpClient = new ThreadUdpClient();
threadUdpClient.EvRequestData += (IPAddress adress, string data) =>
{
    Console.WriteLine($"IP:{adress.Address.ToString()}\n  -> {data}");

};
threadUdpClient.ToListen();








//var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
//{

//});
//var app = builder.Build();

//app.MapGet("/", () => "test");

//app.Run();
