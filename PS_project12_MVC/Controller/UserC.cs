using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PS_project12_MVC.Model;
using PS_project12_MVC.Model.Persistent;
using PS_project12_MVC.View;

namespace PS_project12_MVC.Controller
{
    public partial class UserC
    {
        protected UserV uV;
        protected TicketP tP;

        //Controller constructor
        public UserC()
        {
            this.tP = new TicketP();
            this.uV = new UserV();
            this.setEventsUser();
        }

        //function that returns the view
        public UserV getUserV()
        {
            return this.uV;
        }

        //function that handles the events for each button
        protected void setEventsUser()
        {
            this.uV.getBtnListTrains().Click += new EventHandler(listTrains);
            this.uV.getBtnSearchTrain().Click += new EventHandler(searchTrainsByNumber);
            this.uV.getBtnSearch().Click += new EventHandler(searchTrainsByStation);
            this.uV.getBtnAuthentication().Click += new EventHandler(Authentication);
            this.uV.FormClosing += new FormClosingEventHandler(userClose);
        }

        //function to close the environment on exit
        protected void userClose(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        //function that prints the trains on the data grid -> event for btnListTrains
        protected void listTrains (object sender, EventArgs e)
        {
            List<Ticket> list = new List<Ticket>();

            list = this.tP.TicketList();
            if (list != null)
            {
                dataGridManagement(list);
            }
            else
                MessageBox.Show("The list is empty!");
        }

        //function for searching a specific train by the entered number -> event for btnSearchTrain
        protected void searchTrainsByNumber(object sender, EventArgs e)
        {
            List<Ticket> list = new List<Ticket>();
            //valdiation
            int trainNo = trainNoValidation();

            list = this.tP.FilterTicketsByTrain(trainNo);
            if (list != null)
            {
                dataGridManagement(list);
            }
            else
            {
                this.uV.getTxtTrainNo().Text = "";
                MessageBox.Show("The train does not exist!");
            }
        }

        //function that prints the trains on the data grid based on cmbOrigin and cmbDestination ComboBoc fields -> event for btnSearch
        protected void searchTrainsByStation(object sender, EventArgs e)
        {
            List<Ticket> list = new List<Ticket>();
            string origin = this.uV.getCmbOrigin().Text;
            string destination = this.uV.getCmbDestination().Text;
            list = this.tP.FilterTicketsByLocation(origin, destination);
            if (list != null)
            {
                dataGridManagement(list);
            }
            else
            {
                this.uV.getDgvUser().Rows.Clear();
                MessageBox.Show("The train does not exist!");
            }
        }

        //function used for atuhentication -> event for btnAuthentication
        protected void Authentication(object sender, EventArgs e)
        {
            if (this.uV.getBtnAuthentication().Text == "AUTHENTICATION")
            {
                AuthenticationC aC = new AuthenticationC();
                aC.getAuthenticationV().Show();
                //aV.Show();
                this.uV.Hide();
            }
            else
            {
                this.uV.Hide();
                UserV utV = new UserV();
                utV.Show();
            }
            
        }

        //function used for updating elements on the data grid view
        protected void updateTrainsList(List<DataGridViewRow> rows )
        {
            this.uV.getDgvUser().Rows.Clear();
            foreach (DataGridViewRow row in rows)
            {
                this.uV.getDgvUser().Rows.Add(row);
            }
        }

        //function used for validating the txtTrainNo field
        protected int trainNoValidation()
        {
            string s = this.uV.getTxtTrainNo().Text;
            bool isTrain = string.IsNullOrEmpty(s);
            int trainNo = 0;
            int n;
            bool isNumeric = int.TryParse(s, out n);
            if (!isTrain && isNumeric)
            {
                trainNo = Convert.ToInt32(s);
            }
            return trainNo;
        }

        //function used for adding rows to the datagridview
        protected void dataGridManagement(List<Ticket> list)
        {
            List<DataGridViewRow> row = new List<DataGridViewRow>();
            foreach (Ticket t in list)
            {
                DataGridViewRow rand = new DataGridViewRow();
                rand.Cells.Add(new DataGridViewTextBoxCell { Value = t.getTrainNo().ToString() });
                rand.Cells.Add(new DataGridViewTextBoxCell { Value = t.getOriginStation() });
                rand.Cells.Add(new DataGridViewTextBoxCell { Value = t.getDestinationStation() });
                rand.Cells.Add(new DataGridViewTextBoxCell { Value = t.getDuration().ToString() });
                rand.Cells.Add(new DataGridViewTextBoxCell { Value = t.getSeats().ToString() });
                rand.Cells.Add(new DataGridViewTextBoxCell { Value = t.getPrice().ToString() });
                rand.Cells.Add(new DataGridViewTextBoxCell { Value = t.getId().ToString() });
                row.Add(rand);
            }
            updateTrainsList(row);
        }
    }//UserC
}
