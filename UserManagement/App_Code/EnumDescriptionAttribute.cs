
namespace UserManagement
{
    using System;

    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        public EnumDescriptionAttribute(string description)
        {
            Description = description;
        }

        public string Description { set; get; }
    }
}
