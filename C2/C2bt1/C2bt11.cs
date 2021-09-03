using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SimpleTcpSrvr
{
    public static void Main()
    {
        int recv;
        // tạo đối tượng recv
        byte[] data = new byte[1024];
        // tạo mảng data 
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo đối tượng ipep là IPEndPoint có giá trị là địa chỉ ip bất kì chấp nhận port 9050
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // tạo socket mới
        newsock.Bind(ipep);
        // gán ipep vào newsock
        newsock.Listen(10);
        // chuyển newsock sang trạng thái lắng nghe tối đa 10 kết nối
        Console.WriteLine("Waiting for a client ... ");
        Socket client = newsock.Accept();
        // khởi tạo socket client bên phía server để giao tiếp với client
        IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
        // khởi tạo dối tượng clientep là IPEndPoint và kết nối tới socket client
        Console.WriteLine("Welcome to my test server");
        
        data = Encoding.ASCII.GetBytes(welcome);
        client.Send(data, data.Length, SocketFlags.None);
        // gửi toàn bộ data
        while (true)
        {
            data = new byte[1024];
            recv = client.Receive(data); // đối tượng recv nhận data được gửi vào từ client
            if (recv == 0)
                break;
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv)); 
            // xuất ra data đã nhận ở dạng cuỗi
            client.Send(data, recv, SocketFlags.None);
            
        }

        Console.WriteLine("Disconnected from {0}", clientep.Address);
        client.Close(); // client đóng kết nối
        newsock.Close(); // server đóng kết nối
    }
}
