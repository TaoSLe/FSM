using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  没有生命条件
    /// </summary>
    public class NoHealthTrigger : FSMTrigger
    {
        public override bool HandleTirgger(BaseFSM fsm)
        {
            return fsm.chStatus.HP <= 0;
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.NoHealth;
        }
    }
}
