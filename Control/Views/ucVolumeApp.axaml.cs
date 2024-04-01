using Avalonia.Controls;
using Control.ViewModels;
using System;

namespace Control.Views
{
    public partial class ucVolumeApp : UserControl
    {
        public ViewModel_ucVolumeApp viewModel_UcVolumeApp = new ViewModel_ucVolumeApp();
        public Action<int, float> ValueChanged;
        public ucVolumeApp()
        {
            InitializeComponent();

            viewModel_UcVolumeApp.PropertyChanged += ViewModel_UcVolumeApp_PropertyChanged;


            _slider.ValueChanged += _slider_ValueChanged;
        }

        private void _slider_ValueChanged(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(viewModel_UcVolumeApp.jsonOblectAudioSessionControl.ProcessID, (float)_slider.Value);
        }

        private void ViewModel_UcVolumeApp_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _label.Content = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.ProcessName;
            _slider.Value = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.Volume;
            _label_id.Content = $"({viewModel_UcVolumeApp.jsonOblectAudioSessionControl.ProcessID})";

        }
    }
}
