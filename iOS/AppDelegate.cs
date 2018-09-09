
using Foundation;
using UIKit;

namespace XamarinFirebaseHA.iOS
{
    [Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
            UIRefreshControl.Appearance.TintColor = UIColor.Black;

			LoadApplication(new App());
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            return base.FinishedLaunching(app, options);
		}
	}
}
