using System.Runtime.CompilerServices;

namespace SharedLibrary.Techorama._2025;

public class CustomStringAwaiter(string? s) : ICriticalNotifyCompletion
{
    private readonly TaskAwaiter _awaiter = Task.Delay(TimeSpan.FromMilliseconds(s?.Length ?? 0) * 100).GetAwaiter();

    public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);

    public void UnsafeOnCompleted(Action continuation) => _awaiter.UnsafeOnCompleted(continuation);
    public bool IsCompleted => _awaiter.IsCompleted;
    public void GetResult() => _awaiter.GetResult();
}