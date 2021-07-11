using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingCalculator
{
    using PAAttr = PlayerAttribute.Attributes;
    class Drill
    {
        public string DrillName { get; set; }
        public PAAttr[] DrillAttributes { get; set; }
        public Drill(string name, PAAttr[] attr)
        {
            DrillName = name;
            DrillAttributes = attr;
            //InitListDrill();
        }
        //private List<PA[]> listDrill = new List<PA[]>();
        //public List<PA[]> ListDrill
        //{
        //    get
        //    {
        //        return listDrill;
        //    }
        //    set
        //    {
        //        listDrill = value;
        //    }
        //}
        //public void InitListDrill()
        //{
        //    listDrill.Add(PASS_GO_AND_SHOOT);
        //    listDrill.Add(FAST_COUNTER_ATTACKS);
        //    listDrill.Add(SKILL_DRILL);
        //    listDrill.Add(SHOOTING_TECHNIQUE);
        //    listDrill.Add(SET_PIECE_DELIVERY);
        //    listDrill.Add(SLALOM_DRIBBLE);
        //    listDrill.Add(WING_PLAY);
        //    listDrill.Add(ONE_ON_ONE_FINISHING);

        //    //training.Add(8, PRESS_THE_PLAY);
        //    //training.Add(9, PIGGY_IN_THE_MIDDLE);
        //    //training.Add(10, USE_YOUR_HEAD);
        //    //training.Add(11, STOP_THE_ATTACKER);
        //    //training.Add(12, DEFENDING_CROSSES);
        //    //training.Add(13, VIDEO_ANALYSIS);
        //    //training.Add(14, HOLD_THE_LINE);

        //    //training.Add(15, WARM_UP);
        //    //training.Add(16, STRETCH);
        //    //training.Add(17, SPRINT);
        //    //training.Add(18, CARIOCA_WITH_LADDERS);
        //    //training.Add(19, LONG_RUN);
        //    //training.Add(20, GYM);
        //    //training.Add(21, SHUTTLE_RUNS);
        //    //training.Add(22, HURDLE_JUMPS);

        //}
    }
}
