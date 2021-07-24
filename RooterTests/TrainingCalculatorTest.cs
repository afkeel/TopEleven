using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;
using TrainingCalculator;

namespace RooterTests
{
    using PA = PlayerAttribute;
    [TestClass]
    public class TrainingCalculatorTest
    {
        static PA.Attributes[] PASS_GO_AND_SHOOT = { PA.Attributes.Passing, PA.Attributes.Shooting, PA.Attributes.Speed };
        static PA.Attributes[] FAST_COUNTER_ATTACKS = { PA.Attributes.Passing, PA.Attributes.Crossing, PA.Attributes.Finishing, PA.Attributes.Creativity };
        static PA.Attributes[] SKILL_DRILL = { PA.Attributes.Heading, PA.Attributes.Dribling, PA.Attributes.Creativity };
        static PA.Attributes[] SHOOTING_TECHNIQUE = { PA.Attributes.Shooting, PA.Attributes.Finishing, PA.Attributes.Strength };
        static PA.Attributes[] SET_PIECE_DELIVERY = { PA.Attributes.Marking, PA.Attributes.Heading, PA.Attributes.Crossing, PA.Attributes.Shooting };
        static PA.Attributes[] SLALOM_DRIBBLE = { PA.Attributes.Passing, PA.Attributes.Dribling, PA.Attributes.Fitness, PA.Attributes.Speed };
        static PA.Attributes[] WING_PLAY = { PA.Attributes.Heading, PA.Attributes.Crossing, PA.Attributes.Shooting, PA.Attributes.Finishing };
        static PA.Attributes[] ONE_ON_ONE_FINISHING = { PA.Attributes.Tackling, PA.Attributes.Dribling, PA.Attributes.Finishing };

        [TestMethod]
        public void Test_IncreasePlayerAttribute()
        {
            List<PA> startAttr = new List<PA>
            {
                new PA(PA.Attributes.Tackling, PA.Color.GRAY, 20),
                new PA(PA.Attributes.Marking, PA.Color.GRAY, 20),
                new PA(PA.Attributes.Positioning, PA.Color.WHITE, 60),
                new PA(PA.Attributes.Heading, PA.Color.WHITE, 60),
                new PA(PA.Attributes.Bravery, PA.Color.GRAY, 20),

                new PA(PA.Attributes.Passing, PA.Color.WHITE, 60),
                new PA(PA.Attributes.Dribling, PA.Color.WHITE, 60),
                new PA(PA.Attributes.Crossing, PA.Color.GRAY, 20),
                new PA(PA.Attributes.Shooting, PA.Color.WHITE, 60),
                new PA(PA.Attributes.Finishing, PA.Color.WHITE, 60),

                new PA(PA.Attributes.Fitness, PA.Color.GRAY, 20),
                new PA(PA.Attributes.Strength, PA.Color.WHITE, 60),
                new PA(PA.Attributes.Aggression, PA.Color.GRAY, 20),
                new PA(PA.Attributes.Speed, PA.Color.WHITE, 60),
                new PA(PA.Attributes.Creativity, PA.Color.WHITE, 60)
            };

            CalculationAttributes testClass = new CalculationAttributes(startAttr);
            List<double> expectedList = new List<double>() 
            {
                48.5,20.0,60.0,153.0,20.0,
                290.0,240.0,60.0,233.0,250.0,
                35.0,113.0,20.0,210.0,233.0
            };

            List<Drill> listDrill = new List<Drill>
            {
                new Drill(1, "PASS_GO_AND_SHOOT", PASS_GO_AND_SHOOT),
                new Drill(2, "FAST_COUNTER_ATTACKS", FAST_COUNTER_ATTACKS),
                new Drill(3, "SKILL_DRILL", SKILL_DRILL),
                new Drill(4, "SHOOTING_TECHNIQUE", SHOOTING_TECHNIQUE),
                new Drill(5, "SET_PIECE_DELIVERY", SET_PIECE_DELIVERY),
                new Drill(6, "SLALOM_DRIBBLE", SLALOM_DRIBBLE),
                new Drill(7, "WING_PLAY", WING_PLAY),
                new Drill(8, "ONE_ON_ONE_FINISHING", ONE_ON_ONE_FINISHING)
            };
            int index = listDrill.Count;
            //Since the permutations go from the bottom up, 
            //there is no need to increase data by a new one before the permutation.
            //Let's fill in the data array for each training session 
            //and start from the place where the permutation was.
            List<PA> tempList = new List<PA>();
            testClass.IncreasePlayerAttribute(tempList, listDrill, ref index);
            for (int i = 0; i < tempList.Count; i++)
                Assert.AreEqual(expectedList[i], tempList[i].ValueAttribute);

            expectedList.Clear();
            expectedList.AddRange(new double[]
            {
                60.0,20.0,60.0,183.0,20.0,
                217.0,260.0,60.0,317.0,340.0,
                47.0,180.0,20.0,194.0,149.0
            });
            listDrill.Clear();
            listDrill.AddRange(new Drill[]
            {
                new Drill(4, "SHOOTING_TECHNIQUE", SHOOTING_TECHNIQUE),
                new Drill(1, "PASS_GO_AND_SHOOT", PASS_GO_AND_SHOOT),
                new Drill(7, "WING_PLAY", WING_PLAY),
                new Drill(8, "ONE_ON_ONE_FINISHING", ONE_ON_ONE_FINISHING),
                new Drill(2, "FAST_COUNTER_ATTACKS", FAST_COUNTER_ATTACKS),
                new Drill(3, "SKILL_DRILL", SKILL_DRILL),
                new Drill(5, "SET_PIECE_DELIVERY", SET_PIECE_DELIVERY),
                new Drill(6, "SLALOM_DRIBBLE", SLALOM_DRIBBLE)
            });
            index = 0;
            tempList = startAttr;
            testClass.IncreasePlayerAttribute(tempList, listDrill, index);
            for (int i = 0; i < tempList.Count; i++)
                Assert.AreEqual(expectedList[i], tempList[i].ValueAttribute);
        }
        [TestMethod]
        public void Test_SwapDrill()
        {
            CalculationAttributes testClass = new CalculationAttributes();
            List<Drill> listActual = new List<Drill>
            {
                new Drill(1, "PASS_GO_AND_SHOOT", PASS_GO_AND_SHOOT),
                new Drill(2, "FAST_COUNTER_ATTACKS", FAST_COUNTER_ATTACKS),
                new Drill(3, "SKILL_DRILL", SKILL_DRILL),
                new Drill(4, "SHOOTING_TECHNIQUE", SHOOTING_TECHNIQUE),
                new Drill(5, "SET_PIECE_DELIVERY", SET_PIECE_DELIVERY),
                new Drill(6, "SLALOM_DRIBBLE", SLALOM_DRIBBLE),
                new Drill(7, "WING_PLAY", WING_PLAY),
                new Drill(8, "ONE_ON_ONE_FINISHING", ONE_ON_ONE_FINISHING)
        };
            List<Drill> listExpected = new List<Drill>
            {
                new Drill(8, "ONE_ON_ONE_FINISHING", ONE_ON_ONE_FINISHING),
                new Drill(7, "WING_PLAY", WING_PLAY),
                new Drill(6, "SLALOM_DRIBBLE", SLALOM_DRIBBLE),
                new Drill(5, "SET_PIECE_DELIVERY", SET_PIECE_DELIVERY),
                new Drill(4, "SHOOTING_TECHNIQUE", SHOOTING_TECHNIQUE),
                new Drill(3, "SKILL_DRILL", SKILL_DRILL),
                new Drill(2, "FAST_COUNTER_ATTACKS", FAST_COUNTER_ATTACKS),
                new Drill(1, "PASS_GO_AND_SHOOT", PASS_GO_AND_SHOOT)
            };
            СalcFactorial cf = new СalcFactorial();
            BigInteger bi = cf.Calculate(listActual.Count)-1;
            int n = listActual.Count;
            for (int i = 0; i < bi; i++)
            {
                testClass.SwapDrill(listActual, n);
            }
            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(listExpected[i], listActual[i]);
            }
        }

    }
}
