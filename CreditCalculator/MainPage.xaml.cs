using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CreditCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToCalculator(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Text) || !checkBox.IsChecked)
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля и выберите 'Запомнить меня'.", "OK");
            }
            else
            {
                if (Login.Text == "ects" && Password.Text == "ects2024")
                    await Navigation.PushModalAsync(new TabPage());
                else
                    await DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
            }
        }
    }
}
