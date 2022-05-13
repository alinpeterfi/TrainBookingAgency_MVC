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
    public class EmployeeC : UserC
    {
        private TicketP tiP;
        
        //constructor
        public EmployeeC()
        {
            base.setEventsUser();
            this.tiP = new TicketP();
            uV = new EmployeeV();
            base.setEventsUser();
            setEventsEmp();
            this.uV.getBtnAuthentication().Text = "LOGOUT";
        }

        //function that returns the EmployeeV view
        public UserV getEmployeeV()
        {
            return this.uV;
        }

        //function that handles the events
        private void setEventsEmp()
        {
            
            ((EmployeeV)this.uV).getBtnAddTicket().Click += new EventHandler(addTicket);
            ((EmployeeV)this.uV).getBtnDeleteTicket().Click += new EventHandler(deleteTicket);
            ((EmployeeV)this.uV).getBtnUpdateTicket().Click += new EventHandler(updateTicket);
            ((EmployeeV)this.uV).getBtnSell().Click += new EventHandler(sellTicket);
            ((EmployeeV)this.uV).getBtnRefill().Click += new EventHandler(refillTicket);
            ((EmployeeV)this.uV).getDgvUser().SelectionChanged += new EventHandler(dgvUserSelected);
            ((EmployeeV)this.uV).getBtnAuthentication().Click += new EventHandler(logout);
            ((EmployeeV)this.uV).FormClosing += new FormClosingEventHandler(empClose);
        }

        //windows close event handler
        private void empClose(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        //logout function
        private void logout(object sender, EventArgs e)
        {
            this.uV.Hide();
            UserC uC = new UserC();
            uC.getUserV().Show();
        }
        //function used for adding a ticket -> event for btnAddTicket
        private void addTicket(object sender, EventArgs e)
        {
            string trainString = ((EmployeeV)this.uV).getTxtTrainE().Text;
            string origin = ((EmployeeV)this.uV).getTxtOriginE().Text;
            string destination = ((EmployeeV)this.uV).getTxtDestinationE().Text;
            string durationString = ((EmployeeV)this.uV).getTxtDurationE().Text;
            string seatsString = ((EmployeeV)this.uV).getTxtSeatsE().Text;
            string priceString = ((EmployeeV)this.uV).getTxtPriceE().Text;
            string idString = ((EmployeeV)this.uV).getTxtIdE().Text;

            Ticket ut = validateTicket(trainString, origin, destination, durationString, seatsString, priceString, idString, 0);  //1 for update //0 for add
            if (ut != null)
            {
                bool succes = this.tiP.AddTicket(ut);
                if (!succes)
                    MessageBox.Show("User Add Error!");
                else
                {
                    viewList();
                    MessageBox.Show("User added successfully!");
                    updateLocations();
                }
            }
        }

        //function used for selling a ticket -> event for btnSell
        private void sellTicket(object sender, EventArgs e)
        {
            sellTicketAction(0); //0 for selling
        }

        //function used for refilling ticket quantity -> event for btnRefill
        private void refillTicket(object sender, EventArgs e)
        {
            sellTicketAction(20);//refill 20 tickets
        }


        //function used to fill the text fields with data gathered form the dataGridView1
        private void dgvUserSelected(object sender, EventArgs e)
        {
            if (((EmployeeV)this.uV).getDgvUser().SelectedRows.Count > 0)
            {
                //get information from the dataGridView1
                int trainNo = Convert.ToInt32(this.uV.getDgvUser().SelectedRows[0].Cells[0].Value);
                string origin = (string)this.uV.getDgvUser().SelectedRows[0].Cells[1].Value;
                string destination = (string)this.uV.getDgvUser().SelectedRows[0].Cells[2].Value;
                int duration = Convert.ToInt32(this.uV.getDgvUser().SelectedRows[0].Cells[3].Value);
                int seats = Convert.ToInt32(this.uV.getDgvUser().SelectedRows[0].Cells[4].Value);
                double price = Convert.ToDouble(this.uV.getDgvUser().SelectedRows[0].Cells[5].Value);
                int id = Convert.ToInt32(this.uV.getDgvUser().SelectedRows[0].Cells[6].Value);
                //fill the form fields with the selected user's data from dataGridView1
                ((EmployeeV)this.uV).getTxtTrainE().Text = trainNo.ToString();
                ((EmployeeV)this.uV).getTxtOriginE().Text = origin;
                ((EmployeeV)this.uV).getTxtDestinationE().Text = destination;
                ((EmployeeV)this.uV).getTxtDurationE().Text = duration.ToString();
                ((EmployeeV)this.uV).getTxtSeatsE().Text = seats.ToString();
                ((EmployeeV)this.uV).getTxtPriceE().Text = price.ToString();
                ((EmployeeV)this.uV).getTxtIdE().Text = id.ToString();
            }
            else
            { //empty fields
                ((EmployeeV)this.uV).getTxtTrainE().Text = "";
                ((EmployeeV)this.uV).getTxtOriginE().Text = "";
                ((EmployeeV)this.uV).getTxtDestinationE().Text = "";
                ((EmployeeV)this.uV).getTxtDurationE().Text = "";
                ((EmployeeV)this.uV).getTxtSeatsE().Text = "";
                ((EmployeeV)this.uV).getTxtPriceE().Text = "";
                ((EmployeeV)this.uV).getTxtIdE().Text = "";
            }
        }

        //function used to update the combo boxes the users see with the entered ticket locations
        private void updateLocations()
        {
            if (!((EmployeeV)this.uV).getCmbOrigin().Items.Contains(((EmployeeV)this.uV).getTxtOriginE())) //if the location doesn't exist in cmbOrigin
                ((EmployeeV)this.uV).getCmbOrigin().Items.Add(((EmployeeV)this.uV).getTxtOriginE());
            if (!((EmployeeV)this.uV).getCmbDestination().Items.Contains(((EmployeeV)this.uV).getTxtDestinationE())) //if the location doesn't exist in cmbDestination
                ((EmployeeV)this.uV).getCmbDestination().Items.Add(((EmployeeV)this.uV).getTxtDestinationE());
        }

        //function used to delete a ticket -> event for btnDeleteTicket
        private void deleteTicket(object sender, EventArgs e)
        {
            int selectedTicket = getSelectedTrain();
            if (selectedTicket == -1)
            {
                MessageBox.Show("No ticket selected!");
            }
            else
            {
                bool succes = this.tiP.DeleteTicket(selectedTicket);
                if (!succes)
                {
                    MessageBox.Show("Ticket Delete Error!");
                }
                else
                {
                    this.viewList();
                    MessageBox.Show("Ticket deleted!");
                }
            }
        }

        //function used for updating a ticket -> event for btnUpdateTicket
        private void updateTicket(object sender, EventArgs e)
        {
            int SelectedTicket = getSelectedTrain();
            if (SelectedTicket == -1)
            {
                MessageBox.Show("No user selected!");
            }
            else
            {
                //get data as strings
                string trainString = ((EmployeeV)this.uV).getTxtTrainE().Text;
                string origin = ((EmployeeV)this.uV).getTxtOriginE().Text;
                string destination = ((EmployeeV)this.uV).getTxtDestinationE().Text;
                string durationString = ((EmployeeV)this.uV).getTxtDurationE().Text;
                string seatsString = ((EmployeeV)this.uV).getTxtSeatsE().Text;
                string priceString = ((EmployeeV)this.uV).getTxtPriceE().Text;
                string idString = ((EmployeeV)this.uV).getTxtIdE().Text;
                //input validation
                Ticket ut = this.validateTicket(trainString, origin, destination, durationString, seatsString, priceString, idString, 1); //1 for update //0 for add
                if (ut != null)
                {
                    bool succes = this.tiP.UpdateTicket(SelectedTicket, ut);
                    if (!succes)
                        MessageBox.Show("Ticket Update Error!");
                    else
                    {
                        this.viewList();
                        MessageBox.Show("Ticket updated successfully!");
                        updateLocations();
                    }
                }
            }
        }

        //function used to get the selected train from dgvUser data grid view
        private int getSelectedTrain()
        {
            if (((EmployeeV)this.uV).getDgvUser().SelectedCells.Count > 0)
            {
                int selectedrowindex = ((EmployeeV)this.uV).getDgvUser().SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = ((EmployeeV)this.uV).getDgvUser().Rows[selectedrowindex];
                return Convert.ToInt32(selectedRow.Cells["TicketId"].Value);
            }
            else
                return -1;
        }

        //function used to validate the data prior to adding / updating a ticket
        private Ticket validateTicket(string trainString, string origin, string destination, string durationString, string seatsString, string priceString, string idString, int isUpdate) 
        {
            int n, trainNo = 0, duration = 0, seats = 0, id = 0;
            double y, price = 0;
            bool isCorrect = true;
            //null validation
            if (string.IsNullOrEmpty(trainString) || string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination) || string.IsNullOrEmpty(durationString) || string.IsNullOrEmpty(seatsString) || string.IsNullOrEmpty(priceString) || string.IsNullOrEmpty(idString))
            {
                MessageBox.Show("All the fields are required!");
                isCorrect = false;
                return null;
            }
            else 
            {
                //number validation
                bool isTrain = int.TryParse(trainString, out n);
                bool isSeats = int.TryParse(seatsString, out n);
                bool isDuration = int.TryParse(durationString, out n);
                bool isPrice = double.TryParse(priceString, out y);
                bool isId = int.TryParse(idString, out n);
                //data type conversion
                if (isTrain && isSeats && isDuration && isPrice && isId)//valid input
                {
                    trainNo = Convert.ToInt32(trainString);
                    duration = Convert.ToInt32(durationString);
                    seats = Convert.ToInt32(seatsString);
                    price = Convert.ToDouble(priceString);
                    id = Convert.ToInt32(idString);
                }
                else
                {
                    isCorrect = false;
                    MessageBox.Show("Fields 'Train No', 'duration','seats', 'price', 'id' must be numbers!");
                    return null;
                }
                //isUpdate used for addTicket
                if (this.tiP.TicketSearch(id) != null && isUpdate == 0)
                {
                    isCorrect = false;
                    MessageBox.Show("The ticket number already exists!");
                    return null;
                }
                else
                {
                    if (isCorrect)
                    {
                        return new Ticket(trainNo, origin, destination, duration, seats, price, id);
                    }
                }
            }
            return null;
        }

        //function used to view the ticket list -> event for btnViewTicket
        private void viewList()
        {
            List<Ticket> list = this.tiP.TicketList();
            if (list != null)
            {
                List<DataGridViewRow> rows = new List<DataGridViewRow>();
                foreach (Ticket ut in list)
                {
                    DataGridViewRow rand = new DataGridViewRow();
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getTrainNo() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getOriginStation() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getDestinationStation() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getDuration() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getSeats() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getPrice() });
                    rand.Cells.Add(new DataGridViewTextBoxCell { Value = ut.getId() });
                    rows.Add(rand);
                }
                updateTicketList(rows);
            }
            else
                MessageBox.Show("List is empty!");
        }

        //function used for selling / refilling
        private void sellTicketAction(int offset)
        {
            int SelectedTicket = getSelectedTrain();
            if (SelectedTicket == -1)
            {
                MessageBox.Show("No user selected!");
            }
            else
            {
                Ticket ut = this.tiP.TicketSell(SelectedTicket, offset);
                if (ut == null)
                {
                    MessageBox.Show("Tickets are sold out!");
                }
                else
                {
                    bool succes = this.tiP.UpdateTicket(SelectedTicket, ut);
                    if (!succes)
                        MessageBox.Show("Ticket Sell Error!");
                    else
                    {
                        this.viewList();
                        MessageBox.Show("Ticket updated successfully!");
                    }
                }
            }
        }

        //function used to update dgvUser data grif view
        public void updateTicketList(List<DataGridViewRow> list)
        {
            ((EmployeeV)this.uV).getDgvUser().Rows.Clear();
            foreach (DataGridViewRow row in list)
                ((EmployeeV)this.uV).getDgvUser().Rows.Add(row);
        }

    }//UserC
}
