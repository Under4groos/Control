using NAudio.CoreAudioApi;
using System.Diagnostics;

namespace sv_Control.Module
{
    public static class AudioSessionControlProcess
    {
        public static string ProcessName(this AudioSessionControl audioSessionControl)
        {
            return Process.GetProcessById((int)audioSessionControl.GetProcessID).ProcessName;
        }
        public static float GetVolume(this AudioSessionControl audioSessionControl)
        {
            return audioSessionControl.SimpleAudioVolume.Volume;
        }
        public static float SetVolume(this AudioSessionControl audioSessionControl, float volume)
        {
            return audioSessionControl.SimpleAudioVolume.Volume = volume;
        }
        public static JsonOblectAudioSessionControl ToJsonObject(this AudioSessionControl audioSessionControl)
        {
            return new JsonOblectAudioSessionControl().Set(audioSessionControl);
        }
    }
    public class JsonOblectAudioSessionControl
    {
        public float Volume
        {
            get; set;
        } = 1;

        public string ProcessName
        {
            get; set;
        } = string.Empty;

        public int ProcessID
        {
            get; set;
        } = 0;

        public JsonOblectAudioSessionControl Set(AudioSessionControl audioSessionControl)
        {
            Volume = audioSessionControl.GetVolume();
            ProcessName = audioSessionControl.ProcessName();
            ProcessID = (int)audioSessionControl.GetProcessID;
            return this;
        }
        public override string ToString()
        {
            return $"Name:{ProcessName} ID:{ProcessID} Volume:{Volume}";
        }
    }
}
