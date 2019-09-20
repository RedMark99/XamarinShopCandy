using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using SQLite;
using System.IO;
using Xamarin.Forms.StyleSheets;

namespace MyLastHope
{

    public class Page2 : ContentPage
    {

        //Глобальные переменые
        int sum = 0;
        int costgift = 0;
        Label ename = new Label()
        {
            Text = "Имя",
            TextColor = Color.White,
            FontSize = 18
        };
        Label eadress = new Label()
        {
            Text = "Адрес",
            TextColor = Color.White,
            FontSize = 18
        };
        Label ephone = new Label()
        {
            Text = "Номер телефона",
            TextColor = Color.White,
            FontSize = 18
        };



        Entry name = new Entry()
        {
            TextColor = Color.White,
            Placeholder = "Введите имя",
            PlaceholderColor = Color.Gray,
        };
        Entry adress = new Entry()
        {
            TextColor = Color.White,
            Placeholder = "Введите адресс",
            PlaceholderColor = Color.Gray,
        };
        Entry phone = new Entry()
        {
            TextColor = Color.White,
            Placeholder = "Введите номер телефона",
            PlaceholderColor = Color.Gray,
            MaxLength = 11
            
        };

        Label egift = new Label()
        {
            Text = "Подарочная упаковка",
            TextColor = Color.White,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,

        };

        Switch gift = new Switch()
        {
            OnColor = Color.White,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start

        };



        Label cost = new Label()
        {
            Text = "Цена: ",
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            TextColor = Color.White,
            FontSize = 18
        };

        Button accept = new Button()
        {
            Text = "Заказать",
            TextColor = Color.White,
            FontSize = 18,
            BackgroundColor = Color.FromHex("111111")
        };



        public Page2()
        {

            //Параметры страницы
            Title = "Заказать";
            IconImageSource = "Accept.png";
            using (var reader = new StringReader("^contentpage { background-color: RGB(30,30,30); }"))
            {
                this.Resources.Add(StyleSheet.FromReader(reader));
            }

            for (int i = 0; i < Save.list.Count; i++)
            {
                sum += Save.list[i].Price;
            }

            cost.Text = "Итого:" + sum.ToString();
            phone.Keyboard = Keyboard.Telephone;
            gift.Toggled += MethodToggle;
            accept.Clicked += ButtonAccept;

            StackLayout stackLayout = new StackLayout()
            {
                Children = { ename, name, eadress, adress,ephone,phone, egift, gift, cost, accept }

            };
            this.Content = stackLayout;

        }

        void ButtonAccept(object sender, EventArgs e)
        {
            if (Save.list.Count != 0 && name.Text != null && adress.Text != null && phone.Text != null)
            {
                DisplayAlert("Уведомление", "Вы оформил заказ на адресс:" + adress.Text, "ок");
                Save.list.Clear();
            }
            else
            {
                if (Save.list.Count == 0)
                    DisplayAlert("Уведомление", "Ваша корзина пуста", "ок");
                else if (name == null)
                    DisplayAlert("Уведомление", "Вы не ввели имя", "ок");
                else if (adress == null)
                    DisplayAlert("Уведомление", "Вы не ввели адресс", "ок");
                else if (phone == null)
                    DisplayAlert("Уведомление", "Вы не ввели номер телефона", "ок");


            }
        }

        async void MethodToggle(object sender, ToggledEventArgs e)
        {

            if (gift.IsToggled == true)
            {
                costgift = 350;
                sum = 0;

                for (int i = 0; i < Save.list.Count; i++)
                {
                    sum += Save.list[i].Price;
                }

                sum = sum + costgift;

                cost.Text = "Итого:" + sum.ToString();

            }
            else if (gift.IsToggled == false)
            {
                costgift = 0;
                sum = 0;

                for (int i = 0; i < Save.list.Count; i++)
                {
                    sum += Save.list[i].Price;
                }

                sum = sum + costgift;

                cost.Text = "Итого:" + sum.ToString();
            }

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            if (gift.IsToggled == true)
            {
                costgift = 350;
                sum = 0;

                for (int i = 0; i < Save.list.Count; i++)
                {
                    sum += Save.list[i].Price;
                }

                sum = sum + costgift;

                cost.Text = "Итого:" + sum.ToString();

            }
            else if (gift.IsToggled == false)
            {
                costgift = 0;
                sum = 0;

                for (int i = 0; i < Save.list.Count; i++)
                {
                    sum += Save.list[i].Price;
                }

                sum = sum + costgift;

                cost.Text = "Итого:" + sum.ToString();

            }


        }
    }
}