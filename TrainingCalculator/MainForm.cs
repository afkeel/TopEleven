using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainingCalculator
{   
    public partial class MainForm : Form
    {          
        private void InitFieldNames()
        {
            FieldNames fn = new FieldNames();
            Text = fn["MainForm"];
            buttonInputData.Text = fn["ButtonInputData"];
        }
        public MainForm()
        {
            InitializeComponent();
            InitFieldNames();
        }
        private void buttonInputData_Click(object sender, EventArgs e)
        {
            InputDataForm idf = new InputDataForm();
            idf.ShowDialog();
            if (idf.DialogResult == DialogResult.OK)
            {
                CalculationAttributes calc = new CalculationAttributes(idf.attr);
                calc.Calculation();          
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutAProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dw = new AboutTrainingCalculator();
            dw.ShowDialog();
        }
    }
}
