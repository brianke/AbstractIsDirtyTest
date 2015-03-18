using SimpleMvvmToolkit;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;

namespace AbstractIsDirtyTest
{
    [XmlRoot("DelphiaConfig")]
    public class ConfigModel : ModelBase<ConfigModel>
    {
        #region Initialization & Cleanup

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

        /// <summary>
        /// Create a configuration object bound to "config.xml" 
        /// contained in [CommonApplicationData]\[Manufacturer]\[ProductName]
        /// </summary>
        public ConfigModel()
        {
        }

        #endregion Initialization & Cleanup


        #region Properties

        /// <summary>
        /// DatabaseList 
        /// </summary>
        public ObservableCollection<String> DatabaseList
        {
            get { return _DatabaseList; }
            set
            {
                if (_DatabaseList != value)
                {
                    _DatabaseList = value;
                    NotifyPropertyChanged(m => m.DatabaseList);
                }
            }

        }
        private ObservableCollection<String> _DatabaseList = new ObservableCollection<String>();

        
        #endregion Properties

    }
}
