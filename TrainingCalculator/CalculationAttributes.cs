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
    //public class CalculationAttributesEventArgs : EventArgs
    //{
    //    public BigInteger CountIterations { get; set; }
    //}
    public class CalculationAttributes
    {
        //public static event EventHandler ProgressBarValChanged;
        //public static event EventHandler ProgressBarMax;

        PAAttr[] PASS_GO_AND_SHOOT = { PAAttr.Passing, PAAttr.Shooting, PAAttr.Speed };
        PAAttr[] FAST_COUNTER_ATTACKS = { PAAttr.Passing, PAAttr.Crossing, PAAttr.Finishing, PAAttr.Creativity };
        PAAttr[] SKILL_DRILL = { PAAttr.Heading, PAAttr.Dribling, PAAttr.Creativity };
        PAAttr[] SHOOTING_TECHNIQUE = { PAAttr.Shooting, PAAttr.Finishing, PAAttr.Strength };
        PAAttr[] SET_PIECE_DELIVERY = { PAAttr.Marking, PAAttr.Heading, PAAttr.Crossing, PAAttr.Shooting };
        PAAttr[] SLALOM_DRIBBLE = { PAAttr.Passing, PAAttr.Dribling, PAAttr.Fitness, PAAttr.Speed };
        PAAttr[] WING_PLAY = { PAAttr.Heading, PAAttr.Crossing, PAAttr.Shooting, PAAttr.Finishing };
        PAAttr[] ONE_ON_ONE_FINISHING = { PAAttr.Tackling, PAAttr.Dribling, PAAttr.Finishing };

        PAAttr[] PRESS_THE_PLAY = { PAAttr.Tackling, PAAttr.Marking, PAAttr.Positioning, PAAttr.Bravery, PAAttr.Aggression };
        PAAttr[] PIGGY_IN_THE_MIDDLE = { PAAttr.Tackling, PAAttr.Positioning, PAAttr.Passing, PAAttr.Fitness, PAAttr.Aggression };
        PAAttr[] USE_YOUR_HEAD = { PAAttr.Positioning, PAAttr.Heading, PAAttr.Passing, PAAttr.Creativity };
        PAAttr[] STOP_THE_ATTACKER = { PAAttr.Tackling, PAAttr.Marking, PAAttr.Bravery, PAAttr.Dribling, PAAttr.Strength };
        PAAttr[] DEFENDING_CROSSES = { PAAttr.Marking, PAAttr.Heading, PAAttr.Bravery, PAAttr.Crossing };
        PAAttr[] VIDEO_ANALYSIS = { PAAttr.Positioning, PAAttr.Bravery, PAAttr.Creativity };
        PAAttr[] HOLD_THE_LINE = { PAAttr.Marking, PAAttr.Positioning };

        PAAttr[] WARM_UP = { PAAttr.Heading, PAAttr.Fitness, PAAttr.Aggression };
        PAAttr[] STRETCH = { PAAttr.Fitness, PAAttr.Strength, PAAttr.Speed };
        PAAttr[] SPRINT = { PAAttr.Dribling, PAAttr.Fitness, PAAttr.Speed };
        PAAttr[] CARIOCA_WITH_LADDERS = { PAAttr.Aggression, PAAttr.Speed };
        PAAttr[] LONG_RUN = { PAAttr.Fitness, PAAttr.Speed };
        PAAttr[] GYM = { PAAttr.Fitness, PAAttr.Strength };
        PAAttr[] SHUTTLE_RUNS = { PAAttr.Bravery, PAAttr.Strength, PAAttr.Speed };
        PAAttr[] HURDLE_JUMPS = { PAAttr.Bravery, PAAttr.Aggression, PAAttr.Speed };

        const int maxValueAttribute = 180;
        const int maxGrayAttrVal = 60;      
        public CalculationAttributes(){}
        public CalculationAttributes(List<PlayerAttribute> attr)
        {
            ListAttributes = attr;
        }
        public List<PlayerAttribute> ListAttributes { get; }
        public double[] maxAttrs = new double[15];
        public List<List<Drill>> maxAttrsDrill = new List<List<Drill>>();
        public List<List<PlayerAttribute>> maxAttrsList = new List<List<PlayerAttribute>>();
        public List<List<PlayerAttribute>> tempListAttributes = new List<List<PlayerAttribute>>();
        private void Swap(List<Drill> list, int i , int j)
        {
            Drill temp;
            temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        ////// Permutations without repetitions //////
        public int SwapDrill(List<Drill> list, int n)
        {            
            //The task of generating permutations in lexicographic order. 
            //In this case, all permutations are sorted first by the first number, then by the second, etc.in ascending order. 
            //Thus, the first one will be the permutation 1 2... N, and the last one will be N N-1... 1.
            int i = n - 2;
            while (list[i].DrillIndex >= list[i+1].DrillIndex) 
                i--;
            int j = n - 1;
            while (list[i].DrillIndex >= list[j].DrillIndex) 
                j--;
            Swap(list, i, j);
            int l = i + 1;
            int r = n - 1;
            int count = r - l;
            if (count>0)
                list.Reverse(l, count+1);
            return i;
        }
        private bool CmpMaxAttrsList(List<PlayerAttribute> listAttributes, double[] maxAttr)
        {
            bool ret = false;
            for (int i = 0; i < listAttributes.Count; i++)
            {
                if (listAttributes[i].ColorAttribute == PlayerAttribute.Color.WHITE && 
                    listAttributes[i].ValueAttribute > maxAttr[i])
                {
                    maxAttr[i] = listAttributes[i].ValueAttribute;
                    ret = true;
                }
            }
            return ret;
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
            list = attr.FindAll(
            delegate (PlayerAttribute pa)
            {
                return pa.ColorAttribute == PlayerAttribute.Color.GRAY;
            });
            return list.Count > 0;
        }        
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
            for (int i = 0; i < tempListAttributes.Count; i++)
            {
                if (CmpAttributeName(tempListAttributes[i].AttributeName, drill.DrillAttributes))
                {
                    arrPA.Add(tempListAttributes[i]);
                }
            }
        }      
        public void IncreasePlayerAttribute(List<PlayerAttribute> tempList, List<Drill> listDrill, ref int index)
        {
            ListAttributes.ForEach((item) =>
            {
                tempList.Add((PlayerAttribute)item.Clone());
            });           
            tempListAttributes.Add(tempList.ConvertAll(attr => (PlayerAttribute)attr.Clone()));
            for (int i = 0; i < listDrill.Count; i++)
            {
                List<PlayerAttribute> listPA = new List<PlayerAttribute>();
                FillArrPA(tempList, listDrill[i], listPA);
                int countAttr = listPA.Count;
                while (CalcSumAttr(listPA) + countAttr <= countAttr * maxValueAttribute
                    && (!FindGrayAttr(listPA, out List<PlayerAttribute> listGrayAttr) || CheackMaxVal(listGrayAttr)))
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
                }
                tempListAttributes.Add(tempList.ConvertAll(attr => (PlayerAttribute)attr.Clone()));               
            }
            if (CmpMaxAttrsList(tempList, maxAttrs))
            {
                maxAttrsList.Add(tempList.ConvertAll(attr => (PlayerAttribute)attr.Clone()));
                maxAttrsDrill.Add(listDrill.ConvertAll(drill => (Drill)drill.Clone()));
            }
            int ind = SwapDrill(listDrill, listDrill.Count);
            if (index > ind)
                index = ind;
        }
        public void IncreasePlayerAttribute(List<PlayerAttribute> tempList, List<Drill> listDrill, int index)
        {           
            for (int i = index; i < listDrill.Count; i++)
            {
                List<PlayerAttribute> listPA = new List<PlayerAttribute>();
                FillArrPA(tempList, listDrill[i], listPA);
                int countAttr = listPA.Count;
                while (CalcSumAttr(listPA) + countAttr <= countAttr * maxValueAttribute
                    && (!FindGrayAttr(listPA, out List<PlayerAttribute> listGrayAttr) || CheackMaxVal(listGrayAttr)))
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
                }
            }
        }
        public void MakeTrainingProgram(out List<Drill> listDrill)
        {
            List<Drill> list = new List<Drill>
            {
                new Drill(1, "PASS_GO_AND_SHOOT", PASS_GO_AND_SHOOT),
                new Drill(2, "FAST_COUNTER_ATTACKS", FAST_COUNTER_ATTACKS),
                new Drill(3, "SKILL_DRILL", SKILL_DRILL),
                new Drill(4, "SHOOTING_TECHNIQUE", SHOOTING_TECHNIQUE),
                new Drill(5, "SET_PIECE_DELIVERY", SET_PIECE_DELIVERY),
                new Drill(6, "SLALOM_DRIBBLE", SLALOM_DRIBBLE),
                new Drill(7, "WING_PLAY", WING_PLAY),
                new Drill(8, "ONE_ON_ONE_FINISHING", ONE_ON_ONE_FINISHING),
               
                //new Drill(9, "PRESS_THE_PLAY", PRESS_THE_PLAY),
                //new Drill(10, "PIGGY_IN_THE_MIDDLE", PIGGY_IN_THE_MIDDLE),
                //new Drill(11, "USE_YOUR_HEAD", USE_YOUR_HEAD),
                //new Drill(12, "STOP_THE_ATTACKER", STOP_THE_ATTACKER),
                //new Drill(13, "DEFENDING_CROSSES", DEFENDING_CROSSES),
                //new Drill(14, "VIDEO_ANALYSIS", VIDEO_ANALYSIS),
                //new Drill(15, "HOLD_THE_LINE", HOLD_THE_LINE),
                
                //new Drill(16, "WARM_UP", WARM_UP),
                //new Drill(17, "STRETCH", STRETCH),
                //new Drill(18, "SPRINT", SPRINT),
                //new Drill(19, "CARIOCA_WITH_LADDERS", CARIOCA_WITH_LADDERS),
                //new Drill(20, "LONG_RUN", LONG_RUN),
                //new Drill(21, "GYM", GYM),
                //new Drill(22, "SHUTTLE_RUNS", SHUTTLE_RUNS),
                //new Drill(23, "HURDLE_JUMPS", HURDLE_JUMPS)
            };
            listDrill = list;          
        }
        public void Calculation()
        {
            MakeTrainingProgram(out List<Drill> listDrill);
            int lenListDrill = listDrill.Count();
            int lenListAttr = ListAttributes.Count();
            //The number of permutations for N different elements is N!
            СalcFactorial cf = new СalcFactorial();
            BigInteger countIterationsListDrill = cf.Calculate(lenListDrill)-1;
            //CalculationAttributesEventArgs e = new CalculationAttributesEventArgs
            //{
            //    CountIterations = countIterationsListDrill
            //};
            //ProgressBarMax?.Invoke(this, e);
            int index = lenListDrill;
            //Since the permutations go from the bottom up, 
            //there is no need to increase data by a new one before the permutation.
            //Let's fill in the data array for each training session 
            //and start from the place where the permutation was.
            List<PlayerAttribute> tempList = new List<PlayerAttribute>();
            IncreasePlayerAttribute(tempList, listDrill, ref index);
            BigInteger count = countIterationsListDrill - 1;
            Stopwatch stopWatchCalcAttr = Stopwatch.StartNew();
            for (BigInteger i = 0; i < count; i++)            
            {
                tempList.Clear();
                tempListAttributes[index].ForEach((item) =>
                {
                    tempList.Add((PlayerAttribute)item.Clone());
                });
                IncreasePlayerAttribute(tempList, listDrill, index);
                if (CmpMaxAttrsList(tempList, maxAttrs))
                {
                    maxAttrsList.Add(tempList.ConvertAll(attr => (PlayerAttribute)attr.Clone()));
                    maxAttrsDrill.Add(listDrill.ConvertAll(drill => (Drill)drill.Clone()));
                }
                int ind = SwapDrill(listDrill, lenListDrill);
                if (index > ind)
                    index = ind;
                //ProgressBarValChanged?.Invoke(this, EventArgs.Empty);               
            }
            stopWatchCalcAttr.Stop();
            MessageBox.Show(stopWatchCalcAttr.Elapsed.ToString());
        }
    }
}
