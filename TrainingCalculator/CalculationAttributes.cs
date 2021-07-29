﻿using System;
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
        private void Swap(List<Drill> list, int i , int j)
        {
            Drill temp;
            temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        ////// Permutations without repetitions //////
        public void SwapDrill(List<Drill> list, int n, ref int index)
        {
            //The task of generating permutations in lexicographic order. 
            //In this case, all permutations are sorted first by the first number, then by the second, etc.in ascending order. 
            //Thus, the first one will be the permutation 1 2... N, and the last one will be N N-1... 1.
            if (n>1)
            {
                int i = n - 2;
                while (i!=-1 && list[i].DrillIndex >= list[i + 1].DrillIndex)
                    i--;
                if (i==-1)
                    return;
                int j = n - 1;
                while (list[i].DrillIndex >= list[j].DrillIndex)
                    j--;
                Swap(list, i, j);
                int l = i + 1;
                int r = n - 1;
                int count = r - l;
                if (count > 0)
                    list.Reverse(l, count + 1);
                if (index > i)
                    index = i;
            }            
        }
        private void CmpMaxAttrsList(List<PA> listAttributes, List<Drill>  listDrill)
        {
            bool add = false;
            for (int i = 0; i < listAttributes.Count; i++)
            {
                if (listAttributes[i].ColorAttribute == PA.Color.WHITE && 
                    listAttributes[i].ValueAttribute > MaxAttrs[i])
                {
                    MaxAttrs[i] = listAttributes[i].ValueAttribute;
                    add = true;
                }
            }
            if (add)
            {
                MaxAttrsList.Add(listAttributes.ConvertAll(attr => (PA)attr.Clone()));
                MaxAttrsDrill.Add(listDrill.ConvertAll(drill => (Drill)drill.Clone()));
            }
        }
        private bool CheackMaxVal(List<PA> listGrayAttr)
        {           
            return !listGrayAttr.Exists(x => x.ValueAttribute >= MaxGrayAttrVal);
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
            for (int i = 0; i < arrPA.Count; i++)
                sum += arrPA[i].ValueAttribute;
            return sum;
        }
        public void IncreasePlayerAttribute(List<PA> tempList, List<Drill> listDrill, List<List<PA>> increasedPAList)
        {          
            increasedPAList.Add(tempList.ConvertAll(attr => (PA)attr.Clone()));
            for (int i = 0; i < listDrill.Count; i++)
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
                        if (item.ColorAttribute == PA.Color.WHITE)
                        {
                            ++item.ValueAttribute;
                            ++sumAttributes;
                        }
                        else
                        {
                            item.ValueAttribute += 0.5;
                            sumAttributes += 0.5;
                        } 
                    }
                }
                InitMaxAverageDrillQuality(listDrill[i], sumAttributes, checkGreyA);
                increasedPAList.Add(tempList.ConvertAll(attr => (PA)attr.Clone()));               
            }
            CmpMaxAttrsList(tempList, listDrill);
        }
        public void IncreasePlayerAttribute(List<Drill> listDrill, int index)
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
                        if (item.ColorAttribute == PA.Color.WHITE)
                        {
                            ++item.ValueAttribute;
                            ++sumAttributes;
                        }
                        else
                        {
                            item.ValueAttribute += 0.5;
                            sumAttributes += 0.5;
                        }
                    }
                }
                InitMaxAverageDrillQuality(listDrill[i], sumAttributes, checkGreyA);
            }
        }
        public void MakeTrainingProgram(List<PA> startList, out List<Drill> listDrill)
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
        public CalculationAttributes Calculation()
        {
            List<PA> tempList = new List<PA>();
            ListAttributes.ForEach((item) =>
            {
                tempList.Add((PA)item.Clone());
            });
            MakeTrainingProgram(tempList, out List <Drill> listDrill);           
            if (listDrill.Count != 0)
            {
                int lenListDrill = listDrill.Count();
                int lenListAttr = ListAttributes.Count();
                //The number of permutations for N different elements is N!
                СalcFactorial cf = new СalcFactorial();
                BigInteger countIterationsListDrill = cf.Calculate(lenListDrill);
                //Since the permutations go from the bottom up,
                //there is no need to increase data by a new one before the permutation.
                //Let's fill in the data array for each training session
                //and start from the place where the permutation was.    
                List<List<PA>> increasedPAList = new List<List<PA>>();
                int index = lenListDrill;
                IncreasePlayerAttribute(tempList, listDrill, increasedPAList);
                --countIterationsListDrill;               
                SwapDrill(listDrill, lenListDrill, ref index);
                //CalculationAttributesEventArgs e = new CalculationAttributesEventArgs
                //{
                //    CountIterations = countIterationsListDrill
                //};
                //ProgressBarMax?.Invoke(this, e);
                Stopwatch stopWatchCalcAttr = Stopwatch.StartNew();
                for (BigInteger i = 0; i < countIterationsListDrill; i++)
                {
                    int j = 0;
                    foreach (var item in increasedPAList[index])
                    {
                        tempList[j++].ValueAttribute = item.ValueAttribute;
                    }
                    IncreasePlayerAttribute(listDrill, index);
                    CmpMaxAttrsList(tempList, listDrill);
                    SwapDrill(listDrill, lenListDrill, ref index);
                    //ProgressBarValChanged?.Invoke(this, EventArgs.Empty);
                }
                stopWatchCalcAttr.Stop();
                MessageBox.Show(stopWatchCalcAttr.Elapsed.ToString());
            }                      
            return this;
        }
    }
}
