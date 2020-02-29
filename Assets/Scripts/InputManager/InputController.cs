using System;
using BootManager;
using BuildingPackage;
using Enums;
using UnityEngine;
using PlayerView;
using StateMachine;
using TMPro;
using UIPackage.UIBuildingContent;
using Zenject;

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
        private UiElements uiElements = new UiElements();
        private TextMeshProUGUI buildingLabel;

        [Inject]
        private StateController<GameState> gameStateController;
        [Inject]
        private StateController<RunTimeState> runtimeStateController;
        public  void Awake()
        {
            // wenn mann von Main Menu anfangen moechte.... dann auskommentieren.
            //if (Boot.gameStateController.CurrentState == GameState.GAME)
            //{
               StartCameraController();
            //}
        }

        public void Start()
        {
            showBuildingDataEvent += PlayerViewController.playerViewController.FocusedBuilding;
        }

        public void StartCameraController()
        {
            cameraController = new CameraController();
            focusObject = GameObject.Find("Company")?.gameObject;
            focusPoint = focusObject.transform.position;
            buildingLabel = uiElements.GetCanvas("");
            buildingLabel.gameObject.SetActive(false);
        }

        public void Update()
        {
            if(gameStateController.CurrentState == GameState.GAME)
            {
                CameraEvents();
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
                            
                            var buildingType = (Building)focusedBuilding?.GetComponent(typeof(Building));
                            
                            showBuildingDataEvent(buildingType);
                            
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