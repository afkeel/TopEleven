using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainingCalculator
{
    using PAAttr = PlayerAttribute.Attributes;
    class CalculationAttributes : Form
    {
        static PAAttr[] PASS_GO_AND_SHOOT = { PAAttr.Passing, PAAttr.Shooting, PAAttr.Speed };
        static PAAttr[] FAST_COUNTER_ATTACKS = { PAAttr.Passing, PAAttr.Crossing, PAAttr.Finishing, PAAttr.Creativity };
        static PAAttr[] SKILL_DRILL = { PAAttr.Heading, PAAttr.Dribling, PAAttr.Creativity };
        static PAAttr[] SHOOTING_TECHNIQUE = { PAAttr.Shooting, PAAttr.Finishing, PAAttr.Strength };
        static PAAttr[] SET_PIECE_DELIVERY = { PAAttr.Marking, PAAttr.Heading, PAAttr.Crossing, PAAttr.Shooting };
        static PAAttr[] SLALOM_DRIBBLE = { PAAttr.Passing, PAAttr.Dribling, PAAttr.Fitness, PAAttr.Speed };
        static PAAttr[] WING_PLAY = { PAAttr.Heading, PAAttr.Crossing, PAAttr.Shooting, PAAttr.Finishing };
        static PAAttr[] ONE_ON_ONE_FINISHING = { PAAttr.Tackling, PAAttr.Dribling, PAAttr.Finishing };

        static PAAttr[] PRESS_THE_PLAY = { PAAttr.Tackling, PAAttr.Marking, PAAttr.Positioning, PAAttr.Bravery, PAAttr.Aggression };
        static PAAttr[] PIGGY_IN_THE_MIDDLE = { PAAttr.Tackling, PAAttr.Positioning, PAAttr.Passing, PAAttr.Fitness, PAAttr.Aggression };
        static PAAttr[] USE_YOUR_HEAD = { PAAttr.Positioning, PAAttr.Heading, PAAttr.Passing, PAAttr.Creativity };
        static PAAttr[] STOP_THE_ATTACKER = { PAAttr.Tackling, PAAttr.Marking, PAAttr.Bravery, PAAttr.Dribling, PAAttr.Strength };
        static PAAttr[] DEFENDING_CROSSES = { PAAttr.Marking, PAAttr.Heading, PAAttr.Bravery, PAAttr.Crossing };
        static PAAttr[] VIDEO_ANALYSIS = { PAAttr.Positioning, PAAttr.Bravery, PAAttr.Creativity };
        static PAAttr[] HOLD_THE_LINE = { PAAttr.Marking, PAAttr.Positioning };

        static PAAttr[] WARM_UP = { PAAttr.Heading, PAAttr.Fitness, PAAttr.Aggression };
        static PAAttr[] STRETCH = { PAAttr.Fitness, PAAttr.Strength, PAAttr.Speed };
        static PAAttr[] SPRINT = { PAAttr.Dribling, PAAttr.Fitness, PAAttr.Speed };
        static PAAttr[] CARIOCA_WITH_LADDERS = { PAAttr.Aggression, PAAttr.Speed };
        static PAAttr[] LONG_RUN = { PAAttr.Fitness, PAAttr.Speed };
        static PAAttr[] GYM = { PAAttr.Fitness, PAAttr.Strength };
        static PAAttr[] SHUTTLE_RUNS = { PAAttr.Bravery, PAAttr.Strength, PAAttr.Speed };
        static PAAttr[] HURDLE_JUMPS = { PAAttr.Bravery, PAAttr.Aggression, PAAttr.Speed };

        private const int maxValueAttribute = 180;
        private const int maxGrayAttrVal = 60;
        public CalculationAttributes(List<PlayerAttribute> attr)
        {
            ListAttributes = attr;
        }
        public List<PlayerAttribute> ListAttributes { get; }
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
            //for (int i = 0; i < mas.Count; i++)
            //{
            //    if (dr.PASS_GO_AND_SHOOT.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "PASS_GO_AND_SHOOT");
            //    }
            //    else if (FAST_COUNTER_ATTACKS.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "FAST_COUNTER_ATTACKS");
            //    }
            //    else if (SKILL_DRILL.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "SKILL_DRILL");
            //    }
            //    else if (SHOOTING_TECHNIQUE.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "SHOOTING_TECHNIQUE");
            //    }
            //    else if (SET_PIECE_DELIVERY.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "SET_PIECE_DELIVERY");
            //    }
            //    else if (SLALOM_DRIBBLE.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "SLALOM_DRIBBLE");
            //    }
            //    else if (WING_PLAY.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "WING_PLAY");
            //    } 
            //    else if (ONE_ON_ONE_FINISHING.SequenceEqual((int[])mas[i]))
            //    {
            //        list.Add( "ONE_ON_ONE_FINISHING");
            //    }
            //}
            array = list;
        }
        private bool CheackMaxVal(List<PlayerAttribute> listGrayAttr)
        {
            PlayerAttribute res = listGrayAttr.Find(
                delegate (PlayerAttribute pa)
                {
                    return pa.ValueAttribute >= maxGrayAttrVal;
                });
            return res == null;
        }
        private bool FindGrayAttr(List<PlayerAttribute> attr, out List<PlayerAttribute> list)
        {
            //attr.Add(new PlayerAttribute(PAAttr.Bravery, PlayerAttribute.Color.GRAY, 20));
            //attr.Add(new PlayerAttribute(PAAttr.Heading, PlayerAttribute.Color.GRAY, 20));
            //attr.Add(new PlayerAttribute(PAAttr.Marking, PlayerAttribute.Color.GRAY, 60));
            //attr.Add(new PlayerAttribute(PAAttr.Tackling, PlayerAttribute.Color.GRAY, 20));
            list = attr.FindAll(
            delegate (PlayerAttribute pa)
            {
                return pa.ColorAttribute == PlayerAttribute.Color.GRAY;
            });
            return list.Count > 0;
        }        
        //private void Foo1(List<PlayerAttribute> tempListAttributes, bool[] maskAttr,
        //    out double sumAttr, out int countAttr)
        //{
        //    sumAttr = 0;
        //    countAttr = 0;
        //    for (int i = 0; i < tempListAttributes.Count; i++)
        //    {
        //        if (maskAttr[i])
        //        {
        //            sumAttr += tempListAttributes[i].ValueAttribute;
        //            ++countAttr;
        //            if (tempListAttributes[i].ColorAttribute == PlayerAttribute.Color.GRAY)
        //            {
        //                //mas.Add(tempListAttributes[i].ValueAttribute);
        //            }
        //        }
        //    }
        //}
        private double CalcSumAttr(List<PlayerAttribute> arrPA)
        {
            double sumAttr = 0;
            for (int i = 0; i < arrPA.Count; i++)
            {
                sumAttr += arrPA[i].ValueAttribute;
            }
            return sumAttr;
        }
        private bool CmpAttributeName(PAAttr item, PAAttr[] array)
        {
            return array.Contains(item);
        }
        private void FillArrPA(List<PlayerAttribute> tempListAttributes, Drill drill,
            List<PlayerAttribute> arrPA)
        {
            //sumAttr = 0;
            //countAttr = 0;
            for (int i = 0; i < tempListAttributes.Count; i++)
            {
                if (CmpAttributeName(tempListAttributes[i].AttributeName, drill.DrillAttributes))
                {
                    arrPA.Add(tempListAttributes[i]);
                    //sumAttr += tempListAttributes[i].ValueAttribute;
                    //++countAttr;
                    //if (tempListAttributes[i].ColorAttribute == PlayerAttribute.Color.GRAY)
                    //{
                    //    //mas.Add(tempListAttributes[i].ValueAttribute);
                    //}
                }
            }
        }      
        private void IncreasePlayerAttribute(List<PlayerAttribute> tempListAttributes, List<Drill> listDrill)
        {
            for (int i = 0; i < listDrill.Count; i++)
            {
                List<PlayerAttribute> listPA = new List<PlayerAttribute>();
                FillArrPA(tempListAttributes, listDrill[i], listPA);
                //Foo1(out double sumAttr, out int countAttr);
                bool findGray = FindGrayAttr(listPA, out List<PlayerAttribute> listGrayAttr);
                int countAttr = listPA.Count;
                while (CalcSumAttr(listPA) + countAttr <= countAttr * maxValueAttribute
                    && (!findGray || CheackMaxVal(listGrayAttr)))
                {
                    foreach (var item in listPA)
                    {
                        if (item.ColorAttribute == PlayerAttribute.Color.WHITE)
                        {
                            ++item.ValueAttribute;
                        }
                        else
                        {
                            item.ValueAttribute += 0.5;
                        } 
                    }

                    //for (int j = 0; j < tempListAttributes.Count; j++)
                    //{
                    //    if (CmpAttributeName(tempListAttributes[j].AttributeName, listDrill[i].DrillAttributes))
                    //    {
                            
                    //        sumAttr += tempListAttributes[j].ValueAttribute;
                    //        ++countAttr;
                    //    }
                    //}
                }
            }
        }
        private void FillMaskAttr(bool[][] mask, List<Drill> list)
        {
            //we combine the attributes of the player with drill in a mask, 
            //which we will then use to pump the attribute values
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].DrillAttributes.Length; j++)
                {
                    mask[i][(int)list[i].DrillAttributes[j]] = true;
                }
            }
        }
        private void MakeTrainingProgram(List<Drill> listDrill)
        {
            listDrill.Add(new Drill("PASS_GO_AND_SHOOT", PASS_GO_AND_SHOOT));
            listDrill.Add(new Drill("FAST_COUNTER_ATTACKS", FAST_COUNTER_ATTACKS));
            listDrill.Add(new Drill("SKILL_DRILL", SKILL_DRILL));
            listDrill.Add(new Drill("SHOOTING_TECHNIQUE", SHOOTING_TECHNIQUE));
            listDrill.Add(new Drill("SET_PIECE_DELIVERY", SET_PIECE_DELIVERY));
            listDrill.Add(new Drill("SLALOM_DRIBBLE", SLALOM_DRIBBLE));
            listDrill.Add(new Drill("WING_PLAY", WING_PLAY));
            listDrill.Add(new Drill("ONE_ON_ONE_FINISHING", ONE_ON_ONE_FINISHING));

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
        }
        public void Calculation()
        {
            List<Drill> listDrill = new List<Drill>();
            MakeTrainingProgram(listDrill);

            int lenListDrill = listDrill.Count();
            int lenListAttr = ListAttributes.Count();
            //bool[][] maskAttr = new bool[lenListDrill][];
            //for (int i = 0; i < lenListDrill; i++)
            //{
            //    maskAttr[i] = new bool[lenListAttr];
            //}
            //FillMaskAttr(maskAttr, listDrill);
                     
            double[] maxAttr = new double[lenListAttr];           
            List<double> itog = new List<double>();
            List<bool[,]> maskAttrItog = new List<bool[,]>();
            int next = 0;

            СalcFactorial cf = new СalcFactorial();
            BigInteger countIterationsListDrill = cf.Calculate(lenListDrill);
            
            Stopwatch stopWatchCalcAttr = Stopwatch.StartNew();
            for (BigInteger i = 0; i < countIterationsListDrill; i++)
            {
                List<PlayerAttribute> tempListAttributes = new List<PlayerAttribute>(lenListAttr);
                ListAttributes.ForEach((item) =>
                {
                    tempListAttributes.Add((PlayerAttribute)item.Clone());
                });
                IncreasePlayerAttribute(tempListAttributes, listDrill);



                //if (CmpAttr(tempListAttributes, maxAttr))
                //{
                //    foreach (var item in tempListAttributes)
                //    {
                //        itog.Add(item.ValueAttribute);
                //    }
                //    maskAttrItog.Add((bool[,])maskAttr.Clone()); 
                //}


                //Swap(ref maskAttr, ref next);
                //if (next >= lenListDrill - 2)
                //{
                //    next = 0;
                //}
                //else
                //{
                //    ++next;
                //}               
            }
            stopWatchCalcAttr.Stop();
            //MessageBox.Show(stopWatchCalcAttr.Elapsed.ToString());

            Dictionary<int, ArrayList > newTraining = new Dictionary<int, ArrayList>(); 
            for (int i = 0; i < maskAttrItog.Count; i++)
            {
                ArrayList list = new ArrayList();
                bool[,] temp = maskAttrItog[i];
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
