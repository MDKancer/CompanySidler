using Enums;
using StateManager;
using UIDispatcher.GameComponents.UIBuildingContent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity
{
    public class Human : MonoBehaviour , IHuman
    {
        
        public BuildingType destination = BuildingType.NONE;
        protected ProceduralUiElements proceduralUiElements = new ProceduralUiElements();
        
        private StateController<HumanState> selfState = new StateController<HumanState>();

        public delegate void AttachAnBuilding(Building.Building myOffice);

        public AttachAnBuilding AttachEvent;
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
        
    }
}