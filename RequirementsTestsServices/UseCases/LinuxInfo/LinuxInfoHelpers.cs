using RequirementsTestsDomain.Models;

namespace RequirementsTestsServices.UseCases.LinuxInfo;

public static class LinuxInfoHelpers
{
    public static Info<Dictionary<TKey,TValue>> GenerateInfo<TKey,TValue>(Dictionary<TKey,TValue> dictionary, string categoryName, string name, string description = "For future needs")
    {
        var info = new Info<Dictionary<TKey, TValue>>
        {
            Name = name,
            Category = new InfoCategory{Name = categoryName},
            Value = dictionary,
            Description = description,
        };
        return info;
    }

    public static Info<T> GenerateInfo<T>(T value, string categoryName, string name, string description = "For future needs")
    {
        var info = new Info<T>
        {
            Name = name,
            Category = new InfoCategory{Name = categoryName},
            Value = value,
            Description = description,
        };
        return info;
    }
    
    public static async Task<string> ReadToTheEndAsync(string path)
    {
        using var sReader = new StreamReader(path);
        return await sReader.ReadToEndAsync();
    }
}