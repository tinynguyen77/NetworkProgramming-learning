using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class OddUdpSvr
{
    public static void Main()
    {
        int recv;
        byte[] data = new byte[1024];
        // taoj mảng data chứa dữ liệu dạng byte
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo ipendpoint chứa ip bất kì chấp nhận port 9050
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket 
        newsock.Bind(ipep);
        // gán ipep vào socket 
        Console.WriteLine("Waiting for client...");
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // tạo ipendpoint chứa ip bất kì chấp nhận port 0 (port dành riêng trong mạng TCP/IP)
        EndPoint Remote = (EndPoint)(sender);
        // khởi tạo đối tượng Remote chứa endpoint mà client kết nối đến
        recv = newsock.ReceiveFrom(data, ref Remote);
        // lưu lại endpoint của client, dữ liệu nhận từ client  đc đưa vào mảng data, gán cho recv
        Console.WriteLine("Messenger received form {0}:", Remote.ToString());
        // xuất ra màn hình chuỗi địa chỉ Remote
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // convert dữ liệu thành dạng chuỗi rồi xuất ra màn hình
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào
        newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
        // Gửi số byte dữ liệu được chỉ định (data) đến điểm cuối được chỉ định (Remote),
        // bắt đầu từ vị trí được chỉ định trong bộ đệm (data.Length)
        while (true)
        {
            data = new byte[1024];
            // làm mới mảng byte
            recv = newsock.ReceiveFrom(data, ref Remote);
            // lưu lại endpoint của client, dữ liệu nhận từ client  đc đưa vào mảng data, gán cho rev
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            // dữ liệu lưu trong mảng data được convert thành chuỗi rồi xuất ra 
            newsock.SendTo(data, recv, SocketFlags.None, Remote);
            // Gửi số byte dữ liệu được chỉ định (data) đến điểm cuối được chỉ định (Remote), 
            // bắt đầu từ vị trí được chỉ định trong bộ đệm (rev) và sử dụng SocketFlags được chỉ định.
        }
        newsock.Close();
    }
}