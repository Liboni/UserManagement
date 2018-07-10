
namespace UserManagement
{
    using System;
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using UserManagement.Enums;
    using UserManagement.LocalObjects;

    public class Http
    {
        public HttpWebResponse Post<T>(HttpRequest<T> request)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(request.Url);
                httpWebRequest.ContentType = request.ContentType;
                httpWebRequest.Method = RequestMethod.Post.GetEnumDescription();
                string body = JsonConvert.SerializeObject(request.Body);
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(body);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                return (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception)
            {
                return new HttpWebResponse();
            }
        }

        public HttpWebResponse Get<T>(HttpRequest<T> request)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(request.Url);
                httpWebRequest.ContentType = request.ContentType;
                httpWebRequest.Method = RequestMethod.Get.GetEnumDescription();
                return (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception)
            {
                return new HttpWebResponse();
            }
        }
    }
}
