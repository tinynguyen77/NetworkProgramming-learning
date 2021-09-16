using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleUdpClient 
{
    public static void Main ()
    {
        byte [] data = new byte[1024];
        // tạo mảng chứa dữ liệu dạng byte
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo ipendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket để kết nối đến server
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert welcome thành dạng byte rồi gán vào mảng data
        server.SendTo(data, data.Length, SocketFlags.None, ipep);
        // Gửi dữ liệu trong data bắt đầu từ vị trí đc chỉ định trong mảng,
        // đến endpoint được chỉ định (ipep)
        Endpoint Remote = (Endpoint) (sender);
        // tạo đối tượng Remote để lưu địa chỉ của sender
        data = new byte[1024];
        // làm mới mảng dữ liệu
        int rev = server.ReceiveFrom(data, ref Remote);
        // nhận dữ liệu từ server, lưu vào mảng data, gán vào rev, 
        //lưu lại địa chỉ server kết nối đến
        Console.WriteLine("Message received from {0}: ", Remote.ToString());
        // xuất ra địa chỉ của server
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, rev));
        // xuất ra màn hình dữ liệu trong mảng data đã được convert sang dạng chuỗi
        while (true)
        {
            input = Console.ReadLine();
            // nhập dữ liệu từ bàn phím
            if ( input == "exit")
                break;
            server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
            // gửi dữ liệu đến server có địa chỉ đã lưu trong Remote thông qua socket
            // dữ liệu trong input đã được chuyển dạng dạng bytes
            data = new byte[1024];
            // làm mới mảng data
            rev = server.ReceiveFrom(data, ref Remote);
            // nhận dữ liệu từ server, lưu vào mảng data rồi gán vào recv
            // lưu lại địa chỉ của server trong Remote
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            // convert dữ liệu đã nhận thành dạng chuỗi rồi gán vào stringData
            Console.WriteLine(stringData);
            // xuất ra màn hình
        }
        // nếu vòng lặp bị break thì xuất dòng dưới rồi đóng socket
        Console.WriteLine("Stopping Client");
        server.Close;
    }
}