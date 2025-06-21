using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace PrácticaFinal
{
    /// <summary>
    /// Interaction logic for CDEjecucion.xaml
    /// </summary>
    public partial class CDEjecucion : Window
    {
        // Declaraciones 
        Boolean rep = false, pes = false, fec = false, hora = false ;
        Ejercicio ejercicio;
        float peso; int repeticiones;
        DateTime fechaYHora;
        TimeSpan hour;
        ObservableCollection<Ejecucion> ejecuciones;

        // Constructor
        public CDEjecucion(Ejercicio ej)
        {
            InitializeComponent();
            this.ejercicio = ej;
            this.ejecuciones = ejercicio.Ejecuciones;
            this.Title = "Añadir ejecución a: " + ejercicio.Nombre;
        }

        // Método de activación del botón
        private void activarBoton()
        {
            if (rep & pes & fec & hora) { BotonAddEjecucion.IsEnabled = true; }
            else { BotonAddEjecucion.IsEnabled = false; }
        }

        
        // Eventos
        private void TextBoxPeso_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = TextBoxPeso.Text;

            if (texto.Contains("."))
            {
                pes = false;
                activarBoton();

            }

            if(float.TryParse(texto, out peso)){
                pes = true;
            }
            else
            {
                pes = false;
            }
            activarBoton();
        }

        private void DatePickerTB_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerTB.SelectedDate.HasValue)
            {
                fec = true;
                fechaYHora = (DateTime)DatePickerTB.SelectedDate;
            }
            else
            {
                fec = false;
            }
            activarBoton();
        }

        private void TextBoxHora_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = TextBoxHora.Text;

            if(TimeSpan.TryParse(texto, out hour) && hour.TotalHours < 24)
            {
                hora = true;
            }
            else
            {
                hora = false;
            }
            activarBoton();
        }

        private void TextBoxRep_TextChanged(object sender, TextChangedEventArgs e)
        {
            string texto = TextBoxRep.Text;

            if (int.TryParse(texto, out repeticiones))
            {
                rep = repeticiones > 0 ? rep = true : rep = false;
                
            }
            else
            {
                rep = false;
            }
            activarBoton() ;
        }


        private void BotonAddEjecucion_Click(object sender, RoutedEventArgs e)
        {
            Ejecucion ejecucion = new Ejecucion();
            ejecucion.Repeticiones = repeticiones;
            ejecucion.Peso = peso;
            fechaYHora += hour;
            ejecucion.FechaYHora = fechaYHora;
            ejecuciones.Add(ejecucion);
            this.Close();
        }
    }
}
