using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    using PA = PlayerAttribute;
    public class Drill : ICloneable
    {
        public int DrillIndex { get; set; }
        public string DrillName { get; set; }
        public List<PA> DrillAttributes { get; set; }
        public int MaxAverageDrillQuality { get; set; }
        public int CountAttributes { get; set; }
        public int CountWhite { get; set; }
        public int CountGray { get; set; }
        public Drill(int index, string name, List<PA> attr)
        {
            DrillIndex = index;
            DrillName = name;
            DrillAttributes = attr;
            CountAttributes = attr.Count;
            CountGray = FindGrayAttr(attr).Count;
            CountWhite = CountAttributes - CountGray;
        }
        public List<PA> FindGrayAttr(List<PA> attr)
        {
            return new List<PA>(attr.FindAll(
            delegate (PA pa)
            {
                return pa.ColorAttribute == PA.Color.GRAY;
            }));          
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public override bool Equals(object obj)
        {
            return DrillIndex == ((Drill)obj).DrillIndex
                && DrillName == ((Drill)obj).DrillName
                && DrillAttributes == ((Drill)obj).DrillAttributes;
        }
    }
}
