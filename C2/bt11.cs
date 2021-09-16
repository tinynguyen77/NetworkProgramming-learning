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
        // bộ đếm dữ liệu
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo đối tượng ipep là IPEndPoint có giá trị là địa chỉ ip bất kì ở port 9050
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // tạo socket mới để kết nối tới client trong tầng vận chuyển
        newsock.Bind(ipep);
        // gán socket với địa chỉ
        newsock.Listen(10);
        // chuyển newsock sang trạng thái lắng nghe tối đa 10 kết nối
        Console.WriteLine("Waiting for a client ... ");
        // xuất ra màn hình khi khởi chạy server
        Socket client = newsock.Accept();
        // socket của server chấp nhật kết nối với client
        IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
        // khởi tạo dối tượng clientep là IPEndPoint và kết nối tới socket client
        Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);
        // xuất ra màn hình IPaddress và port của clientep
        string welcome = ("Welcome to my test server");
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành byte
        client.Send(data, data.Length, SocketFlags.None);
        // gửi data đến client
        while (true)
        {
            data = new byte[1024];
            // tạo bộ đếm mới
            recv = client.Receive(data); 
            // đối tượng recv nhận data từ phía client
            if (recv == 0)
                break;
            // nếu k có data nhận vào thì thoát vòng lặp
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv)); 
            // data đc convert thành dạng chuỗi, gán vào convert và xuất ra
            client.Send(data, recv, SocketFlags.None);
            // gửi dữ liệu sang client từ biến recv
            
        }

        Console.WriteLine("Disconnected from {0}", clientep.Address);
        client.Close(); // client đóng kết nối
        newsock.Close(); // server đóng kết nối
    }
}
