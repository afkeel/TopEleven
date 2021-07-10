using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    using FNAttr = FieldNames.Attributes;
    class CalculationAttributes
    {
        public int[] PASS_GO_AND_SHOOT = { (int)FNAttr.Passing, (int)FNAttr.Shooting, (int)FNAttr.Speed };
        public int[] FAST_COUNTER_ATTACKS = { (int)FNAttr.Passing, (int)FNAttr.Crossing, (int)FNAttr.Finishing, (int)FNAttr.Creativity };
        public int[] SKILL_DRILL = { (int)FNAttr.Heading, (int)FNAttr.Dribling, (int)FNAttr.Creativity };
        public int[] SHOOTING_TECHNIQUE = { (int)FNAttr.Shooting, (int)FNAttr.Finishing, (int)FNAttr.Strength };
        public int[] SET_PIECE_DELIVERY = { (int)FNAttr.Marking, (int)FNAttr.Heading, (int)FNAttr.Crossing, (int)FNAttr.Shooting };
        public int[] SLALOM_DRIBBLE = { (int)FNAttr.Passing, (int)FNAttr.Dribling, (int)FNAttr.Fitness, (int)FNAttr.Speed };
        public int[] WING_PLAY = { (int)FNAttr.Heading, (int)FNAttr.Crossing, (int)FNAttr.Shooting, (int)FNAttr.Finishing };
        public int[] ONE_ON_ONE_FINISHING = { (int)FNAttr.Tackling, (int)FNAttr.Dribling, (int)FNAttr.Finishing };
        public CalculationAttributes(List<PlayerAttribute> attr)
        {
            Attributes = attr;
        }
        public List<PlayerAttribute> Attributes { get; }
        private bool CmpAttr(List<PlayerAttribute> tempAttributes, double[] maxAttr)
        {
            bool ret = false;
            for (int i = 0; i < tempAttributes.Count; i++)
            {
                if (tempAttributes[i].ColorAttribute == PlayerAttribute.Color.WHITE)
                {
                    if (tempAttributes[i].ValueAttribute > maxAttr[i])
                    {
                        maxAttr[i] = tempAttributes[i].ValueAttribute;
                        ret = true;
                    }

                } 
            }
            return ret;
        }
        private bool CheackMaxVal(ref List<PlayerAttribute> tempAttributes, ref bool[,] maskAtr, ref int index)
        {
            int max = 60;
            bool ret = true;
            for (int i = 0; i < tempAttributes.Count; i++)
            {
                if (tempAttributes[i].ColorAttribute == PlayerAttribute.Color.GRAY
                    && maskAtr[index,i]
                    && tempAttributes[i].ValueAttribute >= max)
                {
                    ret = false;
                    break;
                }                    
            }
            return ret;
        }
        private void Swap(ref bool[,] mask, ref int next)
        {
            for (int i = 0; i < 15; i++)
            {
                bool temp;
                temp = mask[next,i];
                mask[next, i] = mask[next + 1, i];
                mask[next + 1, i] = temp;
            }
        }
        private void GetTrainingName(ArrayList mas,out List<string> array)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < mas.Count; i++)
            {
                if (PASS_GO_AND_SHOOT.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "PASS_GO_AND_SHOOT");
                }
                else if (FAST_COUNTER_ATTACKS.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "FAST_COUNTER_ATTACKS");
                }
                else if (SKILL_DRILL.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "SKILL_DRILL");
                }
                else if (SHOOTING_TECHNIQUE.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "SHOOTING_TECHNIQUE");
                }
                else if (SET_PIECE_DELIVERY.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "SET_PIECE_DELIVERY");
                }
                else if (SLALOM_DRIBBLE.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "SLALOM_DRIBBLE");
                }
                else if (WING_PLAY.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "WING_PLAY");
                } 
                else if (ONE_ON_ONE_FINISHING.SequenceEqual((int[])mas[i]))
                {
                    list.Add( "ONE_ON_ONE_FINISHING");
                }
            }
            array = list;
        }
        public void Calculation()
        {
            
            
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

            //training.Add(8, PRESS_THE_PLAY);
            //training.Add(9, PIGGY_IN_THE_MIDDLE);
            //training.Add(10, USE_YOUR_HEAD);
            //training.Add(11, STOP_THE_ATTACKER);
            //training.Add(12, DEFENDING_CROSSES);
            //training.Add(13, VIDEO_ANALYSIS);
            //training.Add(14, HOLD_THE_LINE);

            //training.Add(15, WARM_UP);
            //training.Add(16, STRETCH);
            //training.Add(17, SPRINT);
            //training.Add(18, CARIOCA_WITH_LADDERS);
            //training.Add(19, LONG_RUN);
            //training.Add(20, GYM);
            //training.Add(21, SHUTTLE_RUNS);
            //training.Add(22, HURDLE_JUMPS);

            
            int lenTrain = training.Count();
            int lenAttr = Attributes.Count();
            bool[,] maskAtr = new bool[lenTrain, lenAttr];
            double[] maxAttr =
                { 0,0,0,0,0,
                  0,0,0,0,0,
                  0,0,0,0,0};


            int next = 0;
            List<double> itog = new List<double>();
            List<bool[,]> maskAtrItog = new List<bool[,]>();

            for (int i = 0; i < lenTrain; i++)
            {
                for (int j = 0; j < training[i].Length; j++)
                {
                    maskAtr[i, training[i][j]] = true;
                }
            }
            for (int i = 0; i < 40320; i++)
            {
                List<PlayerAttribute> tempAttributes = new List<PlayerAttribute>(Attributes.Count);
                Attributes.ForEach((item) =>
                {
                    tempAttributes.Add((PlayerAttribute)item.Clone());
                });

                int indexTraining = 0;
                for (int ii = 0; ii < lenTrain; ii++)
                {
                    double sum = 0;
                    int count = 0;
                    for (int iii = 0; iii < lenAttr; iii++)
                    {
                        if (maskAtr[ii, iii])
                        {
                            sum += tempAttributes[iii].ValueAttribute;
                            ++count;
                        }
                    }
                    while (sum + count <= count * 180 && CheackMaxVal(ref tempAttributes, ref maskAtr, ref indexTraining))
                    {
                        sum = 0;
                        count = 0;
                        for (int jj = 0; jj < lenAttr; jj++)
                        {
                            if (maskAtr[ii, jj])
                            {
                                if (tempAttributes[jj].ColorAttribute == PlayerAttribute.Color.WHITE)
                                {
                                    ++tempAttributes[jj].ValueAttribute;
                                }
                                else
                                {
                                    tempAttributes[jj].ValueAttribute += 0.5;
                                }
                                sum += (int)tempAttributes[jj].ValueAttribute;
                                ++count;
                            }
                        }
                    }
                    ++indexTraining;
                }

                if (CmpAttr(tempAttributes, maxAttr))
                {
                    foreach (var item in tempAttributes)
                    {
                        itog.Add(item.ValueAttribute);
                    }
                    maskAtrItog.Add((bool[,])maskAtr.Clone()); 
                }


                Swap(ref maskAtr, ref next);
                if (next >= lenTrain-2)
                {
                    next = 0;
                }
                else
                {
                    ++next;
                }               
            }

            Dictionary<int, ArrayList > newTraining = new Dictionary<int, ArrayList>(); 
            for (int i = 0; i < maskAtrItog.Count; i++)
            {
                ArrayList list = new ArrayList();
                bool[,] temp = maskAtrItog[i];
                for (int j = 0; j < 8; j++)
                {
                    List < int > arr= new List<int>();
                    //int[] arr = new int[5];
                    //int x = 0;
                    for (int k = 0; k < 15; k++)
                    {                     
                        if (temp[j, k])
                        {
                            arr.Add(k);
                            //arr[x++] = k;
                        }  
                    }
                    list.Add(arr.ToArray());
                }
                newTraining[i] = list;
            }

            Dictionary<int, List<string>> newTrain = new Dictionary<int, List<string>>();
            List<string> array = new List<string>();
            for (int i = 0; i < newTraining.Count; i++)
            {
               GetTrainingName(newTraining[i],out array);
                newTrain[i] = array;
            }

        }
    }
}
