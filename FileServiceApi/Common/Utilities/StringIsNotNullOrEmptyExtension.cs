namespace FileService.Common.Utilities;

public static class StringIsNotNullOrEmptyExtension
{
    public static bool IsNotNullOrEmpty(this string value)
    {
        return !string.IsNullOrEmpty(value);
    }
}
