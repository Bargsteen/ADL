#region Libraries

using System;
using Xamarin.Forms;

#endregion

namespace ADLApp.Views
{
    public partial class CheckBox
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                "Text",
                typeof(string),
                typeof(CheckBox),
                null,
                propertyChanged:
                (bindable, oldValue, newValue) =>
                {
                    ((CheckBox) bindable).textLabel.Text = (string) newValue;
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
                    CheckBox checkbox = (CheckBox) bindable;
                    checkbox.boxLabel.Text = (bool) newValue ? "\u2611" : "\u2610";

                    // Fire the event.
                    checkbox.CheckedChanged?.Invoke(checkbox, (bool) newValue);
                });

        public CheckBox()
        {
            InitializeComponent();
        }

        public string Text
        {
            set { SetValue(TextProperty, value); }
            get { return (string) GetValue(TextProperty); }
        }

        public bool IsChecked
        {
            set { SetValue(IsCheckedProperty, value); }
            get { return (bool) GetValue(IsCheckedProperty); }
        }

        public event EventHandler<bool> CheckedChanged;

        // TapGestureRecognizer handler.
        private void OnCheckBoxTapped(object sender, EventArgs args)
        {
            IsChecked = !IsChecked;
        }
    }
}