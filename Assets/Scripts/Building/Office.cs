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
        private Resources resources;

        void Awake()
        {
            resources = new Resources
            {
                purchasePrice = 0,
                workplace = 1,
                moneyPerSec = -2
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
        }
        
        public float PurchasePrice
        {
            get { return resources.purchasePrice; }
            set { resources.purchasePrice = value; }
        }

        public int Workplace
        {
            get { return resources.workplace; }
            set { resources.workplace = value; }
        }

        public int HitPoints
        {
            get { return hitPoints; }
            set { this.hitPoints = value; }
        }


        public int Managment()
        {
            return resources.workplace * resources.moneyPerSec;
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