using AI;
using Managers;
using UnityEngine;

namespace StateMachines.State
{
    public class MineCountDownState:IState
    {
        #region Self Variables

        #region Public Variables
        public bool IsTimerDone => (timer >= _mineBrain.mineManager.MineCountDownTime);
        #endregion

        #region Private Variables
        private float timer=0;
        private MineBrain _mineBrain;

        #endregion
        

        #endregion
        
        public MineCountDownState(MineBrain mineBrain)
        {
            _mineBrain=mineBrain;
            
        }
        public void Tick()
        {
            timer += Time.deltaTime;
        }

        public void OnEnter()
        {
            ResetTimer();
        }

        private void ResetTimer()
        {
            timer = 0;
        }

        public void OnExit()
        {
            ResetTimer();
        }
    }
}