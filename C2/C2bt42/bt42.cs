using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class VarTcpClient
{
    //tạo hàm gửi dữ liệu
    private static int SendData(Socket s, byte[] data)
    {
        int total = 0;
        int size = data.Length;
        int dataleft = size;
        int sent;
        byte[] datasize = new byte[4];
        datasize = BitConverter.GetBytes(size);
        sent = s.Send(datasize);
        while (total<size)
        {
            sent = s.Send(data, total, dataleft, SocketFlags.None);
            total += sent;
            dataleft -= sent;
        }
        return total;
    }

    // tạo hàm nhận dữ liệu
    private static byte[] ReceiveData(Socket s)
    {
        int total = 0;
        int recv;
        byte[] datasize = new byte[4];
        recv = s.Receive(datasize, 0, 4, 0);
        int size = BitConverter.ToInt32(datasize, 0);
        int dataleft = size;
        byte[] data = new byte[size];
        while (total < size)
        {
            recv = s.Receive(data, total, dataleft, 0);
            if (recv == 0)
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
        int sent;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            server.Connect(ipep);
        }
        catch (SocketException e)
        {
            Console.WriteLine("Unable to connect to server");
            Console.WriteLine(e.ToString());
            return;
        }
        data = ReceiveData(server);
        string stringData = Encoding.ASCII.GetString(data);
        Console.WriteLine(stringData);
        string mess1 = "This is the first test";
        string mess2 = "A short test";
        string mess3 = "This string is an even longer test. The quick brown for jump" +
            "overthe lazy dog.";
        string mess4 = "a";
        string mess5 = "The last test";
        string mess6 = "one more test";
        sent = SendData(server, Encoding.ASCII.GetBytes(mess1));
        sent = SendData(server, Encoding.ASCII.GetBytes(mess2));
        sent = SendData(server, Encoding.ASCII.GetBytes(mess3));
        sent = SendData(server, Encoding.ASCII.GetBytes(mess4));
        sent = SendData(server, Encoding.ASCII.GetBytes(mess5));
        sent = SendData(server, Encoding.ASCII.GetBytes(mess6));
        Console.WriteLine("Disconnecting from server...");
        server.Shutdown(SocketShutdown.Both);
        server.Close();
    }
}