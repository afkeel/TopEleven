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
            int[] SHOOTING_TECHNIQUE = { (int)FNAttr.Shooting, (int)FNAttr.Finishing, (int)FNAttr.Strength };
            int[] SET_PIECE_DELIVERY = { (int)FNAttr.Marking, (int)FNAttr.Heading, (int)FNAttr.Crossing, (int)FNAttr.Shooting };           
            int[] SLALOM_DRIBBLE = { (int)FNAttr.Passing, (int)FNAttr.Dribling, (int)FNAttr.Fitness, (int)FNAttr.Speed };
            int[] WING_PLAY = { (int)FNAttr.Heading, (int)FNAttr.Crossing, (int)FNAttr.Shooting, (int)FNAttr.Finishing };
            int[] ONE_ON_ONE_FINISHING = { (int)FNAttr.Tackling, (int)FNAttr.Dribling, (int)FNAttr.Finishing };
            
            int[] PRESS_THE_PLAY = { (int)FNAttr.Tackling, (int)FNAttr.Marking, (int)FNAttr.Positioning, (int)FNAttr.Bravery, (int)FNAttr.Aggression };
            int[] PIGGY_IN_THE_MIDDLE = { (int)FNAttr.Tackling, (int)FNAttr.Positioning, (int)FNAttr.Passing, (int)FNAttr.Fitness, (int)FNAttr.Aggression };
            int[] USE_YOUR_HEAD = { (int)FNAttr.Positioning, (int)FNAttr.Heading, (int)FNAttr.Passing, (int)FNAttr.Creativity };
            int[] STOP_THE_ATTACKER = { (int)FNAttr.Tackling, (int)FNAttr.Marking, (int)FNAttr.Bravery, (int)FNAttr.Dribling, (int)FNAttr.Strength };
            int[] DEFENDING_CROSSES = { (int)FNAttr.Marking, (int)FNAttr.Heading, (int)FNAttr.Bravery, (int)FNAttr.Crossing };
            int[] VIDEO_ANALYSIS = { (int)FNAttr.Positioning, (int)FNAttr.Bravery, (int)FNAttr.Creativity };
            int[] HOLD_THE_LINE = { (int)FNAttr.Marking, (int)FNAttr.Positioning };

            int[] WARM_UP = { (int)FNAttr.Heading, (int)FNAttr.Fitness, (int)FNAttr.Aggression };
            int[] STRETCH = { (int)FNAttr.Fitness, (int)FNAttr.Strength, (int)FNAttr.Speed };
            int[] SPRINT = { (int)FNAttr.Dribling, (int)FNAttr.Fitness, (int)FNAttr.Speed };
            int[] CARIOCA_WITH_LADDERS = { (int)FNAttr.Aggression, (int)FNAttr.Speed};
            int[] LONG_RUN = { (int)FNAttr.Fitness, (int)FNAttr.Speed };
            int[] GYM = { (int)FNAttr.Fitness, (int)FNAttr.Strength };
            int[] SHUTTLE_RUNS = { (int)FNAttr.Bravery, (int)FNAttr.Strength, (int)FNAttr.Speed };
            int[] HURDLE_JUMPS = { (int)FNAttr.Bravery, (int)FNAttr.Aggression, (int)FNAttr.Speed };

            Dictionary<int, int[]> training = new Dictionary<int, int[]>();
            training.Add(0, PASS_GO_AND_SHOOT);
            training.Add(1, FAST_COUNTER_ATTACKS);
            training.Add(2, SKILL_DRILL);
            training.Add(3, SHOOTING_TECHNIQUE);
            training.Add(4, SET_PIECE_DELIVERY);
            training.Add(5, SLALOM_DRIBBLE);
            training.Add(6, WING_PLAY);
            training.Add(7, ONE_ON_ONE_FINISHING);
            
            training.Add(8, PRESS_THE_PLAY);
            training.Add(9, PIGGY_IN_THE_MIDDLE);
            training.Add(10, USE_YOUR_HEAD);
            training.Add(11, STOP_THE_ATTACKER);
            training.Add(12, DEFENDING_CROSSES);
            training.Add(13, VIDEO_ANALYSIS);
            training.Add(14, HOLD_THE_LINE);
            
            training.Add(15, WARM_UP);
            training.Add(16, STRETCH);
            training.Add(17, SPRINT);
            training.Add(18, CARIOCA_WITH_LADDERS);
            training.Add(19, LONG_RUN);
            training.Add(20, GYM);
            training.Add(21, SHUTTLE_RUNS);
            training.Add(22, HURDLE_JUMPS);
            
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
                double sum = 0;
                int count = 0;
                for (int iii = 0; iii < lenAttr; iii++)
                {
                    if (maskAtr[ii,iii])
                    {
                        sum += Attributes[iii].ValueAttribute;
                        ++count;
                    }                                                      
                }                
                while (sum + count <= count * 180 && CheackMaxVal(ref maskAtr, ref indexTraining))
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
                }         
                ++indexTraining;
            }           
        }
    }
}
