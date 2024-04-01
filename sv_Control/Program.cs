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
                mMDeviceEnumeratorVolume.Init();
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

                                Console.WriteLine($"SetVolume: {item.ProcessName()}[{item.GetProcessID}]\n -> {item.GetVolume()}");
                            }
                        }
                    }
                }
                break;
            case "setmute":
                if (dataarray.Length == 3)
                {
                    float id = 0f;

                    if (float.TryParse(dataarray[1], out id))
                    {
                        foreach (var item in mMDeviceEnumeratorVolume.AudioSessionControlList)
                        {
                            if (item.GetProcessID == id)
                            {
                                item.SetMute(dataarray[2].ToLower() == "true");

                                Console.WriteLine($"SetMute: {item.ProcessName()}[{item.GetProcessID}]\n -> {item.GetVolume()}");
                            }
                        }
                    }
                }
                break;
            default:

                break;
        }




};
threadUdpClient.ToListen();








//var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
//{

//});
//var app = builder.Build();

//app.MapGet("/", () => "test");

//app.Run();
