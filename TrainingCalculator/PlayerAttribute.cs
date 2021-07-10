using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    public class PlayerAttribute : ICloneable
    {
        public enum Color
        {
            WHITE,
            GRAY
        }
        public Color ColorAttribute { get; set; }
        public double ValueAttribute { get; set; }
        public PlayerAttribute(Color col, double val)
        {
            ColorAttribute = col;
            ValueAttribute = val;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
