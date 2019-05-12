using System;
using System.Threading;
using Xamarin.Forms;

public class StoppableTimer
{
    private readonly TimeSpan timespan;
    private readonly Action callback;

    private CancellationTokenSource cancellation;

    public StoppableTimer(TimeSpan timespan, Action callback)
    {
        this.timespan = timespan;
        this.callback = callback;
        this.cancellation = new CancellationTokenSource();
    }

    public void Start()
    {
        CancellationTokenSource cts = this.cancellation;
        Device.StartTimer(this.timespan,
            () => {
                if (cts.IsCancellationRequested) return false;
                callback.Invoke();
                return true; 
            });
    }

    public void Stop()
    {
        Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
    }

    public void Dispose()
    {

    }
}