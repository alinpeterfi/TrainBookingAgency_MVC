using System;
using System.Windows.Forms;
using PS_project12_MVC.Model;
using PS_project12_MVC.Model.Persistent;
using PS_project12_MVC.View;


namespace PS_project12_MVC.Controller
{
    public class AuthenticationC
    {
        private AuthenticationV aV;
        private UserP uP;

        //controller constructor
        public AuthenticationC()
        {
            this.aV = new AuthenticationV();
            this.uP = new UserP();
            this.setEvents();
        }

        //function that returns the view
        public AuthenticationV getAuthenticationV()
        {
            return this.aV;
        }

        //function that handles the events for each button
        private void setEvents()
        {
            this.aV.getBtnAuthentication().Click += new EventHandler(login);
            this.aV.FormClosed += new FormClosedEventHandler(authClose);
        }

        //window close handler
        private void authClose(object sender, FormClosedEventArgs e)
        {     
                UserC utC = new UserC();
                utC.getUserV().Show();
        }

        //login function -> event for btnAuthnetication
        private void login(object sender, EventArgs e)
        {
            string user = this.aV.getTxtUsername().Text;
            string password = this.aV.getTxtPassword().Text;
            User ut = this.uP.SearchUser(user, password);
            //user does not exist
            if (ut == null)
            {
                MessageBox.Show("Wrong credentials!");
                this.aV.getTxtUsername().Text = "";
                this.aV.getTxtPassword().Text = "";
            }
            else
            {//login based on role
                this.aV.Hide();
                string rol = ut.getRole();
                if (rol.ToUpper() == "ADMIN")
                {
                    AdminC adC = new AdminC();
                    adC.getAdminV().Show();
                }
                else
                {
                    EmployeeC empC = new EmployeeC();
                    empC.getEmployeeV().Show();
                }
            }
        }
    }//AuthenticationC
}
