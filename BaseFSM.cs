using ARPGDemo.Character;
using ARPGDemo.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.FSM
{
	/// <summary>
	///  基础状态机
	/// </summary>
	public class BaseFSM : MonoBehaviour 
	{
        public string configFile;

        #region 脚本生命周期
        private void Start()
        {
            InitComponent();
            ConfigFSM();
            InitDefaultState();
        }

        public FSMStateID currentStateID;
        private void Update()
        { 
            currentState.Reason(this);
            currentState.Action(this);
            FindTarget();
            //***********调试********
            currentStateID = currentState.StateID;
        }
        #endregion

        #region 状态机自身成员
        //状态列表
        private List<FSMState> states;

        //读取配置文件 反射创建对象
        //配置状态机
        //private void ConfigFSM()
        //{
        //    //创建列表
        //    states = new List<FSMState>();
        //    //创建状态
        //    IdleState idle = new IdleState();
        //    //添加到列表中
        //    states.Add(idle);
        //    //配置状态(添加映射)
        //    idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);

        //    DeadState dead = new DeadState();
        //    states.Add(dead);

        //    PursuitState pursuit = new PursuitState();
        //    pursuit.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    pursuit.AddMap(FSMTriggerID.ReachTarget, FSMStateID.Attacking);
        //    pursuit.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Default);
        //    states.Add(pursuit);

        //    AttackingState attack = new AttackingState();
        //    attack.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    attack.AddMap(FSMTriggerID.WithoutAttackRange, FSMStateID.Pursuit);
        //    attack.AddMap(FSMTriggerID.KilledTarget, FSMStateID.Default);
        //    states.Add(attack);

        //    PatrollingState patrolling = new PatrollingState();
        //    patrolling.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    patrolling.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    patrolling.AddMap(FSMTriggerID.CompletePatrol, FSMStateID.Idle);

        //    states.Add(patrolling);
        //}

        //当前状态
         
        private void ConfigFSM()
        {
            //var map = AIConfigurationReader.map;
            //var map = new AIConfigurationReader(configFile).map;
            var map = AIConfigurationReaderFactory.GetConfigMap(configFile);

            //作业：
            //读取配置文件
            //形成数据结构 Dictionary<状态, Dictionary<条件编号,状态编号>>
            //注意：行数据的可能性[Idle]     NoHealth>Dead   空

            states = new List<FSMState>();
            //遍历大字典 状态
            //遍历小字典 映射
            foreach (string stateName in map.Keys)
            {
                Type type = Type.GetType("AI.FSM." + stateName + "State");
                FSMState stateObj = Activator.CreateInstance(type) as FSMState;
                states.Add(stateObj);
                foreach (var item in map[stateName])
                {
                    //item.Key
                    //item.Value
                    var triggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), item.Key);
                    var stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), item.Value);
                    stateObj.AddMap(triggerID, stateID);
                }
            }
        }
         
        private FSMState currentState;

        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;
        private FSMState defaultState;
        private void InitDefaultState()
        {
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentState.EnterState(this);
        }

        //切换状态（状态类调用）
        public void ChangeState(FSMStateID stateID)
        {
            FSMState targetState;
            //如果需要切换的是默认状态 则
            //FSMStateID.Default
            if (stateID == FSMStateID.Default)
                targetState = defaultState;
            else
                targetState = states.Find(s => s.StateID == stateID);

            //退出之前状态
            currentState.ExitState(this);
            //切换状态
            currentState = targetState;
            //进入当前状态
            currentState.EnterState(this);
        }
        #endregion

        #region 为条件和状态提供的成员
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public CharacterStatus chStatus;
        [Tooltip("目标标签")]
        public string[] targetTags = { "Player" };
        [Tooltip("搜索距离")]
        public float findDistance = 10;
        [Tooltip("跑步速度")]
        public float runSpeed = 5;
        [Tooltip("走路速度")]
        public float walkSpeed = 2;
        [HideInInspector]
        public Transform targetTF; 
        private NavMeshAgent navAgent;
        [HideInInspector]
        public CharacterSkillSystem skillSystem;
        [Tooltip("巡逻点")]
        public Transform[] wayPoints;
        [Tooltip("巡逻模式")]
        public PatrolMode patrolMode;
        [Tooltip("巡逻是否完成")]
        public bool isPatrolComplete;

        private void InitComponent()
        {
            anim = GetComponentInChildren<Animator>();
            chStatus = GetComponent<CharacterStatus>();
            navAgent = GetComponent<NavMeshAgent>();
            skillSystem = GetComponent<CharacterSkillSystem>();
        }
          
        //查找目标方法(由Update调用)
        private void FindTarget()
        { 
            SkillData data = new SkillData()
            {
                attackTargetTags = targetTags,
                attackDistance = findDistance,
                attackAngle = 360,
                attackType = SkillAttackType.Single
            };
            Transform[] array = new SectorSelector().SelectTarget(transform, data);
            targetTF = array.Length == 0 ? null : array[0];
        }

        //供追逐、巡逻状态调用
        public void MoveToTarget(Vector3 pos,float speed,float stopDistance)
        { 
            //通过寻路组件运动
            navAgent.SetDestination(pos);
            navAgent.speed = speed;
            navAgent.stoppingDistance = stopDistance;
        }

        public void StopMove()
        {
            navAgent.enabled = false;
            navAgent.enabled = true; 
        }
        #endregion

    }
}
