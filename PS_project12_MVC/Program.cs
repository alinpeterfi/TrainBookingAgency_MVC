using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS_project12_MVC.Controller;

namespace PS_project12_MVC
{
    static class Program
    {
        /*
        Application for train ticket booking agencies. The application has 3 types of users: traveler, employee and administrator.
        Passenger-type users can perform the following operations without authentication:
        ❖ Viewing the list of trains by station of departure, destination, duration;
        ❖ Viewing the list of trains between 2 locations, including price and availability of free seats;
        ❖ Search for a train by number.
        Employee-type users can perform the following operations after authentication:
        ❖ All operations allowed to traveling users;
        ❖ CRUD operations regarding the persistence of trains and tickets sold;
        ❖ Selling a ticket to a traveler.
        Administrator users can perform the following operations after authentication:
        ❖ All operations allowed to traveling users;
        ❖ CRUD operations for information related to users of the application that requires authentication
        ❖ View the list of users of the application that requires authentication and filter this list by user type. 
        */


        /*
        The model is responsible for managing the data of the application. It receives user input from the controller.
        The view renders presentation of the model in a particular format.
        The controller responds to the user input and performs interactions on the data model objects. The controller receives the input, validates it and then passes the input to the model.
         */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UserC uC = new UserC();
            Application.Run(uC.getUserV());
        }
    }
}
