using Constants;
using StateMachine;
using UnityEngine;

namespace BuildingPackage
{
    public class SozialRoom : MonoBehaviour , iSozialRoom, iBuilding
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

        public void Relax()
        {
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
    }
}