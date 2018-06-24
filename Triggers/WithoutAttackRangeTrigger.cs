using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  目标离开攻击范围
    /// </summary>
    public class WithoutAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTirgger(BaseFSM fsm)
        {
            if (fsm.targetTF == null) return false;
            return Vector3.Distance(fsm.targetTF.position, fsm.transform.position) > fsm.chStatus.attackDistance;
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.WithoutAttackRange;
        }
    }
}
