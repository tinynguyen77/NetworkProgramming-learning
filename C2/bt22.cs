using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class BadTcpClient
{
    public static void Main()
    {
        byte[] data = new byte[1024];
        // tạo mảng đếm
        string stringData;
        // tạo biến
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo IPendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // tạoc socket để kết nối đến server
        try
        {
            server.Connect(ipep);
            // thử kết nối đến server

        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server");
            Console.WriteLine(e.ToString());
            return;
            // Nếu không kết nối đc đến sv thì đưa ra thông báo
        }

        int recv = server.Receive(data);
        // gán dữ liệu (dạng bytes) nhận được từ sv vào biến recv
        stringData = Encoding.ASCII.GetString(data, 0, recv);
        // convert dữ liệu thành dạng chuỗi rồi gán vào stringData
        Console.WriteLine(stringData);
        // xuất ra màn hình stringData

        server.Send(Encoding.ASCII.GetBytes("message 1"));
        server.Send(Encoding.ASCII.GetBytes("message 2"));
        server.Send(Encoding.ASCII.GetBytes("message 3"));
        server.Send(Encoding.ASCII.GetBytes("message 4"));
        server.Send(Encoding.ASCII.GetBytes("message 5"));
        server.Send(Encoding.ASCII.GetBytes("message 5"));
        server.Send(Encoding.ASCII.GetBytes("message 6"));
        server.Send(Encoding.ASCII.GetBytes("message 7"));
        server.Send(Encoding.ASCII.GetBytes("message 8"));
        // convert dữ liệu sang dạng bytes rồi gửi cho sv
        Console.WriteLine("Disconnecting from server...");
        server.Shutdown(SocketShutdown.Both);
        server.Close();
    }
}