namespace ClassLibrary1.HttpHandlers;

/// <summary>
/// A demo <see cref="DelegatingHandler"/> that adds the time of sending to the request headers.
/// </summary>
public class TimeOfSendingHeaderHandler(string headerName = "TimeOfSending") : DelegatingHandler
{
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AddHeader(request);

        return base.Send(request, cancellationToken);
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        AddHeader(request);
        return base.SendAsync(request, cancellationToken);
    }

    private void AddHeader(HttpRequestMessage request)
        => request.Headers.Add(headerName, DateTime.Now.ToString("O"));
}