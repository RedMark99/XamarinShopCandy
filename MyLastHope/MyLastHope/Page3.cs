using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using System.Collections.ObjectModel;

namespace MyLastHope
{
    public class Page3 : ContentPage
    {
        ListView listView;
        List<Test> tempBasket;

        public ObservableCollection<Grouping<string, Test>> TestGroups { get; set; }

        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Type { get; private set; }
            public Grouping(K type, IEnumerable<T> items)
            { 
                Type = type;
                foreach (T item in items)
                    Items.Add(item);
            }
        }


        public Page3()
        {
            Title = "Корзина";
            IconImageSource = "Basket.png";
            //BackgroundColor = Color.FromHex("1E1E1E");

            using (var reader = new StringReader("^contentpage { background-color: RGB(30,30,30); }"))
            {
                this.Resources.Add(StyleSheet.FromReader(reader));
            }




            tempBasket = new List<Test>() { };
            tempBasket = Save.list;

            


            listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = Save.list,

                ItemTemplate = new DataTemplate(() =>// Определяем формат отображения данных    
                {

                    ImageCell imageCell = new ImageCell { TextColor = Color.Red, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Name");
                    Binding companyBinding = new Binding { Path = "Price", StringFormat = "Цена: {0}" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "ImagePath");
                    return imageCell;

                })
            };


            listView.ItemTapped += OnListViewItemTapped;

            StackLayout stackLayout = new StackLayout()
            {
                Children = { listView }

            };
            this.Content = stackLayout;

        }

        async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Test tappedItem = e.Item as Test;
            
            bool result = await DisplayAlert("Подтвердить действие", "Вы точно хотите удалить из корзины: " + tappedItem.Name.ToString(), "Да", "Нет");


            if (result == true)
            {
                await DisplayAlert("Уведомление", "Вы удалили из корзины: " + tappedItem.Name.ToString(), "OK");
                tempBasket.RemoveAt(0);
            }
            else
            {
                await DisplayAlert("Уведомление", "Вы отменили удаление из корзину: " + tappedItem.Name.ToString(), "OK");
                
            }

            updateApp();


        }

        async void updateApp()
        {
            tempBasket = Save.list;

            var groups = tempBasket.GroupBy(p => p.Type).Select(g => new Grouping<string, Test>(g.Key, g));
            TestGroups = new ObservableCollection<Grouping<string, Test>>(groups);
            this.BindingContext = this;


            listView.SeparatorColor = Color.Black;

            listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = TestGroups,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("Type"),

                ItemTemplate = new DataTemplate(() =>// Определяем формат отображения данных    
                {

                    ImageCell imageCell = new ImageCell { TextColor = Color.White, DetailColor = Color.Green };
                    imageCell.SetBinding(ImageCell.TextProperty, "Name");
                    Binding companyBinding = new Binding { Path = "Price", StringFormat = "Цена: {0} руб" };
                    imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "ImagePath");
                    return imageCell;

                })
            };
            
            listView.ItemTapped += OnListViewItemTapped;

            StackLayout stackLayout = new StackLayout()
            {
                Children = { listView }

            };
            this.Content = stackLayout;

        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            updateApp();

        }


    }
}