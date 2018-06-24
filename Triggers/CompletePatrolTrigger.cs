using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  巡逻完成条件
    /// </summary>
    public class CompletePatrolTrigger : FSMTrigger
    {
        public override bool HandleTirgger(BaseFSM fsm)
        {
            return fsm.isPatrolComplete;
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.CompletePatrol;
        }
    }
}
