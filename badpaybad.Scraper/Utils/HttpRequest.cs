using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace badpaybad.Scraper.Utils
{
    public enum HttpRequestError
    {
        None,
        TimeOut,
        Cookies,
        Other
    }

    public class HttpRequestException
    {
        public HttpRequestError Error { get; set; }
        public Exception HttpException { get; set; }
        public string Message { get; set; }
    }
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public sealed class HttpRequest : IDisposable
    {
        private string _currentUrl;
        private string _previousUrl;
        string _host;
        string _userAgent;
        string _accept;
        string _acceptLanguage;
        string _acceptEncoding;
        string _acceptCharset;
        string _connection;
        string _referer;
        string _cookies;
        private string _redirectUrl;
        private string _contentType;
        private bool _keepAlive;
        private IWebProxy _proxy;

        private HttpWebRequest _httpRequest;
        // CookieCollection _cookieCollection;
        //  RequestState _requestState =new RequestState();

        public HttpWebRequest CurrentRequest { get { return _httpRequest; } }

        public event Action<HttpRequestException> Error;

        public CookieCollection CookieCollection
        {
            //get { return _cookieCollection; }
            //set
            //{
            //    _cookieCollection = value;

            //}
            get;
            set;
        }

        public string UrlRedirect { get { return _redirectUrl; } }

        public HttpRequest(string url, string host = "", string userAgent = ""
                           , string accept = "", string acceptLanguage = ""
                           , string acceptEncoding = "", string acceptCharset = ""
                           , string connection = "", string referer = "",
                           string cookies = "", string contentType = "", bool keepAlive = true)
        {
            var uri = new Uri(url);
            CookieCollection = new CookieCollection();


            _currentUrl = url;
            _host = host;
            _userAgent = userAgent;
            _accept = accept;
            _acceptCharset = acceptCharset;
            _acceptLanguage = acceptLanguage;
            _acceptEncoding = acceptEncoding;
            _connection = connection;
            _referer = referer;

            _keepAlive = keepAlive;

            _httpRequest = (HttpWebRequest)WebRequest.Create(_currentUrl);

            if (string.IsNullOrEmpty(userAgent))
            {
                _userAgent = "Mozilla/5.0 (Windows NT 6.1; rv:6.0.2) Gecko/20100101 Firefox/6.0.2";
            }
            if (string.IsNullOrEmpty(accept))
            {
                _accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                //	text/html, */*; q=0.01
            }

            if (string.IsNullOrEmpty(host))
            {
                _host = uri.Host;
            }

            if (string.IsNullOrEmpty(contentType))
            {
                _contentType = "text/html";// "application/x-www-form-urlencoded";
            }

            //_httpRequest.Host = _host;
            _httpRequest.UserAgent = _userAgent;
            _httpRequest.Accept = accept;
            //_httpRequest.Connection = _connection;
            _httpRequest.Referer = _referer;
            _httpRequest.ContentType = _contentType;
            _httpRequest.KeepAlive = _keepAlive;
            _httpRequest.Timeout = 120000;

            if (!string.IsNullOrEmpty(cookies))
            {
                var arr = cookies.Split(';');
                foreach (var s in arr)
                {
                    var indexOf = s.IndexOf("=");
                    if (indexOf > 0)
                    {
                        var cName = s.Substring(0, indexOf).Trim();
                        var cValue = s.Substring(indexOf + 1).Trim();

                        CookieCollection.Add(new Cookie(cName, cValue, "/", _host));
                    }
                }
            }
            var tempCc = new CookieContainer();
            tempCc.Add(CookieCollection);
            _httpRequest.CookieContainer = tempCc;
        }

        #region build HttpRequest

        public HttpRequest CreateNew(string url)
        {
            _previousUrl = _currentUrl;
            _currentUrl = url;
            _httpRequest = (HttpWebRequest)WebRequest.Create(url);
            _httpRequest.Timeout = 120000;

            ////_httpRequest.Host = _host;
            //_httpRequest.UserAgent = _userAgent;
            //_httpRequest.Accept = _accept;
            ////_httpRequest.Connection = _connection;
            //_httpRequest.Referer = _referer;
            //_httpRequest.ContentType = _contentType;
            _httpRequest.KeepAlive = true;
            var tempCc = new CookieContainer();
            lock (CookieCollection)
            {
                if (CookieCollection != null)
                {
                    tempCc.Add(CookieCollection);
                }
            }

            lock (_httpRequest)
            {

                _httpRequest.CookieContainer = tempCc;
            }
            if (_proxy != null)
            {
                lock (_httpRequest)
                {
                    _httpRequest.Proxy = _proxy;
                }
            }

            return this;
        }

        public HttpRequest WithCredentials(ICredentials credentials)
        {
            lock (_httpRequest)
            {
                _httpRequest.Credentials = credentials;
            }
            return this;
        }

        public HttpRequest WithTimeout(int timeout)
        {
            lock (_httpRequest)
            {
                _httpRequest.Timeout = timeout;
            }
            return this;
        }

        public HttpRequest WithReferUrl(string urlRefer)
        {
            lock (_httpRequest)
            {
                _referer = urlRefer;
                _httpRequest.Referer = _referer;
            }
            return this;
        }

        public HttpRequest WithCookies(CookieCollection cookieCollection)
        {
            try
            {
                lock (CookieCollection)
                {
                    this.CookieCollection = cookieCollection;
                }
            }
            catch (Exception ex)
            {

            }
            try
            {
                lock (_httpRequest)
                {
                    var tempCc = new CookieContainer();

                    tempCc.Add(CookieCollection);
                    _httpRequest.CookieContainer = tempCc;
                }
            }
            catch (Exception ex)
            {

            }

            return this;
        }


        public HttpRequest WithCookies(string cookies)
        {
            try
            {
                lock (CookieCollection)
                {
                    if (!string.IsNullOrEmpty(cookies))
                    {
                        var arr = cookies.Split(';');
                        foreach (var s in arr)
                        {
                            var indexOf = s.IndexOf("=");
                            if (indexOf > 0)
                            {
                                var cName = s.Substring(0, indexOf).Trim();
                                var cValue = s.Substring(indexOf + 1).Trim();

                                var cookie = new Cookie(cName, cValue, "/", _host);

                                CookieCollection.Add(cookie);
                                // CookieCollection.Add(new Cookie(cName, cValue));
                            }
                        }
                    }
                }
                lock (_httpRequest)
                {

                    var tempCc = new CookieContainer();
                    tempCc.Add(CookieCollection);
                    _httpRequest.CookieContainer = tempCc;
                }
            }
            catch { }
            return this;
        }


        public HttpRequest WithKeepAlive(bool keepAlive)
        {
            lock (_httpRequest)
            {
                _keepAlive = keepAlive;
                _httpRequest.KeepAlive = _keepAlive;
            }
            return this;
        }

        public HttpRequest WithUserAgent(string agent)
        {
            lock (_httpRequest)
            {
                _userAgent = agent;
                _httpRequest.UserAgent = _userAgent;
            }
            return this;
        }

        public HttpRequest WithContentType(string contentType)
        {
            lock (_httpRequest)
            {
                _contentType = contentType;
                _httpRequest.ContentType = _contentType;
            }
            return this;
        }

        public HttpRequest WithAcceptEncoding(string acceptEncoding)
        {
            lock (_httpRequest)
            {
                _httpRequest.Headers["Accept-Encoding"] = acceptEncoding;
                _acceptEncoding = acceptEncoding;
            }
            return this;
        }

        public HttpRequest WithAccept(string accept)
        {
            lock (_httpRequest)
            {
                _accept = accept;
                _httpRequest.Accept = _accept;
            }
            return this;
        }

        public HttpRequest WithXRequestedWith(string xRequested)
        {
            lock (_httpRequest)
            {
                if (string.IsNullOrEmpty(xRequested))
                {
                    xRequested = "XMLHttpRequest";
                }
                _httpRequest.Headers["X-Requested-With"] = xRequested;
            }
            return this;
        }

        public HttpRequest WithAcceptLanguage(string lang)
        {
            lock (_httpRequest)
            {
                _httpRequest.Headers["Accept-Language"] = lang;
            }
            return this;
        }

        public HttpRequest WithProxy(string address, int port, string username, string password)
        {
            if (string.IsNullOrEmpty(address)) return this;

            _proxy = new WebProxy(address.Trim(), port);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                _proxy.Credentials = new NetworkCredential(username.Trim(), password.Trim());
            }
            lock (_httpRequest)
            {
                _httpRequest.Proxy = _proxy;
            }
            return this;
        }

        public HttpRequest WithMethod(string method)
        {
            lock (_httpRequest)
            {
                _httpRequest.Method = method.ToUpper();
            }
            return this;
        }

        public HttpRequest WithData(string data)
        {
            lock (_httpRequest)
            {
                var postBuffer = Encoding.UTF8.GetBytes(data);
                using (var newStream = _httpRequest.GetRequestStream())
                {
                    newStream.Write(postBuffer, 0, postBuffer.Length);
                    newStream.Close();
                }
            }
            return this;
        }

        #endregion

        #region for download

        public string DownloadHtml()
        {
            var result = "";
            try
            {
                using (var response = (HttpWebResponse)_httpRequest.GetResponse())
                {
                    response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
                    lock (CookieCollection)
                    {
                        this.CookieCollection.Add(response.Cookies);

                    }
                    _redirectUrl = GetRedirectUrl(response, ref _cookies);

                    using (var sr = new StreamReader(response.GetResponseStream() ?? System.IO.Stream.Null, Encoding.UTF8))
                    {
                        if (NetworkInterface.GetIsNetworkAvailable())
                            result = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                result = RefineHtml(result);
                //_httpRequest.Abort();
            }
            catch (Exception we)
            {
            }
            return result;
        }

        public Stream DownloadData(out HttpWebResponse webResponse)
        {
            webResponse = null;
            try
            {
                var response = (HttpWebResponse)_httpRequest.GetResponse();

                _redirectUrl = GetRedirectUrl(response, ref _cookies);

                response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);

                lock (CookieCollection)
                {
                    this.CookieCollection.Add(response.Cookies);
                }
                webResponse = response;
                return NetworkInterface.GetIsNetworkAvailable() ? response.GetResponseStream() : Stream.Null;
            }
            catch
            {
                return Stream.Null;
            }
        }

        public Stream DownloadData()
        {
            try
            {
                var response = (HttpWebResponse)_httpRequest.GetResponse();

                _redirectUrl = GetRedirectUrl(response, ref _cookies);

                response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
                lock (CookieCollection)
                {
                    this.CookieCollection.Add(response.Cookies);
                }
                return NetworkInterface.GetIsNetworkAvailable() ? response.GetResponseStream() : Stream.Null;
            }
            catch
            {
                //_httpRequest.Abort();
                return Stream.Null;
            }
        }


        #endregion

        #region action result of request
        public string HtmlByGet()
        {
            var error = HttpRequestError.None;
            var result = "";
            //lock (_httpRequest)
            //{
            _httpRequest.Method = "GET";
            // }
            try
            {
                using (var response = (HttpWebResponse)_httpRequest.GetResponse())
                {
                    lock (_httpRequest)
                    {
                        response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
                    }
                    lock (CookieCollection)
                    {
                        this.CookieCollection.Add(response.Cookies);

                    }
                    _redirectUrl = GetRedirectUrl(response, ref _cookies);
                    using (var resStream = response.GetResponseStream())
                    {
                        using (var sr = new StreamReader(resStream ?? System.IO.Stream.Null, Encoding.UTF8))
                        {
                            if (NetworkInterface.GetIsNetworkAvailable())
                                result = sr.ReadToEnd();
                            sr.Close();
                        }
                    }

                    response.Close();
                }
                result = RefineHtml(result);
                //_httpRequest.Abort();

            }
            catch (Exception ex)
            {
                error = HttpRequestError.TimeOut;
                var msg = "Can not make request to " + _httpRequest.RequestUri.ToString();
                if (Error != null)
                {
                    Error(new HttpRequestException() { Error = HttpRequestError.TimeOut, HttpException = ex, Message = msg });
                }
            }
            return result;
        }


        public string HtmlByPostJson(string data, string contentType = "application/json")
        {
            var htmlData = "";
            var error = HttpRequestError.None;
            try
            {
                // data = HttpUtility.UrlEncode(data);
                var postBuffer = Encoding.UTF8.GetBytes(data);
                // lock (_httpRequest)
                //  {
                _httpRequest.Method = "POST";
                _contentType = contentType; //"application/x-www-form-urlencoded";
                _httpRequest.ContentType = _contentType;

                _httpRequest.ContentLength = postBuffer.Length;



                // }
                using (var newStream = _httpRequest.GetRequestStream())
                {
                    using (var sw = new StreamWriter(newStream))
                    {
                        sw.Write(data);
                    }
                    newStream.Flush();
                }



                using (var response = (HttpWebResponse)_httpRequest.GetResponse())
                {
                    lock (_httpRequest)
                    { response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri); }
                    lock (CookieCollection) { this.CookieCollection.Add(response.Cookies); }

                    using (var stream = response.GetResponseStream())
                    {
                        _redirectUrl = GetRedirectUrl(response, ref _cookies);

                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8"), true))
                        {
                            if (NetworkInterface.GetIsNetworkAvailable())
                                htmlData = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                    response.Close();
                }
                htmlData = RefineHtml(htmlData);
                //_httpRequest.Abort();
            }
            catch (Exception ex)
            {
                error = HttpRequestError.TimeOut;
                var msg = "Can not make request to " + _httpRequest.RequestUri.ToString();
                if (Error != null)
                {
                    Error(new HttpRequestException() { Error = HttpRequestError.TimeOut, HttpException = ex, Message = msg });
                }
            }
            return htmlData;
        }


        public string HtmlByPost(string data, string contentType = "application/x-www-form-urlencoded")
        {
            var htmlData = "";
            var error = HttpRequestError.None;
            try
            {
                // data = HttpUtility.UrlEncode(data);
                var postBuffer = Encoding.UTF8.GetBytes(data);
                // lock (_httpRequest)
                //  {
                _httpRequest.Method = "POST";
                _contentType = contentType; //"application/x-www-form-urlencoded";
                _httpRequest.ContentType = _contentType;

                _httpRequest.ContentLength = postBuffer.Length;



                // }
                using (var newStream = _httpRequest.GetRequestStream())
                {
                    newStream.Write(postBuffer, 0, postBuffer.Length);
                    newStream.Close();
                }



                using (var response = (HttpWebResponse)_httpRequest.GetResponse())
                {
                    lock (_httpRequest)
                    { response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri); }
                    lock (CookieCollection) { this.CookieCollection.Add(response.Cookies); }

                    using (var stream = response.GetResponseStream())
                    {
                        _redirectUrl = GetRedirectUrl(response, ref _cookies);

                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8"), true))
                        {
                            if (NetworkInterface.GetIsNetworkAvailable())
                                htmlData = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                    response.Close();
                }
                htmlData = RefineHtml(htmlData);
                //_httpRequest.Abort();
            }
            catch (Exception ex)
            {
                error = HttpRequestError.TimeOut;
                var msg = "Can not make request to " + _httpRequest.RequestUri.ToString();
                if (Error != null)
                {
                    Error(new HttpRequestException() { Error = HttpRequestError.TimeOut, HttpException = ex, Message = msg });
                }
            }
            return htmlData;
        }

        public Stream DataByGet()
        {
            try
            {
                //  lock (_httpRequest)
                //  {
                _httpRequest.Method = "GET";
                //   }
                var response = (HttpWebResponse)_httpRequest.GetResponse();

                _redirectUrl = GetRedirectUrl(response, ref _cookies);

                response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
                lock (CookieCollection)
                {

                    this.CookieCollection.Add(response.Cookies);
                }
                return NetworkInterface.GetIsNetworkAvailable() ? response.GetResponseStream() : Stream.Null;
            }
            catch
            {
                //_httpRequest.Abort();
                return Stream.Null;
            }
        }

        public Stream DataByPost(string data)
        {
            try
            {
                var postBuffer = Encoding.UTF8.GetBytes(data);
                //  lock (_httpRequest)
                // {
                _httpRequest.Method = "POST";
                _contentType = "application/x-www-form-urlencoded";
                _httpRequest.ContentType = _contentType;
                _httpRequest.ContentLength = postBuffer.Length;
                //  }
                using (var newStream = _httpRequest.GetRequestStream())
                {
                    newStream.Write(postBuffer, 0, postBuffer.Length);
                    newStream.Close();
                }

                var response = (HttpWebResponse)_httpRequest.GetResponse();

                response.Cookies = _httpRequest.CookieContainer.GetCookies(_httpRequest.RequestUri);
                lock (CookieCollection)
                {
                    this.CookieCollection.Add(response.Cookies);
                }

                _redirectUrl = GetRedirectUrl(response, ref _cookies);
                var stream = Stream.Null;
                if (NetworkInterface.GetIsNetworkAvailable())
                    stream = response.GetResponseStream();
                return stream;
            }
            catch
            {
                //_httpRequest.Abort();
                return Stream.Null;
            }
        }

        #endregion

        string GetRedirectUrl(HttpWebResponse response, ref string cookies)
        {

            var uri = "";

            uri = response.Headers["Location"];

            if (string.IsNullOrEmpty(uri))
            {
                response.GetResponseHeader("Location");
            }
            if (string.IsNullOrEmpty(uri))
            {
                uri = response.ResponseUri.ToString();
            }

            if (response.Headers["Set-Cookie"] != null)
            {
                cookies = response.Headers["Set-Cookie"];
            }

            return uri.Trim();
        }

        string RefineHtml(string source)
        {
            if (string.IsNullOrEmpty(source)) return "";

            source = Regex.Replace(source, "\t", " ");
            source = Regex.Replace(source, "\r", " ");
            source = Regex.Replace(source, "\n", " ");
            // source = Regex.Replace(source, "\r\n", " ");
            source = Regex.Replace(source, @"\s{2,}", " ");
            return source;
        }

        public void TryAbort()
        {
            try { _httpRequest.Abort(); }
            catch { }
        }


        public bool TryParseResponseHeader(out HttpWebResponse webResponse, out string contentType)
        {
            contentType = "";
            webResponse = null;
            try
            {
                _httpRequest.Method = "HEAD";
                var response = (HttpWebResponse)_httpRequest.GetResponse();
                webResponse = response;
                contentType = response.ContentType;
            }
            catch
            {
                return false;
            }
            return true;
        }


        public void Dispose()
        {

        }
    }
}