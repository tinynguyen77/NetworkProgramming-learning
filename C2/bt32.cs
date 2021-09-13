using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class FixTcpClient
{
    // tạo hàn SendData để gửi dữ liệu đến server
    private static int SendData (Socket s, byte[] data)
    {
        int total = 0; // tổng dữ liệu đã gửi
        int size = data.Length; // kích thước dữ liệu trong mảng
        int dataleft = size; // kích thước dữ liệu cần gửi còn lại
        int sent; // dữ liệu được gửi
        while (total < size)
        {
            sent = s.Send(data, total, dataleft, SocketFlags.None);
            // Send(byte[], offset, size, SocketFlags)
            // gửi các dữ liệu có kích thước (dataleft) bắt đầu từ độ lệch bù (tại vị trí 0(total)) trong mảng byte
            total += sent; // cộng dồn các dữ liệu đã gửi
            dataleft -= sent; // trừ dần kích thước các dữ liệu đã gửi
        }
        return total;
    }
    
    // tạo hàm ReceiveData để nhận dữ liệu nhận từ server
    private static byte[] ReceiveData(Socket s, int size)
    {
        int total = 0; //tổng dữ liệu đã nhận
        int dataleft = size; // kích thước dữ liệu cần nhận
        byte[] data = new byte[size]; // tạo mảng đếm byte
        int recv; // dữ liệu được nhận
        while (total < size)
        {
            recv = s.Receive(data, total, dataleft, 0);
            // Receive(byte[] data, offset, size, SocketLags)
            // nhận các dữ liệu có kích thước (dataleft) bắt đầu từ độ lệch bù (tại vị trí 0(total)) trong mảng byte
            if (recv == 0)
            {
                data = Encoding.ASCII.GetBytes("exit");
                break;
                // điều kiện: nếu không có giá trị nhận vào thì thoát vòng lặp
            }
            total += recv; // tăng dần dữ liệu nhận vào
            dataleft -= recv; // giảm dần kích thước dữ liệu cần nhận
        }
        return data; //trả về dữ liệu đã nhận
    }

    public static void Main()
    {
        byte[] data = new byte[1024];
        // tạo mảng đếm byte
        int sent;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo IPendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // tạo socket để kết nối tới server
        try
        {
            server.Connect(ipep);
            // thử kết nối tới server thông qua ipep
        }
        catch(SocketException e)
        {
            Console.WriteLine("Unable to connect to server.");
            Console.WriteLine(e.ToString());
            return;
            // nếu không thể kết nối tới server thì trả về thông báo lỗi
        }
        int recv = server.Receive(data);
        // Nhận dữ liệu được gửi từ server rồi đưa vào biến recv
        string stringData = Encoding.ASCII.GetString(data, 0, recv);
        // convert dữ liệu trong recv sang dạng chuỗi rồi đưa vào biến stringData
        Console.WriteLine(stringData);
        // xuất ra màn hình stringData
        // dùng hàm SendData gửi dữ liệu đã được convert về dạng byte đến server
        sent = SendData(server, Encoding.ASCII.GetBytes("message 123456"));
        sent = SendData(server, Encoding.ASCII.GetBytes("message 223456"));
        sent = SendData(server, Encoding.ASCII.GetBytes("message 323456"));
        sent = SendData(server, Encoding.ASCII.GetBytes("message 423456"));
        sent = SendData(server, Encoding.ASCII.GetBytes("message 525984609386")); 
        // do server chỉ nhận kích thước data = 14, nên chỉ gửi đến đoạn "message 525984"
        
        Console.WriteLine("Disconnecting from server...");
        server.Shutdown(SocketShutdown.Both);
        server.Close();

    }
}