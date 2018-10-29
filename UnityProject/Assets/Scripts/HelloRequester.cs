using System.Threading;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

/// <summary>
/// Example of requester who only sends Hello. Very nice guy.
/// You can copy this class and modify RequestHello() to suits your needs.
/// </summary>
public class HelloRequester
{
    private readonly Thread _requestHelloThread;
    private bool _running;

    public HelloRequester()
    {
        // we need to create a thread instead of calling RequestHello() directly because it would block unity
        // from doing other tasks like drawing game scenes
        _requestHelloThread = new Thread(RequestHello);
    }

    public void Start()
    {
        Debug.Log("Starting thread...");
        _running = true;
        _requestHelloThread.Start();
    }

    public void Stop()
    {
        Debug.Log("Stopping thread...");
        _running = false;
        // block main thread, wait for _requestHelloThread to finish its job first, so we can be sure that 
        // NetMQConfig.Cleanup() will be called before Unity object gets destroyed
        _requestHelloThread.Join();
    }

    /// <summary>
    /// Send Hello message to server and receive message back. Do it 10 times.
    /// </summary>
    private void RequestHello()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (var client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            for (int i = 0; i < 10 && _running; i++)
            {
                Debug.Log("Sending Hello");
                client.SendFrame("Hello");

                var message = client.ReceiveFrameString();
                Debug.Log($"Received {message}");
            }
        }

        NetMQConfig.Cleanup(); // this line is needed to prevent unity freeze after one use, not sure why yet
    }
}