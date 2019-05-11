using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManometerPage : ContentPage
    {
        public ManometerPage()
        {
            InitializeComponent();
            OptionsList.ItemsSource = new List<object>
            {
                new {Name = "Vacuum a:0.0"},
                new {Name = "Vacuum b:0.0"},
                new {Name = "Vacuum c:0.0"},
                new {Name = "Vacuum d:0.0"},
                new {Name = "Vacuum e:0.0"},
                new {Name = "Vacuum f:0.0"}
            };
        }
    }
}