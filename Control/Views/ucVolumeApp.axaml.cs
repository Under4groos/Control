using Avalonia.Controls;
using Control.ViewModels;

namespace Control.Views
{
    public partial class ucVolumeApp : UserControl
    {
        public ViewModel_ucVolumeApp viewModel_UcVolumeApp = new ViewModel_ucVolumeApp();

        public ucVolumeApp()
        {
            InitializeComponent();

            viewModel_UcVolumeApp.PropertyChanged += ViewModel_UcVolumeApp_PropertyChanged;
        }

        private void ViewModel_UcVolumeApp_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _label.Content = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.ProcessName;
            _slider.Value = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.Volume;
        }
    }
}
