using Foundation;
using UIKit;

namespace FlashCards;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
	{
		if (DeviceInfo.Platform == DevicePlatform.iOS)
		{
			Firebase.Core.App.Configure();
		}

		return base.FinishedLaunching(application, launchOptions);
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
