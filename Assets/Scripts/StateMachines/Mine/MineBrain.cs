using System;
using System.Collections.Generic;
using AI;
using AI.EnemyAI;
using AI.States;
using Data.UnityObjects;
using Data.ValueObjects.AiData;
using Data.ValueObjects.FrontyardData;
using Enum;
using Managers;
using StateMachines.State;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace StateMachines
{
    public class Mine : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        

        #endregion

        #region Serialized Variables

        [SerializeField]
        private MineManager mineManager; 

        #endregion

        #region Private Variables

        private BombData Data;

        #endregion

        #endregion
        private StateMachine _stateMachine;

        [SerializeField]
        private Animator animator;

        private void Awake()
        {
           Data= GetEnemyData();
            
            GetStatesReferences();
           
        }
        
        private BombData GetEnemyData()=>Resources.Load<CD_Level>("Data/CD_Level").LevelDatas[0].FrontyardData.Bomb[0];

        private void GetStatesReferences()
        {
            var _readyState = new ReadyState();
            var _lureState = new LureState();
            var _explosionState =new ExplosionState();
            var _mineCountDownState =new MineCountDownState();
            // var ExplosionState = ();
            _stateMachine = new StateMachine();
            At(_readyState,_lureState,()=>mineManager.IsPayedTotalAmount);
            // At(_lureState,_explosionState,mineManager.IsPayedTotalAmount);
            // At(_explosionState,_mineCountDownState,mineManager.IsPayedTotalAmount);
            // At(_mineCountDownState,_readyState,mineManager.IsPayedTotalAmount);
            
            _stateMachine.SetState(_readyState);
            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            
            Func<bool> IsMineBought() => () => mineManager.IsPayedTotalAmount;
           
        }


        private void Update() => _stateMachine.Tick();

       
    }
}