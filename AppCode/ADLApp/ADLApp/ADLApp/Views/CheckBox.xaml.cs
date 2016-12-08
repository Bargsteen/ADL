using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class CheckBox : ContentView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(CheckBox),
                null,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CheckBox)bindable).textLabel.Text = (string)newValue;
                });

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(
                "IsChecked",
                typeof(bool),
                typeof(CheckBox),
                false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    // Set the graphic.
                    CheckBox checkbox = (CheckBox)bindable;
                    checkbox.boxLabel.Text = (bool)newValue ? "\u2611" : "\u2610";

                    // Fire the event.
                    checkbox.CheckedChanged?.Invoke(checkbox, (bool)newValue);
                });

        public event EventHandler<bool> CheckedChanged;
        public CheckBox()
        {
            InitializeComponent();
			Padding = Device.OnPlatform(new Thickness(20, 20, 20, 0),
						   new Thickness(10, 00, 10, 00),
						   new Thickness(0));
        }

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string)GetValue(TextProperty); }
        }

        public bool IsChecked
        {
            set { SetValue(IsCheckedProperty, value); }
            get { return (bool)GetValue(IsCheckedProperty); }
        }

        // TapGestureRecognizer handler.
        void OnCheckBoxTapped(object sender, EventArgs args)
        {
            IsChecked = !IsChecked;
        }
    }
}
