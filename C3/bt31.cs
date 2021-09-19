using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class TestUdpSrvr
{
    public static void Main()
    {
        int recv;
        byte[] data = new byte[1024];
        // tạo mảng chứa dữ liệu dạng byte
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo ipendpoint chứa ip bất kì chấp nhận port 9050 
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket để giao tiếp với client
        newsock.Bind(ipep);
        // gán ipendpoint vào socket 
        Console.WriteLine("Waiting for a client...");
        // xuất ra màn hình
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // tạo ipendpoint chứa ip bất kì chấp nhận port 0 (port dành riêng trong mạng TCP/IP)
        EndPoint tmpRemote = (EndPoint)(sender);
        // tạo đối tượng endpoint chứa giá trị endpoint của sender
        recv = newsock.ReceiveFrom(data, ref tmpRemote);
        // socket nhận dữ liệu đc gửi từ client vào mảng data, gán vào recv, lưu lại thông tin của tmpRemote
        Console.WriteLine("Message received from {0}:", tmpRemote.ToString());
        // xuất ra màn hình chuỗi địa chỉ tmpRemote
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // convert dữ liệu của recv thành dạng chuỗi rồi xuất ra màn hình
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào data
        newsock.SendTo(data, data.Length, SocketFlags.None, tmpRemote);
        // gửi dữ liệu có độ dài chỉ định (data.Length) trong mảng data đến địa chỉ client đã lưu trong tmpRemote
        for (int i = 0; i < 5; i++)
        {
            data = new byte[1024];
            // làm mới mảng
            recv = newsock.ReceiveFrom(data, ref tmpRemote);
            // socket nhận dữ liệu đc gửi từ client vào mảng data, gán vào recv, lưu lại thông tin của tmpRemote
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            // convert dữ liệu của recv thành dạng chuỗi rồi xuất ra màn hình
        }
        // sau 5 lượt gửi dữ liệu thì thoát vòng lặp, đóng socket
        newsock.Close();
    }
}
