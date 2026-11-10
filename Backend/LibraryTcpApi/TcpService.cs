using System.Net;
using System.Net.Sockets;

namespace LibraryTcpApi;

public class TcpService
{
    private TcpListener? Server;
    TcpClient? Client;
    NetworkStream? _Stream;
    readonly IPAddress Address;
    readonly int Port;
    public event Func<string,string> ResquestReceived;
    public TcpService(IPAddress localaddr, int port, Func<string, string> actionRequestReceived)
    {
        Address = localaddr;
        Port = port;
        ResquestReceived = actionRequestReceived;
    }

    async Task StartAndWaitClientConnectionAsync(CancellationToken token)
    {
        Stop();

        Server = new(Address, Port);
        Server?.Start();
        Console.WriteLine("Server started. Waiting for client connection...");

        Client = Server != null ? await Server.AcceptTcpClientAsync(token) : null;
        Console.WriteLine("Client connected.");
    }

    private void Stop()
    {
        _Stream?.Dispose();
        Client?.Dispose();
        Server?.Dispose();
    }

    async Task StartReadingCommandsAsync(CancellationToken token)
    {
        try
        {

            _Stream = Client?.GetStream()?? throw new NullReferenceException(nameof(_Stream));
            
            byte[] buffer = new byte[1024];
            int bytesRead;
            Console.WriteLine($"Command's reader is working");

            while ((bytesRead = await _Stream.ReadAsync(buffer, token)) != 0)
            {
                string command = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received command: {command}");
                // Process the command here
                var response = ResquestReceived.Invoke(command);
                await _Stream.WriteAsync(System.Text.Encoding.UTF8.GetBytes(response).AsMemory(0, response.Length), token);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }
}
