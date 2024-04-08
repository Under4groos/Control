using NAudio.CoreAudioApi;
using Newtonsoft.Json;
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
    Console.WriteLine(data);
    if (data.StartsWith("getdevice"))
    {
        mMDeviceEnumeratorVolume.Init();
        string data_ = mMDeviceEnumeratorVolume.AudioSessionControlList.ToJsonObjectString();

        threadUdpClient.Send(data_);
        return;
    }


    JsonOblectAudioSessionControl _JsonOblectAudioSessionControl = JsonConvert.DeserializeObject<JsonOblectAudioSessionControl>(data);


    AudioSessionControl sess_ = mMDeviceEnumeratorVolume.AudioSessionControlList.Where(x => x.GetProcessID == _JsonOblectAudioSessionControl.ProcessID).First();
    if (sess_ != null)
    {
        sess_.SetVolume(_JsonOblectAudioSessionControl.Volume);
        sess_.SetMute(_JsonOblectAudioSessionControl.IsMute);
    }

};
threadUdpClient.ToListen();








//var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
//{

//});
//var app = builder.Build();

//app.MapGet("/", () => "test");

//app.Run();
