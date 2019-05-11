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
    public partial class OpenTrendPage : ContentPage
    {
        public OpenTrendPage()
        {
            InitializeComponent();

            var layout = new StackLayout();
            layout.BackgroundColor = Color.Yellow;
            layout.Children.Add(new Label{Text = "64.4", FontAttributes = FontAttributes.Bold, FontSize = 20});
            layout.Children.Add(new Label { Text = "931" });

            TablePulsationParams.Children.Add(layout,1, 0);

            var layout1 = new StackLayout();
            layout1.BackgroundColor = Color.Yellow;
            layout1.Children.Add(new Label { Text = "64.0", FontAttributes = FontAttributes.Bold, FontSize = 20 });
            layout1.Children.Add(new Label { Text = "937.5" });

            TablePulsationParams.Children.Add(layout1, 2, 0);
        }
    }
}