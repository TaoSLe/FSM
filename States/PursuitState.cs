using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  追逐
    /// </summary>
    public class PursuitState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Pursuit;
        }

        public override void EnterState(BaseFSM fsm)
        {
            base.EnterState(fsm);
            fsm.anim.SetBool(fsm.chStatus.animParams.run, true);
        }

        public override void ExitState(BaseFSM fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.chStatus.animParams.run, false);
            fsm.StopMove();
        }

        public override void Action(BaseFSM fsm)
        {
            base.Action(fsm);

            fsm.MoveToTarget(fsm.targetTF.position, fsm.runSpeed, fsm.chStatus.attackDistance);
        }
    }
}
