namespace Helpers;

public static class FileHelpers
{
    public static IEnumerable<string> GetFileContent(string fileName)
    {
        var file = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        return File.ReadAllLines(file);
    }
}