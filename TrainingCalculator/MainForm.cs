using System;
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
        List<List<PlayerAttribute>> list = new List<List<PlayerAttribute>>();

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
            //{ progressBar1.Maximum = (int)((CalculationAttributesEventArgs)args).CountIterations;
            //});
            //CalculationAttributes.ProgressBarValChanged += (sender, args) => Invoke((Action)delegate ()
            //{
            //    progressBar1.Value += 1;
            //});
        }
        private void buttonInputData_Click(object sender, EventArgs e)
        {
            
            InputDataForm idf = new InputDataForm();
            idf.ShowDialog();
            if (idf.DialogResult == DialogResult.OK)
            {
                CalculationAttributes calc = new CalculationAttributes(idf.attr);
                //calc.Calculation();

                Thread t = new Thread(calc.Calculation)
                {
                    Name = "Calculation"
                };
                t.Start();

                listBox1.Items.Clear();
                //list = calc.maxAttrsList;
                
                foreach (var item in calc.maxAttrsDrill)
                {
                    listBox1.Items.Add(item);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //listView2.Items.Add("60");
            //listView2.Items.Add("20");
            //listView2.Items.Add("120");
            //listView2.Items.Add("30");
            //listView2.Items.Add("60");

            int nom = listBox1.SelectedIndex;
            if (nom >= 0)
            {
                listView1.Items.Clear();
                foreach (var item in (List<Drill>)listBox1.SelectedItem)
                {
                    listView1.Items.Add(item.DrillName);
                }
                //listView2.Items.Clear();
                //foreach (var item in list[nom])
                //{
                //    listView2.Items.Add(item.ValueAttribute.ToString());                   
                //}

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
