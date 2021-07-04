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
            FieldNames fn = new FieldNames();
            playerInfo.Text = fn["PlayerInfo"];
            roleBox.Text = fn["RoleBox"];
            labelTacklin.Text = fn["LabelTacklin"];
            labelMarking.Text = fn["LabelMarking"];
            labelPositioning.Text = fn["LabelPositioning"];
            labelHeading.Text = fn["LabelHeading"];
            labelBravery.Text = fn["LabelBravery"];
            labelPassing.Text = fn["LabelPassing"];
            labelDribling.Text = fn["LabelDribling"];
            labelCrossing.Text = fn["LabelCrossing"];
            labelShooting.Text = fn["LabelShooting"];
            labelFinishing.Text = fn["LabelFinishing"];
            labelFitness.Text = fn["LabelFitness"];
            labelStrength.Text = fn["LabelStrength"];
            labelAggression.Text = fn["LabelAggression"];
            labelSpeed.Text = fn["LabelSpeed"];
            labelCreativity.Text = fn["LabelCreativity"];
        }
        public InputDataForm()
        {
            InitializeComponent();
            InitFieldNames();
        }
    }
}
