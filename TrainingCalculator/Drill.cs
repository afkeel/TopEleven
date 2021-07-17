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
        public string DrillName { get; set; }
        public PAAttr[] DrillAttributes { get; set; }
        public Drill(string name, PAAttr[] attr)
        {
            DrillName = name;
            DrillAttributes = attr;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
