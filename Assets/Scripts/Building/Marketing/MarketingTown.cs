using System.Collections;
using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class MarketingTown : MonoBehaviour, iBuilding,iMarketing
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
                moneyPerSec = -3
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }
        //coroutine -> alternative zu update

        public int ToTrade()
        {
            return buildingData.workPlacesLimit * buildingData.moneyPerSec;
        }

        public void Upgrade()
        {
            //resources.workplace += 5;
        }

        public void GetDamage()
        {
            
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
                StopCoroutine(UpdateManyGenerator());
            }
            else
            {
                stateController.SwitchToLastState();
                StartCoroutine( UpdateManyGenerator());
            }
        }
        public BuildingData BuildingData { get=> buildingData; set => buildingData = value; }

        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (true)
                {
                    money += ToTrade();
                    UIDispatcher.currentBuget += ToTrade();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}