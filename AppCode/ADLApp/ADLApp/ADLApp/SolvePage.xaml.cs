using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADLApp.Model;
using Xamarin.Forms;

namespace ADLApp
{
    public partial class SolvePage : ContentPage
    {
        public SolvePage(Item a)
        {
            currentAssignment = a;
            InitializeComponent();
        }
        public Item currentAssignment { get; set; }
    }
}
