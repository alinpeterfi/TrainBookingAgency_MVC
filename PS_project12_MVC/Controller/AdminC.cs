using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PS_project12_MVC.Model;
using PS_project12_MVC.Model.Persistent;
using PS_project12_MVC.View;


namespace PS_project12_MVC.Controller
{

    public class AdminC : UserC
    {
        
        private UserP uP;

        public AdminC()
        {
            uP = new UserP();
            uV = new AdminV();
            base.setEventsUser();
            setEvents();
            this.uV.getBtnAuthentication().Text = "LOGOUT";
        }

        //function that returns the AdminV view
        public UserV getAdminV()
        {
            return this.uV;            
        }

        //function that handles the events
        private void setEvents()
        {
            
            ((AdminV)this.uV).getBtnAddUser().Click += new EventHandler(userAdd);
            ((AdminV)this.uV).getBtnDeleteUser().Click += new EventHandler(deleteUser);
            ((AdminV)this.uV).getBtnUpdateUser().Click += new EventHandler(updateUser);
            ((AdminV)this.uV).getBtnListUser().Click += new EventHandler(viewList);
            ((AdminV)this.uV).getBtnAuthentication().Click += new EventHandler(logout);
            ((AdminV)this.uV).getDataGridView1().SelectionChanged += new EventHandler(dataGridView1Selected);
            ((AdminV)this.uV).FormClosing += new FormClosingEventHandler(adminClose);
        }


        //windows close event handler
        private void adminClose(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        //function to delete users
        private void deleteUser(object sender, EventArgs e)
        {
            string selectedUser = getSelectedUser();
            if (selectedUser == null)
                MessageBox.Show("No user selected!");
            else
            {
                bool succes = this.uP.DeleteUser(selectedUser);
                if (!succes)
                    MessageBox.Show("User not Deleted!");
                else
                    this.ViewList();
            }
        }

        //function for adding a user -> event for btnAddUser
        private void userAdd(object sender, EventArgs e)
        {
            User ut = validateUser(1);
            if (ut != null)
            {
                bool succes = this.uP.AddUser(ut);
                if (!succes)
                    MessageBox.Show("User Add Error!");
                else
                    ViewList();
            }              
        }

        //function to update a user -> event for btnUpdateUser
        private void updateUser(object sender, EventArgs e)
        {
            User ut = validateUser(0);
            string selectedUser = getSelectedUser();
            if (ut != null)
            {
                bool succes = this.uP.UpdateUser(selectedUser, ut);
                if (!succes)
                    MessageBox.Show("User Update Error!");
                else
                    this.ViewList();
            }        
        }

        //function used to view the user list -> event for btnViewUser
        private void viewList(object sender, EventArgs e)
        {
            int viewIndex = ((AdminV)this.uV).getCmbFilterUser().SelectedIndex;
            List<User> list = this.uP.ListUser(viewIndex);
            if (list != null)
            {
                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                foreach (User ut in list)
                {
                    DataGridViewRow rand = new DataGridViewRow();
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getUserName() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getPassword() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getRole() });
                    rows.Add(rand);
                }
                updateUserList(rows);
            }
            else if (viewIndex == -1)
                MessageBox.Show("Select a viewing filter!");
            else
                MessageBox.Show("List is empty!");
        }

        private void logout(object sender, EventArgs e)
        {
            this.uV.Hide();
            UserC uC = new UserC();
            uC.getUserV().Show();
        }

        //function to fill the text fields with data gathered form the dataGridView1
        private void dataGridView1Selected(object sender, EventArgs e)
        {
            if (((AdminV)this.uV).getDataGridView1().SelectedRows.Count > 0)
            {
                //get information from the dataGridView1
                string username = (string)((AdminV)this.uV).getDataGridView1().SelectedRows[0].Cells[0].Value;
                string password = (string)((AdminV)this.uV).getDataGridView1().SelectedRows[0].Cells[1].Value;
                string role = (string)((AdminV)this.uV).getDataGridView1().SelectedRows[0].Cells[2].Value;
                //fill the form fields with the selected user's data from dataGridView1
                int index = 0;
                if (role == "ADMIN")
                    index = 1;

                ((AdminV)this.uV).getTxtUser().Text = username;
                ((AdminV)this.uV).getTxtPassword().Text = password;
                ((AdminV)this.uV).getCmbRoleUser().SelectedIndex = index;
            }
            else
            {
                ((AdminV)this.uV).getTxtUser().Text = "";
                ((AdminV)this.uV).getTxtPassword().Text = "";
                ((AdminV)this.uV).getCmbRoleUser().SelectedIndex = 0;
            }
        }

        //function used to get the username from the selected row from dataGridView1
        private string getSelectedUser()
        {
            int selectedrowindex = 0;
            ((AdminV)this.uV).getTxtUser().Text = "";
            ((AdminV)this.uV).getTxtPassword().Text = "";
            ((AdminV)this.uV).getCmbRoleUser().SelectedIndex = 0;
            //if any row is selected
            if (((AdminV)this.uV).getDataGridView1().SelectedCells.Count > 0)
            {
                //return the name for deletion
                selectedrowindex = ((AdminV)this.uV).getDataGridView1().SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = ((AdminV)this.uV).getDataGridView1().Rows[selectedrowindex];
                return selectedRow.Cells["User"].Value.ToString();
            }
            else
                return null;
        }

        //function to view users on the dataGridView1 != viewList() used for event handling
        private void ViewList()
        {
            List<User> list = this.uP.ListUser(0);
            if (list != null)
            {
                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                foreach (User ut in list)
                {
                    DataGridViewRow rand = new DataGridViewRow();
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getUserName() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getPassword() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getRole() });
                    rows.Add(rand);
                }
                updateUserList(rows);
            }
            else
                MessageBox.Show("List is empty!");
        }

        //function to update dataGridView1
        private void updateUserList(List<DataGridViewRow> list)
        {
            ((AdminV)this.uV).getDataGridView1().Rows.Clear();
            foreach (DataGridViewRow row in list)
                ((AdminV)this.uV).getDataGridView1().Rows.Add(row);
        }

        //function used for validation //0 for addUser functionality //1 for updateUser functionality
        private User validateUser(int command)
        {
            //fetch data from the input fields
            int selectedRole = ((AdminV)this.uV).getCmbRoleUser().SelectedIndex;
            string username = ((AdminV)this.uV).getTxtUser().Text;
            string password = ((AdminV)this.uV).getTxtPassword().Text;
            string selectedUser = getSelectedUser();
            //if no role is selected
            if (selectedRole < 0)
            {
                MessageBox.Show("No role selected!");
                return null;
            }
            //if the fields are empty
            else if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Empty username or password!");
                return null;
            }
            else
            {
                //role setting based on index
                string role = "EMPLOYEE";
                if (selectedRole == 1)
                    role = "ADMIN";
                //conditional validation
                if (command == 1)
                {
                    if (this.uP.SearchUser(username) != null)
                    {
                        MessageBox.Show("Account name already exists!");
                        return null;
                    }
                    return new User(username, password, role);
                }//for addUser
                if (command == 0)
                {
                    //if the username field is different than the username from the dataGridView1 cell
                    if (username != selectedUser)
                    {//return the username from the text field 
                        return new User(username, password, role);
                    }
                    else
                    {
                        return new User(selectedUser, password, role);
                    }
                }//for updateUser     
            }
            return null;
        }
    }//AdminC
}
