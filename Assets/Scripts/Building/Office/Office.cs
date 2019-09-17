using System.Collections;
using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class Office : MonoBehaviour , iBuilding, iOffice
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
                moneyPerSec = -2
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }
    


        public int Managment()
        {
            return buildingData.workPlacesLimit * buildingData.moneyPerSec;
        }

        public void Upgrade()
        {
            throw new System.NotImplementedException();
        }

        public void GetDamage()
        {
            throw new System.NotImplementedException();
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
                    money += Managment();
                    UIDispatcher.currentBuget += Managment();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}