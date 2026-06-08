using System.ComponentModel;

namespace ClassCall.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(object value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0] is DescriptionAttribute attr ? attr.Description : value.ToString();
            }
            return value.ToString();
        }
    }
}
