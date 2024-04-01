using NAudio.CoreAudioApi;
using Newtonsoft.Json;

namespace sv_Control.Module
{
    public static class s_SessionCollection
    {
        public static List<AudioSessionControl> ToList(this SessionCollection sessionCollection)
        {
            List<AudioSessionControl> audioSessionControls = new List<AudioSessionControl>();
            for (int i = 0; i < sessionCollection.Count; i++)
            {
                audioSessionControls.Add(sessionCollection[i]);
            }
            return audioSessionControls;
        }
        public static string ToJsonObjectString(this List<AudioSessionControl> audioSessionControls)
        {
            return JsonConvert.SerializeObject(audioSessionControls);
        }
    }


    public class MMDeviceEnumeratorVolume : IDisposable
    {
        private List<AudioSessionControl> _AudioSessionControlList = new List<AudioSessionControl>();

        public List<AudioSessionControl> AudioSessionControlList
        {
            get
            {
                return _AudioSessionControlList;
            }
        }

        public Action<List<AudioSessionControl>> EventRefreshed;


        public void Dispose()
        {

        }

        public void Init()
        {
            Refresh();


        }

        private void Refresh()
        {
            _AudioSessionControlList.Clear();
            using (MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator())
            {
                var v_ = mMDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
                foreach (var md in v_)
                {
                    for (global::System.Int32 i = 0; i < md.AudioSessionManager.Sessions.Count; i++)
                    {
                        _AudioSessionControlList.Add(md.AudioSessionManager.Sessions[i]);
                    }


                }
            }
            if (EventRefreshed != null)
                EventRefreshed(_AudioSessionControlList);

        }
    }
}
