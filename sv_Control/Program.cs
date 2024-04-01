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
    string[] dataarray = data.Trim().Split(',');
    if (dataarray.Length > 0)
        switch (dataarray[0])
        {
            case "getdevice":
                string data_ = mMDeviceEnumeratorVolume.AudioSessionControlList.ToJsonObjectString();
                Console.WriteLine(data_);
                threadUdpClient.Send(data_);
                break;
            case "setvolume":
                if (dataarray.Length == 3)
                {
                    float f = 0f;
                    int id = 0;
                    if (float.TryParse(dataarray[1], out f) && int.TryParse(dataarray[2], out id))
                    {
                        foreach (var item in mMDeviceEnumeratorVolume.AudioSessionControlList)
                        {
                            if (item.GetProcessID == id)
                            {
                                item.SetVolume(f);
                            }
                        }
                    }
                }
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
