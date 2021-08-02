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
        public Color AttributeColor { get; set; }
        public double AttributeInputValue { get; set; }
        public double AttributeEstimatedValue { get; set; }
        //public List<double> EstimatedValue { get; set; }
        PlayerAttribute() { }
        public PlayerAttribute(Attributes name, Color col, double val)
        {
            AttributeName = name;
            AttributeColor = col;
            AttributeInputValue = val;
            AttributeEstimatedValue = AttributeInputValue;
            //EstimatedValue = new List<double>
            //{
            //    AttributeValue
            //};
        }
        public object Clone()
        {
            //List<double> list = new List<double>();
            //EstimatedValue.ForEach((item) =>
            //{
            //    list.Add(item);
            //});
            return new PlayerAttribute()
            {
                AttributeName = AttributeName,
                AttributeColor = AttributeColor,
                AttributeInputValue = AttributeInputValue,
                AttributeEstimatedValue = AttributeEstimatedValue
            };
        }
    }
}
