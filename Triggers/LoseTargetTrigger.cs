using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  丢失目标
    /// </summary>
    public class LoseTargetTrigger : FSMTrigger
    {
        public override bool HandleTirgger(BaseFSM fsm)
        {
            return fsm.targetTF == null;
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.LoseTarget;
        }
    }
}
