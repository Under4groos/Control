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
    string[] dataarray = data.Trim().Split('|');
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
                    float id = 0f;
                    float volume = 0;
                    if (float.TryParse(dataarray[1], out id) && float.TryParse(dataarray[2], out volume))
                    {
                        foreach (var item in mMDeviceEnumeratorVolume.AudioSessionControlList)
                        {
                            if (item.GetProcessID == id)
                            {
                                item.SetVolume(volume);
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
