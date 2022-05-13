using System.Windows.Forms;

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
