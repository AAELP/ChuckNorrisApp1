﻿namespace ChuckNorrisApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFavoriteCharacterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterPage());
        }
    }
}
