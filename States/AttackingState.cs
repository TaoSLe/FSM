using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	/// <summary>
	///  攻击状态
	/// </summary>
	public class AttackingState : FSMState
	{
        protected override void Init()
        {
            StateID = FSMStateID.Attacking;
        }

        private float attackTime;
        public override void Action(BaseFSM fsm)
        {
            base.Action(fsm);
            if (attackTime <= Time.time)
            {
                fsm.skillSystem.UseRandomSkill();
                attackTime = Time.time + fsm.chStatus.attackInterval;
            }
            fsm.transform.LookAt(fsm.targetTF);
        }

    }
}
