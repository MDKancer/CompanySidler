using Constants;
using StateMachine;
using UnityEngine;

namespace Human
{
    public class Human : MonoBehaviour , IHuman
    {
        
        public BuildingType destination = BuildingType.NONE;
        
        
        private StateController<HumanState> selfState = new StateController<HumanState>();

        public StateController<HumanState> SelfState { 
            get=>selfState;
            protected set => selfState = value;
        }
        
        protected Vector3 GenerateRandomPosition(Vector3 position)
        {
            return new Vector3(
                Random.Range(position.x-9, position.x+9),      // x
                position.y,                                    // y
                Random.Range(position.z-9, position.z+9)    // z
            );
        }
    }
}