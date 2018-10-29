using UnityEngine;

public class ClientObject : MonoBehaviour
{
    private HelloRequester _helloRequester;

    private void Start()
    {
        _helloRequester = new HelloRequester();
        _helloRequester.Start();
    }

    private void OnDestroy()
    {
        _helloRequester.Stop();
    }
}