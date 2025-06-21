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
    /// Interaction logic for CDAddEjercicio.xaml
    /// </summary>
    public partial class CDAddEjercicio : Window
    {
        // Declaraciones de variables
        Boolean nombre = false, gruposelec = false;
        string nom, desc;
        string nombreEjercicioModificar;
        ObservableCollection<GrupoMuscular> gruposMusculares;
        ObservableCollection<Ejercicio> ejercicios;
        int tipoCuadro;

        // Constructor añadir ejercicio
        public CDAddEjercicio(ObservableCollection<Ejercicio> ejercicios)
        {
            InitializeComponent();
            this.ejercicios = ejercicios;
            this.Title = "Añadir Ejercicio";

            if(ejercicios == null ) { 
                ejercicios = new ObservableCollection<Ejercicio>();
            }
            gruposMusculares = new ObservableCollection<GrupoMuscular>();

            ButtonAnadirEjercicio.Content = "Añadir";

            tipoCuadro = 0;
        }

        // Constructor modificar ejercicio
        public CDAddEjercicio(ObservableCollection<Ejercicio> ejercicios, Ejercicio ejercicio)
        {
            InitializeComponent();
            this.ejercicios = ejercicios;

            ButtonAnadirEjercicio.Content = "Modificar";
            TextBoxNombre.Text = ejercicio.Nombre;
            TextBoxDescripcion.Text = ejercicio.Descripcion;
            gruposMusculares = new ObservableCollection<GrupoMuscular>();
            foreach (var grupMusc in ejercicio.Grupo)
            {
                foreach(ListBoxItem item in ListBoxGruposMusculares.Items)
                {
                    if(item.Content.ToString() == grupMusc.ToString())
                    {
                        item.IsSelected = true;
                    }
                }
            }

            nombreEjercicioModificar = ejercicio.Nombre;
            ButtonAnadirEjercicio.Content = "Modificar";
            ButtonAnadirEjercicio.IsEnabled = true;

            tipoCuadro = 1;
            
        }

        // Método que comprueba la validez de los inputs del usuario
        public void activarBoton()
        {
            if (nombre & gruposelec)
            {
                ButtonAnadirEjercicio.IsEnabled = true;
            }
            else
            {
                ButtonAnadirEjercicio.IsEnabled = false;
            }
        }


        // Eventos 
        private void TextBoxDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            desc = TextBoxDescripcion.Text;
        }

        private void TextBoxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            String texto = TextBoxNombre.Text;

            if(string.IsNullOrWhiteSpace(texto))
            {
                nombre = false;
                activarBoton();
            }
            else
            {
                nombre = true;
                nom = texto;
                activarBoton();
            }
        }


        private void ListBoxGruposMusculares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var seleccionados = ListBoxGruposMusculares.SelectedItems.Cast<ListBoxItem>()
                    .Select(item => item.Content.ToString())
                    .ToList();

            if (!seleccionados.Any())
            {
                gruposelec = false;
                activarBoton();
            }
            else
            {
                gruposelec = true;
                ObservableCollection<GrupoMuscular> gMusc = new ObservableCollection<GrupoMuscular>();

                foreach (var grupo in seleccionados)
                {
                    if (Enum.TryParse(grupo, out GrupoMuscular grupoMuscular))
                    {
                        gMusc.Add(grupoMuscular);
                    }
                }

                gruposMusculares = gMusc;
                activarBoton();
            }
        }
        private void ButtonAnadirEjercicio_Click(object sender, RoutedEventArgs e)
        {
            if (tipoCuadro == 0)
            {
                Ejercicio ej = new Ejercicio();
                ej.Nombre = nom;
                ej.Descripcion = desc;
                ej.Grupo = gruposMusculares;
            
                if(ejercicios.Any(ejer => ejer.Nombre == ej.Nombre))
                {
                    MessageBox.Show("Error. El ejercicio \"" + ej.Nombre + "\" ya existe. Si lo desea, puede modificarlo pulsando " +
                        "el botón correspondiente.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
                else
                {
                    ejercicios.Add(ej);
                }
                
            }
            else
            {
                Ejercicio ejercicioModificar = ejercicios.FirstOrDefault(ej => ej.Nombre == nombreEjercicioModificar);
                ejercicioModificar.Nombre = nom;
                ejercicioModificar.Descripcion = desc;
                ejercicioModificar.Grupo.Clear(); 

                foreach(var gr in gruposMusculares)
                {
                    ejercicioModificar.Grupo.Add(gr);
                }
                

            }

            this.Close();
        }
    }
}
