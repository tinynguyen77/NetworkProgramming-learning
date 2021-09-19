using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class BadUdpClient
{
    public static void Main()
    {
        byte[] data = new byte[30];
        // tạo mảng data chứa dữ liệu dạng byte
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo ipendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket để giao tiếp với server
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào mảng data
        server.SendTo(data, data.Length, SocketFlags.None, ipep);
        // gửi dữ liệu dạng byte bắt đầu từ vị trí chỉ định (data.Length) không sử dụng flag, đến ipendpoint chỉ định ipep
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // tạo ipendpoint chứa ip bất kì chấp nhận port 0 (port danh riêng trong mạng TCP/IP)
        EndPoint tmpRemote = (EndPoint)sender;
        // tạo endpoint  tmpRemote chứa thông tin của sender 
        data = new byte[30];
        // tạo mảng data chứa dữ liệu dạng byte
        int recv = server.ReceiveFrom(data, ref tmpRemote);
        // dữ liệu dạng byte từ server đc đưa vào mảng data, gán vào recv, lưu lại thông tin của tmpRemote
        Console.WriteLine("Message received from {0}:", tmpRemote.ToString());
        // xuất ra màn hình chuỗi địa chỉ tmpRemote
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // xuất ra màn hình dữ liệu đã convert sang dạng chuỗi đc lưu trong recv
        while (true)
        {
            input = Console.ReadLine();
            // nhập từ bàn phím
            if (input == "exit")
                break;
            server.SendTo(Encoding.ASCII.GetBytes(input), tmpRemote);
            // gửi dữ liệu dạng byte đến địa chỉ đã lưu trong tmpRemote
            data = new byte[30];
            // làm mới mảng data
            recv = server.ReceiveFrom(data, ref tmpRemote);
            // nhận dữ liệu từ server rồi lưu trong mảng data, gán vào recv, lưu lại thông tin tmpRemote
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);
        }
        // nếu vòng lặp bị break thì xuất dòng dưới rồi đóng socket
        Console.WriteLine("Stopping client");
        server.Close();
    }
}