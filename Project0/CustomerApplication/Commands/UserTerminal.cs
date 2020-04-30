/// <summary>
/// The <c>UserTerminal<c> class.
/// Controls the loop for handling displaying menus and accepting console input.
/// Contains fields for <c>User</c> and <c>Location</c> information.
/// </summary>
namespace CustomerApplication
{
    public class UserTerminal
    {
        #region Fields
        public User User;
        public Location Location;
        #endregion

        #region Methods
        /// <summary>
        /// Method that controls and the main <c>UserTerminal</c> Loop.
        /// Displays the Login menu until a user is logged in.
        /// Displays the Command menu for the appropriate <c>UserType</c> when a user is logged in.
        /// </summary>
        public void Run()
        {
            //New instance of Login.
            Login login = new Login();
            while (User == null)
            {
                //Displays login menu until a user is logged in.
                User = login.DisplayLogin();
            }

            //Commands stores all commands accessible for the user.
            ICommands Commands;

            //User has access to Admin commands.
            if (User.UserType.Name == "Admin")
            {
                Commands = new AdminCommands(this);
            }
            
            //Customer user.
            else
            {
                Commands = new CustomerCommands(this);
            }
            
            //User != null while User is logged in.
            while (User != null)
            {
                Commands.DisplayCommands();
            }
        }
        #endregion
    }
}