using System.Runtime.CompilerServices;

namespace SharedLibrary.Techorama._2025;

public class CustomStringAwaiter : ICriticalNotifyCompletion
{
    private const int DelayPerCharacter = 100;

    private static readonly TaskAwaiter Completed = Task.CompletedTask.GetAwaiter();
    private readonly TaskAwaiter _awaiter;

    public CustomStringAwaiter(string? s)
    {
        var delay = (s?.Length ?? 0) * DelayPerCharacter;
        _awaiter = delay > 0
            ? Task.Delay(delay).GetAwaiter()
            : Completed;
    }

    public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);

    public void UnsafeOnCompleted(Action continuation) => _awaiter.UnsafeOnCompleted(continuation);
    public bool IsCompleted => _awaiter.IsCompleted;
    public void GetResult() => _awaiter.GetResult();
}