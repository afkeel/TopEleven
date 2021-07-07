using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    using FNAttr = FieldNames.Attributes;
    class CalculationAttributes
    {
        public CalculationAttributes(List<PlayerAttribute> attr)
        {
            Attributes = attr;
        }
        public List<PlayerAttribute> Attributes { get; }
        private bool CheackMaxVal(ref bool[,] maskAtr, ref int index)
        {
            int max = 60;
            bool ret = true;
            for (int i = 0; i < Attributes.Count; i++)
            {
                if (Attributes[i].ColorAttribute == PlayerAttribute.Color.GRAY
                    && maskAtr[index,i]
                    && Attributes[i].ValueAttribute >= max)
                {
                    ret = false;
                    break;
                }                    
            }
            return ret;
        }
        public void Calculation()
        {
            int[] PASS_GO_AND_SHOOT = { (int)FNAttr.Passing, (int)FNAttr.Shooting, (int)FNAttr.Speed };
            int[] FAST_COUNTER_ATTACKS = { (int)FNAttr.Passing, (int)FNAttr.Crossing, (int)FNAttr.Finishing, (int)FNAttr.Creativity };
            int[] SKILL_DRILL = { (int)FNAttr.Heading, (int)FNAttr.Dribling, (int)FNAttr.Creativity };
           
            Dictionary<int, int[]> training = new Dictionary<int, int[]>
            {
                [0] = PASS_GO_AND_SHOOT,
                [1] = FAST_COUNTER_ATTACKS,
                [2] = SKILL_DRILL
            };

            int lenTrain = training.Count();
            int lenAttr = Attributes.Count();
            bool[,] maskAtr = new bool[lenTrain, lenAttr];
            int indexTraining = 0;

            for (int i = 0; i < lenTrain; i++)
            {
                for (int j = 0; j < training[i].Length; j++)
                {
                    maskAtr[i, training[i][j]] = true;
                }
            }
            for (int ii = 0; ii < lenTrain; ii++)
            {
                int sum;
                int count;
                do
                {
                    sum = 0;
                    count = 0;
                    for (int jj = 0; jj < lenAttr; jj++)
                    {
                        if (maskAtr[ii, jj])
                        {
                            if (Attributes[jj].ColorAttribute == PlayerAttribute.Color.WHITE)
                            {
                                ++Attributes[jj].ValueAttribute;
                            }
                            else
                            {
                                Attributes[jj].ValueAttribute += 0.5;
                            }
                            sum += (int)Attributes[jj].ValueAttribute;
                            ++count;
                        }
                    }                   
                } while (sum+count <= count * 180 && CheackMaxVal(ref maskAtr, ref indexTraining));
                ++indexTraining;
            }           
        }
    }
}
