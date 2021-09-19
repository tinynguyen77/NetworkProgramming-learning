using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class TestUdpClient
{
    public static void Main()
    {
        byte[] data = new byte[1024];
        // tạo mảng data chứa dữ liệu dạng byte
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo ipendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
        // tạo socket để kết nối đến server
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào mảng data
        server.SendTo(data, data.Length, SocketFlags.None, ipep);
        // gửi dữ liệu dạng byte bắt đầu từ vị trí chỉ định (data.Length), không sử dụng flag, đến vị trí chỉ định ipep
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // tạo ipendpoint chứa ip bất kì chấp nhận port 0 (port danh riêng trong mạng TCP/IP)
        EndPoint tmpRemote = (EndPoint)sender;
        // tạo endpoint tmpRemote chứa endpoint của sender
        data = new byte[1024];
        // làm mới mảng
        int recv = server.ReceiveFrom(data, ref tmpRemote);
        // dữ liệu nhận được từ server đc đưa vào mảng data, gán vào recv, lưu lại thông tin của tmpRemote
        Console.WriteLine("Message received from {0}:", tmpRemote.ToString());
        // xuất ra màn hình chuỗi địa chỉ tmpRemote
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // convert dữ liệu của recv thành dạng chuỗi rồi xuất ra màn hình
        server.SendTo(Encoding.ASCII.GetBytes("message 1"), tmpRemote);
        server.SendTo(Encoding.ASCII.GetBytes("message 2"), tmpRemote);
        server.SendTo(Encoding.ASCII.GetBytes("message 3"), tmpRemote);
        server.SendTo(Encoding.ASCII.GetBytes("message 4"), tmpRemote);
        server.SendTo(Encoding.ASCII.GetBytes("message 5"), tmpRemote);
        Console.WriteLine("Stopping client");
        server.Close();
        // sau khi gửi 5 chuỗi dữ liệu đã đc convert thành dạng byte thì xuất thông báo stopping client, đóng socket
    }
}