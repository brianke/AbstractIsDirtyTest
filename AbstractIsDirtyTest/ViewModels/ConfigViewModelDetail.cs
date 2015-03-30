using System;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

namespace AbstractIsDirtyTest
{
    /// <summary>
    /// This class extends ViewModelDetailBase which implements IEditableDataObject.
    /// <para>
    /// Specify type being edited <strong>DetailType</strong> as the second type argument
    /// and as a parameter to the seccond ctor.
    /// </para>
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class ConfigViewModelDetail : ViewModelDetailBase<ConfigViewModelDetail, ConfigModel>
    {
        // Overriding the GetHashCode prevents the Clone operation from marking an 
        // object Dirty when it is first cloned
        public override int GetHashCode()
        {
            return GetHashCode(this, "DatabaseList");
        }
        private int GetHashCode(object item, params string[] excludeProps)
        {
            int hashCode = 0;
            foreach (var prop in item.GetType().GetProperties())
            {
                if (!excludeProps.Contains(prop.Name))
                {
                    object propVal = prop.GetValue(item, null);
                    if (propVal != null)
                    {
                        hashCode = hashCode ^ propVal.GetHashCode();
                    }
                }
            }
            return hashCode;
        }

        public bool IsDirtyInfo
        {
            // DEBUG: Place breakpoint here
            get { return IsDirty; }
        }

        // Default ctor
        public ConfigViewModelDetail() 
        {
            base.Model = ConfigModel;
            BeginEdit();        
        }

        public ConfigModel ConfigModel
        {
            get
            {
                return _ConfigModel;
            }
            set
            {
                _ConfigModel = value;
                NotifyPropertyChanged(m => m.ConfigModel);
            }
        }
        private ConfigModel _ConfigModel = new ConfigModel();

    }
}