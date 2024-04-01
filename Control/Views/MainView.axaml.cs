using Avalonia.Controls;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

            var RequestData = Encoding.ASCII.GetBytes(tag_);
            var ServerEp = new IPEndPoint(IPAddress.Any, 0);


            Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, 8888));





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