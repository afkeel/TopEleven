using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    class FieldNames
    {
        private Dictionary<string, string> fieldNames = new Dictionary<string, string>
        {
            ["MainForm"] = "TrainingCalculator",
            ["ButtonInputData"] = "Input Data",
            ["PlayerInfo"] = "PLAYER INFORMATION",
            ["RoleBox"] = "Choose a role",
            ["LabelTacklin"] = "Tacklin",
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
        };
        public string this[string i]
        {
            get { return fieldNames[i]; }
            set { fieldNames[i] = value; }
        }
    }
}
