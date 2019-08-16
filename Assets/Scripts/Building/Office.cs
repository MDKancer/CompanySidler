using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class Office : MonoBehaviour , iBuilding, iOffice
    {
        private int hitPoints;
        private float time;
        private BuildingState buildingState;
        private StateController<BuildingState> stateController = new StateController<BuildingState>();
        private Resources resources;

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

        void Awake()
        {
            stateController.CurrentState = BuildingState.EMPTY;
        }

        void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
        }

        void Update()
        {
        }

        public void Managment()
        {
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
    }
}