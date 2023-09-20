using System.Net;
using System.Net.Sockets;

IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8080);
Socket socket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(localEndPoint);
socket.Listen(5);
        
while (true)
{
    Socket handler = socket.Accept();
            
    while (true)
    {
        var buffer = new byte[1024];
        int bytesReceived = handler.Receive(buffer);
                
        if (bytesReceived > 0)
        {
            ArraySegment<byte> arraySlice = new ArraySegment<byte>(buffer, 0, bytesReceived);
            handler.Send(arraySlice);
        }
        else
        {
            break;
        }
    }

    handler.Shutdown(SocketShutdown.Send);
    handler.Close();
}