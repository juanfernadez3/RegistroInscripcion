using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegistroInscripcion.Entidades;
using RegistroInscripcion.BLL;


namespace RegistroInscripcion.UI.Registros
{
    /// <summary>
    /// Interaction logic for FormPersonas.xaml
    /// </summary>
    public partial class FormPersonas : Window
    {
        public FormPersonas()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            PersonaIDTexBox.Text = "0";
            NombreTexBox.Text = string.Empty;
            CedulaTexBox.Text = string.Empty;
            TelefonoTexBox.Text = string.Empty;
            DireccionTexBox.Text = string.Empty;
            FechaDeNacimientoDatePiker.SelectedDate = DateTime.Now;
        }



        private Personas LlenaClase()
        {
            Personas persona = new Personas();

            persona.PersonaID = Convert.ToInt32(PersonaIDTexBox.Text);
            persona.Nombre = NombreTexBox.Text;
            persona.Telefono = TelefonoTexBox.Text;
            persona.Cedula = CedulaTexBox.Text;
            persona.Direccion = DireccionTexBox.Text;
            persona.FechaNacimiento = (DateTime)FechaDeNacimientoDatePiker.SelectedDate;
            persona.Balance = Convert.ToDecimal(TextBoxBalance.Text);
            return persona;
        }



        private void LlenaCampo(Personas persona)
        {
            PersonaIDTexBox.Text = Convert.ToString(persona.PersonaID);
            NombreTexBox.Text = persona.Nombre;
            CedulaTexBox.Text = persona.Cedula;
            TelefonoTexBox.Text = persona.Telefono;
            DireccionTexBox.Text = persona.Direccion;
            FechaDeNacimientoDatePiker.SelectedDate = persona.FechaNacimiento;
            TextBoxBalance.Text = Convert.ToString(persona.Balance);
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Personas persona = PersonasBLL.Buscar(Convert.ToInt32(PersonaIDTexBox.Text));
            return (persona != null);
        }

        private bool Validar() //Validar los campos
        {
            bool paso = true;
            if (string.IsNullOrEmpty(PersonaIDTexBox.Text))
            {
                MessageBox.Show("El campo PersonaID debe estar en 0 para agregar una nueva persona");
                PersonaIDTexBox.Focus();
                paso = false;
            }

            if (NombreTexBox.Text == string.Empty)
            {
                MessageBox.Show("El campo direccion no puede estar vacio");
                NombreTexBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(CedulaTexBox.Text))
            {
                MessageBox.Show("El campo  cedula no puede estar vacio");
                CedulaTexBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(TelefonoTexBox.Text))
            {
                MessageBox.Show("El campo telefono no puede estar vacio");
                TelefonoTexBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(DireccionTexBox.Text))
            {
                MessageBox.Show("El campo direccion no puede estar vacio");
                DireccionTexBox.Focus();
                paso = false;
            }
            return paso;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Personas persona;
            bool paso = false;

            if (!Validar())
                return;

            persona = LlenaClase();

            if (string.IsNullOrEmpty(PersonaIDTexBox.Text) || PersonaIDTexBox.Text == "0")
                paso = PersonasBLL.Guardar(persona);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("Error", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = PersonasBLL.Modificar(persona);
            }

            //Informar resultado
            if (paso)
            {
                Limpiar();
            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(PersonaIDTexBox.Text, out id);
            Limpiar();

            if (PersonasBLL.Eliminar(id))
            {
                MessageBox.Show("Eliminado!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("No se puede eliminar una persona que no existe");
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BucarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Personas persona = new Personas();
            int.TryParse(PersonaIDTexBox.Text, out id);

            Limpiar();

            persona = PersonasBLL.Buscar(id);

            if (persona != null)
            {
                MessageBox.Show("Persona encontrada");
                LlenaCampo(persona);
            }
            else
            {
                MessageBox.Show("Persona no encontrada");
            }
        }
    }
}
