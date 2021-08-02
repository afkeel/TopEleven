using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainingCalculator
{
    using PA = PlayerAttribute;
    //public class CalculationAttributesEventArgs : EventArgs
    //{
    //    public BigInteger CountIterations { get; set; }
    //}
    public class CalculationAttributes
    {
        //public static event EventHandler ProgressBarValChanged;
        //public static event EventHandler ProgressBarMax;
        const int maxValueAttribute = 180;
        readonly List<List<Drill>> maxAttrsDrill = new List<List<Drill>>();
        readonly List<List<PA>> maxAttrsList = new List<List<PA>>();
        readonly List<List<double[]>> estimatedValueList = new List<List<double[]>>();
        public CalculationAttributes(){}
        public CalculationAttributes(List<PA> attr)
        {
            ListAttributes = attr;
        }        
        public List<PA> ListAttributes { get; }                
        public List<List<Drill>> MaxAttrsDrill { get => maxAttrsDrill; }
        public List<List<PA>> MaxAttrsList { get => maxAttrsList; }
        public double[] MaxAttrs { get; } = new double[15];
        public int MaxGrayAttrVal { get; set; } = 60;
        public List<List<double[]>> EstimatedValueList { get => estimatedValueList; }

        private void Swap(List<Drill> list, int i , int j)
        {
            Drill temp;
            temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        ////// Permutations without repetitions //////
        public void SwapDrill(List<Drill> listDrill, ref int index)
        {
            //The task of generating permutations in lexicographic order. 
            //In this case, all permutations are sorted first by the first number, then by the second, etc.in ascending order. 
            //Thus, the first one will be the permutation 1 2... N, and the last one will be N N-1... 1.
            int n = listDrill.Count();
            if (n>1)
            {
                int i = n - 2;
                while (i!=-1 && listDrill[i].DrillIndex >= listDrill[i + 1].DrillIndex)
                    i--;
                if (i==-1)
                    return;
                int j = n - 1;
                while (listDrill[i].DrillIndex >= listDrill[j].DrillIndex)
                    j--;
                Swap(listDrill, i, j);
                int l = i + 1;
                int r = n - 1;
                int count = r - l;
                if (count > 0)
                    listDrill.Reverse(l, count + 1);
                if (index == 0 || index > i)
                    index = i;
            }            
        }
        private void CmpMaxAttrsList(List<PA> listAttributes, List<Drill> listDrill, List<double[]> estList)
        {
            bool add = false;
            for (int i = 0; i < listAttributes.Count; i++)
            {
                if (listAttributes[i].AttributeColor == PA.Color.WHITE &&
                    listAttributes[i].AttributeEstimatedValue > MaxAttrs[i])
                {
                    MaxAttrs[i] = listAttributes[i].AttributeEstimatedValue;
                    add = true;
                }
            }
            if (add)
            {
                MaxAttrsList.Add(listAttributes.ConvertAll(attr => (PA)attr.Clone()));
                MaxAttrsDrill.Add(listDrill.ConvertAll(drill => (Drill)drill.Clone()));
                EstimatedValueList.Add(estList.ConvertAll(item => (double[])item.Clone()));
            }
        }
        private bool CheackMaxVal(List<PA> listGrayAttr)
        {           
            return !listGrayAttr.Exists(x => x.AttributeEstimatedValue >= MaxGrayAttrVal);
        }                     
        private void InitMaxAverageDrillQuality(Drill drill, double sum, bool ga)
        {
            int aq = (int)Math.Round(sum / drill.DrillAttributes.Count);
            if (aq <= maxValueAttribute && ga)
            {
                drill.MaxAverageDrillQuality = aq;
            }
            else
            {
                drill.MaxAverageDrillQuality = 0;
            }
        }
        private double CalcSumAttr(List<PA> arrPA)
        {
            double sum = 0;
            foreach (var item in arrPA)
                sum += item.AttributeEstimatedValue;    
            return sum;
        }
        public void IncreasePlayerAttribute(
            List<PA> listAttributes, List<Drill> listDrill, List<List<PA>> increasedPAList, List<double[]> estList, int index)
        {
            for (int i = index; i < listDrill.Count; i++)
            {
                int countAttr = listDrill[i].DrillAttributes.Count;
                List<PA> listGrayAttr = new List<PA>(listDrill[i].FindGrayAttr(listDrill[i].DrillAttributes));
                int countGray = listGrayAttr.Count;
                double sumAttributes = CalcSumAttr(listDrill[i].DrillAttributes);
                bool checkGreyA = CheackMaxVal(listGrayAttr);
                while (sumAttributes + countAttr <= countAttr * maxValueAttribute
                    && (countGray.Equals(0) || CheackMaxVal(listGrayAttr)))
                {
                    foreach (var item in listDrill[i].DrillAttributes)
                    {
                        if (item.AttributeColor == PA.Color.WHITE)
                        {
                            ++item.AttributeEstimatedValue;
                            ++sumAttributes;
                            
                        }
                        else
                        {
                            item.AttributeEstimatedValue += 0.5;
                            sumAttributes += 0.5;
                        }
                    }
                }
                double[] arr= new double[listAttributes.Count];
                foreach (var item in listDrill[i].DrillAttributes)
                {
                    double sum = 0;
                    foreach (var list in estList)
                        sum += list[(int)item.AttributeName];
                    arr[(int)item.AttributeName] = item.AttributeEstimatedValue - item.AttributeInputValue - sum ;
                }
                estList.Add(arr);
                InitMaxAverageDrillQuality(listDrill[i], sumAttributes, checkGreyA);
                if (index == 0)
                    increasedPAList.Add(listAttributes.ConvertAll(attr => (PA)attr.Clone()));
            }
        }
        public void MakeTrainingProgram(List <PA> startList, out List<Drill> listDrill)
        {
            List<Drill> list = new List<Drill>
            {                             
                new Drill(1, "PASS_GO_AND_SHOOT", new List<PA>() 
                {
                    startList[(int)PA.Attributes.Passing],
                    startList[(int)PA.Attributes.Shooting],
                    startList[(int)PA.Attributes.Speed]
                }),
                new Drill(2, "FAST_COUNTER_ATTACKS", new List<PA>() 
                {
                    startList[(int)PA.Attributes.Passing],
                    startList[(int)PA.Attributes.Crossing],
                    startList[(int)PA.Attributes.Finishing],
                    startList[(int)PA.Attributes.Creativity]
                }),
                new Drill(3, "SKILL_DRILL", new List<PA>()
                {
                    startList[(int)PA.Attributes.Heading],
                    startList[(int)PA.Attributes.Dribling],
                    startList[(int)PA.Attributes.Creativity]
                }),
                new Drill(4, "SHOOTING_TECHNIQUE", new List<PA>()
                {
                    startList[(int)PA.Attributes.Shooting],
                    startList[(int)PA.Attributes.Finishing],
                    startList[(int)PA.Attributes.Strength]
                }),
                new Drill(5, "SET_PIECE_DELIVERY", new List<PA>()
                {
                    startList[(int)PA.Attributes.Marking],
                    startList[(int)PA.Attributes.Heading],
                    startList[(int)PA.Attributes.Crossing],
                    startList[(int)PA.Attributes.Shooting]
                }),
                new Drill(6, "SLALOM_DRIBBLE", new List<PA>()
                {
                    startList[(int)PA.Attributes.Passing],
                    startList[(int)PA.Attributes.Dribling],
                    startList[(int)PA.Attributes.Fitness],
                    startList[(int)PA.Attributes.Speed]
                }),
                new Drill(7, "WING_PLAY", new List<PA>()
                {
                    startList[(int)PA.Attributes.Heading],
                    startList[(int)PA.Attributes.Crossing],
                    startList[(int)PA.Attributes.Shooting],
                    startList[(int)PA.Attributes.Finishing]
                }),
                new Drill(8, "ONE_ON_ONE_FINISHING", new List<PA>()
                {
                    startList[(int)PA.Attributes.Tackling],
                    startList[(int)PA.Attributes.Dribling],
                    startList[(int)PA.Attributes.Finishing]
                }),
                //new Drill(9, "PRESS_THE_PLAY", new List<PA>()
                //{
                //    startList[(int)PA.Attributes.Tackling],
                //    startList[(int)PA.Attributes.Marking],
                //    startList[(int)PA.Attributes.Positioning],
                //    startList[(int)PA.Attributes.Bravery],
                //    startList[(int)PA.Attributes.Aggression]
                //}),
                //new Drill(10, "PIGGY_IN_THE_MIDDLE", new List<PA>()
                //{
                //    startList[(int)PA.Attributes.Tackling],
                //    startList[(int)PA.Attributes.Positioning],
                //    startList[(int)PA.Attributes.Passing],
                //    startList[(int)PA.Attributes.Fitness],
                //    startList[(int)PA.Attributes.Aggression]
                //}),
                //new Drill(11, "USE_YOUR_HEAD", new List<PA>()
                //{
                //    startList[(int)PA.Attributes.Positioning],
                //    startList[(int)PA.Attributes.Heading],
                //    startList[(int)PA.Attributes.Passing],
                //    startList[(int)PA.Attributes.Creativity]
                //}),
                //new Drill(12, "STOP_THE_ATTACKER", new List<PA>()
                //{
                //    startList[(int)PA.Attributes.Tackling],
                //    startList[(int)PA.Attributes.Marking],
                //    startList[(int)PA.Attributes.Bravery],
                //    startList[(int)PA.Attributes.Dribling],
                //    startList[(int)PA.Attributes.Strength]
                //}),
                //new Drill(13, "DEFENDING_CROSSES", new List<PA>()
                //{
                //    startList[(int)PA.Attributes.Marking],
                //    startList[(int)PA.Attributes.Heading],
                //    startList[(int)PA.Attributes.Bravery],
                //    startList[(int)PA.Attributes.Crossing]
                //}),
                //new Drill(14, "VIDEO_ANALYSIS", new List<PA>()
                //{
                //    startList[(int)PA.Attributes.Positioning],
                //    startList[(int)PA.Attributes.Bravery],
                //    startList[(int)PA.Attributes.Creativity]
                //}),
                //new Drill(15, "HOLD_THE_LINE", new List<PA>()
                //{
                //    startList[(int)PA.Attributes.Marking],
                //    startList[(int)PA.Attributes.Positioning]
                //}) 
                
                //PAAttr[] WARM_UP = { PAAttr.Heading, PAAttr.Fitness, PAAttr.Aggression };
                //PAAttr[] STRETCH = { PAAttr.Fitness, PAAttr.Strength, PAAttr.Speed };
                //PAAttr[] SPRINT = { PAAttr.Dribling, PAAttr.Fitness, PAAttr.Speed };
                //PAAttr[] CARIOCA_WITH_LADDERS = { PAAttr.Aggression, PAAttr.Speed };
                //PAAttr[] LONG_RUN = { PAAttr.Fitness, PAAttr.Speed };
                //PAAttr[] GYM = { PAAttr.Fitness, PAAttr.Strength };
                //PAAttr[] SHUTTLE_RUNS = { PAAttr.Bravery, PAAttr.Strength, PAAttr.Speed };
                //PAAttr[] HURDLE_JUMPS = { PAAttr.Bravery, PAAttr.Aggression, PAAttr.Speed };
            };
            listDrill = list;
        }
        //public void MakeTrainingProgram2(List<PA> startList, out List<Drill> listDrill)
        //{
        //    List<Drill> list = new List<Drill>
        //    {
        //        new Drill(5, "SET_PIECE_DELIVERY", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Marking],
        //            startList[(int)PA.Attributes.Heading],
        //            startList[(int)PA.Attributes.Crossing],
        //            startList[(int)PA.Attributes.Shooting]
        //        }),
        //        new Drill(1, "PASS_GO_AND_SHOOT", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Passing],
        //            startList[(int)PA.Attributes.Shooting],
        //            startList[(int)PA.Attributes.Speed]
        //        }),
        //        new Drill(2, "FAST_COUNTER_ATTACKS", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Passing],
        //            startList[(int)PA.Attributes.Crossing],
        //            startList[(int)PA.Attributes.Finishing],
        //            startList[(int)PA.Attributes.Creativity]
        //        }),
        //        new Drill(3, "SKILL_DRILL", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Heading],
        //            startList[(int)PA.Attributes.Dribling],
        //            startList[(int)PA.Attributes.Creativity]
        //        }),
        //        new Drill(4, "SHOOTING_TECHNIQUE", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Shooting],
        //            startList[(int)PA.Attributes.Finishing],
        //            startList[(int)PA.Attributes.Strength]
        //        }),
        //        new Drill(6, "SLALOM_DRIBBLE", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Passing],
        //            startList[(int)PA.Attributes.Dribling],
        //            startList[(int)PA.Attributes.Fitness],
        //            startList[(int)PA.Attributes.Speed]
        //        }),
        //        new Drill(7, "WING_PLAY", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Heading],
        //            startList[(int)PA.Attributes.Crossing],
        //            startList[(int)PA.Attributes.Shooting],
        //            startList[(int)PA.Attributes.Finishing]
        //        }),
        //        new Drill(8, "ONE_ON_ONE_FINISHING", new List<PA>()
        //        {
        //            startList[(int)PA.Attributes.Tackling],
        //            startList[(int)PA.Attributes.Dribling],
        //            startList[(int)PA.Attributes.Finishing]
        //        }),
        //        //new Drill(9, "PRESS_THE_PLAY", new List<PA>()
        //        //{
        //        //    startList[(int)PA.Attributes.Tackling],
        //        //    startList[(int)PA.Attributes.Marking],
        //        //    startList[(int)PA.Attributes.Positioning],
        //        //    startList[(int)PA.Attributes.Bravery],
        //        //    startList[(int)PA.Attributes.Aggression]
        //        //}),
        //        //new Drill(10, "PIGGY_IN_THE_MIDDLE", new List<PA>()
        //        //{
        //        //    startList[(int)PA.Attributes.Tackling],
        //        //    startList[(int)PA.Attributes.Positioning],
        //        //    startList[(int)PA.Attributes.Passing],
        //        //    startList[(int)PA.Attributes.Fitness],
        //        //    startList[(int)PA.Attributes.Aggression]
        //        //}),
        //        //new Drill(11, "USE_YOUR_HEAD", new List<PA>()
        //        //{
        //        //    startList[(int)PA.Attributes.Positioning],
        //        //    startList[(int)PA.Attributes.Heading],
        //        //    startList[(int)PA.Attributes.Passing],
        //        //    startList[(int)PA.Attributes.Creativity]
        //        //}),
        //        //new Drill(12, "STOP_THE_ATTACKER", new List<PA>()
        //        //{
        //        //    startList[(int)PA.Attributes.Tackling],
        //        //    startList[(int)PA.Attributes.Marking],
        //        //    startList[(int)PA.Attributes.Bravery],
        //        //    startList[(int)PA.Attributes.Dribling],
        //        //    startList[(int)PA.Attributes.Strength]
        //        //}),
        //        //new Drill(13, "DEFENDING_CROSSES", new List<PA>()
        //        //{
        //        //    startList[(int)PA.Attributes.Marking],
        //        //    startList[(int)PA.Attributes.Heading],
        //        //    startList[(int)PA.Attributes.Bravery],
        //        //    startList[(int)PA.Attributes.Crossing]
        //        //}),
        //        //new Drill(14, "VIDEO_ANALYSIS", new List<PA>()
        //        //{
        //        //    startList[(int)PA.Attributes.Positioning],
        //        //    startList[(int)PA.Attributes.Bravery],
        //        //    startList[(int)PA.Attributes.Creativity]
        //        //}),
        //        //new Drill(15, "HOLD_THE_LINE", new List<PA>()
        //        //{
        //        //    startList[(int)PA.Attributes.Marking],
        //        //    startList[(int)PA.Attributes.Positioning]
        //        //}) 

        //        //PAAttr[] WARM_UP = { PAAttr.Heading, PAAttr.Fitness, PAAttr.Aggression };
        //        //PAAttr[] STRETCH = { PAAttr.Fitness, PAAttr.Strength, PAAttr.Speed };
        //        //PAAttr[] SPRINT = { PAAttr.Dribling, PAAttr.Fitness, PAAttr.Speed };
        //        //PAAttr[] CARIOCA_WITH_LADDERS = { PAAttr.Aggression, PAAttr.Speed };
        //        //PAAttr[] LONG_RUN = { PAAttr.Fitness, PAAttr.Speed };
        //        //PAAttr[] GYM = { PAAttr.Fitness, PAAttr.Strength };
        //        //PAAttr[] SHUTTLE_RUNS = { PAAttr.Bravery, PAAttr.Strength, PAAttr.Speed };
        //        //PAAttr[] HURDLE_JUMPS = { PAAttr.Bravery, PAAttr.Aggression, PAAttr.Speed };
        //    };
        //    listDrill = list;
        //}
        //public struct CalculationStruct
        //{
        //    public int Index { get; set; }
        //    public List<PA> ListAttributes { get; set; }
        //    public List<Drill> ListDrill { get; set; }
        //    public List<List<PA>> IncreasedPAList { get; set; }
        //}
        public CalculationAttributes Calculation()
        {
            #region parallel
            //Stopwatch stopWatchCalcAttr = Stopwatch.StartNew();
            //Parallel.Invoke(
            //        () =>
            //        {
            //            List<PA> tempList = new List<PA>();
            //            ListAttributes.ForEach((item) =>
            //            {
            //                tempList.Add((PA)item.Clone());
            //            });
            //            MakeTrainingProgram1(tempList, out List<Drill> listDrill);
            //            СalcFactorial cf = new СalcFactorial();
            //            BigInteger countIterations = cf.Calculate(listDrill.Count());
            //            List<List<PA>> increasedPAList = new List<List<PA>>
            //            {
            //                tempList.ConvertAll(attr => (PA)attr.Clone())
            //            };
            //            int index = 0;
            //            for (BigInteger i = 0; i < 20159; i++)
            //            {
            //                int j = 0;
            //                tempList.ForEach(item => item.ValueAttribute = increasedPAList[index][j++].ValueAttribute);
            //                for (int k = index; k < listDrill.Count; k++)
            //                {
            //                    int countAttr = listDrill[k].DrillAttributes.Count;
            //                    List<PA> listGrayAttr = new List<PA>(listDrill[k].FindGrayAttr(listDrill[k].DrillAttributes));
            //                    int countGray = listGrayAttr.Count;
            //                    double sumAttributes = CalcSumAttr(listDrill[k].DrillAttributes);
            //                    bool checkGreyA = CheackMaxVal(listGrayAttr);
            //                    while (sumAttributes + countAttr <= countAttr * maxValueAttribute
            //                        && (countGray.Equals(0) || CheackMaxVal(listGrayAttr)))
            //                    {
            //                        foreach (var item in listDrill[k].DrillAttributes)
            //                        {
            //                            if (item.ColorAttribute == PA.Color.WHITE)
            //                            {
            //                                ++item.ValueAttribute;
            //                                ++sumAttributes;
            //                            }
            //                            else
            //                            {
            //                                item.ValueAttribute += 0.5;
            //                                sumAttributes += 0.5;
            //                            }
            //                        }
            //                    }
            //                    InitMaxAverageDrillQuality(listDrill[k], sumAttributes, checkGreyA);
            //                    if (index == 0)
            //                        increasedPAList.Add(tempList.ConvertAll(attr => (PA)attr.Clone()));
            //                }
            //                bool add = false;
            //                for (int z = 0; z < tempList.Count; z++)
            //                {
            //                    if (tempList[z].ColorAttribute == PA.Color.WHITE &&
            //                        tempList[z].ValueAttribute > MaxAttrs[z])
            //                    {
            //                        MaxAttrs[z] = tempList[z].ValueAttribute;
            //                        add = true;
            //                    }
            //                }
            //                if (add)
            //                {
            //                    MaxAttrsList.Add(tempList.ConvertAll(attr => (PA)attr.Clone()));
            //                    MaxAttrsDrill.Add(listDrill.ConvertAll(drill => (Drill)drill.Clone()));
            //                }
            //                int n = listDrill.Count();
            //                if (n > 1)
            //                {
            //                    int x = n - 2;
            //                    while (x != -1 && listDrill[x].DrillIndex >= listDrill[x + 1].DrillIndex)
            //                        x--;
            //                    if (x == -1)
            //                        return;
            //                    int c = n - 1;
            //                    while (listDrill[x].DrillIndex >= listDrill[c].DrillIndex)
            //                        c--;
            //                    Drill temp;
            //                    temp = listDrill[x];
            //                    listDrill[x] = listDrill[c];
            //                    listDrill[c] = temp;
            //                    int l = x + 1;
            //                    int r = n - 1;
            //                    int count = r - l;
            //                    if (count > 0)
            //                        listDrill.Reverse(l, count + 1);
            //                    if (index == 0 || index > x)
            //                        index = x;
            //                }
            //                if (index == 0)
            //                    increasedPAList.RemoveRange(1, increasedPAList.Count - 1);
            //                //ProgressBarValChanged?.Invoke(this, EventArgs.Empty);
            //            }
            //        },
            //        () =>
            //        {
            //            List<PA> tempList = new List<PA>();
            //            ListAttributes.ForEach((item) =>
            //            {
            //                tempList.Add((PA)item.Clone());
            //            });
            //            MakeTrainingProgram2(tempList, out List<Drill> listDrill);
            //            СalcFactorial cf = new СalcFactorial();
            //            BigInteger countIterations = cf.Calculate(listDrill.Count());
            //            List<List<PA>> increasedPAList = new List<List<PA>>
            //            {
            //                tempList.ConvertAll(attr => (PA)attr.Clone())
            //            };
            //            int index = 0;
            //            for (BigInteger i = 20160; i < countIterations; i++)
            //            {
            //                int j = 0;
            //                tempList.ForEach(item => item.ValueAttribute = increasedPAList[index][j++].ValueAttribute);
            //                IncreasePlayerAttribute(tempList, increasedPAList, listDrill, index);
            //                CmpMaxAttrsList(tempList, listDrill);
            //                SwapDrill(listDrill, ref index);
            //                if (index == 0)
            //                    increasedPAList.RemoveRange(1, increasedPAList.Count - 1);
            //                //ProgressBarValChanged?.Invoke(this, EventArgs.Empty);
            //                if (i == 40319)
            //                {
            //                    int a = 0;
            //                    a++;
            //                }
            //            }
            //        });
            //stopWatchCalcAttr.Stop();
            //MessageBox.Show(stopWatchCalcAttr.Elapsed.ToString());
            #endregion

            List<PA> listAttributes = new List<PA>();            
            ListAttributes.ForEach((item) =>
            {
                listAttributes.Add((PA)item.Clone());
            });
            MakeTrainingProgram(listAttributes, out List<Drill> listDrill);           
            if (listDrill.Count != 0)
            {
                //The number of permutations for N different elements is N!
                СalcFactorial cf = new СalcFactorial();
                BigInteger countIterations = cf.Calculate(listDrill.Count());
                //Since the permutations go from the bottom up,
                //there is no need to increase data by a new one before the permutation.
                //Let's fill in the data array for each training session
                //and start from the place where the permutation was.    
                List<List<PA>> increasedPAList = new List<List<PA>>
                {
                    listAttributes.ConvertAll(attr => (PA)attr.Clone())
                };
                List<double[]> estimatedValueList = new List<double[]>();
                int index = 0;
                //CalculationAttributesEventArgs e = new CalculationAttributesEventArgs
                //{
                //    CountIterations = countIterationsListDrill
                //};
                //ProgressBarMax?.Invoke(this, e);
                Stopwatch stopWatchCalcAttr = Stopwatch.StartNew();
                for (BigInteger i = 0; i < countIterations; i++)
                {
                    int j = 0;
                    listAttributes.ForEach(
                        item => item.AttributeEstimatedValue = increasedPAList[index][j++].AttributeEstimatedValue);
                    estimatedValueList.RemoveRange(index, estimatedValueList.Count - index);
                    IncreasePlayerAttribute(listAttributes, listDrill, increasedPAList, estimatedValueList, index);
                    CmpMaxAttrsList(listAttributes, listDrill, estimatedValueList);
                    SwapDrill(listDrill, ref index);
                    if (index == 0)
                        increasedPAList.RemoveRange(1, increasedPAList.Count - 1);
                    //ProgressBarValChanged?.Invoke(this, EventArgs.Empty);
                }
                stopWatchCalcAttr.Stop();
                MessageBox.Show(stopWatchCalcAttr.Elapsed.ToString());
            }
            return this;
        }
    }
}
