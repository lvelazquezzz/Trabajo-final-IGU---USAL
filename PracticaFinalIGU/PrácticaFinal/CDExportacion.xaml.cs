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
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Security;

namespace PrácticaFinal
{
    /// <summary>
    /// Interaction logic for CDExportacion.xaml
    /// </summary>
    public partial class CDExportacion : Window
    {
        // Declaraciones 
        ObservableCollection<Ejercicio> ejercicios;


        // Constructor
        public CDExportacion(ObservableCollection<Ejercicio> ejercicios)
        {
            InitializeComponent();
            this.ejercicios = ejercicios;
            
        }

        // Eventos
        private void buttonBrowser_Click(object sender, RoutedEventArgs e)
        {
            using(var fd = new FolderBrowserDialog())
            {
                if(fd.ShowDialog() == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fd.SelectedPath))
                {
                    tbRutaExportar.Text = fd.SelectedPath;
                }
            }
        }

        private void buttonExp_Click(object sender, RoutedEventArgs e)
        {
            exportarXML(ejercicios, tbRutaExportar.Text);
        }

        // Método de exportación
        public void exportarXML(ObservableCollection<Ejercicio> listaEjercicios, string rutaExportacion)
        {
            if (listaEjercicios.Count <= 0)
            {

                System.Windows.MessageBox.Show("No hay ejercicios registrados, no se puede exportar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Error);

                this.Close();

                return;
            }
            else
            {
                try
                {
                    string resultado;
                    if (!comprobarValidezRuta(rutaExportacion, out resultado))
                    {
                        System.Windows.MessageBox.Show("Ruta inválida.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    else
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Ejercicio>));

                        resultado = System.IO.Path.Combine(resultado, "Ejercicio.xml");

                        StreamWriter streamWriter = new StreamWriter(resultado);

                        xmlSerializer.Serialize(streamWriter, listaEjercicios);
                        System.Windows.MessageBox.Show("Exportación realizada con éxito.", "Éxito.", MessageBoxButton.OK, MessageBoxImage.Information);


                        this.Close();
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    System.Windows.MessageBox.Show("Error. No tiene acceso para guardar archivos en esa ruta. Por favor seleccione otra.", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch(IOException ex)
                {
                    System.Windows.MessageBox.Show($"Error al escribir el fichero: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show($"Se ha producido un error inesperado: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        public bool comprobarValidezRuta(string rutaExportacion, out string resultado) {
            resultado = string.Empty;
            if (string.IsNullOrWhiteSpace(rutaExportacion)) {
                return false;
            }

            bool estado = false;
            try
            {
                resultado = System.IO.Path.GetFullPath(rutaExportacion);
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
