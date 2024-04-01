using Control.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Control.ViewModels
{

    public class ViewModel_ucVolumeApp : ViewModelBase
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private JsonOblectAudioSessionControl _jsonOblectAudioSessionControl { get; set; }
        public JsonOblectAudioSessionControl jsonOblectAudioSessionControl
        {
            get
            {
                return _jsonOblectAudioSessionControl;
            }
            set
            {
                _jsonOblectAudioSessionControl = value;
                OnPropertyChanged();
            }
        }

    }
}
