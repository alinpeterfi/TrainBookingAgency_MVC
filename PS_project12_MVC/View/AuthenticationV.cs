using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS_project12_MVC.View;

namespace PS_project12_MVC.View
{
    public partial class AuthenticationV : Form
    {
        public AuthenticationV()
        {
            InitializeComponent();
        }

        //txtUsername object
        public TextBox getTxtUsername()
        {
            return this.txtUsername;
        }

        //txtPassword obejct
        public TextBox getTxtPassword()
        {
            return this.txtPassword;
        }

        //btnAuthentication object
        public Button getBtnAuthentication()
        {
            return this.btnAuthentication;
        }
    }//AuthneticationV
}
