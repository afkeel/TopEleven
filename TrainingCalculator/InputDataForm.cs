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
    public partial class InputDataForm : Form
    {       
        private void InitFieldNames()
        {
            string[] textFieldNames =
            {
                "TrainingCalculator",
                "PLAYER INFORMATION",
                "Choose a role",
                "Tacklin",
                "Marking",
                "Positioning",
                "Heading",
                "Bravery",
                "Passing",
                "Dribling",
                "Crossing",
                "Shooting",
                "Finishing",
                "Fitness",
                "Strength",
                "Aggression",
                "Speed",
                "Creativity"
            };
            int i = 0;
            Text = textFieldNames[i++];
            playerInfo.Text = textFieldNames[i++];
            roleBox.Text = textFieldNames[i++];
            labelTacklin.Text = textFieldNames[i++];
            labelMarking.Text = textFieldNames[i++];
            labelPositioning.Text = textFieldNames[i++];
            labelHeading.Text = textFieldNames[i++];
            labelBravery.Text = textFieldNames[i++];
            labelPassing.Text = textFieldNames[i++];
            labelDribling.Text = textFieldNames[i++];
            labelCrossing.Text = textFieldNames[i++];
            labelShooting.Text = textFieldNames[i++];
            labelFinishing.Text = textFieldNames[i++];
            labelFitness.Text = textFieldNames[i++];
            labelStrength.Text = textFieldNames[i++];
            labelAggression.Text = textFieldNames[i++];
            labelSpeed.Text = textFieldNames[i++];
            labelCreativity.Text = textFieldNames[i++];
        }
        public InputDataForm()
        {
            InitializeComponent();
            InitFieldNames();
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
