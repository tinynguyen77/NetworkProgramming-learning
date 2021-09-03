using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class VarTcpSrvr
{
    // tạo hàm gửi dữ liệu
    private static int SendData(Socket s, byte[] data)
    {
        int total = 0; // tổng dữ liệu đã gửi
        int size = data.Length; // lượng dữ liệu
        int dataleft = size; // lượng dữ liệu còn lại 
        int sent;
        byte[] datasize = new byte[4]; // mảng chứa dữ liệu gửi
        datasize = BitConverter.GetBytes(size); // convert dữ liệu thành dạng byte

        sent = s.Send(datasize); // gửi mảng chứa dữ liệu
        while (total < size)
        {
            sent = s.Send(data, total, dataleft, SocketFlags.None);
            // Send(byte[] data, offset, size, SocketFlags)
            // gửi các byte dữ liệu có kích thước (dataleft) bắt đầu từ độ lệch bù(vị trí 0(total))
            total += sent;
            dataleft -= sent;
        }
        return total; 
    }

    // tạo hàm gửi dữ liệu
    private static byte[] ReceiveData(Socket s)
    {
        int total = 0; // tổng dữ liệu nhân
        int recv;
        byte[] datasize = new byte[4]; //mảng chứa dữ liệu nhận
        recv = s.Receive(datasize, 0, 4, 0);
        // Receive(byte[] data, offset, size, Socketflags)
        int size = BitConverter.ToInt32(datasize, 0);
        // convert dữ liệu dạng byte nhận được thành dữ liệu cơ sở, gán vào biến size
        int dataleft = size;
        byte[] data = new byte[size];
        while (total < size)
        {
            recv = s.Receive(data, total, dataleft, 0);
            if (recv == 0) // nếu hhông nhận được dữ liệu thì thoát vòng lặp
            {
                data = Encoding.ASCII.GetBytes("exit");
                break;
            }
            total += recv;
            dataleft -= recv;
        }
        return data;
    }


    public static void Main()
    {
        byte[] data = new byte[1024]; 
        // tạo mảng
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo ipep có giá trị là bất cứ ip nào chấp nhận port 9050
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // tạo socket
        newsock.Bind(ipep);
        // gán ipep vào socket
        newsock.Listen(10);
        // chuyển socket vào trạng thái lắng nghe kết nối (tối đa 10)
        Console.WriteLine("Waiting for a client...");
        Socket client = newsock.Accept();
        // chấp nhận kết nối từ client
        IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
        // liên kết đến ipendpoint của client
        Console.WriteLine("Connect with {0} and port {1}", newclient.Address, newclient.Port);
        // xuát ra màn hình ipendpoint của client
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert welcome thành dạng byte rồi gán vào data
        int sent = SendData(client, data);
        // dùng hàm SendData gửi dữ liệu đến client
        for (int i =0; i <6; i++) // vòng lặp kết thúc khi i = 5
        {
            data = ReceiveData(client); 
            // dùng hàm ReceiveData để nhận dữ liệu từ client
            Console.WriteLine(Encoding.ASCII.GetString(data));
            // convert dữ liệu nhận được sang dạng chuỗi rồi xuất ra màn hình
        }
        Console.WriteLine("Disconnected from {0}", newclient.Address);
        client.Close();
        newsock.Close();
    }
}