using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PrácticaFinal
{
    /// <summary>
    /// Interaction logic for CDImportacion.xaml
    /// </summary>
    public partial class CDImportacion : Window
    {
        // Declaraciones
        ObservableCollection<Ejercicio> ejerciciosExistentes, ejerciciosImportados;

        // Constructor 
        public CDImportacion(ObservableCollection<Ejercicio> ejercicios)
        {
            InitializeComponent();
            this.ejerciciosExistentes = ejercicios;
        }

        // Eventos
        private void buttonBrowser_Click(object sender, RoutedEventArgs e)
        {
            using (var fd = new System.Windows.Forms.OpenFileDialog())
            {
                fd.Filter = "Archivos XML (*.xml)|*.xml|Todos los archivos (*.*)|*.*";
                fd.Title = "Selecciona un archivo XML";

                fd.ShowDialog();
            
                tbRutaExportar.Text = fd.FileName;
            };
        }

        private void buttonImp_Click(object sender, RoutedEventArgs e)
        {
            ejerciciosImportados = importarXML(tbRutaExportar.Text);
            if (ejerciciosImportados.Count > 0)
            {
                foreach (var ejercicioImportado in ejerciciosImportados)
                {
                    Ejercicio ejercicioCoincidente = ejerciciosExistentes.FirstOrDefault(ej => ej.Nombre == ejercicioImportado.Nombre);
                    if (ejercicioCoincidente != null){
                        foreach(var ejecucion in ejercicioImportado.Ejecuciones)
                        {
                            ejercicioCoincidente.Ejecuciones.Add(ejecucion);
                        }
                    }else{
                        ejerciciosExistentes.Add(ejercicioImportado);
                    }
                    
                }
                System.Windows.MessageBox.Show("Importacion realizada con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("No hay ejercicios que importar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Métodos
        private ObservableCollection<Ejercicio> importarXML(string rutaImportacion)
        {
            if (!File.Exists(rutaImportacion))
            {
                System.Windows.MessageBox.Show("El archivo especificado no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return new ObservableCollection<Ejercicio>();
            }
            try
            {
                string resultado;
                if (!comprobarValidezRuta(rutaImportacion, out resultado))
                {
                    System.Windows.MessageBox.Show("Ruta inválida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
                else
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Ejercicio>));

                    StreamReader streamReader = new StreamReader(rutaImportacion);

                    return (ObservableCollection<Ejercicio>)xmlSerializer.Deserialize(streamReader);


                }
            }
            catch (UnauthorizedAccessException)
            {
                System.Windows.MessageBox.Show("Error. No tiene acceso para abrir archivos en esa ruta. Por favor seleccione otra.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show($"Error al leer del fichero: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Se ha producido un error inesperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return new ObservableCollection<Ejercicio>();
        }

        public bool comprobarValidezRuta(string rutaImportacion, out string resultado)
        {
            resultado = string.Empty;
            if (string.IsNullOrWhiteSpace(rutaImportacion))
            {
                return false;
            }

            bool estado = false;
            try
            {
                resultado = System.IO.Path.GetFullPath(rutaImportacion);
                estado = true;
            }
            catch (Exception)
            {
                return false;
            }

            return estado;
        }


    }
}
