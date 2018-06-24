using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    ///  状态基类
    /// </summary>
    public abstract class FSMState
    {
        //编号
        public FSMStateID StateID { get; set; }
         
        //条件列表
        private List<FSMTrigger> Triggers;

        //映射表
        private Dictionary<FSMTriggerID, FSMStateID> map;

        public FSMState()
        {
            Triggers = new List<FSMTrigger>();
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            Init();
        }

        protected abstract void Init();

        //添加映射(由状态机调用)
        public void AddMap(FSMTriggerID triggerID,FSMStateID stateID)
        {
            map.Add(triggerID, stateID);
            CreateTriggerObject(triggerID);
        }

        private void CreateTriggerObject(FSMTriggerID triggerID)
        {//反射
            Type type = Type.GetType("AI.FSM."+triggerID+"Trigger");
            FSMTrigger obj = Activator.CreateInstance(type) as FSMTrigger;
            Triggers.Add(obj);
        }

        //检测条件 ( 由状态机调用)
        public void Reason(BaseFSM fsm)
        {
            for (int i = 0; i < Triggers.Count; i++)
            {
                if (Triggers[i].HandleTirgger(fsm))
                {
                    //满足条件
                    //Triggers[i].TriggerID   --->  状态编号？
                    FSMStateID stateID = map[Triggers[i].TriggerID];
                    //调用状态机的切换方法
                    fsm.ChangeState(stateID);
                    return;
                }
            }
        }

        //行为(由状态机调用，子类重写)
        public virtual void EnterState(BaseFSM fsm) { }
        public virtual void Action(BaseFSM fsm) { }
        public virtual void ExitState(BaseFSM fsm) { }

    }
}
