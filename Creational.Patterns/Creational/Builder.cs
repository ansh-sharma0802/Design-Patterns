namespace Creational
{
    internal class HttpRequest
    {
        public string Url { get; }
        public string? Method { get; }
        public IReadOnlyDictionary<string, string> Headers { get; }
        public IReadOnlyDictionary<string, string> QueryParameters { get; }
        public string? Body { get; }
        public int Timeout { get; }

        private HttpRequest(Builder builder) 
        {
            Url = builder.Url;
            Method = builder.Method;
            Headers = builder.Headers;
            QueryParameters = builder.QueryParameters;
            Body = builder.Body;
            Timeout = builder.Timeout;
        }

        public override string ToString()
        {
            return $"HttpRequest{{Url='{Url}', Method='{Method}', " +
               $"Headers={Headers.Count}, QueryParams={QueryParameters.Count}, " +
               $"Body='{Body}', Timeout={Timeout}}}";
        }

        internal class Builder
        {
            public string Url { get; }
            public string? Method { get; private set; }
            public Dictionary<string, string> Headers { get; } = [];
            public Dictionary<string, string> QueryParameters { get; } = [];
            public string? Body { get; private set; }
            public int Timeout { get; private set; } = 30000;

            public Builder(string url)
            {
                Url = url;
            }

            public Builder SetMethod(string method)
            {
                Method = method;
                return this;
            }

            public Builder AddHeader(string key, string value)
            {
                Headers[key] = value;
                return this;
            }

            public Builder AddQueryParam(string key, string value)
            {
                QueryParameters[key] = value;
                return this;
            }

            public Builder SetBody(string body)
            {
                Body = body;
                return this;
            }

            public Builder SetTimeout(int timeout)
            {
                Timeout = timeout;
                return this;
            }

            public HttpRequest Build()
            {
                return new HttpRequest(this);
            }
        }
    }

    internal static class HttpRequestDirector
    {
        public static HttpRequest BuildSimpleGet(string url)
        {
            return new HttpRequest.Builder(url)
                .SetMethod("GET")
                .SetTimeout(3000)
                .Build();
        }

        public static HttpRequest BuildAuthenticatedPost(string url, string token, string body)
        {
            return new HttpRequest.Builder(url)
                .SetMethod("POST")
                .AddHeader("Authorization", $"Bearer {token}")
                .AddHeader("Content-Type", "application/json")
                .SetBody(body)
                .SetTimeout(10000)
                .Build();
        }

        public static HttpRequest BuildInternalServiceCall(string url)
        {
            return new HttpRequest.Builder(url)
                .SetMethod("GET")
                .AddHeader("X-Internal-Service", "true")
                .AddHeader("X-Trace-Id", Guid.NewGuid().ToString())
                .SetTimeout(5000)
                .Build();
        }
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var get = new HttpRequest.Builder("https://api.example.com/users")
    //            .Build();

    //        var post = new HttpRequest.Builder("https://api.example.com/users")
    //            .SetMethod("POST")
    //            .AddHeader("Content-Type", "application/json")
    //            .SetBody("{\"name\":\"Alice\",\"email\":\"alice@example.com\"}")
    //            .SetTimeout(5000)
    //            .Build();

    //        var put = new HttpRequest.Builder("https://api.example.com/config")
    //        .SetMethod("PUT")
    //        .AddHeader("Authorization", "Bearer token123")
    //        .AddHeader("Content-Type", "application/json")
    //        .AddQueryParam("env", "production")
    //        .AddQueryParam("version", "2")
    //        .SetBody("{\"feature_flag\":true}")
    //        .SetTimeout(10000)
    //        .Build();

    //        var getFromDirector = HttpRequestDirector.BuildSimpleGet("https://api.example.com/users");

    //        var postFromDirector = HttpRequestDirector.BuildAuthenticatedPost("https://api.example.com/orders", "token123", "{\"item\":\"book\"}");

    //        var internalServiceReq = HttpRequestDirector.BuildInternalServiceCall("https://internal.service/health");

    //        Console.WriteLine(get);
    //        Console.WriteLine(post);
    //        Console.WriteLine(put);
    //        Console.WriteLine(getFromDirector);
    //        Console.WriteLine(postFromDirector);
    //        Console.WriteLine(internalServiceReq);
    //    }
    //}
}