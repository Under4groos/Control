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
        var RequestData = Encoding.UTF8.GetBytes(data);
        udp.Send(RequestData, RequestData.Length, endPoint);

    }
    public static string? ReadString(this UdpClient udp)
    {
        byte[] daata__ = { };
        try
        {
            var ServerEp = new IPEndPoint(IPAddress.Any, 0);

            daata__ = udp.Receive(ref ServerEp);

        }
        catch (System.Exception e)
        {
            Debug.WriteLine(e.Message);
            return null;
        }
        return Encoding.UTF8.GetString(daata__);

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

            //string? data__ = Client.ReadString();


            var ServerEp = new IPEndPoint(IPAddress.Any, 0);

            byte[] daata__ = Client.Receive(ref ServerEp);
            string data__ = Encoding.UTF8.GetString(daata__);

            if (data__ == null)
                return;
            List<JsonOblectAudioSessionControl> jsonOblectAudioSessionControl = JsonConvert.DeserializeObject<List<JsonOblectAudioSessionControl>>(data__);

            _list.Items.Clear();
            foreach (JsonOblectAudioSessionControl item in jsonOblectAudioSessionControl)
            {
                var v__ = new ucVolumeApp();
                v__.viewModel_UcVolumeApp.jsonOblectAudioSessionControl = item;
                v__.ValueChanged += (int id, float volume) =>
                {
                    Client.SendString($"setvolume|{id}|{volume}", new IPEndPoint(IPAddress.Broadcast, 8888));
                };

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