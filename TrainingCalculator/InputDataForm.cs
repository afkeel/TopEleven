using System;
using System.Collections;
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
        //public Dictionary<string, decimal> attr = new Dictionary<string, decimal>();
        public List<PlayerAttribute> attr = new List<PlayerAttribute>();
        private void InitFieldNames()
        {
            FieldNames fn = new FieldNames();
            playerInfo.Text = fn["PlayerInfo"];
            roleBox.Text = fn["RoleBox"];
            labelTackling.Text = fn["LabelTackling"];
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
            buttonOk.Text = fn["ButtonOk"];
            buttonCancel.Text = fn["ButtonCancel"];
        }
        public InputDataForm()
        {
            InitializeComponent();
            InitFieldNames();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            //attr.Add("Tackling", numericUpDownTaclin.Value);
            //attr.Add("Marking", numericUpDownMarking.Value);
            //attr.Add("Positioning", numericUpDownPositioning.Value);
            //attr.Add("Heading", numericUpDownHeading.Value);
            //attr.Add("Bravery", numericUpDownBravery.Value);

            //attr.Add("Passing", numericUpDownPassing.Value);
            //attr.Add("Dribling", numericUpDownDribling.Value);
            //attr.Add("Crossing", numericUpDownCrossing.Value);
            //attr.Add("Shooting", numericUpDownShooting.Value);
            //attr.Add("Finishing", numericUpDownFinishing.Value);

            //attr.Add("Fitness", numericUpDownFitness.Value);
            //attr.Add("Strength", numericUpDownStrength.Value);
            //attr.Add("Aggression", numericUpDownAggression.Value);
            //attr.Add("Speed", numericUpDownSpeed.Value);
            //attr.Add("Creativity", numericUpDownCreativity.Value);

            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownTaclin.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, (double)numericUpDownMarking.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownPositioning.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownHeading.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, (double)numericUpDownBravery.Value));

            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownPassing.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownDribling.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, (double)numericUpDownCrossing.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownShooting.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownFinishing.Value));

            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, (double)numericUpDownFitness.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownStrength.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, (double)numericUpDownAggression.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownSpeed.Value));
            //attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, (double)numericUpDownCreativity.Value));

            attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, 20));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, 20));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, 20));

            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, 20));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));

            attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, 20));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.GRAY, 20));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
            attr.Add(new PlayerAttribute(PlayerAttribute.Color.WHITE, 60));
        }
    }
}
