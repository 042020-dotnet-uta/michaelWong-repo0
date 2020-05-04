using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.Models;

/// <summary>
/// Controls database access related to <c>Location</c> objects.
/// Used by classes in the business logic layer.
/// </summary>
namespace CustomerApplication.DbAccess
{
    public class LocationDb
    {
        #region Fields
        //Used for validation when inputting location names.
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
                if (NameRx.IsMatch(value.Trim())) _name = value.Trim();
                else throw new FormatException("Invalid location name.");
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Validates console input to instantiate a new location and insert it into the database.
        /// </summary>
        /// <returns>
        /// null if console input fails validation.
        /// Location object if new location inserted into database.
        /// </returns>
        public Location Build()
        {
            Console.WriteLine("Creating a New Location:\n");

            //Console input.
            NameInput();
            using (var db = new CustomerApplicationContext())
            {
                //Creates a new Location instance and inserts into database.
                Location location = db.Locations.Add(new Location(Name)).Entity;
                db.SaveChanges();
                return location;
            }

        }

        /// <summary>
        /// Gets console input for location name.
        /// </summary>
        public void NameInput()
        {
            Console.Write("Enter Location Name:\n> ");
            Name = Console.ReadLine();
        }
        #endregion

        public Location GetLocation(int id)
        {

            using (var db = new CustomerApplicationContext())
            {
                return db.Locations.Find(id);
            }

        }

        /// <summary>
        /// Loads all <c>Location</c> from the database.
        /// </summary>
        /// <returns>
        /// Collection of <c>Location</c>.
        /// </returns>
        public ICollection<Location> GetLocations()
        {

            using (var db = new CustomerApplicationContext())
            {
                return db.Locations
                    .AsNoTracking()
                    .ToList();
            }

        }

        /// <summary>
        /// Finds and removes a <c>Location</c> from the database.
        /// Removes by Id.
        /// </summary>
        /// <param name="locationId">Id of the location object to be deleted from the database.</param>
        /// <returns>Returns the Location object removed.</returns>
        public Location RemoveLocation(int locationId)
        {
            using (var db = new CustomerApplicationContext())
            {
                var location = db.Locations.Find(locationId);
                db.Locations.Remove(location);
                db.SaveChanges();
                return location;
            }
        }
    }
}