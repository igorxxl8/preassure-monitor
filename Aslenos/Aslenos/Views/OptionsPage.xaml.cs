using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aslenos.Views
{
    class Option
    {
        public string Name { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsPage : ContentPage
    {
        public OptionsPage()
        {
            InitializeComponent();
            OptionsList.ItemsSource = new List<Option>
            {
                new Option {Name = "Pulsation analyze"},
                new Option {Name = "Manometr"},
                new Option {Name = "Open trend"},
                new Option {Name = "Open trend graphic"},
                new Option {Name = "Open vacuum"}
            };
        }
    }
}