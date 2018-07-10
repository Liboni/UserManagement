
namespace UserManagement.Models.ValuesModels
{
    using System.Runtime.Serialization;

    [DataContract]
    public class CountryModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
