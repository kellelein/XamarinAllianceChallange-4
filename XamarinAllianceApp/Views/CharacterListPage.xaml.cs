using System;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using XamarinAllianceApp.Helpers;
using XamarinAllianceApp.Models;
using XamarinAllianceApp.Services;
using XamarinAllianceApp.ViewModels;

namespace XamarinAllianceApp.Views
{
    public partial class CharacterListPage : ContentPage
    {
        private CharacterListViewModel viewModel;
        private bool authenticated;

        public CharacterListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CharacterListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Character;
            if (item == null)
                return;

            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(item)));

            // Manually deselect item
            characterList.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Login();
        }

        private async void Login()
        {

            //var client = new MobileServiceClient(Constants.MobileServiceClientUrl);
            //var user = await client.LoginAsync(MobileServiceAuthenticationProvider.Facebook);

            if (App.Authenticator != null)
            {
                authenticated = await App.Authenticator.Authenticate();
            }

            // Set syncItems to true to synchronize the data on startup when offline is enabled.
            if (authenticated)
            {
                //await RefreshItems(true, syncItems: false);

                if (viewModel.Items.Count == 0)
                {
                    viewModel.LoadItemsCommand.Execute(null);
                }
            }

        }
    }
}

