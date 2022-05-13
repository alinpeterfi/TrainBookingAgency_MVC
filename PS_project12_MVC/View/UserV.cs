
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
    public partial class UserV : Form
    {
        public UserV()
        {
            InitializeComponent();
        }
        //txtOrigin object
        public ComboBox getCmbOrigin()
        {
            return this.cmbOrigin;
        }
        //txtDetination object
        public ComboBox getCmbDestination()
        {
            return this.cmbDestination;
        }
        //txtTrainNo object
        public TextBox getTxtTrainNo()
        {
            return this.txtTrainNo;
        }
        //btnSearch object
        public Button getBtnSearch()
        {
            return this.btnSearch;
        }
        //btnListTrains object
        public Button getBtnListTrains()
        {
            return this.btnListTrains;
        }
        //btnAuthentication object
        public Button getBtnAuthentication()
        {
            return this.btnAuthentication;
        }
        //btnSearchTrain object
        public Button getBtnSearchTrain()
        {
            return this.btnSearchTrain;
        }
        //dgvUser object
        public DataGridView getDgvUser()
        {
            return this.dgvUser;
        }
    } //UserV
}
