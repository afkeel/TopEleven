using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    using PAAttr = PlayerAttribute.Attributes;
    public class Drill : ICloneable
    {
        public int DrillIndex { get; set; }
        public string DrillName { get; set; }
        public PAAttr[] DrillAttributes { get; set; }
        public Drill(int index, string name, PAAttr[] attr)
        {
            DrillIndex = index;
            DrillName = name;
            DrillAttributes = attr;
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
