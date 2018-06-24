using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  闲置状态
    /// </summary>
    public class IdleState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Idle;
        }

        public override void EnterState(BaseFSM fsm)
        {
            base.EnterState(fsm);
            fsm.anim.SetBool(fsm.chStatus.animParams.idle, true);
        }

        public override void ExitState(BaseFSM fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.chStatus.animParams.idle, false);
        }
    }
}
