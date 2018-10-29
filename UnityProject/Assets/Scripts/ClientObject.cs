using System.Threading;
using AsyncIO;
using NetMQ;
using NetMQ.Sockets;
using UnityEngine;

public class NetMqListener
{
    private readonly Thread _listenerWorker;
    private bool _running;

    public NetMqListener()
    {
        _listenerWorker = new Thread(ListenerWork);
    }

    private void ListenerWork()
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

    public void Start()
    {
        _running = true;
        _listenerWorker.Start();
    }

    public void Stop()
    {
        _running = false;
        // block main thread, wait for _listenerWorker to finish its job first, so we can be sure that 
        // NetMQConfig.Cleanup() will be called before Unity object gets destroyed
        _listenerWorker.Join();
    }
}

public class ClientObject : MonoBehaviour
{
    private NetMqListener _netMqListener;

    private void Start()
    {
        _netMqListener = new NetMqListener();
        _netMqListener.Start();
    }

    private void OnDestroy()
    {
        _netMqListener.Stop();
    }
}