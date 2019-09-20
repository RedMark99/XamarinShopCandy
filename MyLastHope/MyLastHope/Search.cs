using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyLastHope
{
    public delegate void SearchEventHandler(string text);

    public class Search : ContentView
    {
        public event SearchEventHandler Test;


        public Search()
        {

            Button searchBtn = new Button { Text = "Поиск" };
            Entry searchEntry = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, TextColor = Color.White };

            var trigger = new Trigger(typeof(Entry))
            {
                Property = Entry.IsFocusedProperty,
                Value = true
            };

            trigger.Setters.Add(new Setter
            {
                Property = Entry.TextColorProperty,
                Value = Color.FromHex("894343")
            });
            searchEntry.Triggers.Add(trigger);

            searchBtn.Clicked += (sender, e) => Test?.Invoke(searchEntry.Text);
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5,
                Children =
                {
                    searchEntry,
                    searchBtn
                }
            };
        }
    }
}