using System.Collections;
using BuildingPackage;
using Enums;
using UnityEngine;
using PlayerView;
using StateMachine;
using TMPro;
using UIPackage.UIBuildingContent;
using Zenject;
using Zenject_Signals;

namespace InputManager
{
    public class InputController : MonoBehaviour
    {
        public float speed = 1f;

        public delegate void FocusedBuild(Building focusesBuilding);
        public FocusedBuild showBuildingDataEvent;
        
        private GameObject focusObject;
        private CameraController cameraController;
        private Ray ray;
        private RaycastHit rayCastHit;
        private Vector3 middleDirection = Vector3.zero;
        //private float distance;
        private Vector3 focusPoint;
        private static GameObject focusedBuilding;
        private ProceduralUiElements proceduralUiElements = new ProceduralUiElements();
        private TextMeshProUGUI buildingLabel;

        private SignalBus signalBus;
        private StateController<GameState> gameStateController;
        private StateController<RunTimeState> runtimeStateController;
        private MonoBehaviour monoBehaviour;
        private string companyName = "Company";
        [Inject]
        private void Init(SignalBus signalBus, StateController<GameState> gameStateController,
            StateController<RunTimeState> runtimeStateController,MonoBehaviourSignal monoBehaviourSignal,CompanyData companyData)
        {

            this.signalBus = signalBus;
            this.gameStateController = gameStateController;
            this.runtimeStateController = runtimeStateController;
            this.monoBehaviour = monoBehaviourSignal;
            this.companyName = companyData.nameCompany;
            //signalBus.Subscribe<ShowBuildingData>(OnWindowOpen);
            signalBus.Subscribe<GameStateSignal>(StateDependency);
        }

        private void StateDependency(GameStateSignal gameStateSignal)
        {
            switch (gameStateSignal.state)
            {
                case GameState.NONE:
                    break;
                case GameState.INTRO:
                    break;
                case GameState.LOADING:
                    break;
                case GameState.MAIN_MENU:
                    break;
                case GameState.PREGAME:
                    break;
                case GameState.GAME:
                    SetCameraController();
                    showBuildingDataEvent += PlayerViewController.playerViewController.FocusedBuilding;
                    StartCoroutine(CameraUpdate());
                    break;
                case GameState.EXIT:
                    break;
            }
        }
        public  void Awake()
        {

        }

        public void Start()
        {
            
        }

        public void SetCameraController()
        {
            cameraController = new CameraController(signalBus,monoBehaviour,runtimeStateController);
            focusObject = GameObject.Find(companyName)?.gameObject;
            focusPoint = focusObject.transform.position;
            buildingLabel = proceduralUiElements.GetCanvas("");
            buildingLabel.gameObject.SetActive(false);
        }

        private IEnumerator CameraUpdate()
        {
            while (gameStateController.CurrentState == GameState.GAME)
            {
                    CameraEvents();
                yield return null;
            }
        }

        private void CameraEvents()
        {
            var transform = cameraController.mainCamera.transform;
            middleDirection = (transform.up + transform.forward) / 2; 
            //distance = Vector3.Distance(cameraController.mainCameraGameObject.transform.position, focusObject.transform.position);

            if (Input.GetKey(KeyCode.W))
            {
                cameraController.Move( middleDirection * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                cameraController.Move( -(middleDirection * speed));
            }
            if (Input.GetKey(KeyCode.A))
            {
                cameraController.Move( -(cameraController.mainCamera.transform.right * speed));
            }
            if (Input.GetKey(KeyCode.D))
            {
                cameraController.Move( cameraController.mainCamera.transform.right * speed);
            }
            if (Input.GetKey(KeyCode.E))
            {
                cameraController.Rotate(Vector3.down,focusPoint,speed);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                cameraController.Rotate(Vector3.up,focusPoint,speed);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (runtimeStateController.CurrentState != RunTimeState.FOCUS_ON &&
                    runtimeStateController.CurrentState != RunTimeState.BUILDING_INFO)
                {
                    ray = cameraController.mainCamera.ScreenPointToRay(Input.mousePosition);
                    
                    if (Physics.Raycast(ray,out rayCastHit))
                    {
                        if(isBuilding(rayCastHit.collider.gameObject))
                        {
                            
                            focusPoint = rayCastHit.point;
                            focusedBuilding = rayCastHit.collider.gameObject;
                            
                            var building = (Building)focusedBuilding?.GetComponent(typeof(Building));
                            
                            showBuildingDataEvent(building);
                            
                            cameraController.FocusOn(rayCastHit.point);
                        }
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(runtimeStateController.CurrentState == RunTimeState.FOCUS_ON || 
                   runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO || 
                   runtimeStateController.CurrentState == RunTimeState.GAME_MENU)
                {
                    cameraController.ToEmptyPos();

                    runtimeStateController.CurrentState = RunTimeState.PLAYING;
                    signalBus.Fire(new ShowBuildingData{});
                    focusPoint = focusObject.transform.position;
                }
            }
            ShowNameOffice();
        }
        
        /// <summary>
        /// Gibt den gecklikten Gebäude zurrück.
        /// </summary>
//        public static GameObject FocusedBuilding
//        {
//            get => focusedBuilding;
//            private set => focusedBuilding = value;
//        }

        private bool isBuilding(GameObject targetObject)
        {
            Component[] buildingComponent = targetObject.GetComponents(typeof(Building));

            return buildingComponent.Length > 0;
        }

        private void ShowNameOffice()
        {
            ray = cameraController.mainCamera.ScreenPointToRay(Input.mousePosition);
              
            if (Physics.Raycast(ray,out rayCastHit))
            {
                if(isBuilding(rayCastHit.collider.gameObject))
                {
                    buildingLabel.gameObject.SetActive(true);        
                    var targetBuilding = (Building)rayCastHit.collider.GetComponent(typeof(Building));
                    buildingLabel.SetText(targetBuilding.BuildingData.name);
                    
                    
                    RectTransform rectTransform = buildingLabel.GetComponent<RectTransform>();
                    var transform = targetBuilding.transform;
                    rectTransform.position =
                        transform.position + (transform.up * 30f);
                    rectTransform.rotation = Quaternion.LookRotation(cameraController.mainCamera.transform.forward);
                }
                else
                {
                    buildingLabel.gameObject.SetActive(false);     
                }
            }
        }
    }
}