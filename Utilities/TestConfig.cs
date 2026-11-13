namespace SoftwareTestingExample.Utilities;

public static class TestConfig
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("BASE_URL")
        ?? "http://127.0.0.1:4173/";
}
