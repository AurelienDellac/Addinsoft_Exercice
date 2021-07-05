using Client.Models;
using Client.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LicenceRepository licenceRepository;

        public MainWindow(LicenceRepository licenceRepository)
        {
            InitializeComponent();
            this.licenceRepository = licenceRepository;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int quantity;

            if(int.TryParse(licenceNumber.Text, out quantity))
            {
                licencePricePreview.Text = await licenceRepository.GetLicencePricePreview(
                    quantity,
                    currency.Text);
            } else
            {
                licencePricePreview.Text = "La quantité de licence indiqué n'est pas un nombre correct !";
            }
         
            licencePricePreview.Visibility = Visibility.Visible;
        }
    }
}
