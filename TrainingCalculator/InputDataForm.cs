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
    using PA = PlayerAttribute;
    public partial class InputDataForm : Form
    {
        public List<PA> attr = new List<PA>();
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
            attr.Add(new PA(PA.Attributes.Tackling, PA.Color.GRAY, 20));
            attr.Add(new PA(PA.Attributes.Marking,PA.Color.GRAY, 20));
            attr.Add(new PA(PA.Attributes.Positioning,PA.Color.WHITE, 60));
            attr.Add(new PA(PA.Attributes.Heading,PA.Color.WHITE, 60));
            attr.Add(new PA(PA.Attributes.Bravery,PA.Color.GRAY, 20));

            attr.Add(new PA(PA.Attributes.Passing,PA.Color.WHITE, 60));
            attr.Add(new PA(PA.Attributes.Dribling,PA.Color.WHITE, 60));
            attr.Add(new PA(PA.Attributes.Crossing, PA.Color.GRAY, 20));
            attr.Add(new PA(PA.Attributes.Shooting,PA.Color.WHITE, 60));
            attr.Add(new PA(PA.Attributes.Finishing,PA.Color.WHITE, 60));

            attr.Add(new PA(PA.Attributes.Fitness,PA.Color.GRAY, 20));
            attr.Add(new PA(PA.Attributes.Strength,PA.Color.WHITE, 60));
            attr.Add(new PA(PA.Attributes.Aggression,PA.Color.GRAY, 20));
            attr.Add(new PA(PA.Attributes.Speed,PA.Color.WHITE, 60));
            attr.Add(new PA(PA.Attributes.Creativity,PA.Color.WHITE, 60));
        }
    }
}
