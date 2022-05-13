using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS_project12_MVC.View
{
    public partial class EmployeeV : UserV
    {
        //constructor

        public EmployeeV()
        {
            InitializeComponent();
        }

        //txtTrainE object
        public TextBox getTxtTrainE()
        {
            return this.txtTrainE;
        }

        //txtOrigin object
        public TextBox getTxtOriginE()
        {
            return this.txtOriginE;
        }

        //txtDestination obejct
        public TextBox getTxtDestinationE()
        {
            return this.txtDestinationE;
        }

        //txtDuration object
        public TextBox getTxtDurationE()
        {
            return this.txtDurationE;
        }

        //txtSeats object
        public TextBox getTxtSeatsE()
        {
            return this.txtSeatsE;
        }

        //txtPrice object
        public TextBox getTxtPriceE()
        {
            return this.txtPriceE;
        }

        //txtId object
        public TextBox getTxtIdE()
        {
            return this.txtIdE;
        }

        //btnAddTicket object
        public Button getBtnAddTicket()
        {
            return this.btnAddTicket;
        }

        //btnDeleteTicket object
        public Button getBtnDeleteTicket()
        {
            return this.btnDeleteTicket;
        }

        //btnUpdateTicket object
        public Button getBtnUpdateTicket()
        {
            return this.btnUpdateTicket;
        }

        //btnSell object
        public Button getBtnSell()
        {
            return this.btnSell;
        }

        //btnRefill object
        public Button getBtnRefill()
        {
            return this.btnRefill;
        }
    }
}
