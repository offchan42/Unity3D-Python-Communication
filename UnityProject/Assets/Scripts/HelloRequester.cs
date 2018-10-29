using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

/// <summary>
/// Example of requester who only sends Hello. Very nice guy.
/// You can copy this class and modify Run() to suits your needs.
/// </summary>
public class HelloRequester : RunAbleThread
{
    /// <summary>
    /// Request Hello message to server and receive message back. Do it 10 times.
    /// </summary>
    protected override void Run()
    {
        ForceDotNet.Force(); // this line is needed to prevent unity freeze after one use, not sure why yet
        using (var client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            for (int i = 0; i < 10 && Running; i++)
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