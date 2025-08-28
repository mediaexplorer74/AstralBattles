using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel;

namespace AstralBattles.Core.Infrastructure
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public static bool IsInDesignModeStatic => DesignMode.DesignModeEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return false;
            
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}