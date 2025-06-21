using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrácticaFinal
{

    public class Ejecucion : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int repeticiones; 
        private float peso;
        private DateTime fechaYHora;

        public int Repeticiones { 
            get{
                return repeticiones;
            }
            set
            {
                repeticiones = value;
                OnPropertyChanged("Repeticiones");
            }
        }

        public float Peso { 
            get { return peso; } 
            
            set
            {
                peso = value;
                OnPropertyChanged("Peso");
            }
        }

        public DateTime FechaYHora
        {
            get { return fechaYHora; }
            set
            {
                fechaYHora = value;
                OnPropertyChanged("FechaYHora");
            }
        }

        public Ejecucion()
        {

        }

        public Ejecucion(int repeticiones, int peso, DateTime fechaYHora)
        {
            this.Repeticiones = repeticiones;
            this.Peso = peso;
            this.FechaYHora = fechaYHora;
        }

        public virtual void OnPropertyChanged(string propertyName) { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return "Fecha: " + fechaYHora.ToString() + "Repeticiones: " + repeticiones + "Peso: " + peso;
        }
    }
}
