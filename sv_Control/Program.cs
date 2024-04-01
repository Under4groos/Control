using NAudio.CoreAudioApi;
using sv_Control;
using sv_Control.Module;
using System.Net;


MMDeviceEnumeratorVolume mMDeviceEnumeratorVolume = new MMDeviceEnumeratorVolume();
mMDeviceEnumeratorVolume.EventRefreshed += (List<AudioSessionControl> sessionCollections) =>
{
    foreach (AudioSessionControl AudioSession in sessionCollections)
    {
        Console.WriteLine($"{AudioSession.ToJsonObject()}");
    }
};
mMDeviceEnumeratorVolume.Init();


ThreadUdpClient threadUdpClient = new ThreadUdpClient();
threadUdpClient.EvRequestData += (IPAddress adress, string data) =>
{
    data = data.Trim();
    switch (data)
    {
        case "getdevice":
            string data_ = mMDeviceEnumeratorVolume.AudioSessionControlList.ToJsonObjectString();
            Console.WriteLine(data_);
            threadUdpClient.Send(data_);
            break;
        default:

            break;
    }


    Console.WriteLine($"IP:{adress.Address.ToString()}\n  -> {data}");

};
threadUdpClient.ToListen();








//var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
//{

//});
//var app = builder.Build();

//app.MapGet("/", () => "test");

//app.Run();
