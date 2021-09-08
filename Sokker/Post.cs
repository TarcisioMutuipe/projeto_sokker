using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Sokker
{
    public class Post
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class WebClientEx : WebClient
    {
        private CookieContainer _cookieContainer = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = _cookieContainer;
            }
            return request;
        }
    }

    public class Config
    {
        public Service service { get; set; }
        public Message message { get; set; }
        public Data data { get; set; }
    }

    public class Service
    {
        public string name { get; set; }
    }

    public class Message
    {
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Data
    {
        public TipoData tipo { get; set; }
    }

    public class TipoData
    {
        public int codigo { get; set; }
        public string name { get; set; }
        public List<MetaData> metadata { get; set; }
    }

    public class MetaData
    {
        public string name { get; set; }
        public bool required { get; set; }
        public string type { get; set; }
        public int max_length { get; set; }
    }
}
