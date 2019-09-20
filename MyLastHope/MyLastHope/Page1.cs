using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using SQLite;
using System.IO;
using System.Linq;
using Xamarin.Forms.StyleSheets;

namespace MyLastHope
{

    public class Page1 : ContentPage
    {
        //Глобальные переменые
        List<Test> basket;
        List<Test> temp;
        List<Test> marmelad;
        List<Test> cance;
        ListView listView;
        Label basketCount = new Label()
        {
            Text = "В корзине:",
            FontSize = 20,
            TextColor = Color.White
        };


        Picker picker = new Picker()
        {
            TextColor = Color.White,
            Title = "Категории",
            TitleColor = Color.White

        };

        public Page1()
        {

            //Параметры страницы
            Title = "Товар";
            IconImageSource = "List.png";
            using (var reader = new StringReader("^contentpage { background-color: RGB(30,30,30); }"))
            {
                this.Resources.Add(StyleSheet.FromReader(reader));
            }

            //Инициализация
            listView = new ListView();
            basket = new List<Test> { };
            temp = new List<Test> { };
            cance = new List<Test> { };
            marmelad = new List<Test> { };
            basketCount.Text = "В корзине: " + Save.list.Count.ToString(); ;
            picker.Items.Add("Леденцы");
            picker.Items.Add("Мармелад");
            picker.Items.Add("Все");


            //ListView подключение к базе данных
            string documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MyCans.db3");
            SQLiteConnection db = new SQLiteConnection(documentPath);
            var contact = db.Table<Test>().ToList();

            List<Test> myList = new List<Test>();

            foreach (Test el in contact)
            {
                myList.Add(el);
            }

            listView.HasUnevenRows = true;
            listView.ItemsSource = myList;
            listView.ItemTemplate = new DataTemplate(() =>
            {
                ImageCell imageCell = new ImageCell { TextColor = Color.White, DetailColor = Color.Green };
                imageCell.SetBinding(ImageCell.TextProperty, "Name");
                Label authorLabel = new Label();
                authorLabel.SetBinding(Label.TextProperty, "Type");

                Binding companyBinding = new Binding { Path = "Price", StringFormat = "Цена: {0} руб" };
                imageCell.SetBinding(ImageCell.DetailProperty, companyBinding);
                imageCell.SetBinding(ImageCell.ImageSourceProperty, "ImagePath");
                return imageCell;
            });

            //+=
            listView.ItemTapped += OnListViewItemTapped;
            picker.SelectedIndexChanged += MethodName;
            picker.SelectedIndexChanged += picker_SelectedIndexChanged;
            listView.SeparatorColor = Color.Black;

            Search searchView = new Search();
            
            searchView.Test += (text) =>
            {

                if (!string.IsNullOrEmpty(text))
                {
                    listView.ItemsSource = myList.Where(u => u.Name.Contains(text));
                }
                else
                {
                    listView.ItemsSource = myList;
                }
            };

            //Подключение элементов интерфейса
            StackLayout stackLayout = new StackLayout()
            {
                Children = {picker, searchView,listView, basketCount}

            };
            this.Content = stackLayout;



            //Методы
            

            async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
            {
                Test tappedItem = e.Item as Test;

                bool result = await DisplayAlert("Подтвердить действие", "Вы точно хотите добавить в корзину: " + tappedItem.Name.ToString(), "Да", "Нет");
                

                if (result == true)
                {
                    Save.list.Add(tappedItem);
                    await DisplayAlert("Уведомление", "Вы добавили в корзину: " + tappedItem.Name.ToString(), "OK");
                    basketCount.Text = "В корзине: " + Save.list.Count.ToString();
                }
                else
                {
                    await DisplayAlert("Уведомление", "Вы отменили добавление в корзину: " + tappedItem.Name.ToString(), "OK");
                    basketCount.Text = "В корзине: " + Save.list.Count.ToString();
                }

                

             }

            void MethodName(object sender, EventArgs args)
            {

                marmelad.Clear();
                cance.Clear();


                for (int i = 0; i < myList.Count; i++)
                {
                    if (myList[i].Type == "Мармелад")
                    {
                        marmelad.Add(myList[i]);
                    }
                    else if (myList[i].Type == "Леденец")
                    {
                        cance.Add(myList[i]);
                    }
                }



            }


            void picker_SelectedIndexChanged(object sender, EventArgs e)
            {

                if (picker.SelectedIndex == 0)
                {
                    listView.ItemsSource = cance;
                    searchView = new Search();

                    searchView.Test += (text) =>
                    {

                        if (!string.IsNullOrEmpty(text))
                        {
                            listView.ItemsSource = cance.Where(u => u.Name.Contains(text));
                        }
                        else
                        {
                            listView.ItemsSource = cance;
                        }

                    };

                    stackLayout = new StackLayout()
                    {
                        Children = { picker, searchView, listView, basketCount }

                    };
                    this.Content = stackLayout;
                }
                else if (picker.SelectedIndex == 1)
                {
                    listView.ItemsSource = marmelad;
                    searchView = new Search();

                    searchView.Test += (text) =>
                    {

                        if (!string.IsNullOrEmpty(text))
                        {
                            listView.ItemsSource = marmelad.Where(u => u.Name.Contains(text));
                        }
                        else
                        {
                            listView.ItemsSource = marmelad;
                        }

                    };

                    stackLayout = new StackLayout()
                    {
                        Children = { picker, searchView, listView, basketCount }

                    };
                    this.Content = stackLayout;
                }
                else if (picker.SelectedIndex == 2)
                {
                    listView.ItemsSource = myList;
                    searchView = new Search();

                    searchView.Test += (text) =>
                    {

                        if (!string.IsNullOrEmpty(text))
                        {
                            listView.ItemsSource = myList.Where(u => u.Name.Contains(text));
                        }
                        else
                        {
                            listView.ItemsSource = myList;
                        }

                    };

                    stackLayout = new StackLayout()
                    {
                        Children = { picker, searchView, listView, basketCount }

                    };
                    this.Content = stackLayout;
                }


            }



        }
        protected override async void OnAppearing()
        {

            base.OnAppearing();

            basketCount.Text = "В корзине: " + Save.list.Count.ToString();

        }
    }

}