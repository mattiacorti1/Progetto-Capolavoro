using System;
using System.Net;
using System.Net.Http;

public static class HttpClientHelper
{
    public static HttpClient CreateHttpClient()
    {
        var handler = new HttpClientHandler
        {
            // Usa il proxy di sistema (funziona anche con PAC/WPAD)
            Proxy = HttpClient.DefaultProxy,
            UseProxy = true,

            // Usa credenziali Windows se richieste dal proxy scolastico
            PreAuthenticate = true,
            UseDefaultCredentials = true
        };

        return new HttpClient(handler);
    }
}
