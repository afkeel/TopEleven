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
            //CalculationAttributes.ProgressBarMax += (sender, args) => Invoke((Action)delegate ()
            //{
            //    progressBar1.Maximum = (int)((CalculationAttributesEventArgs)args).CountIterations;
            //});
            //CalculationAttributes.ProgressBarValChanged += (sender, args) => Invoke((Action)delegate ()
            //{
            //    progressBar1.Value += 1;
            //});
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
                maxAttributesListView.Columns.Clear();                
                foreach (var item in ResultCalculationAttributes.MaxAttrs)
                {
                    maxAttributesListView.Columns.Add(item.ToString(), 40, HorizontalAlignment.Center);
                }
                int i = 2;
                foreach (var item in ResultCalculationAttributes.MaxAttrsList[n])
                {                   
                    attributesListView.Columns[i++].Text = item.AttributeEstimatedValue.ToString();
                }
                attributesListView.Items.Clear();
                int j = 0;
                foreach (var item in ResultCalculationAttributes.MaxAttrsDrill[n])
                {
                    ListViewItem lvi = new ListViewItem(item.DrillName)
                    {
                        Font = new Font(Font, FontStyle.Regular)
                    };                    
                    lvi.SubItems.Add(item.MaxAverageDrillQuality.ToString());
                    double[] arr = ResultCalculationAttributes.EstimatedValueList[n][j++];
                    foreach (var val in arr)
                        lvi.SubItems.Add(val.ToString());
                    attributesListView.Items.Add(lvi);
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
        private void buttonSort_Click(object sender, EventArgs e)
        {
            double sum = 0;
            int c = ResultCalculationAttributes.MaxAttrsDrill.Count;
            double[] itog = new double[c];
            for (int i = 0; i < c; i++)
            {
                sum = 0;
                foreach (var item in ResultCalculationAttributes.MaxAttrsDrill[i])
                {
                    sum += item.MaxAverageDrillQuality;
                }
                itog[i] = sum;
            }
            var orderedNumbers = from i in itog
                                 orderby i
                                 select i;
        }
    }
}
