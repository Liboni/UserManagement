
namespace UserManagement
{
    using System;
    using System.Reflection;

    public static class FieldExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            try
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                string description = value.ToString();
                FieldInfo fieldInfo = value.GetType().GetField(description);
                EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    description = attributes[0].Description;
                return description;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
