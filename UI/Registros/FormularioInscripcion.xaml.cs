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
    /// Interaction logic for FormInscripciones.xaml
    /// </summary>
    public partial class FormInscripciones : Window
    {
        public FormInscripciones()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            InscripcionesIDTextBox.Text = "0";
            FechaDeInscripcionDatePicker.SelectedDate = DateTime.Now;
            PersonaIdTextBox.Text = "0";
            ComentarioTextBox.Text = string.Empty;
            MontoTextBox.Text = string.Empty;
            BalanceTextBox.Text = string.Empty;
        }
        /*  private void ButtonNuevo_Click(object sender, RoutedEventArgs e)
          {
              Limpiar();
          }*/

        private Inscripciones LlenaClase()
        {
            Inscripciones inscripcion = new Inscripciones();

            inscripcion.IncripcionId = inscripcion.PersonaId = Convert.ToInt32(InscripcionesIDTextBox.Text);
            inscripcion.PersonaId = Convert.ToInt32(PersonaIdTextBox.Text);
            inscripcion.Fecha = (DateTime)FechaDeInscripcionDatePicker.SelectedDate;
            inscripcion.Comentarios = ComentarioTextBox.Text;
            inscripcion.Monto = Convert.ToDecimal(MontoTextBox.Text);
            inscripcion.Balance = Convert.ToDecimal(BalanceTextBox.Text);
            inscripcion.Deposito = Convert.ToDecimal(DepositoTextBox.Text);

            if (Convert.ToDecimal(BalanceTextBox.Text) > 0)
            {
                inscripcion.Balance = (Convert.ToDecimal(BalanceTextBox.Text) - inscripcion.Deposito);
                PersonasBLL.GuardarBalance(Convert.ToInt32(PersonaIdTextBox.Text), (inscripcion.Balance));
            }
            else
            {
                inscripcion.Balance = inscripcion.Balance - inscripcion.Deposito;
                PersonasBLL.GuardarBalance(Convert.ToInt32(PersonaIdTextBox), (inscripcion.Monto - inscripcion.Deposito));

            }

            return inscripcion;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Inscripciones inscripcion = InscripcionesBLL.Buscar(Convert.ToInt32(InscripcionesIDTextBox.Text));
            return (inscripcion != null);
        }

        private bool Validar() //Validar los campos
        {
            bool paso = true;
            if (string.IsNullOrEmpty(InscripcionesIDTextBox.Text))
            {
                MessageBox.Show("El campo inscripcionesId debe estar en 0 para agregar una inscripcion");
                PersonaIdTextBox.Focus();
                paso = false;
            }
            if (string.IsNullOrEmpty(PersonaIdTextBox.Text))
            {
                MessageBox.Show("El campo PersonaId no puede estar vacio y debe ser mayor o igual que 1");
                PersonaIdTextBox.Focus();
                paso = false;
            }

            if (ComentarioTextBox.Text == string.Empty)
            {
                MessageBox.Show("El campo comentario no puede estar vacio");
                ComentarioTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(MontoTextBox.Text))
            {
                MessageBox.Show("El campo  monto no puede estar vacio");
                BalanceTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(BalanceTextBox.Text))
            {
                MessageBox.Show("El campo balance no puede estar vacio");
                BalanceTextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private void ButtonGuardar_Click(object sender, RoutedEventArgs e)
        {
            Inscripciones inscripcion;
            bool paso = false;

            if (!Validar())
                return;

            inscripcion = LlenaClase();

            if (string.IsNullOrEmpty(InscripcionesIDTextBox.Text) || InscripcionesIDTextBox.Text == "0")
                paso = InscripcionesBLL.Guardar(inscripcion);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = InscripcionesBLL.Modificar(inscripcion);
            }

            //Informar resultado
            if (paso)
            {
                Limpiar();
            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButtonNuevo_Click_1(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void ButtonGuardar_Click_1(object sender, RoutedEventArgs e)
        {
            Inscripciones inscripcion;
            bool paso = false;

            if (!Validar())
                return;

            inscripcion = LlenaClase();

            if (string.IsNullOrEmpty(InscripcionesIDTextBox.Text) || InscripcionesIDTextBox.Text == "0")
                paso = InscripcionesBLL.Guardar(inscripcion);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = InscripcionesBLL.Modificar(inscripcion);
            }

            //Informar resultado
            if (paso)
            {
                Limpiar();
            }
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void LlenaCampo(Inscripciones inscripcion)
        {
            InscripcionesIDTextBox.Text = Convert.ToString(inscripcion.IncripcionId);
            PersonaIdTextBox.Text = Convert.ToString(inscripcion.PersonaId);
            ComentarioTextBox.Text = inscripcion.Comentarios;
            MontoTextBox.Text = Convert.ToString(inscripcion.Monto);
            BalanceTextBox.Text = Convert.ToString(inscripcion.Balance);
            FechaDeInscripcionDatePicker.SelectedDate = (DateTime)inscripcion.Fecha;
        }

        private void btn_Buscar_Click(object sender, RoutedEventArgs e) //Buscar
        {
            int id;
            Inscripciones inscripcion = new Inscripciones();
            int.TryParse(InscripcionesIDTextBox.Text, out id);

            Limpiar();

            inscripcion = InscripcionesBLL.Buscar(id);

            if (inscripcion != null)
            {
                MessageBox.Show("Persona encontrada");
                LlenaCampo(inscripcion);
            }
            else
            {
                MessageBox.Show("Persona no encontrada");
            }
        }

        private void ButtonBuscar_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Inscripciones inscripcion = new Inscripciones();
            int.TryParse(InscripcionesIDTextBox.Text, out id);

            Limpiar();

            inscripcion = InscripcionesBLL.Buscar(id);

            if (inscripcion != null)
            {
                LlenaCampo(inscripcion);
            }
            else
            {
                MessageBox.Show("Inscripcion no encontrada", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            Inscripciones inscripcion = new Inscripciones();
            int id;
            int.TryParse(PersonaIdTextBox.Text, out id);
            Limpiar();

            inscripcion = InscripcionesBLL.Buscar(id);
            if (InscripcionesBLL.Eliminar(id))
            {
                PersonasBLL.GuardarBalance(inscripcion.PersonaId, (0 * inscripcion.Balance));
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se puede eliminar una inscripcion que no existe", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
