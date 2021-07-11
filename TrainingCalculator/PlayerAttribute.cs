using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    public class PlayerAttribute : ICloneable
    {
        public enum Attributes
        {
            Tackling,
            Marking,
            Positioning,
            Heading,
            Bravery,
            Passing,
            Dribling,
            Crossing,
            Shooting,
            Finishing,
            Fitness,
            Strength,
            Aggression,
            Speed,
            Creativity
        }
        public enum Color
        {
            WHITE,
            GRAY
        }
        public Attributes AttributeName { get; set; }
        public Color ColorAttribute { get; set; }
        public double ValueAttribute { get; set; }
        public PlayerAttribute(Attributes name, Color col, double val)
        {
            AttributeName = name;
            ColorAttribute = col;
            ValueAttribute = val;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public static explicit operator int(PlayerAttribute v)
        {
            throw new NotImplementedException();
        }
    }
}
