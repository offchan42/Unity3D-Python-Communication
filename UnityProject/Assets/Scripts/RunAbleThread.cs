using System.Threading;

/// <summary>
/// The superclass that you should derive from. It provides Start() and Stop() method and Running property.
/// It will start the thread to run Run() when you call Start().
/// </summary>
public abstract class RunAbleThread
{
    private readonly Thread _requestThread;
    protected bool Running { get; private set; }

    protected RunAbleThread()
    {
        // we need to create a thread instead of calling Run() directly because it would block unity
        // from doing other tasks like drawing game scenes
        _requestThread = new Thread(Run);
    }

    protected abstract void Run();

    public void Start()
    {
        Running = true;
        _requestThread.Start();
    }

    public void Stop()
    {
        Running = false;
        // block main thread, wait for _requestThread to finish its job first, so we can be sure that 
        // _requestThread will end before main thread end
        _requestThread.Join();
    }
}