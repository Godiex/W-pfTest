using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifica a la vista cuando una propiedad cambia.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que cambió.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Establece el valor de una propiedad y notifica el cambio si es necesario.
        /// </summary>
        /// <typeparam name="T">Tipo de la propiedad.</typeparam>
        /// <param name="field">Campo que almacena el valor de la propiedad.</param>
        /// <param name="value">Nuevo valor a establecer.</param>
        /// <param name="propertyName">Nombre de la propiedad (automático).</param>
        /// <returns>True si el valor cambió, False si el valor ya era igual.</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
