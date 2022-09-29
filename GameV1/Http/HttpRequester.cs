using Newtonsoft.Json;
using System.Text;

namespace GameV1.Http;

public class HttpRequester
{
    private HttpClient _client;

    public HttpRequester(string baseAddress)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(baseAddress)
        };
    }

    public void Post(string action, object body)
    {
        var httpRequestMessage = CreateRequestMessage(action, body);

        _client.Send(httpRequestMessage);
    }

    public T Post<T>(string action, object body)
    {
        var httpRequestMessage = CreateRequestMessage(action, body);

        var httpResponseMessage = _client.Send(httpRequestMessage);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            return default!;
        }

        var str = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var t = JsonConvert.DeserializeObject<T>(str);
        if (t == null)
        {
            return default!;
        }

        return t!;
    }

    private static HttpRequestMessage CreateRequestMessage(string action, object body)
    {
        return new HttpRequestMessage(HttpMethod.Post, action)
        {
            Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")
        };
    }
}
