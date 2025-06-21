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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Forms;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Media.Media3D;

namespace PrácticaFinal
{
    public partial class MainWindow : Window
    {
        // Declaraciones
        private DetalleEjercicio detalleEjercicio;
        ObservableCollection<Ejercicio> ejercicios;
        private CDAddEjercicio CDAddEjercicio;
        private CDExportacion cdExportacion;
        private CDImportacion cdImportacion;
        DateTime fecha = DateTime.Now;


        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            laLabelFecha.Content = fecha.ToString("dd/MM/yyyy");
            ejercicios = new ObservableCollection<Ejercicio>            {
                new Ejercicio {Nombre="Sentadillas", Descripcion="Excelente ejercicio para fortalecer las piernas y los gluteos", Grupo = {GrupoMuscular.Piernas}},
                new Ejercicio {Nombre="Dominadas", Descripcion="Ejercicio ideal para desarrollar la espalda y los bíceps.", Grupo = {GrupoMuscular.Espalda, GrupoMuscular.Brazos}},
                new Ejercicio {Nombre="Plancha", Descripcion="Un ejercicio isométrico para trabajar el core, especialmente los abdominales", Grupo = {GrupoMuscular.Core, GrupoMuscular.Espalda}},
                new Ejercicio {Nombre="Curl de bíceps", Descripcion="Un ejercicio simple pero efectivo para desarrollar los brazos, especialmente los bíceps.", Grupo = {GrupoMuscular.Brazos}},
                new Ejercicio {Nombre="Press de banca", Descripcion="Este ejercicio se realiza en una máquina guiada y permite trabajar los músculos del pecho con mayor control.", Grupo = {GrupoMuscular.Pecho, GrupoMuscular.Brazos}},
                new Ejercicio {Nombre="Jalón al pecho", Descripcion="Un ejercicio para trabajar la espalda, especialmente el dorsal ancho.",Grupo = {GrupoMuscular.Espalda}},
                new Ejercicio {Nombre="Prensa de pierna", Descripcion="Una máquina guiada para trabajar los músculos de las piernas, especialmente los cuadríceps", Grupo = {GrupoMuscular.Piernas}},
                new Ejercicio {Nombre="Extensión de pierna", Descripcion="Este ejercicio se enfoca en el desarrollo de los cuadríceps mediante una máquina guiada.", Grupo = {GrupoMuscular.Piernas}},
                new Ejercicio {Nombre="Press de hombros", Descripcion="Un ejercicio para trabajar los hombros con una máquina guiada.", Grupo = { GrupoMuscular.Brazos }}
            };
            ejercicios.CollectionChanged += Ejercicios_CollectionChanged;


            Loaded += MainWindow_Loaded; // Introducimos los valores por defecto en este  
        }

        // Eventos 
        private void Ejercicios_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            elCanvaMainWindow.Children.Clear();
            dibujar_GraficoEstrella();
        }


        // Evento de carga de la ventana
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var plancha = ejercicios.FirstOrDefault(x => x.Nombre == "Plancha");
            if (plancha != null)
            {
                plancha.agregarEjecucion(new Ejecucion { Repeticiones = 60, Peso = 0, FechaYHora = new DateTime(2025, 1, 24) });
                plancha.agregarEjecucion(new Ejecucion { Repeticiones = 70, Peso = 0, FechaYHora = new DateTime(2025, 1, 24) });
                plancha.agregarEjecucion(new Ejecucion { Repeticiones = 80, Peso = 0, FechaYHora = new DateTime(2025, 1, 24) });
                plancha.agregarEjecucion(new Ejecucion { Repeticiones = 60, Peso = 0, FechaYHora = new DateTime(2025, 2, 2) });
                plancha.agregarEjecucion(new Ejecucion { Repeticiones = 80, Peso = 0, FechaYHora = new DateTime(2025, 2, 2) });
                plancha.agregarEjecucion(new Ejecucion { Repeticiones = 80, Peso = 0, FechaYHora = new DateTime(2025, 2, 5) });


            }

            var prensaDePierna = ejercicios.FirstOrDefault(x => x.Nombre == "Prensa de pierna");
            if (prensaDePierna != null)
            {
                prensaDePierna.agregarEjecucion(new Ejecucion { Repeticiones = 12, Peso = 100, FechaYHora = new DateTime(2025, 1, 24) });
                prensaDePierna.agregarEjecucion(new Ejecucion { Repeticiones = 15, Peso = 110, FechaYHora = new DateTime(2025, 1, 24) });
                prensaDePierna.agregarEjecucion(new Ejecucion { Repeticiones = 14, Peso = 115, FechaYHora = new DateTime(2025, 2, 3) });
                prensaDePierna.agregarEjecucion(new Ejecucion { Repeticiones = 12, Peso = 120, FechaYHora = new DateTime(2025, 2, 3) });
                prensaDePierna.agregarEjecucion(new Ejecucion { Repeticiones = 15, Peso = 125, FechaYHora = new DateTime(2025, 2, 8) });
            }
            elDataGridTab1.ItemsSource = ejercicios;
           
        }

        // Evento de cierre de la ventana
        private void Window_Closed(object sender, EventArgs e)
        {
            if (detalleEjercicio != null)
            {
                detalleEjercicio.Close();
                detalleEjercicio = null;
            }
        }

        //Cambio de la ventana 
        private void elDataGridTab1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (elDataGridTab1.SelectedItem is Ejercicio ejercicioSeleccionado)
            {
                if (detalleEjercicio == null)
                {
                    detalleEjercicio = new DetalleEjercicio(ejercicioSeleccionado);
                    detalleEjercicio.Closed += DetalleEjercicio_Closed;
                    detalleEjercicio.Show();
                    detalleEjercicio.cambioDetalleEjecucion += DetalleEjercicio_cambioDetalleEjecucion;
                }
                else
                {
                    detalleEjercicio.cambiarEjercicio(ejercicioSeleccionado);
                }

                modifyExercise.IsEnabled = true;
                removeExercise.IsEnabled = true;
            }
            else
            {
                modifyExercise.IsEnabled = false;
                removeExercise.IsEnabled = false;
            }

        }

        // Relativos a la ventana secundaria no modal
        private void DetalleEjercicio_cambioDetalleEjecucion(Object sender, cambioDetalleEjecucionEventArgs e)
        {
            fecha = e.fechaCambio;
            laLabelFecha.Content = fecha.ToString("dd/MM/yyyy");
            elCanvaMainWindow.Children.Clear();
            dibujar_GraficoEstrella();
        }

        private void DetalleEjercicio_Closed(object sender, EventArgs e)
        {
            detalleEjercicio = null;
        }

        // Relativos a los botones de adición, modificación y eliminación de ejercicios
        private void addExercise_Click(object sender, RoutedEventArgs e)
        {
            this.CDAddEjercicio = new CDAddEjercicio(ejercicios);
            CDAddEjercicio.Owner = this;
            CDAddEjercicio.ShowDialog();
        }

        private void modifyExercise_Click(object sender, RoutedEventArgs e)
        {
            if (elDataGridTab1.SelectedItems.Count > 0)
            {
                if (elDataGridTab1.SelectedItem is Ejercicio ejercicioSeleccionado)
                {
                    this.CDAddEjercicio = new CDAddEjercicio(ejercicios, ejercicioSeleccionado);
                    CDAddEjercicio.Owner = this;
                    CDAddEjercicio.ShowDialog();

                    elDataGridTab1.ItemsSource = null;
                    elDataGridTab1.ItemsSource = ejercicios;
                }


            }



        }

        private void removeExercise_Click(object sender, RoutedEventArgs e)
        {
            if (elDataGridTab1.SelectedItem is Ejercicio ejercicioSeleccionado)
            {
                if (detalleEjercicio != null)
                {
                    detalleEjercicio.Close();
                    detalleEjercicio = null;
                }
                ejercicios.Remove(ejercicioSeleccionado);
            }
        }


        private void buttonExportarXML_Click(object sender, RoutedEventArgs e)
        {
            this.cdExportacion = new CDExportacion(ejercicios);
            this.cdExportacion.Owner = this;
            this.cdExportacion.ShowDialog();
        }

        private void buttonImportarXML_Click(object sender, RoutedEventArgs e)
        {
            this.cdImportacion = new CDImportacion(ejercicios);
            this.cdImportacion.Owner = this;
            this.cdImportacion.ShowDialog();
        }



        // Relativos a los botones de navegación de fechas
        private void diaAnterior_Click(object sender, RoutedEventArgs e)
        {
            fecha = fecha.AddDays(-1);
            laLabelFecha.Content = fecha.ToString("dd/MM/yyyy");
            elCanvaMainWindow.Children.Clear();
            dibujar_GraficoEstrella();
        }

        private void diaSiguiente_Click(object sender, RoutedEventArgs e)
        {
            fecha = fecha.AddDays(1);
            laLabelFecha.Content = fecha.ToString("dd/MM/yyyy");
            elCanvaMainWindow.Children.Clear();
            dibujar_GraficoEstrella();
        }

        private void diaHoy_Click(object sender, RoutedEventArgs e)
        {
            fecha = DateTime.Now;
            laLabelFecha.Content = fecha.ToString("dd/MM/yyyy");
            elCanvaMainWindow.Children.Clear();
            dibujar_GraficoEstrella();
        }



        // Método para dibujar el gráfico de estrella
        private void dibujar_GraficoEstrella()
        {
            double centX = elCanvaMainWindow.ActualWidth / 2;
            double centY = elCanvaMainWindow.ActualHeight / 2;

            double radio = Math.Min(elCanvaMainWindow.ActualWidth, elCanvaMainWindow.ActualHeight) / 3;
            double x, y;

            int numPuntos = Enum.GetNames(typeof(GrupoMuscular)).Length; // = 5
            double anguloB = (2 * Math.PI) / numPuntos;
            double desplazamiento = Math.PI / 2;

            const double maxReps = 100;
            double xPunto, yPunto; // para obtener las coordenadas necesarias
            double repeticionesPecho = 0, repeticionesBrazo = 0, repeticionesPierna = 0, repeticionesEspalda = 0, repeticionesCore = 0;

            Point punto, puntoGrafica;
            PointCollection pc = new PointCollection();
            Polygon poligono;
            double ponderacion = 1;

            foreach (var ejercicio in ejercicios)
            {
                ponderacion = 1;
                if (ejercicio.Grupo.Count > 1)
                {
                    ponderacion /= ejercicio.Grupo.Count;
                }

                ObservableCollection<Ejecucion> ejecucionesFecha = new ObservableCollection<Ejecucion>
                    (ejercicio.Ejecuciones.Where(e => e.FechaYHora.Date == fecha.Date));

                foreach (var ejecucion in ejecucionesFecha)
                {
                    if (ejercicio.Grupo.Contains(GrupoMuscular.Pecho))
                    {
                        repeticionesPecho += ejecucion.Repeticiones * ponderacion;
                    }
                    if (ejercicio.Grupo.Contains(GrupoMuscular.Brazos))
                    {
                        repeticionesBrazo += ejecucion.Repeticiones * ponderacion;
                    }
                    if (ejercicio.Grupo.Contains(GrupoMuscular.Piernas))
                    {
                        repeticionesPierna += ejecucion.Repeticiones * ponderacion;
                    }
                    if (ejercicio.Grupo.Contains(GrupoMuscular.Espalda))
                    {
                        repeticionesEspalda += ejecucion.Repeticiones * ponderacion;
                    }
                    if (ejercicio.Grupo.Contains(GrupoMuscular.Core))
                    {
                        repeticionesCore += ejecucion.Repeticiones * ponderacion;
                    }
                }
            }

            repeticionesBrazo = (repeticionesBrazo > maxReps) ? maxReps : repeticionesBrazo;
            repeticionesPecho = (repeticionesPecho > maxReps) ? maxReps : repeticionesPecho;
            repeticionesPierna = (repeticionesPierna > maxReps) ? maxReps : repeticionesPierna;
            repeticionesEspalda = (repeticionesEspalda > maxReps) ? maxReps : repeticionesEspalda;
            repeticionesCore = (repeticionesCore > maxReps) ? maxReps : repeticionesCore;


            for (int i = 0; i < numPuntos; i++)
            {
                double angulo = ((i) * anguloB) - desplazamiento;
                x = centX + radio * Math.Cos(angulo);
                y = centY + radio * Math.Sin(angulo);

                punto = new Point(x, y);
                Line line = new Line
                {
                    X1 = x,
                    Y1 = y,
                    X2 = centX,
                    Y2 = centY,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                elCanvaMainWindow.Children.Add(line);

                System.Windows.Controls.Label label = new System.Windows.Controls.Label
                {
                    Height = 30,
                    Width = 60
                };
                elCanvaMainWindow.Children.Add(label);

                Ellipse ellipse = new Ellipse
                {
                    Width = elCanvaMainWindow.ActualWidth * 0.01,
                    Height = elCanvaMainWindow.ActualWidth * 0.01,
                    Stroke = Brushes.DarkRed,
                    Fill = Brushes.Red
                };
                elCanvaMainWindow.Children.Add(ellipse);

                switch (i)
                {
                    case 0:
                        label.Content = "Pecho";
                        Canvas.SetLeft(label, x - label.Width / 3);
                        Canvas.SetTop(label, y - label.Height);

                        xPunto = centX + radio * (repeticionesPecho / maxReps) * Math.Cos(angulo);
                        yPunto = centY + radio * (repeticionesPecho / maxReps) * Math.Sin(angulo);

                        if (repeticionesPecho > 0)
                        {
                            Canvas.SetTop(ellipse, yPunto - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, xPunto - ellipse.Width / 2);
                            puntoGrafica = new Point(xPunto , yPunto );
                        }
                        else
                        {
                            Canvas.SetTop(ellipse, centY - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, centX - ellipse.Width / 2);
                            puntoGrafica = new Point(centX , centY );
                        }

                        pc.Add(puntoGrafica);
                        break;
                    case 1:
                        label.Content = "Brazos";
                        Canvas.SetLeft(label, x + label.Width * 0.1);
                        Canvas.SetTop(label, y - label.Height * 0.7);

                        xPunto = centX + radio * (repeticionesBrazo / maxReps) * Math.Cos(angulo);
                        yPunto = centY + radio * (repeticionesBrazo / maxReps) * Math.Sin(angulo);

                        if (repeticionesBrazo > 0)
                        {
                            Canvas.SetTop(ellipse, yPunto - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, xPunto - ellipse.Width / 2);
                            puntoGrafica = new Point(xPunto , yPunto );
                        }
                        else
                        {
                            Canvas.SetTop(ellipse, centY - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, centX - ellipse.Width / 2);
                            puntoGrafica = new Point(centX , centY );
                        }
                        pc.Add(puntoGrafica);
                        break;
                    case 2:
                        label.Content = "Piernas";
                        Canvas.SetLeft(label, x - label.Width / 2);
                        Canvas.SetTop(label, y - label.Height / 5);

                        xPunto = centX + radio * (repeticionesPierna / maxReps) * Math.Cos(angulo);
                        yPunto = centY + radio * (repeticionesPierna / maxReps) * Math.Sin(angulo);

                        if (repeticionesPierna > 0)
                        {
                            Canvas.SetTop(ellipse, yPunto - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, xPunto - ellipse.Width / 2);
                            puntoGrafica = new Point(xPunto , yPunto );
                        }
                        else
                        {
                            Canvas.SetTop(ellipse, centY - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, centX - ellipse.Width / 2);
                            puntoGrafica = new Point(centX , centY );
                        }
                        pc.Add(puntoGrafica);
                        break;
                    case 3:
                        label.Content = "Espalda";
                        Canvas.SetLeft(label, x - label.Width / 2);
                        Canvas.SetTop(label, y - label.Height / 5);

                        xPunto = centX + radio * (repeticionesEspalda / maxReps) * Math.Cos(angulo);
                        yPunto = centY + radio * (repeticionesEspalda / maxReps) * Math.Sin(angulo);

                        if (repeticionesEspalda > 0)
                        {
                            Canvas.SetTop(ellipse, yPunto - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, xPunto - ellipse.Width / 2);
                            puntoGrafica = new Point(xPunto , yPunto );
                        }
                        else
                        {
                            Canvas.SetTop(ellipse, centY - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, centX - ellipse.Width / 2);
                            puntoGrafica = new Point(centX, centY);
                        }
                        pc.Add(puntoGrafica);
                        break;
                    case 4:
                        label.Content = "Core";
                        Canvas.SetLeft(label, x - label.Width / 1.5);
                        Canvas.SetTop(label, y - label.Height * 0.7);

                        xPunto = centX + radio * (repeticionesCore / maxReps) * Math.Cos(angulo);
                        yPunto = centY + radio * (repeticionesCore / maxReps) * Math.Sin(angulo);

                        if (repeticionesCore > 0)
                        {
                            Canvas.SetTop(ellipse, yPunto - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, xPunto - ellipse.Width / 2);
                            puntoGrafica = new Point(xPunto, yPunto);
                        }
                        else
                        {
                            Canvas.SetTop(ellipse, centY - ellipse.Height / 2);
                            Canvas.SetLeft(ellipse, centX - ellipse.Width / 2);
                            puntoGrafica = new Point(centX, centY);
                        }
                        pc.Add(puntoGrafica);
                        
                        break;
                }

                Storyboard sb = (Storyboard)this.Resources["EllipseGrowth"];
                sb.Children[0].SetValue(DoubleAnimation.ToProperty, elCanvaMainWindow.ActualWidth * 0.01);
                sb.Children[1].SetValue(DoubleAnimation.ToProperty, elCanvaMainWindow.ActualWidth * 0.01);
                Storyboard.SetTarget(sb, ellipse);
                sb.Begin();

                Canvas.SetZIndex(ellipse, 1);

            }

            poligono = new Polygon()
            {
                Points = pc,
                Stroke = Brushes.Blue,
                Fill = Brushes.AliceBlue,
                RenderTransform = new ScaleTransform(0, 0, centX, centY)
            };
            elCanvaMainWindow.Children.Add(poligono);

            Canvas.SetZIndex(poligono, 0);
            Storyboard sbPolygon = (Storyboard)this.Resources["PolygonAparition"];
            Storyboard.SetTarget(sbPolygon, poligono); 
            sbPolygon.Begin();

            Storyboard sbPolygon2 = (Storyboard)this.Resources["PolygonExpands"];
            Storyboard.SetTarget(sbPolygon2, poligono);
            sbPolygon2.Begin();


        }

        private void elCanvaMainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            elCanvaMainWindow.Children.Clear();
            dibujar_GraficoEstrella();
        }

        // Relativos a la exportacion de imagenes 
        private void buttonExportarImagen_Click(object sender, RoutedEventArgs e)
        {
            exportarCanvasComoImagen();

        }

        // Método de exportación de imágenes
        private void exportarCanvasComoImagen()
        {
            string rutaImagen;

            using (var fd = new System.Windows.Forms.SaveFileDialog())
            {
                fd.Filter = "Archivos PNG (*.png)|*.png|Archivos JPG (*.jpg)|*.jpg|Archivos JPEG (*.jpeg)|*.jpeg";
                fd.Title = "Guardar imagen como...";

                fd.ShowDialog();

                rutaImagen = fd.FileName;
                if (string.IsNullOrWhiteSpace(rutaImagen)) {
                    return;
                }
            };

           
            string extension = System.IO.Path.GetExtension(rutaImagen);

            double ancho = elCanvaMainWindow.ActualWidth;
            double alto = elCanvaMainWindow.ActualHeight;

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)ancho, (int)alto, 96d, 96d,
            PixelFormats.Pbgra32);

            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(elCanvaMainWindow), null, new Rect(new Point(), new Size(ancho, alto)));
            }

            renderTargetBitmap.Render(visual);

            BitmapEncoder encoder;

            if(extension == ".png")
            {
                encoder = new PngBitmapEncoder();
            }
            else if(extension == ".jpg" || extension == ".jpeg")
            {
                encoder = new JpegBitmapEncoder();

            }
            else
            {
                System.Windows.MessageBox.Show("La extensión del archivo no es correcta.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            try
            {
                FileStream file = File.Create(rutaImagen);
                encoder.Save(file);

                System.Windows.MessageBox.Show("Se ha guardado la imagen con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex) {
                System.Windows.MessageBox.Show($"Se ha producido un error al guardar la imagen: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }

    
}
