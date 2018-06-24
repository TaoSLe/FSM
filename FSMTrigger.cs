using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
	/// <summary>
	///  条件基类
	/// </summary>
	public abstract class FSMTrigger  
	{
        public FSMTriggerID TriggerID { get; set; }

        public FSMTrigger()
        {
            Init();
        }

        protected abstract void Init();

        /// <summary>
        /// 条件的逻辑处理
        /// </summary>
        /// <returns></returns>
        public abstract bool HandleTirgger(BaseFSM fsm);
	}
}
