using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  巡逻
    /// </summary>
    public class PatrollingState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Patrolling;
        }

        public override void EnterState(BaseFSM fsm)
        {
            base.EnterState(fsm);
            fsm.anim.SetBool(fsm.chStatus.animParams.walk, true);
            fsm.isPatrolComplete = false;
        }

        public override void ExitState(BaseFSM fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.chStatus.animParams.walk, false);
        }

        public override void Action(BaseFSM fsm)
        {
            base.Action(fsm);
            switch (fsm.patrolMode)
            {
                case PatrolMode.Once:
                    OncePatrolling(fsm);
                    break;
                case PatrolMode.Loop:
                    LoopPatrolling(fsm);
                    break;
                case PatrolMode.PingPong:
                    PingPongPatrolling(fsm);
                    break;
            }
        }

        private int index;
        //单次
        private void OncePatrolling(BaseFSM fsm)
        {
            //A   B   C  
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 0.5f)
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    //巡逻完成
                    fsm.isPatrolComplete = true;
                    return;
                }
                index++;
            }
            //移动
            fsm.MoveToTarget(fsm.wayPoints[index].position, fsm.walkSpeed, 0);
        }
        //循环
        private void LoopPatrolling(BaseFSM fsm)
        {
            //A   B   C   ……  A   B   C

            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) <0.5f)
            {
                //if (index == fsm.wayPoints.Length - 1)
                //    index = -1;
                //index++;
                //0   +   1     %   3   
                //1   +   1           3
                //2   +   1           3
                index = (index + 1) % fsm.wayPoints.Length; 
            }
            //移动
            fsm.MoveToTarget(fsm.wayPoints[index].position, fsm.walkSpeed, 0);
        }
        //往返
        private void PingPongPatrolling(BaseFSM fsm)
        {
            //A   B   C   ……  A   B   C 
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 0.5f)
            {
                //索引反转：0    1    2   ……    1   0
                //             A    B   C   …… C   B   A
                //数组反转：0   1   2  ……  0   1   2
                Array.Reverse(fsm.wayPoints);
                index++;
                index = (index + 1) % fsm.wayPoints.Length;
            }
            //移动
            fsm.MoveToTarget(fsm.wayPoints[index].position, fsm.walkSpeed, 0);
        }
    }
}
