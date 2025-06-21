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
using System.Windows.Shapes;

namespace PrácticaFinal
{

    public partial class DetalleEjercicio : Window
    {

        // Declaraciones 
        private Ejercicio ej;
        private CDEjecucion cdEjecucion;
        private bool nuevaEntrada = true;
        private int numFechasTotales;
        ObservableCollection<Ejecucion> ejecuciones;
        CDEjecucion CDEjecucion { get; set; }
        

        public event EventHandler<cambioDetalleEjecucionEventArgs> cambioDetalleEjecucion;


        public Ejercicio Ej
        {
            get { return ej;}
            set { ej = value; }
        }

        // Constructores 
        
        public DetalleEjercicio()
        {
            InitializeComponent();
            dibujaEscalaIzquierda();
            dibujaEscalaDerecha();
            dibujarGrafico();
        }

        public DetalleEjercicio(Ejercicio ej)
        {

            if (ej == null)
            {
                this.Close();
                return;
            }

            InitializeComponent();
            this.Title = "Detalle del ejercicio: " + ej.Nombre;
            ejecuciones = ej.Ejecuciones;
            ejecuciones.CollectionChanged += Ejecuciones_CollectionChanged;
            this.Loaded += DetalleEjercicio_Loaded;
            this.ej = ej;
            numFechasTotales = ejecuciones.Select(e => e.FechaYHora.Date).Distinct().Count();
            dibujaEscalaIzquierda();
            dibujaEscalaDerecha();
            dibujarGrafico();
        }

        // Set de la propiedad Ej 
        public void cambiarEjercicio(Ejercicio ej)
        {
            this.Title = "Detalle del ejercicio: " + ej.Nombre;
            ejecuciones = ej.Ejecuciones;
            elDataGridSecondaryWindow.ItemsSource = ejecuciones;
            Ej = ej;
            numFechasTotales = ejecuciones.Select(e => e.FechaYHora.Date).Distinct().Count();
            dibujaEscalaIzquierda();
            dibujaEscalaDerecha();
            dibujarGrafico();
        }

        // Eventos 
        private void Ejecuciones_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            dibujaEscalaDerecha();
            dibujaEscalaIzquierda();
            dibujarGrafico();
        }

        // Relativos al DataGrid
        private void DetalleEjercicio_Loaded(object sender, RoutedEventArgs e)
        {
            elDataGridSecondaryWindow.ItemsSource = ejecuciones;

        }

        private void elDataGridSecondaryWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (elDataGridSecondaryWindow.SelectedItems.Count > 0)
            {
                removeEjecucion.IsEnabled = true;

                if (elDataGridSecondaryWindow.SelectedItems[0] is Ejecucion ejecucionSeleccionada)
                {
                    OnCambioDetalleEjecucion(ejecucionSeleccionada.FechaYHora);
                }

            }
            else
            {
                removeEjecucion.IsEnabled = false;

            }

        }


        
        // Relativos a los botones 
        private void removeEjecucion_Click(object sender, RoutedEventArgs e)
        {
            if(elDataGridSecondaryWindow.ItemsSource is ObservableCollection<Ejecucion> listaejecuciones)
            {
                var ejecucionesEliminar = elDataGridSecondaryWindow.SelectedItems.Cast<Ejecucion>().ToList();

                foreach (var ejecucion in ejecucionesEliminar)
                {
                    listaejecuciones.Remove(ejecucion);
                    OnCambioDetalleEjecucion(ejecucion.FechaYHora);
                }

                
            }

        }

        private void addEjecucion_Click(object sender, RoutedEventArgs e)
        {
            int numEjecucionesAntes = ejecuciones.Count();
            this.CDEjecucion = new CDEjecucion(ej);
            this.CDEjecucion.Owner = this;
            this.CDEjecucion.ShowDialog();

            if(ejecuciones.Count != numEjecucionesAntes)
            {
                OnCambioDetalleEjecucion(ejecuciones.Last().FechaYHora);
                nuevaEntrada = true;
                ajustarTamanoCanvas();
                dibujarGrafico();
            }
        }

        // Método para invocar al evento de EventArgs
        void OnCambioDetalleEjecucion(DateTime fechaSeleccionada)
        {
            if (cambioDetalleEjecucion != null)
            {
                cambioDetalleEjecucion(this, new cambioDetalleEjecucionEventArgs(fechaSeleccionada));
            }
        }



        // Métodos y eventos del Canvas Izquierdo
        private void elCanvasColumnaIzq_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            elCanvasColumnaIzq.Children.Clear();
            dibujaEscalaIzquierda();
        }
        private void dibujaEscalaIzquierda()
        {
            elCanvasColumnaIzq.Children.Clear();
            if (ejecuciones.Count <= 0)
            {
                return;
            }

            double altura = elCanvaGraficos.ActualHeight;

            double intervalo = altura / 9;

            TextBlock textBlockReps = new TextBlock()
            {
                Text = "Reps",
                FontSize = 13,
                Foreground = Brushes.Red
            };
            elCanvasColumnaIzq.Children.Add(textBlockReps);
            Canvas.SetTop(textBlockReps, altura - 10);
            Canvas.SetLeft(textBlockReps, elCanvasColumnaIzq.ActualWidth / 2 - 10);

            for (int i = 0; i < 9; i++)
            {
                double y = i * intervalo;
                Line linea = new Line();
                linea.X1 = 3 * (elCanvasColumnaIzq.ActualWidth / 4);
                linea.Y1 = y;
                linea.X2 = elCanvasColumnaIzq.ActualWidth;
                linea.Y2 = y;
                linea.Stroke = Brushes.Red;
                elCanvasColumnaIzq.Children.Add(linea);

            }

            
            if(ejecuciones != null & ejecuciones.Count > 0)
            {
                double maxreps = ejecuciones.Max(e => e.Repeticiones);
                double X, Y;
                int j = 0;

                for(int i = 8; i >= 0; i--)
                {

                    TextBlock tb = new TextBlock();
                    tb.Text = Math.Round((((double)maxreps * (double)i )/ 8),1).ToString();
                    tb.Foreground = Brushes.Red;
                    
                    X = 0;
                    Y = j * intervalo;

                    elCanvasColumnaIzq.Children.Add(tb);
                    Canvas.SetLeft(tb, X + 15);
                    Canvas.SetTop(tb, Y - 10);

                    j++;

                }
            }
            
            
        }

        // Métodos y eventos del Canvas Derecho
        private void elCanvasColumnaDer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            elCanvasColumnaDer.Children.Clear();
            dibujaEscalaDerecha();
        }
        private void dibujaEscalaDerecha()
        {
            elCanvasColumnaDer.Children.Clear();
            if (ejecuciones.Count <= 0)
            {
                return;
            }
            double altura = elCanvaGraficos.ActualHeight;
            double intervalo = altura / 9;

            TextBlock textBlockPesos = new TextBlock()
            {
                Text = "Peso (kg)",
                FontSize = 13,
                Foreground = Brushes.Blue
            };
            elCanvasColumnaDer.Children.Add(textBlockPesos);
            Canvas.SetTop(textBlockPesos, altura - 10);
            Canvas.SetLeft(textBlockPesos, elCanvasColumnaIzq.ActualWidth / 2 - 20);

            for (int i = 0; i < 9; i++)
            {
                double y = i * intervalo;
                Line linea = new Line();
                linea.X1 = 0;
                linea.Y1 = y;
                linea.X2 = elCanvasColumnaDer.ActualWidth / 4;
                linea.Y2 = y;
                linea.Stroke = Brushes.Blue;
                elCanvasColumnaDer.Children.Add(linea);

            }


            if (ejecuciones != null && ejecuciones.Count > 0)
            {
                double maxpeso = ejecuciones.Max(e => e.Peso);
                double X, Y;
                int j = 0;

                for (int i = 8; i >= 0; i--)
                {

                    TextBlock tb = new TextBlock();
                    tb.Text = Math.Round(((maxpeso * (double)i) / 8), 1).ToString();
                    tb.Foreground = Brushes.Blue;

                    X = elCanvasColumnaDer.ActualWidth/2;
                    Y = j * intervalo;

                    elCanvasColumnaDer.Children.Add(tb);
                    Canvas.SetLeft(tb, X);
                    Canvas.SetTop(tb, Y - 10);

                    j++;

                }
            }


        }


        // Métidos y eventos del Canvas donde se dibujan los gráficos
        private void elCanvaGraficos_Loaded(object sender, RoutedEventArgs e)
        {
            dibujarGrafico();
        }

        private void elCanvaGraficos_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            ajustarTamanoCanvas();

            dibujarGrafico();
        }

        private void ajustarTamanoCanvas()
        { 
            double espacioFecha = 580 / 10;
            int numFechas = ejecuciones.Select(e => e.FechaYHora.Date).Distinct().Count();

            if (numFechas > 5 && nuevaEntrada && numFechas != numFechasTotales)
            {
                double anchuraCanvasNecesaria = elCanvaGraficos.ActualWidth + (numFechas - 4) * espacioFecha * 2;

                elCanvaGraficos.Width = anchuraCanvasNecesaria;

                nuevaEntrada = false;
                numFechasTotales = numFechas;

            }

        }

        private void dibujarGrafico()
        {
            elCanvaGraficos.Children.Clear();
            double altura = elCanvaGraficos.ActualHeight;
            double intervalo = altura / 9;
            double espacioFecha = 580 / 10;
            double espacioEjecucion = espacioFecha / 4;;

            double x = 0, yreps = 0, ypeso = 0;

            if (ejecuciones.Count > 0)
            {
                double maxreps = ejecuciones.Max(e => e.Repeticiones);
                double maxpeso = ejecuciones.Max(e => e.Peso);
                var ejecucionesOrdenadas = ejecuciones.OrderBy(e => e.FechaYHora);
                DateTime Fecha = DateTime.MinValue;
                PointCollection points = new PointCollection();

                int i = 0, j = 0;

                foreach (var ejecucion in ejecucionesOrdenadas)
                {
                    int repeticiones = ejecucion.Repeticiones;
                    double peso = ejecucion.Peso;

                    yreps = (repeticiones / maxreps) * (altura - intervalo);
                    ypeso = (peso / maxpeso) * (altura - intervalo);

                    if (ejecucion.FechaYHora.Date != Fecha.Date)
                    {
                        x = espacioFecha * j;

                        espacioEjecucion = espacioFecha / 4;

                        Fecha = ejecucion.FechaYHora;

                        int ejecucionesPorFecha = ejecucionesOrdenadas.Where(e => e.FechaYHora.Date == Fecha.Date).Count();

                        if (ejecucionesPorFecha > 4)
                        {
                            if(ejecucionesPorFecha % 4 == 0)
                            {
                                espacioEjecucion /= (2 * ((ejecucionesPorFecha / 4) - 1));
                            }
                            else
                            {
                                espacioEjecucion /= (2 * (ejecucionesPorFecha / 4)) ;
                            }
                            
                        }

                        TextBox textBox = new TextBox()
                        {
                            Text = Fecha.Date.ToString("dd/MM/yyyy"),
                            IsReadOnly = true
                        };

                        elCanvaGraficos.Children.Add(textBox);
                        Canvas.SetTop(textBox, altura-intervalo);
                        Canvas.SetLeft(textBox, x);

                        j += 2;
                        i = 0;
                    }

                    Rectangle rectangle = new Rectangle()
                    {
                        Width = espacioEjecucion,
                        Height = yreps,
                        Stroke = Brushes.DarkRed,
                        Fill = Brushes.Red,
                        RenderTransformOrigin = new Point(0.5, 1),
                        RenderTransform = new ScaleTransform(1, 0)
                    };

                            
                    elCanvaGraficos.Children.Add(rectangle);
                    Canvas.SetTop(rectangle, altura - yreps - intervalo);                    
                    Canvas.SetLeft(rectangle, x + i * espacioEjecucion);

                    if (peso > 0)
                    {
                        Ellipse ellipse = new Ellipse()
                        {
                            Width = elCanvaGraficos.ActualWidth * 0.01,
                            Height = elCanvaGraficos.ActualWidth * 0.01,
                            Stroke = Brushes.DarkBlue,
                            Fill = Brushes.Blue
                        };

                        elCanvaGraficos.Children.Add(ellipse);
                        Canvas.SetTop(ellipse, altura - ypeso - intervalo - ellipse.Height/2);
                        Canvas.SetLeft(ellipse, x + i * espacioEjecucion + espacioEjecucion / 2 - ellipse.Width / 2);
                        Point point = new Point(x + i * espacioEjecucion + espacioEjecucion / 2,
                            altura - ypeso - intervalo);

                        points.Add(point);
                    }

                    DoubleAnimation barChartGrowth = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromSeconds(1)
                    };

                    Storyboard.SetTarget(barChartGrowth, rectangle);
                    Storyboard.SetTargetProperty(barChartGrowth, new PropertyPath("RenderTransform.ScaleY"));

                    Storyboard sbBarChart = new Storyboard();
                    sbBarChart.Children.Add(barChartGrowth);
                    sbBarChart.Begin();


                    i++;


                }

                for (i = 0; i < points.Count - 1; i++)
                {
                    Line linea = new Line()
                    {
                        StrokeDashArray = { 2, 4 },
                        Stroke = Brushes.Blue,
                        X1 = points[i].X,
                        X2 = points[i].X,
                        Y1 = points[i].Y,
                        Y2 = points[i].Y

                    };

                    DoubleAnimation animationX = new DoubleAnimation(points[i + 1].X, new Duration(TimeSpan.FromMilliseconds(500)));
                    animationX.BeginTime = TimeSpan.FromMilliseconds(500 * i);

                    DoubleAnimation animationY = new DoubleAnimation(points[i + 1].Y, new Duration(TimeSpan.FromMilliseconds(500)));
                    animationY.BeginTime = TimeSpan.FromMilliseconds(500 * i);

                    elCanvaGraficos.Children.Add(linea);
                    
                    Storyboard lineGrowth = new Storyboard();
                    lineGrowth.Children.Add(animationX);
                    lineGrowth.Children.Add (animationY);

                    Storyboard.SetTarget(animationX, linea);
                    Storyboard.SetTarget(animationY, linea);

                    Storyboard.SetTargetProperty(animationY, new PropertyPath(Line.Y2Property));
                    Storyboard.SetTargetProperty(animationX, new PropertyPath(Line.X2Property));

                    lineGrowth.Begin();
                }

            }
        }

       
    }
}