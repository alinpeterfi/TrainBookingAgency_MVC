using System.Windows.Forms;

namespace PS_project12_MVC.View
{
    public partial class AdminV : UserV
    {
      
        public AdminV()
        {
            InitializeComponent();
        }

        //txtUser object
        public TextBox getTxtUser()
        {
            return this.txtUser;
        }

        //txtPassword object
        public TextBox getTxtPassword()
        {
            return this.txtPassword;
        }

        //cbmRoleUser object
        public ComboBox getCmbRoleUser()
        {
            return this.cmbRoleUser;
        }
        
        //cmbFilterUser object
        public ComboBox getCmbFilterUser()
        {
            return this.cmbFilterUser;
        }

        //btnAddUser object//I changed the reference in case it breaks
        public Button getBtnAddUser()
        {
            return this.btnAddUser;
        }

        //btnDeleteUser object
        public Button getBtnDeleteUser()
        {
            return this.btnDeleteUser;
        }

        //btnUpdateUser object
        public Button getBtnUpdateUser()
        {
            return this.btnUpdateUser;
        }

        //btnListUser object
        public Button getBtnListUser()
        {
            return this.btnListUser;
        }

        //dataGridView1 object
        public DataGridView getDataGridView1()
        {
            return this.dataGridView1;
        }
    }//AdminV
}
