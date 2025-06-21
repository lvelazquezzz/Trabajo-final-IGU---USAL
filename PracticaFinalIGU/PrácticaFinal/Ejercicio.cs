using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PrácticaFinal
{
    public enum GrupoMuscular
    {
        Pecho,
        Core,
        Brazos,
        Espalda,
        Piernas
    }
    public class Ejercicio : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string nombre, descripcion;
        private ObservableCollection<Ejecucion> ejecuciones;
        private ObservableCollection<GrupoMuscular> grupo;

        public String Nombre {
            get
            {
                return nombre;
            }
            set 
            { 
                nombre = value;
                OnPropertyChanged("Nombre");
            }
            }
        public String Descripcion { 
            get { return descripcion; } 
            set
            {
                descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }
        public ObservableCollection<GrupoMuscular> Grupo { 
            get { return grupo; } 
            set
            {
                grupo = value;
                OnPropertyChanged("Grupo");
            }
        }

        public ObservableCollection<Ejecucion> Ejecuciones { 
            get {
                return ejecuciones;
            } 
            set
            {
                ejecuciones = value;
                OnPropertyChanged("Ejecuciones");
            }
        }

        public Ejercicio() {
            this.grupo = new ObservableCollection<GrupoMuscular>();
            this.ejecuciones = new ObservableCollection<Ejecucion>();
        }

        public string GruposMuscularesString => Grupo != null ? string.Join(", ", Grupo) : string.Empty;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
         
        }

        public void agregarEjecucion(Ejecucion ej)
        {
            if (ej != null)
            {
                Ejecuciones.Add(ej);
                OnPropertyChanged("Ejecuciones");
                
            }
        }
    }
}
