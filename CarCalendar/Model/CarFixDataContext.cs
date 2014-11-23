using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using SQLite;
namespace CarCalendar.Model
{
    public class CarFixModel
    {
        public SQLiteConnection db;
        public async Task<bool> onCreate(string connectionString)
        {
            try
            {
                if(!CheckFileExist(connectionString).Result)
                {
                    using (db = new SQLiteConnection(connectionString))
                    {
                        db.CreateTable<CarFix>();
                    }
                }
                return true;
            }
            catch
            {
                return true;
            }
        }
        private async Task<bool> CheckFileExist(string filename)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CarFixModel(string connectionString)
        {
            int numEntries;
            db = new SQLiteConnection(connectionString);
            Debug.WriteLine("[Model] - Creating Database");
            db.CreateTable<CarFix>();
            //Debug.WriteLine("[Model] - Creating Table CarFix");
            //CarFix repair = new CarFix();
            //repair.Description = "Cambio Gomme";
            //repair.Price = 200;
            //Debug.WriteLine("[Model] - Adding objects to CarFix Table");
            //numEntries = db.Insert(repair);
            //Debug.WriteLine("[Model] - Num Entries"+numEntries.ToString());
            //CarFix repair2 = new CarFix();
            //repair2.Description = "Cambio Olio";
            //repair2.Price = 100;
            //Debug.WriteLine("[Model] - Adding objects to CarFix Table");
            //numEntries = db.Insert(repair2);
           // Debug.WriteLine("[Model] - Num Entries" + numEntries.ToString());

            var table = db.Table<CarFix>();
            foreach (var s in table)
                Debug.WriteLine(s.Id + ") " + s.Description + "-" + s.Price);
        }

        public List<CarFix> GetAllItems()
        {
            List<CarFix> Items = new List<CarFix>();
            var table = db.Table<CarFix>();
            foreach (var s in table)
            {
                CarFix item = new CarFix();
                item.Id = s.Id;
                item.Description = s.Description;
                item.Price = s.Price;
                Items.Add(item);
            }
                
            return Items;
        }
        
        public void InsertItem(CarFix repair)
        {
            db.Insert(repair);
            Debug.WriteLine("[Model] - Adding Item to database");
            var table = db.Table<CarFix>();
            foreach (var s in table)
                Debug.WriteLine(s.Id + ") " + s.Description + "-" + s.Price);
        }

        public void DeleteItem(CarFix repair)
        {
            db.Delete(repair);
        }
    }

    [Table("CarFix")]
    public class CarFix : INotifyPropertyChanged
    {

        private int _id;
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id 
        { 
            get
            { 
                return _id; 
            }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged("_id");
                }
                
            }
        }

        private string _description;
        [Column("Description")]
        public string Description 
        { 
            get
            {
                return _description;
            }
            set
            {
                if(_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged("Description");
                }
                
            }
        }

        private decimal _price;
        [Column("Price")]
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if(_price != value)
                {
                    _price = value;
                    NotifyPropertyChanged("Price");
                }
                
            }
        }
        private DateTime _date;
        [Column("Date")]
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if(_date != value)
                {
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
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
