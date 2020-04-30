using System;
using System.Text.RegularExpressions;

namespace CustomerApplication
{
    public class LocationBuilder
    {
        #region Fields
        private static Regex NameRx = new Regex(@"^\w[\w\s\p{P}]{0,49}$");
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
        public Location Build()
        {
            Console.WriteLine("Creating a New Location:\n");
            try
            {
                NameInput();
                using(var db = new CustomerApplicationContext())
                {
                    Location location = db.Locations.Add(new Location(Name)).Entity;
                    db.SaveChanges();
                    return location;
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
            Console.Write("Enter Location Name:\n> ");
            Name = Console.ReadLine();
        }
        #endregion
    }
}