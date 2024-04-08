using Avalonia.Controls;
using Avalonia.Threading;
using Control.Model;
using Control.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Control.Views;


public partial class MainView : UserControl
{
    private UdpClient Client = new UdpClient()
    {
        EnableBroadcast = true
    };

    public MainView()
    {
        InitializeComponent();
        Loaded += MainView_Loaded;
    }

    private void MainView_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {


        _button_refresh.Click += _button_Click;
        __send("getdevice");
    }

    private void _button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string tag_ = (sender as Button).Tag as string ?? string.Empty;


        __send(tag_);

    }

    private void __send(string tag)
    {
        try
        {
            Client.SendString(tag, new IPEndPoint(IPAddress.Broadcast, 8888));
            IPEndPoint ServerEp = new IPEndPoint(IPAddress.Any, 0);
            Client.ReadStringAsync().ContinueWith((Task<string> da) =>
            {
                List<JsonOblectAudioSessionControl> jsonOblectAudioSessionControl = JsonConvert.DeserializeObject<List<JsonOblectAudioSessionControl>>(da.Result);
                Dispatcher.UIThread.Invoke(() =>
                {
                    _list.Children.Clear();
                    foreach (JsonOblectAudioSessionControl item in jsonOblectAudioSessionControl)
                    {
                        ucVolumeApp v__ = new ucVolumeApp();
                        v__.viewModel_UcVolumeApp.jsonOblectAudioSessionControl = item;
                        v__.ValueChanged += (ViewModel_ucVolumeApp viewModel_UcVolumeApp) =>
                        {
                            Client.SendString(viewModel_UcVolumeApp.jsonOblectAudioSessionControl.ToJson(), new IPEndPoint(IPAddress.Broadcast, 8888));
                        };

                        _list.Children.Add(v__);
                    }
                });
            });
        }
        catch (System.Exception ee)
        {
            Debug.WriteLine(ee.Message);
        }

    }






}