using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


class FixedTcpSrvr
{
    // tạo hàm SendData gồm 2 đối tượng socket s và mảng data
    private static int SendData(Socket s, byte[] data)
    {
        int total = 0; // tổng dữ liệu đã gửi
        int size = data.Length; // size = kích thước dữ liệu trong mảng
        int dataleft = size; // dataleft = lượng dữ liệu còn lại cần gửi
        int sent; // dữ liểu được gửi
        while (total < size)
        {
            sent = s.Send(data, total, dataleft, SocketFlags.None);
            // Send(byte[] data, offset, size, SocketFlags)
            // gửi các byte dữ liệu có kích thước (dataleft) bắt đầu từ độ lệch bù (tại vị trí 0(total)) trong mảng byte
            total += sent; // total = total + sent;
            dataleft -= sent; // dataleft = dataleft - sent
        }
        return total; //trả về tổng dữ liệu đã gửi
    }
    
    // tạo hàm ReceiveData nhận dữ liệu từ client gồm 2 đối tượng socket s và kích thước dữ liệu
    private static byte[] ReceiveData(Socket s, int size)
    {
        int total = 0; // tổng dữ liệu đã nhận
        int dataleft = size; // kích thước dữ liệu cần nhận
        byte[] data = new byte[size]; // mảng đếm byte
        int recv; // dữ liệu được nhận
        while (total < size)
        {
            recv = s.Receive(data, total, dataleft, 0);
            // Receive(byte[] data, offset, size, SocketFlags)
            // nhận các dữ liệu có kích thước (dataleft) bắt đầu từ độ lệch bù (tại vị trí 0(total)) trong mảng byte
            if (recv == 0) 
            {
                data = Encoding.ASCII.GetBytes("exit");
                break;
                // nếu không nhận được dữ liệu nữa, thoát vòng lặp while
            }
            total += recv; // tăng dần dữ liệu đã nhận
            dataleft -= recv; // giảm dần kích thước dữ liệu cần nhận
        }
        return data; // trả về dữ liệu đã nhận
    }
    
    
    public static void Main()
    {
        byte[] data = new byte[1024];
        // tạo bộ đếm mảng byte
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo IPEndpoint ipep với bất cứ địa chỉ IP nào chấp nhập port 9050
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // khởi tạo socket mới
        newsock.Bind(ipep);
        // gán IPendpoint ipep vào socket
        newsock.Listen(10);
        // chuyển socket vào trạng thái lắng nghe kết nối (tối đa 10)
        Console.WriteLine("Waiting for a client...");
        Socket client = newsock.Accept();
        // socket server chấp nhận các kết nối tới từ client
        IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
        // liên kết tới ipendpoint của client
        Console.WriteLine("Connected with {0} at {1}", newclient.Address, newclient.Port);
        // xuất ra màn hình ipendpoint của client
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào mảng data
        int sent = SendData(client, data);
        // dùng hàm SendData chuyển data đến client thông qua socket của client đã lấy thông tin ở trên
        for (int i = 0; i < 5; i++)
        {
            data = ReceiveData(client, 14); 
            // dùng hàm ReceiveData để nhận dữ liệu từ client gửi đến với độ dài của dữ liệu là 14 rồi gán dữ liệu vào biến data
            Console.WriteLine(Encoding.ASCII.GetString(data));
            // convert dữ liệu đã nhận từ client thành dạng chuỗi string rồi xuất ra màn hình
        }
        Console.WriteLine("Disconnected from {0} port {1}", newclient.Address, newclient.Port);
        client.Close();
        newsock.Close();
    }
}