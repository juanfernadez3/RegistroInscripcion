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
using RegistroInscripcion.UI.Registros;

namespace RegistroInscripcion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemPersonas_Click(object sender, RoutedEventArgs e)
        {
            FormPersonas formPersonas = new FormPersonas();
            formPersonas.Show();

        }

        private void MenuItemInscripcion_Click(object sender, RoutedEventArgs e)
        {
            FormInscripciones formInscripcion = new FormInscripciones();
            formInscripcion.Show();
        }
    }
}
