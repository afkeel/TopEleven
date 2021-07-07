using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    class FieldNames
    {
        public enum Attributes
        {
            Tackling,
            Marking,
            Positioning,
            Heading,
            Bravery,
            Passing,
            Dribling,
            Crossing,
            Shooting,
            Finishing,
            Fitness,
            Strength,
            Aggression,
            Speed,
            Creativity
        }
        private Dictionary<string, string> fieldNames = new Dictionary<string, string>
        {
            ["MainForm"] = "TrainingCalculator",
            ["ButtonInputData"] = "Input Data",
            ["PlayerInfo"] = "PLAYER INFORMATION",
            ["RoleBox"] = "Choose a role",
            ["LabelTackling"] = "Tackling",
            ["LabelMarking"] = "Marking",
            ["LabelPositioning"] = "Positioning",
            ["LabelHeading"] = "Heading",
            ["LabelBravery"] = "Bravery",
            ["LabelPassing"] = "Passing",
            ["LabelDribling"] = "Dribling",
            ["LabelCrossing"] = "Crossing",
            ["LabelShooting"] = "Shooting",
            ["LabelFinishing"] = "Finishing",
            ["LabelFitness"] = "Fitness",
            ["LabelStrength"] = "Strength",
            ["LabelAggression"] = "Aggression",
            ["LabelSpeed"] = "Speed",
            ["LabelCreativity"] = "Creativity",
            ["ButtonOk"] = "OK",
            ["ButtonCancel"] = "Cancel"
        };
        public string this[string i]
        {
            get { return fieldNames[i]; }
            set { fieldNames[i] = value; }
        }
    }
}
