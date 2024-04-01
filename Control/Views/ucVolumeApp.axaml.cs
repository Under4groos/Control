using Avalonia.Controls;
using Avalonia.Media;
using Control.ViewModels;
using System;

namespace Control.Views
{
    public partial class ucVolumeApp : UserControl
    {
        public ViewModel_ucVolumeApp viewModel_UcVolumeApp = new ViewModel_ucVolumeApp();
        public Action<int, float> ValueChanged;
        public Action<int, string> EvButtonClick;
        private int AppId
        {
            get
            {
                return viewModel_UcVolumeApp.jsonOblectAudioSessionControl.ProcessID;
            }
        }
        public ucVolumeApp()
        {
            InitializeComponent();

            viewModel_UcVolumeApp.PropertyChanged += ViewModel_UcVolumeApp_PropertyChanged;

            _button.Click += _button_Click;
            _slider.ValueChanged += _slider_ValueChanged;
        }

        private void _button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            viewModel_UcVolumeApp.jsonOblectAudioSessionControl.IsMute = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.IsMute == "true" ? "false" : "true";
            if (EvButtonClick != null)
                EvButtonClick(AppId, viewModel_UcVolumeApp.jsonOblectAudioSessionControl.IsMute);
            ViewModel_UcVolumeApp_PropertyChanged(null, null);
        }

        private void _slider_ValueChanged(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(AppId, (float)_slider.Value);
            viewModel_UcVolumeApp.jsonOblectAudioSessionControl.Volume = (float)_slider.Value;
        }

        private void ViewModel_UcVolumeApp_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _label.Content = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.ProcessName;
            _slider.Value = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.Volume;
            _label_id.Content = $"({AppId})";

            if (viewModel_UcVolumeApp.jsonOblectAudioSessionControl.IsMute == "true")
            {
                _slider.IsEnabled = false;
            }
            else
            {
                _slider.IsEnabled = true;
            }
            _button.Background = viewModel_UcVolumeApp.jsonOblectAudioSessionControl.IsMute == "true" ? Brushes.Red : Brushes.White;
        }
    }
}
