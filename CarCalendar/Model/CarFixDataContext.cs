using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SQLite;
namespace CarCalendar.Model
{
    public class CarFixDataContext
    {
        public CarFixDataContext(string connectionString)
        {
            int numEntries;
            var db = new SQLiteConnection(connectionString);
            Debug.WriteLine("[Model] - Creating Database");
            db.CreateTable<CarFix>();
            Debug.WriteLine("[Model] - Creating Table CarFix");
            CarFix repair = new CarFix();
            repair.Description = "Cambio Gomme";
            repair.Price = 200;
            Debug.WriteLine("[Model] - Adding objects to CarFix Table");
            numEntries = db.Insert(repair);
            Debug.WriteLine("[Model] - Num Entries"+numEntries.ToString());
            CarFix repair2 = new CarFix();
            repair2.Description = "Cambio Olio";
            repair2.Price = 100;
            Debug.WriteLine("[Model] - Adding objects to CarFix Table");
            numEntries = db.Insert(repair2);
            Debug.WriteLine("[Model] - Num Entries" + numEntries.ToString());

            var table = db.Table<CarFix>();
            foreach (var s in table)
                Debug.WriteLine(s.Id + ") " + s.Description + "-" + s.Price);
        }
    }

    [Table("CarFix")]
    public class CarFix
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Price")]
        public decimal Price{get;set;}

    }
}
