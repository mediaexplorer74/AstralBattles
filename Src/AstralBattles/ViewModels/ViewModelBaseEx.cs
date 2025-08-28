using AstralBattles.Core.Infrastructure;
using AstralBattles.Localizations.Cyclops.MainApplication.Localization;
using System;

using System.Xml.Serialization;

namespace AstralBattles.ViewModels
{
    public class ViewModelBaseEx : NotifyPropertyChangedBase
    {
        private bool _isBusy;
        private ResourceWrapper _resources;

        public ViewModelBaseEx()
        {
            // _resources = Application.Current.Resources["ResourceWrapper"] as ResourceWrapper;
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        [XmlIgnore]
        public ResourceWrapper Resources
        {
            get
            {
                // if (_resources == null)
                //     _resources = Application.Current.Resources["ResourceWrapper"] as ResourceWrapper;
                return _resources;
            }
            set => SetProperty(ref _resources, value);
        }

        protected bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        public DateTime SerializationDate { get; set; }

        public static bool IsInDesignModeStatic { get; set; } = false; //!
    }
}
