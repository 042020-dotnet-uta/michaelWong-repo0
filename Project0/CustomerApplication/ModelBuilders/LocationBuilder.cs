using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class LocationBuilder
    {
        #region Fields
        private static Regex NameRx = new Regex(@"^\w[\w\s\p{P}]*$");
        private String _name;
        private String Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(NameRx.IsMatch(value.Trim())) _name = value.Trim();
                else throw new FormatException("Invalid location name.");
            }
        }
        #endregion

        #region Methods
        public Location BuildLocation()
        {
            Console.WriteLine("Creating a New Location:\n");
            try
            {
                NameInput();
                using(var db = new CustomerApplicationContext())
                {
                    Location l = db.Locations.Add(new Location {Name = Name}).Entity;
                    db.SaveChanges();
                    return l;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n" + ex.Message);
                return null;
            }
        }

        public void NameInput()
        {
            Console.WriteLine("Enter Location Name:");
            Name = Console.ReadLine();
        }
        #endregion
    }
}