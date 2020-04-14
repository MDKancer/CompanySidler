using System.Collections;
using Enums;
using StateManager;
using UIDispatcher.GameComponents.UIBuildingContent;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Entity
{
    public class Human : MonoBehaviour , IHuman
    {
        public BuildingType destination = BuildingType.NONE;
        protected ProceduralUiElements proceduralUiElements = new ProceduralUiElements();
        protected NavMeshAgent navMeshAgent;
        private StateController<HumanState> selfState = new StateController<HumanState>();

        public void Awake()
        {
            
        }

        public StateController<HumanState> SelfState { 
            get=>selfState;
            protected set => selfState = value;
        }
        
        protected Vector3 GenerateRandomPosition(Vector3 position)
        {
            return new Vector3(
                Random.Range(position.x-5, position.x+5),      // x
                position.y,                                    // y
                Random.Range(position.z-5, position.z+5)    // z
            );
        }

        protected virtual IEnumerator LifeCycle()
        {
            yield return null;
        }

    }
}