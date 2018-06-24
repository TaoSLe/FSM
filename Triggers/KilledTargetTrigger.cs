using ARPGDemo.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  打死目标
    /// </summary>
    public class KilledTargetTrigger : FSMTrigger
    {
        public override bool HandleTirgger(BaseFSM fsm)
        {
           return fsm.targetTF !=null &&  fsm.targetTF.GetComponent<CharacterStatus>().HP <= 0; 
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.KilledTarget;
        }
    }
}
