using System;
using System.Collections;
using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class Accounting : MonoBehaviour, iBuilding, iAccounting
    {
        private int hitPoints;
        private float time;
        private BuildingState buildingState;
        private StateController<BuildingState> stateController = new StateController<BuildingState>();
        private Resources resources;
        public int Money;

        void Awake()
        {
            resources = new Resources
            {
                purchasePrice = 0,
                workplace = 1,
                moneyPerSec = 5
            };
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
            StartCoroutine(UpdateManyGenerator());
            //Debug.Log("blabla");
        }
        
        public float PurchasePrice
        {
            get { return resources.purchasePrice; }
            set { resources.purchasePrice = value; }
        }

        public int workplace
        {
            get { return resources.workplace; }
            set { resources.workplace = value; }
        }

        public int HitPoints
        {
            get { return hitPoints; }
            set { this.hitPoints = value; }
        }

        //coroutine -> alternative zu update

        public int Compute()
        {
            int generatedMoney = resources.workplace * resources.moneyPerSec;
            return generatedMoney;
        }

        public void Upgrade()
        {
            //resources.workplace += 5;
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
                StopCoroutine(UpdateManyGenerator());
            }
            else
            {
                stateController.SwitchToLastState();
                StartCoroutine( UpdateManyGenerator());
            }
        }


        private IEnumerator UpdateManyGenerator()
        {
            if (stateController.CurrentState == BuildingState.WORK)
            {
                while (true)
                {
                    Money = Money + Compute();
                   // Debug.Log(Money);
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}