using Firebase.Xamarin.Auth;
using Xamarin.Forms;
using XamarinFirebaseHA.Helper;
using XamarinFirebaseHA.Views;

namespace XamarinFirebaseHA
{
    public partial class App : Application
    {

        const string CONFIG_KEY = "AIzaSyBLzjmsffN54_Tbp8WzUKFF2GEvI4ug_YY";
        public static FirebaseAuthProvider firebaseAuthProvier = new FirebaseAuthProvider(new FirebaseConfig(CONFIG_KEY));


        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new StudentListPage());
            if (!string.IsNullOrEmpty(UserLocalData.userToken))
            {
                MainPage = new NavigationPage(new StudentListPage());
            }
            else
            {
                MainPage = new LoginPage();

            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
