using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using CarCalendar.Model;

namespace CarCalendar.ViewModel
{
    public class CarFixViewModel: INotifyPropertyChanged
    {
        public CarFixModel repairsDB;

        public CarFixViewModel(string connectionString)
        {
            repairsDB = new CarFixModel(connectionString);
        }

        private ObservableCollection<CarFix> _carFixes;
        public ObservableCollection<CarFix> CarFixes
        {
            get 
            {
                return _carFixes; 
            }
            set
            {
                _carFixes = value;
                NotifyPropertyChanged("CarFixes");
            }
        }

        public void AddItemToDatabase(CarFix item)
        {
            repairsDB.InsertItem(item);
        }
        //Get Items from DB
        public void LoadCollectionFromDatabase()
        {
            List<CarFix> Items = repairsDB.GetAllItems();
            CarFixes = new ObservableCollection<CarFix>(Items);
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
