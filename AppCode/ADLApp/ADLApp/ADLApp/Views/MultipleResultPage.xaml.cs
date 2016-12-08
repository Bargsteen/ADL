﻿using ADLApp.Models;
using ADLApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ADLApp.Views
{
    public partial class MultipleResultPage : ContentPage
    {
        public MultipleResultPage(MultipleResultViewModel mrvm)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(new Thickness(20, 20, 20, 0),
						   new Thickness(10, 00, 10, 00),
						   new Thickness(0));
            BindingContext = mrvm;
        }
        private void OnItemSelected(object sender, EventArgs e)
        {
            answerOptionView.SelectedItem = null;
        }
        private void OnChecked(object sender, bool isChecked)
        {
            (sender as CheckBox).IsChecked = !isChecked;
        }
        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
