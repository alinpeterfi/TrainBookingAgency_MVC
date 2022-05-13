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
