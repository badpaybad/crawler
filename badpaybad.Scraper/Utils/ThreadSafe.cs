using System;
using System.Threading;

namespace badpaybad.Scraper.Utils
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public sealed class ThreadSafe:IDisposable
    {
        private bool _isStop;
        private bool _isStarted;
        private Thread _thread;
        private Action _action;
        public event Action<ThreadSafe> ThreadStarted;
        public event Action<ThreadSafe> ThreadStoped;
        public event Action<ThreadSafe, Exception, Action> ThreadError;
        public ThreadSafe(Action action)
        {
            _action = action;
            _thread = new Thread(() => Loop());
        }

        public void Start()
        {
            if (!_isStarted)
            {
                _isStarted = true;
                _thread.Start();
                if (ThreadStarted != null)
                    ThreadStarted(this);
            }
        }

        void Loop()
        {
            while (!_isStop)
            {
                try
                {
                    _action();
                }
                catch (Exception ex)
                {
                    LogError.Write(ex, _action.Method.Module.Assembly.FullName);
                    if (ThreadError != null)
                        ThreadError(this, ex, _action);
                }
                finally
                {
                    _isStop = true;
                }
            }
            if (ThreadStoped != null)
                ThreadStoped(this);
        }

        public void Stop()
        {
            _isStop = true;
            _isStarted = false;
        }

        public void Dispose()
        {
            
        }

        public static void Sleep(int miliSeconds)
        {
            Thread.Sleep(miliSeconds);
        }
    }


    //public class HttpRequest : IDisposable
    //{
    //    private string _currentUrl;
    //    private string _previousUrl;
    //    string _host;
    //    string _userAgent;
    //    string _accept;
    //    string _acceptLanguage;
    //    string _acceptEncoding;
    //    string _acceptCharset;
    //    string _connection;
    //    string _referer;
    //    string _cookies;
    //    private string _redirectUrl;
    //    private string _contentType;
    //    private bool _keepAlive;

    //    private HttpWebRequest _httpRequest;
    //    CookieCollection _cookieCollection;

    //    public CookieCollection CookieCollection
    //    {
    //        get { return _cookieCollection; }
    //        //set
    //        //{
    //        //    _cookieCollection = value;

    //        //}
    //    }

    //    public string Cookies { get { return _cookies; } }

    //    public string UrlRedirect { get { return _redirectUrl; } }

    //    public HttpRequest(string Link, string host = "", string userAgent = ""
    //                       , string accept = "", string acceptLanguage = ""
    //                       , string acceptEncoding = "", string acceptCharset = ""
    //                       , string connection = "", string referer = "",
    //                       string cookies = "", string contentType = "", bool keepAlive = true)
    //    {
    //        var uri = new Uri(Link);
    //        _cookieCollection = new CookieCollection();


    //        _currentUrl = Link;
    //        _host = host;
    //        _userAgent = userAgent;
    //        _accept = accept;
    //        _acceptCharset = acceptCharset;
    //        _acceptLanguage = acceptLanguage;
    //        _acceptEncoding = acceptEncoding;
    //        _connection = connection;
    //        _referer = referer;

    //        _keepAlive = keepAlive;

    //        _httpRequest = (HttpWebRequest)WebRequest.Create(_currentUrl);

    //        if (string.IsNullOrEmpty(userAgent))
    //        {
    //            _userAgent = "Mozilla/5.0 (Windows NT 6.1; rv:6.0.2) Gecko/20100101 Firefox/6.0.2";
    //        }
    //        if (string.IsNullOrEmpty(accept))
    //        {
    //            _accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
    //            //	text/html, */*; q=0.01
    //        }

    //        if (string.IsNullOrEmpty(host))
    //        {
    //            _host = uri.Host;
    //        }

    //        if (string.IsNullOrEmpty(contentType))
    //        {
    //            _contentType = "text/html";// "application/x-www-form-urlencoded";
    //        }

    //        //_httpRequest.Host = _host;
    //        _httpRequest.UserAgent = _userAgent;
    //        _httpRequest.Accept = accept;
    //        //_httpRequest.Connection = _connection;
    //        _httpRequest.Referer = _referer;
    //        _httpRequest.ContentType = _contentType;
    //        _httpRequest.KeepAlive = _keepAlive;

    //        if (!string.IsNullOrEmpty(cookies))
    //        {
    //            var arr = cookies.Split(';');
    //            foreach (var s in arr)
    //            {
    //                var indexOf = s.IndexOf("=");
    //                if (indexOf > 0)
    //                {
    //                    var cName = s.Substring(0, indexOf).Trim();
    //                    var cValue = s.Substring(indexOf + 1).Trim();

    //                    _cookieCollection.Add(new Cookie(cName, cValue, "/", _host));
    //                }
    //            }
    //        }
    //        var tempCc = new CookieContainer();
    //        tempCc.Add(_cookieCollection);
    //        _httpRequest.CookieContainer = tempCc;
    //    }

    //    #region build HttpRequest

    //    public HttpRequest CreateNew(string Link)
    //    {
    //        _previousUrl = _currentUrl;
    //        _currentUrl = Link;
    //        var tempCc = new CookieContainer();
    //        if (_cookieCollection != null)
    //        {
    //            tempCc.Add(_cookieCollection);
    //        }

    //        _httpRequest = (HttpWebRequest)WebRequest.Create(Link);

    //        ////_httpRequest.Host = _host;
    //        //_httpRequest.UserAgent = _userAgent;
    //        //_httpRequest.Accept = _accept;
    //        ////_httpRequest.Connection = _connection;
    //        //_httpRequest.Referer = _referer;
    //        //_httpRequest.ContentType = _contentType;
    //        //_httpRequest.KeepAlive = _keepAlive;
    //        //lock (_httpRequest)
    //        //{

    //        _httpRequest.CookieContainer = tempCc;
    //        //}

    //        return this;
    //    }

    //    public HttpRequest WithReferUrl(string urlRefer)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        _referer = urlRefer;
    //        _httpRequest.Referer = _referer;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithCookies(CookieCollection cookieCollection)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        this._cookieCollection = cookieCollection;

    //        var tempCc = new CookieContainer();
    //        tempCc.Add(_cookieCollection);
    //        _httpRequest.CookieContainer = tempCc;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithKeepAlive(bool keepAlive)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        _keepAlive = keepAlive;
    //        _httpRequest.KeepAlive = _keepAlive;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithUserAgent(string agent)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        _userAgent = agent;
    //        _httpRequest.UserAgent = _userAgent;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithContentType(string contentType)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        _contentType = contentType;
    //        _httpRequest.ContentType = _contentType;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithAcceptEncoding(string acceptEncoding)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        _httpRequest.Headers["Accept-Encoding"] = acceptEncoding;
    //        _acceptEncoding = acceptEncoding;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithAccept(string accept)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        _accept = accept;
    //        _httpRequest.Accept = _accept;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithXRequestedWith(string xRequested)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        if (string.IsNullOrEmpty(xRequested))
    //        {
    //            xRequested = "XMLHttpRequest";
    //        }
    //        _httpRequest.Headers["X-Requested-With"] = xRequested;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithXMicrosoftAjax(string xMicrosoftAjax)
    //    {
    //        if (string.IsNullOrEmpty(xMicrosoftAjax))
    //        {
    //            xMicrosoftAjax = "Delta=true";
    //        }
    //        _httpRequest.Headers["X-MicrosoftAjax"] = xMicrosoftAjax;
    //        return this;
    //    }

    //    public HttpRequest WithCacheControl(string cacheControl)
    //    {
    //        if (string.IsNullOrEmpty(cacheControl))
    //        {
    //            cacheControl = "no-cache";
    //        }
    //        _httpRequest.Headers["Cache-Control"] = cacheControl;
    //        return this;
    //    }



    //    public HttpRequest WithAcceptLanguage(string lang)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        _httpRequest.Headers["Accept-Language"] = lang;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithCookies(string cookies)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        if (!string.IsNullOrEmpty(cookies))
    //        {
    //            var arr = cookies.Split(';');
    //            foreach (var s in arr)
    //            {
    //                var indexOf = s.IndexOf("=");
    //                if (indexOf > 0)
    //                {
    //                    var cName = s.Substring(0, indexOf).Trim();
    //                    var cValue = s.Substring(indexOf + 1).Trim();

    //                    _cookieCollection.Add(new Cookie(cName, cValue, "/", _host));
    //                }
    //            }
    //        }
    //        var tempCc = new CookieContainer();
    //        tempCc.Add(_cookieCollection);
    //        _httpRequest.CookieContainer = tempCc;
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithMethod(string method)
    //    {
    //        //lock (_httpRequest)
    //        // {
    //        _httpRequest.Method = method.ToUpper();
    //        // }
    //        return this;
    //    }

    //    public HttpRequest WithData(string data)
    //    {
    //        // lock (_httpRequest)
    //        // {
    //        var postBuffer = Encoding.UTF8.GetBytes(data);
    //        using (var newStream = _httpRequest.GetRequestStream())
    //        {
    //            newStream.Write(postBuffer, 0, postBuffer.Length);
    //            newStream.Close();
    //        }
    //        // }
    //        return this;
    //    }

    //    public string DownloadHtml()
    //    {
    //        var result = "";
    //        using (var response = (HttpWebResponse)_httpRequest.GetResponse())
    //        {
    //            response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
    //            lock (CookieCollection) this.CookieCollection.Add(response.Cookies);
    //            _redirectUrl = GetRedirectUrl(response, out _cookies);

    //            using (var sr = new StreamReader(response.GetResponseStream() ?? System.IO.Stream.Null, Encoding.UTF8))
    //            {
    //                result = sr.ReadToEnd();
    //                sr.Close();
    //            }
    //        }
    //        result = RefineHtml(result);

    //        return result;
    //    }

    //    public Stream DownloadData()
    //    {
    //        var response = (HttpWebResponse)_httpRequest.GetResponse();

    //        _redirectUrl = GetRedirectUrl(response, out _cookies);

    //        response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
    //        lock (CookieCollection) this.CookieCollection.Add(response.Cookies);
    //        return response.GetResponseStream();
    //    }

    //    #endregion

    //    #region action result of request
    //    public string HtmlByGet()
    //    {
    //        var result = "";
    //        //lock (_httpRequest)
    //        //{
    //        _httpRequest.Method = "GET";
    //        // }
    //        //try
    //        //{
    //        using (var response = (HttpWebResponse)_httpRequest.GetResponse())
    //        {
    //            //   lock (_httpRequest)
    //            //  {
    //            response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
    //            //   }
    //            lock (CookieCollection) this.CookieCollection.Add(response.Cookies);

    //            _redirectUrl = GetRedirectUrl(response, out _cookies);

    //            using (
    //                var sr = new StreamReader(response.GetResponseStream() ?? System.IO.Stream.Null, Encoding.UTF8))
    //            {
    //                result = sr.ReadToEnd();
    //                sr.Close();
    //            }
    //            response.Close();
    //        }
    //        result = RefineHtml(result);
    //        //}
    //        //catch
    //        //{
    //        //    throw new Exception("Can not make request to " + _httpRequest.RequestUri.ToString());
    //        //}
    //        return result;
    //    }

    //    public string HtmlByPost(string data)
    //    {
    //        // data = HttpUtility.UrlEncode(data);
    //        var postBuffer = Encoding.UTF8.GetBytes(data);
    //        // lock (_httpRequest)
    //        //  {
    //        _httpRequest.Method = "POST";
    //        _contentType = "application/x-www-form-urlencoded; charset=utf-8";
    //        _httpRequest.ContentType = _contentType;

    //        _httpRequest.ContentLength = postBuffer.Length;
    //        // }
    //        using (var newStream = _httpRequest.GetRequestStream())
    //        {
    //            newStream.Write(postBuffer, 0, postBuffer.Length);
    //            newStream.Close();
    //        }

    //        var htmlData = "";
    //        //try
    //        //{
    //        var webResponse = _httpRequest.GetResponse();

    //        using (var response = (HttpWebResponse)webResponse)
    //        {
    //            //  lock (_httpRequest)
    //            response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);

    //            lock (CookieCollection) this.CookieCollection.Add(response.Cookies);

    //            _redirectUrl = GetRedirectUrl(response, out _cookies);

    //            var stream = response.GetResponseStream();


    //            using (var reader = new StreamReader(stream,
    //                                                 Encoding.GetEncoding("UTF-8"), true))
    //            {
    //                htmlData = reader.ReadToEnd();
    //                reader.Close();
    //            }
    //            response.Close();
    //        }
    //        htmlData = RefineHtml(htmlData);
    //        //}
    //        //catch
    //        //{
    //        //    throw new Exception("Can not make request to " + _httpRequest.RequestUri.ToString());
    //        //}
    //        return htmlData;
    //    }

    //    public Stream DataByGet()
    //    {
    //        //  lock (_httpRequest)
    //        //  {
    //        _httpRequest.Method = "GET";
    //        //   }
    //        var response = (HttpWebResponse)_httpRequest.GetResponse();

    //        _redirectUrl = GetRedirectUrl(response, out _cookies);

    //        response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);

    //        lock (CookieCollection) this.CookieCollection.Add(response.Cookies);

    //        return response.GetResponseStream();
    //    }

    //    public Stream DataByPost(string data)
    //    {
    //        var postBuffer = Encoding.UTF8.GetBytes(data);
    //        //  lock (_httpRequest)
    //        // {
    //        _httpRequest.Method = "POST";
    //        _contentType = "application/x-www-form-urlencoded";
    //        _httpRequest.ContentType = _contentType;
    //        _httpRequest.ContentLength = postBuffer.Length;
    //        //  }
    //        using (var newStream = _httpRequest.GetRequestStream())
    //        {
    //            newStream.Write(postBuffer, 0, postBuffer.Length);
    //            newStream.Close();
    //        }

    //        var response = (HttpWebResponse)_httpRequest.GetResponse();

    //        response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);

    //        lock (CookieCollection) this.CookieCollection.Add(response.Cookies);

    //        _redirectUrl = GetRedirectUrl(response, out _cookies);

    //        var stream = response.GetResponseStream();

    //        return stream;
    //    }

    //    #endregion

    //    string GetRedirectUrl(HttpWebResponse response, out string cookies)
    //    {
    //        cookies = "";
    //        var uri = "";

    //        if (response.Headers["Set-Cookie"] != null)
    //        {
    //            cookies = response.Headers["Set-Cookie"];
    //        }
    //        if ((response.StatusCode == HttpStatusCode.Found) ||
    //                           (response.StatusCode == HttpStatusCode.Redirect) ||
    //                           (response.StatusCode == HttpStatusCode.Moved) ||
    //                           (response.StatusCode == HttpStatusCode.MovedPermanently))
    //        {
    //            uri = response.Headers["Location"];
    //            uri = uri.Trim();
    //        }
    //        return uri;
    //    }

    //    string RefineHtml(string source)
    //    {
    //        if (string.IsNullOrEmpty(source)) return "";

    //        source = Regex.Replace(source, "\t", " ");
    //        source = Regex.Replace(source, "\r", " ");
    //        source = Regex.Replace(source, "\n", " ");
    //        source = Regex.Replace(source, "\r\n", " ");
    //        source = Regex.Replace(source, @"\s{2,}", " ");
    //        return source;
    //    }

    //    public void Dispose()
    //    {

    //    }
    //}



}
