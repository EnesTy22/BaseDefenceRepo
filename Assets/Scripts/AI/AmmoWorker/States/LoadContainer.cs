using AI;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrain.States
{
    public class LoadContainer:IState
    {
        private NavMeshAgent agent;
        private Animator animator;
        private float movementSpeed;
        private Transform ammoWareHouse;
      
        public LoadContainer(NavMeshAgent agent, Animator animator, float movementSpeed, Transform ammoWareHouse)
        {
            this.agent = agent;
            this.animator = animator;
            this.movementSpeed = movementSpeed;
            this.ammoWareHouse = ammoWareHouse;
        }

        public void OnEnter()
        {
            Debug.Log("LoadContayner");
            agent.speed = 0;
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }

        public void SendAmmoStack()
        {

        }
    }
}