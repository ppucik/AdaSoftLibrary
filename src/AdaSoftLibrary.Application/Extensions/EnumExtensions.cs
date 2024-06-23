using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdaSoftLibrary.Application.Extensions;

public static class EnumExtensions
{
    public class EnumEntryInfo
    {
        public int Value { get; set; }

        public string DisplayName { get; set; } = string.Empty;

        public int DisplayOrder { get; set; } = 9999; // enums without attribute will be last
    }

    public static string GetDisplayName(this Enum @enum)
    {
        string name = @enum.ToString();
        var fieldInfo = @enum.GetType().GetField(name);

        if (fieldInfo == null)
            return name;

        var displayAttribute = fieldInfo
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault();

        if (displayAttribute == null)
            return name;

        name = displayAttribute.GetName()!;

        return name;
    }

    public static string GetShortName(this Enum @enum)
    {
        string name = @enum.ToString();

        var fieldInfo = @enum.GetType().GetField(name);

        if (fieldInfo == null)
            return name;

        var displayAttribute = fieldInfo
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault();

        if (displayAttribute == null)
            return name;

        name = displayAttribute.GetShortName()!;

        return name;
    }

    public static T? GetPropertyValue<T>(this Enum @enum, string propName)
    {
        if (@enum == null)
            throw new ArgumentNullException(nameof(@enum));

        string name = @enum.ToString();
        var fieldInfo = @enum.GetType().GetField(name);

        if (fieldInfo == null)
            throw new ArgumentException(nameof(@enum));

        var propertyValueAttribute = fieldInfo
            .GetCustomAttributes(typeof(PropertyValueAttribute), false)
            .OfType<PropertyValueAttribute>()
            .FirstOrDefault(x => x.Name.Equals(propName, StringComparison.InvariantCultureIgnoreCase));

        if (propertyValueAttribute == null)
            return default;

        return (T)propertyValueAttribute.Value;
    }

    public static EnumEntryInfo ToEntryInfo(this Enum @enum)
    {
        string name = @enum.ToString();

        var info = new EnumEntryInfo
        {
            Value = Convert.ToInt32(@enum),
            DisplayName = name
        };

        var fieldInfo = @enum.GetType().GetField(name);

        if (fieldInfo == null)
            return info;

        var displayAttribute = fieldInfo
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .OfType<DisplayAttribute>()
            .FirstOrDefault();

        if (displayAttribute == null)
            return info;

        info.DisplayName = displayAttribute.GetName()!;
        info.DisplayOrder = displayAttribute.GetOrder() ?? info.Value;

        return info;
    }

    public static IEnumerable<EnumEntryInfo> GetEntryInfos<TEnum>()
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        var enumType = typeof(TEnum);

        if (!enumType.IsEnum)
            throw new ArgumentException($"'{enumType}' is not an enum.");

        return Enum.GetValues(enumType)
            .OfType<Enum>()
            .Select(e => e.ToEntryInfo())
            .OrderBy(e => e.DisplayOrder)
            .ToArray();
    }

    public static TEnum GetValue<TEnum>(string enumName) where TEnum : struct
    {
        TEnum val;

        if (Enum.TryParse(enumName, true, out val))
            return val;

        return default;
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    private class PropertyValueAttribute : Attribute
    {
        public string Name { get; }
        public object Value { get; }

        public PropertyValueAttribute(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }

    public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue) where TEnum : struct
    {
        try
        {
            TEnum enumValue;

            if (!Enum.TryParse(value, true, out enumValue))
            {
                return defaultValue;
            }

            return enumValue;
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    public static TEnum? ToEnum<TEnum>(this string? value, TEnum? defaultValue = null) where TEnum : struct
    {
        return !string.IsNullOrEmpty(value) ? value.ToEnum<TEnum>(defaultValue) : null;
    }

    public static string? GetEnumDescription(this Enum en)
    {
        if (en == null) return null;

        var type = en.GetType();

        var memberInfo = type.GetMember(en.ToString());
        var description = (memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute),
            false).FirstOrDefault() as DescriptionAttribute)?.Description;

        return description;
    }
}
