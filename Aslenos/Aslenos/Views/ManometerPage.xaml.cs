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
                new {Name = BuildVacuumString("a", 0.0)},
                new {Name = BuildVacuumString("b", 0.0)},
                new {Name = BuildVacuumString("c", 0.0)},
                new {Name = BuildVacuumString("d", 0.0)},
                new {Name = BuildVacuumString("e", 0.0)},
                new {Name = BuildVacuumString("f", 0.0)},
            };
        }

        private string BuildVacuumString(string phase, double value)
        {
            return $"Vacuum {phase}: {value}";
        }
    }
}