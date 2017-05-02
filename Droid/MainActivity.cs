
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Microsoft.WindowsAzure.MobileServices;
using XamarinAllianceApp.Interfaces;
using System.Threading.Tasks;

namespace XamarinAllianceApp.Droid
{
    [Activity (Label = "Xamarin Alliance",Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate
    {
        // Define an authenticated user.
        private MobileServiceUser user;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init (this, bundle);

            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);

            // Load the main application
            LoadApplication (new App ());
		}
        

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                user =  await XamarinAllianceApp.App.MobileService.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook); //https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-get-started-users

                //MobileServiceClient client = new MobileServiceClient("https://xamarinalliancesecurebackend.azurewebsites.net/");
                //user = await client.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook); 

                if (user != null)
                {
                    message = string.Format("you are now signed-in as {0}.", user.UserId);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }
    }
}

