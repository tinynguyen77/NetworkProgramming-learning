using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class OddUdpClient
{
    public static void Main()
    {
        byte[] data = new byte[1024];
        // tạo mảng chứa dữ liệu dạng byte
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo ipendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket để kết nối đến server
        server.Connect(ipep);
        // socket thiết lập kết nối tới ipep 
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào mảng data
        server.Send(data);
        // gửi dữ liệu đến socket đã kết nối
        data = new byte[1024];
        // làm mới mảng
        int recv = server.Receive(data);
        // nhận dữ liệu từ một socket bị ràng buộc, gán dữ liệu vào recv
        Console.WriteLine("Message received from {0}:", ipep.ToString());
        // xuất ra màn hình ipep dạng chuỗi
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // xuất ra màn hình dữ liệu đã được convert qua dạng chuỗi
        while (true)
        {
            input = Console.ReadLine();
            // nhập từ bàn phím 
            if (input == "exit")
                break;
            server.Send(Encoding.ASCII.GetBytes(input));
            // gửi dữ liệu đã được convert thành dạng byte đến socket đã kết
            data = new byte[1024];
            // làm mới mảng data
            recv = server.Receive(data);
            // nhận dữ liệu từ một socket bị ràng buộc, gán dữ liệu vào recv
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            // convert dữ liệu thành chuỗi rồi gán vào stringData
            Console.WriteLine(stringData);
            // xuất ra màn hình stringData
        }
        // nếu vòng lặp bị break thì xuất dòng dưới rồi đóng socket
        Console.WriteLine("Stopping client");
        server.Close();
    }
}