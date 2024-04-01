using Avalonia.Controls;
using Control.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Control.Views;

public static class s_UdpClient
{
    public static void SendString(this UdpClient udp, string data, IPEndPoint? endPoint)
    {
        var RequestData = Encoding.ASCII.GetBytes(data);
        udp.Send(RequestData, RequestData.Length, endPoint);

    }
    public static string ReadString(this UdpClient udp)
    {
        var ServerEp = new IPEndPoint(IPAddress.Any, 0);
        return Encoding.ASCII.GetString(udp.Receive(ref ServerEp));

    }
}

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

        foreach (Button item in _stack.Children)
        {

            item.Click += _button_Click;
        }

    }

    private void _button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string tag_ = (sender as Button).Tag as string ?? string.Empty;

        try
        {







            Client.SendString(tag_, new IPEndPoint(IPAddress.Broadcast, 8888));

            List<JsonOblectAudioSessionControl> jsonOblectAudioSessionControl = JsonConvert.DeserializeObject<List<JsonOblectAudioSessionControl>>(Client.ReadString());

            _list.Items.Clear();
            foreach (JsonOblectAudioSessionControl item in jsonOblectAudioSessionControl)
            {
                var v__ = new ucVolumeApp();
                v__.viewModel_UcVolumeApp.jsonOblectAudioSessionControl = item;
                //v__.viewModel_UcVolumeApp.Volume = item.Volume;
                //v__.viewModel_UcVolumeApp.ProcessName = item.ProcessName;

                _list.Items.Add(v__);
            }




        }
        catch (System.Exception ee)
        {
            Debug.WriteLine(ee.Message);
        }



    }




    private void logadd(string line)
    {

        Debug.WriteLine(line);
    }





}