using Constants;
using Life;
using StateMachine;
using UnityEngine;

namespace Life
{
    public class Human : MonoBehaviour , IHuman
    {
        public BuildingType destination = BuildingType.NONE;
        [SerializeField]
        protected HumanData humanData;
        
        private StateController<HumanState> selfState = new StateController<HumanState>();
        
        public StateController<HumanState> SelfState { 
            get=>selfState;
            protected set => selfState = value;
        }
        
        protected Vector3 GenerateRandomPosition(Vector3 position)
        {
            return new Vector3(
                Random.Range(position.x-9, position.x+9),
                position.y,
                Random.Range(position.z-9, position.z+9)
            );
        }

        protected float TimeToDo()
        {
            if (SelfState.CurrentState == HumanState.WALK)
            {
                return Random.Range(0.2f,1f);
            }
            else if (SelfState.CurrentState == HumanState.WORK)
            {
                return Random.Range(5f,20f);
            }
            else if (SelfState.CurrentState == HumanState.LEARN)
            {
                return Random.Range(5f,15f);
            }
            else if (SelfState.CurrentState == HumanState.PAUSE)
            {
                return Random.Range(2f,15f);
            }
            else if (SelfState.CurrentState == HumanState.NONE)
            {
                return Random.Range(0.3f,5f);
            }

            return 0f;
        }
    }
}