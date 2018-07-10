
namespace UserManagement.LocalObjects
{
    using System.Runtime.Serialization;

    [DataContract]
    public class HttpRequest<T>
    {
        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string ContentType { get; set; }

        [DataMember]
        public T Body { get; set; }
    }
}
