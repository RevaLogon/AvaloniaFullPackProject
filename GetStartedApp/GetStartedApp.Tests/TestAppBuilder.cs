using Avalonia;
using Avalonia.Headless;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<GetStartedApp.App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
