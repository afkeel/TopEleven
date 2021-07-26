using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainingCalculator
{
    public partial class MainForm : Form
    {
        public CalculationAttributes ResultCalculationAttributes { get; set; }
        public ListBox ListBoxMaxAttrsDrill => maxAttrsDrillListBox;
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
            CalculationAttributes.ProgressBarMax += (sender, args) => Invoke((Action)delegate ()
            {
                progressBar1.Maximum = (int)((CalculationAttributesEventArgs)args).CountIterations;
            });
            CalculationAttributes.ProgressBarValChanged += (sender, args) => Invoke((Action)delegate ()
            {
                progressBar1.Value += 1;
            });
        }
        private void DisplayResulTtaskCalculation(CalculationAttributes c)
        {
            ResultCalculationAttributes = c;
            Invoke((Action)delegate ()
            {
                int n = 0;
                maxAttrsDrillListBox.Items.Clear();             
                foreach (var item in c.MaxAttrsDrill)
                {
                    maxAttrsDrillListBox.Items.Add("List"+$"{n++}");
                }
            });
        }
        private void buttonInputData_Click(object sender, EventArgs e)
        {            
            InputDataForm idf = new InputDataForm();
            idf.ShowDialog();
            if (idf.DialogResult == DialogResult.OK)
            {
                CalculationAttributes calc = new CalculationAttributes(idf.attr);
                Task<CalculationAttributes> taskCalculation = new Task<CalculationAttributes>(() => calc.Calculation());
                taskCalculation.ContinueWith(t => DisplayResulTtaskCalculation(t.Result));
                taskCalculation.Start();               
            }
        }
        private void maxAttrsDrillListBox_DoubleClick(object sender, EventArgs e)
        {
            int n = maxAttrsDrillListBox.SelectedIndex;
            if (n >= 0)
            {
                drillListView.Items.Clear();
                foreach (var item in ResultCalculationAttributes.MaxAttrsDrill[n])
                {
                    drillListView.Items.Add(item.DrillName);
                }
                listView1.Columns.Clear();
                foreach (var item in ResultCalculationAttributes.MaxAttrs)
                {
                    listView1.Columns.Add(item.ToString(), 40, HorizontalAlignment.Center);
                }
                attributesListView.Columns.Clear();
                foreach (var item in ResultCalculationAttributes.MaxAttrsList[n])
                {
                    attributesListView.Columns.Add(item.ValueAttribute.ToString(), 40, HorizontalAlignment.Center);
                }
            }
            else
            {
                MessageBox.Show("Элемент не выбран!", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
