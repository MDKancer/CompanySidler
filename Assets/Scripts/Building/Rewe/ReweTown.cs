using System.Collections;
using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class ReweTown : MonoBehaviour, iBuilding, iRewe
    {
        public int money;
        
        private int hitPoints;
        private float time;
        private BuildingState buildingState;
        private StateController<BuildingState> stateController = new StateController<BuildingState>();
        private BuildingData buildingData;

        void Awake()
        {
            buildingData = new BuildingData
            {
                name = name,
                upgradePrice = 0,
                workPlacesLimit = 1,
                moneyPerSec = 10
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }

        public int Programming()
        {
            return buildingData.workPlacesLimit * buildingData.moneyPerSec;
        }

        public void Upgrade()
        {
            //es kann nicht geupgradet werden
        }

        public void GetDamage()
        {
            //es kann keinen Schaden bekommen
        }

        public void Work()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
            }
        }

        public void SwitchState()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                stateController.CurrentState = BuildingState.PAUSE;
            }
            else
            {
                stateController.SwitchToLastState();
            }
        }
        
        public BuildingData BuildingData { get=> buildingData; set => buildingData = value; }
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (true)
                {
                    money += Programming();
                    UIDispatcher.currentBuget += Programming();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}