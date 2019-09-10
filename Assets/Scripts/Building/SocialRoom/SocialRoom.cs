using System.Collections;
using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class SocialRoom : MonoBehaviour, iBuilding, iSocialRoom
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
                moneyPerSec = -5
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


        public int Relax()
        {
            return  resources.workplace * resources.moneyPerSec;
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
        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (true)
                {
                    money += Relax();
                    UIDispatcher.currentBuget += Relax();
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}