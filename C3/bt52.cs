using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class BetterdUdpClient
{
    public static void Main()
    {
        byte[] data = new byte[30];
        // tạo mảng data chứa dữ liệu dạng byte
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo ipendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket mới
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào mảng data
        server.SendTo(data, data.Length, SocketFlags.None, ipep);
        // gửi dữ liệu trong mảng data đến endpoint chỉ định (ipep)
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // tạo ipendpoint chứa ip bất kì chấp nhận port 0 (port dành riêng trong mạng TCP/IP)
        EndPoint tmpRemote = (EndPoint)sender;
        // tạo endpoint  tmpRemote chứa thông tin endpoint của server đc kết nối
        data = new byte[30];
        // tạo mảng data chứa dữ liệu dạng byte
        int recv = server.ReceiveFrom(data, ref tmpRemote);
        // dữ liệu lưu trong từ server đc đưa vào mảng data, gán vào recv, lưu lại thông tin của tmpRemote
        Console.WriteLine("Message received from {0}:", tmpRemote.ToString());
        // xuất ra màn hình chuỗi địa chỉ tmpRemote
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // convert dữ liệu của recv thành dạng chuỗi rồi xuất ra màn hình
        int i = 30;
        while (true)
        {
            input = Console.ReadLine();
            if (input == "exit")
                break;
                // nếu nhập exit thì thoát vòng lặp
            server.SendTo(Encoding.ASCII.GetBytes(input), tmpRemote);
            // gửi dữ liệu trong input đã convert thành dạng byte rồi gửi đến endpoint chỉ định tmpRemote
            data = new byte[i];
            // tạo mảng data có độ dài bằng i 
            try 
            {
                recv = server.ReceiveFrom(data, ref tmpRemote);
                // nhận dữ liệu từ server rồi lưu trong mảng data, gán vào recv, lưu lại thông tin tmpRemote
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                // convert dữ liệu của recv thành dạng chuỗi rồi gán vào stringData
                Console.WriteLine(stringData);
                // xuất ra màn hình stringData
            }// xuất dữ liệu nếu độ rộng của mảng đủ
            catch (SocketException)
            {
                Console.WriteLine("WARNING: data lost, retry message.");
                i += 10;
            } // nếu bị thất thoát dữ liệu do mảng không đủ vị trí thì xuất thông báo rồi tăng độ dài mảng
        }// thoát vòng lặp thì nhận exit, xuất dòng dưới rồi đóng socket
        Console.WriteLine("Stopping client");
        server.Close();
    }
}