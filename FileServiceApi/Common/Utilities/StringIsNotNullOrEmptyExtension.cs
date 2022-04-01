namespace FileService.Common.Utilities;

public static class StringIsNotNullOrEmptyExtension
{
    public static bool StringIsNotNullOrEmpty(this string value)
    {
        return !string.IsNullOrEmpty(value);
    }
}
