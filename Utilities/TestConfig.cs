namespace SoftwareTestingExample.Utilities;

public static class TestConfig
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("BASE_URL")
        ?? "https://practicesoftwaretesting.com/";
}