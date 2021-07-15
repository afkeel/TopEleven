using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TrainingCalculator;

namespace RooterTests
{
    using PA = PlayerAttribute;
    [TestClass]
    public class TrainingCalculatorTest
    {
        [TestMethod]
        public void Test_IncreasePlayerAttribute()
        {
            List<PA> actual = new List<PA>();
            actual.Add(new PA(PA.Attributes.Tackling, PA.Color.GRAY, 20));
            actual.Add(new PA(PA.Attributes.Marking, PA.Color.GRAY, 20));
            actual.Add(new PA(PA.Attributes.Positioning, PA.Color.WHITE, 60));
            actual.Add(new PA(PA.Attributes.Heading, PA.Color.WHITE, 60));
            actual.Add(new PA(PA.Attributes.Bravery, PA.Color.GRAY, 20));

            actual.Add(new PA(PA.Attributes.Passing, PA.Color.WHITE, 60));
            actual.Add(new PA(PA.Attributes.Dribling, PA.Color.WHITE, 60));
            actual.Add(new PA(PA.Attributes.Crossing, PA.Color.GRAY, 20));
            actual.Add(new PA(PA.Attributes.Shooting, PA.Color.WHITE, 60));
            actual.Add(new PA(PA.Attributes.Finishing, PA.Color.WHITE, 60));

            actual.Add(new PA(PA.Attributes.Fitness, PA.Color.GRAY, 20));
            actual.Add(new PA(PA.Attributes.Strength, PA.Color.WHITE, 60));
            actual.Add(new PA(PA.Attributes.Aggression, PA.Color.GRAY, 20));
            actual.Add(new PA(PA.Attributes.Speed, PA.Color.WHITE, 60));
            actual.Add(new PA(PA.Attributes.Creativity, PA.Color.WHITE, 60));

            CalculationAttributes rooterTestList = new CalculationAttributes();
            List<double> expected = new List<double>() 
            {
                48.5,20.0,60.0,153.0,20.0,
                290.0,240.0,60.0,233.0,250.0,
                35.0,113.0,20.0,210.0,233.0
            };
            List<Drill> listDrill = new List<Drill>();
            rooterTestList.MakeTrainingProgram(listDrill);
            rooterTestList.IncreasePlayerAttribute(actual, listDrill);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i].ValueAttribute);
            }           
        }

    }
}
