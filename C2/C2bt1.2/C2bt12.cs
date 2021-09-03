using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleTcpClient
{
    public static void Main()
    {
        byte[] data = new byte[1024];
        // tạo bộ đếm
        string input, stringData;
        // tạo biến 
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo đối tượng ipep là IPEndPorn có giá trị là 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // tạo sockket mới để kết nối với server trên tầng vận chuyển
        try
        {
            server.Connect(ipep);
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unale to connect to server,");
            Console.WriteLine(e.ToString());
            return;
        }
        // cho socket "server" của client kết nối tới server
        // nếu thất bại thì xuất thông báo, thành công thì làm tiếp phía dưới
        var recv = server.Receive(data);
        // khai báo biến recv để nhận dữ liệu được gửi từ server qua socket "server"
        stringData = Encoding.ASCII.GetString(data, 0, recv);
        // convert data đã nhận từ server sang dạng chuỗi và gán vào stringData
        Console.WriteLine(stringData);
        // xuất ra màn hình dữ liệu trong stringData
        // thực hiện vòng lặp:
        while (true)
        {
            input = Console.ReadLine();
            if (input == "exit")
                break;
            // nếu nhập exit thì thoát vòng lặp, disconnect server, tắt socket
            // nếu data != exit thì thực hiện tiếp bên dưới
            server.Send(Encoding.ASCII.GetBytes(input));
            // dữ liệu đc convert thành dạng byte và gửi đến server
            data = new byte[1024];
            // tạo bộ đếm mới
            recv = server.Receive(data);
            // đưa dữ liệu đã nhận tự server vào recv
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            // convert data thành dạng chuỗi, gán vào stringData
            Console.WriteLine(stringData);
        }
        

        Console.WriteLine("Disconnecting from server ...");
        server.Shutdown(SocketShutdown.Both);
        server.Close();
    }
}