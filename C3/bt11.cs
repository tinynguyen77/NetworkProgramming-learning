using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleUdpSvr
{
    public static void Main ()
    {
        int rev;
        byte [] data = new byte[1024];
        // tạo mảng chứa dữ liệu dạng byte
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo ipendpoint giá địa chỉ IP bất kì chấp nhận port 9050 port
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket 
        newsock.Bind(ipep);
        // gán ipendpoint vào socket
        Console.WriteLine("Waiting for a client...");
        IPEndPoint Remote = (EndPoint) (sender);
        // khởi tạo đối tượng EndPoint lưu trữ địa chỉ sender
        rev = newsock.ReceiveFrom(data, ref Remote);
        // lưu lại endpoint của client, dữ liệu nhận từ client  đc đưa vào mảng data, gán cho rev
        Console.WriteLine("Message received from {0}: ", Remote.ToString());
        // xuất địa chỉ Remote
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, rev));
        // dữ liệu lưu trong mảng data được convert thành chuỗi rồi xuất ra 
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi truyền vào mảng data
        newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
        // Gửi số byte dữ liệu được chỉ định (data) đến điểm cuối được chỉ định (Remote), 
        // bắt đầu từ vị trí được chỉ định trong bộ đệm (data.Length) và sử dụng SocketFlags được chỉ định.
        while (true)
        {
            data = new byte[1024];
            // làm mới mảng byte
            rev = newsock.ReceiveFrom(data, ref Remote);
            // lưu lại endpoint của client, dữ liệu nhận từ client  đc đưa vào mảng data, gán cho rev
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, rev));
            // dữ liệu lưu trong mảng data được convert thành chuỗi rồi xuất ra 
            newsock.SendTo(data, rev, SocketFlags.None, Remote);
            // Gửi số byte dữ liệu được chỉ định (data) đến điểm cuối được chỉ định (Remote), 
            // bắt đầu từ vị trí được chỉ định trong bộ đệm (rev) và sử dụng SocketFlags được chỉ định.
        }
    }
}