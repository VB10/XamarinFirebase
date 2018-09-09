
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

namespace XamarinFirebaseHA.Droid
{
    [Activity(Label = "SplashActivity",MainLauncher =true,Theme ="@style/Theme.Splash")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            start();
        }

        async void start(){
            await Task.Delay(300);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
