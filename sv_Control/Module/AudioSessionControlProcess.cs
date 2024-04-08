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
        public static bool SetMute(this AudioSessionControl audioSessionControl, bool volume)
        {
            return audioSessionControl.SimpleAudioVolume.Mute = volume;
        }
        public static string GetMute(this AudioSessionControl audioSessionControl)
        {
            return audioSessionControl.SimpleAudioVolume.Mute.ToString().ToLower();
        }
        public static bool GetMuteBool(this AudioSessionControl audioSessionControl)
        {
            return audioSessionControl.SimpleAudioVolume.Mute;
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

        public bool IsMute
        {
            get; set;
        } = false;

        public JsonOblectAudioSessionControl Set(AudioSessionControl audioSessionControl)
        {
            Volume = audioSessionControl.GetVolume();
            ProcessName = audioSessionControl.ProcessName();
            ProcessID = (int)audioSessionControl.GetProcessID;
            IsMute = audioSessionControl.GetMuteBool();
            return this;
        }
        public override string ToString()
        {
            return $"Name:{ProcessName} ID:{ProcessID} Volume:{Volume}";
        }
    }
}
