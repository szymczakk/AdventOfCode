namespace Common;

public class TestBase
{
    public Task<string[]> LoadFile(string path = "./input.txt")
    {
        return Load(path);
    }

    public Task<string[]> LoadTestFile(string testPath = "./test-input.txt")
    {
        return Load(testPath);
    }

    private static async Task<string[]> Load(string path)
    {
        return await File.ReadAllLinesAsync(path);
    }
}