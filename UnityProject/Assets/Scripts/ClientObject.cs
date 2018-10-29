using System.Collections.Concurrent;
using System.Threading;
using NetMQ;
using UnityEngine;
using NetMQ.Sockets;

public class NetMqListener
{
    private readonly Thread _listenerWorker;

    private bool _listenerCancelled;

    public delegate void MessageDelegate(string message);

    private readonly MessageDelegate _messageDelegate;

    private readonly ConcurrentQueue<string> _messageQueue = new ConcurrentQueue<string>();

    private void ListenerWork()
    {
        AsyncIO.ForceDotNet.Force();
        using (var client = new RequestSocket())
        {
            client.Connect("tcp://localhost:5555");

            for (int i = 0; i < 10; i++)
            {
                Debug.Log("Sending Hello");
                client.SendFrame("Hello");

                var message = client.ReceiveFrameString();
                Debug.Log($"Received {message}");
            }
        }
//        using (var subSocket = new SubscriberSocket())
//        {
//            subSocket.Options.ReceiveHighWatermark = 1000;
//            subSocket.Connect("tcp://localhost:12345");
//            subSocket.Subscribe("");
//            while (!_listenerCancelled)
//            {
//                string frameString;
//                if (!subSocket.TryReceiveFrameString(out frameString)) continue;
//                Debug.Log(frameString);
//                _messageQueue.Enqueue(frameString);
//            }
//            subSocket.Close();
//        }
        NetMQConfig.Cleanup();
    }

    public void Update()
    {
        while (!_messageQueue.IsEmpty)
        {
            string message;
            if (_messageQueue.TryDequeue(out message))
            {
                _messageDelegate(message);
            }
            else
            {
                break;
            }
        }
    }

    public NetMqListener(MessageDelegate messageDelegate)
    {
        _messageDelegate = messageDelegate;
        _listenerWorker = new Thread(ListenerWork);
    }

    public void Start()
    {
        _listenerCancelled = false;
        _listenerWorker.Start();
    }

    public void Stop()
    {
        _listenerCancelled = true;
        _listenerWorker.Join();
    }
}

public class ClientObject : MonoBehaviour
{
    private NetMqListener _netMqListener;

    private void HandleMessage(string message)
    {
        Debug.Log("Received Message: " + message);
        var splittedStrings = message.Split(' ');
        if (splittedStrings.Length != 3) return;
        var x = float.Parse(splittedStrings[0]);
        var y = float.Parse(splittedStrings[1]);
        var z = float.Parse(splittedStrings[2]);
        transform.position = new Vector3(x, y, z);
    }

    private void Start()
    {
        _netMqListener = new NetMqListener(HandleMessage);
        _netMqListener.Start();
    }

    private void Update()
    {
        _netMqListener.Update();
    }

    private void OnDestroy()
    {
        _netMqListener.Stop();
    }
}
//using System.Collections.Concurrent;
//using System.Threading;
//using NetMQ;
//using UnityEngine;
//using NetMQ.Sockets;
//
//public class NetMqListener
//{
//    private readonly Thread _listenerWorker;
//
//    private bool _listenerCancelled;
//
//    public delegate void MessageDelegate(string message);
//
//    private readonly MessageDelegate _messageDelegate;
//
//    private readonly ConcurrentQueue<string> _messageQueue = new ConcurrentQueue<string>();
//
//    private void ListenerWork()
//    {
//        AsyncIO.ForceDotNet.Force();
//        using (var subSocket = new SubscriberSocket())
//        {
//            subSocket.Options.ReceiveHighWatermark = 1000;
//            subSocket.Connect("tcp://localhost:12345");
//            subSocket.Subscribe("");
//            while (!_listenerCancelled)
//            {
//                string frameString;
//                if (!subSocket.TryReceiveFrameString(out frameString)) continue;
//                Debug.Log(frameString);
//                _messageQueue.Enqueue(frameString);
//            }
//            subSocket.Close();
//        }
//        NetMQConfig.Cleanup();
//    }
//
//    public void Update()
//    {
//        while (!_messageQueue.IsEmpty)
//        {
//            string message;
//            if (_messageQueue.TryDequeue(out message))
//            {
//                _messageDelegate(message);
//            }
//            else
//            {
//                break;
//            }
//        }
//    }
//
//    public NetMqListener(MessageDelegate messageDelegate)
//    {
//        _messageDelegate = messageDelegate;
//        _listenerWorker = new Thread(ListenerWork);
//    }
//
//    public void Start()
//    {
//        _listenerCancelled = false;
//        _listenerWorker.Start();
//    }
//
//    public void Stop()
//    {
//        _listenerCancelled = true;
//        _listenerWorker.Join();
//    }
//}
//
//public class ClientObject : MonoBehaviour
//{
//    private NetMqListener _netMqListener;
//
//    private void HandleMessage(string message)
//    {
//        Debug.Log("Received Message: " + message);
//        var splittedStrings = message.Split(' ');
//        if (splittedStrings.Length != 3) return;
//        var x = float.Parse(splittedStrings[0]);
//        var y = float.Parse(splittedStrings[1]);
//        var z = float.Parse(splittedStrings[2]);
//        transform.position = new Vector3(x, y, z);
//    }
//
//    private void Start()
//    {
//        _netMqListener = new NetMqListener(HandleMessage);
//        _netMqListener.Start();
//    }
//
//    private void Update()
//    {
//        _netMqListener.Update();
//    }
//
//    private void OnDestroy()
//    {
//        _netMqListener.Stop();
//    }
//}
