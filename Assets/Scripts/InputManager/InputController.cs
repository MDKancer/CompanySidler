using Enums;
using InputWrapper;
using So_Template;
using StateManager;
using UnityEngine;
using TMPro;
using UIDispatcher.UIBuildingContent;
using Zenject;
using Zenject.ProjectContext.Signals;
using Zenject.SceneContext.Signals;

namespace InputManager
{
    public class InputController
    {
        public float speed = 1f;

        public delegate void FocusedBuild(Building.Building focusesBuilding);
        public FocusedBuild showBuildingDataEvent;
        
        private GameObject focusObject;
        private CameraController.CameraController cameraController;
        private Ray ray;
        private RaycastHit rayCastHit;
        private Vector3 middleDirection = Vector3.zero;
        //private float distance;
        private Vector3 focusPoint;
        private static GameObject focusedBuilding;
        private ProceduralUiElements proceduralUiElements = new ProceduralUiElements();
        private TextMeshProUGUI buildingLabel;

        private SignalBus signalBus;
        private StateController<RunTimeState> runtimeStateController;
        private InputBinding inputBinding;
        private MonoBehaviour monoBehaviour;
        private string companyName = "Company";
        
        [Inject]
        private void Init(SignalBus signalBus,
            StateController<RunTimeState> runtimeStateController,
            InputBinding inputBinding,
            MonoBehaviourSignal monoBehaviour,
            CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.runtimeStateController = runtimeStateController;
            this.inputBinding = inputBinding;
            this.monoBehaviour = monoBehaviour;
            this.companyName = companyData.nameCompany;
            //signalBus.Subscribe<ShowBuildingData>(OnWindowOpen);
        }
        public void SetCameraController()
        {
            cameraController = new CameraController.CameraController(signalBus,monoBehaviour,runtimeStateController);
            focusObject = GameObject.Find(companyName)?.gameObject;
            focusPoint = focusObject.transform.position;
            buildingLabel = proceduralUiElements.GetCanvas("");
            buildingLabel.gameObject.SetActive(false);
        }

        public void CameraEvents()
        {
            var transform = cameraController.mainCamera.transform;
            middleDirection = (transform.up + transform.forward) / 2; 
            //distance = Vector3.Distance(cameraController.mainCameraGameObject.transform.position, focusObject.transform.position);

            if (inputBinding.OnPress(Action.MOVE_FORWARD))
            {
                cameraController.Move( middleDirection * speed);
            }
            if (inputBinding.OnPress(Action.MOVE_BACK))
            {
                cameraController.Move( -(middleDirection * speed));
            }
            if (inputBinding.OnPress(Action.MOVE_LEFT))
            {
                cameraController.Move( -(cameraController.mainCamera.transform.right * speed));
            }
            if (inputBinding.OnPress(Action.MOVE_RIGHT))
            {
                cameraController.Move( cameraController.mainCamera.transform.right * speed);
            }
            if (inputBinding.OnPress(Action.ROTATE_RIGHT))
            {
                cameraController.Rotate(Vector3.down,focusPoint,speed);
            }
            if (inputBinding.OnPress(Action.ROTATE_LEFT))
            {
                cameraController.Rotate(Vector3.up,focusPoint,speed);
            }
            if (inputBinding.OnPress(Action.FOCUS_ON))
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
                            
                            var building = (Building.Building)focusedBuilding?.GetComponent(typeof(Building.Building));
                            
                            showBuildingDataEvent(building);
                            
                            cameraController.FocusOn(rayCastHit.point);
                        }
                    }
                }
            }
            if (inputBinding.OnPress(Action.RETURN))
            {
                if(runtimeStateController.CurrentState == RunTimeState.FOCUS_ON || 
                   runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO || 
                   runtimeStateController.CurrentState == RunTimeState.GAME_MENU)
                {
                    cameraController.ToEmptyPos();

                    runtimeStateController.CurrentState = RunTimeState.PLAYING;
                    // the signal bus is to early instantiate as the ShowBuildingData in Game scene new declarated
                    signalBus.Fire(new ShowBuildingData{});
                    focusPoint = focusObject.transform.position;
                }
            }
            ShowNameOffice();
        }
        private bool isBuilding(GameObject targetObject)
        {
            Component[] buildingComponent = targetObject.GetComponents(typeof(Building.Building));

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
                    var targetBuilding = (Building.Building)rayCastHit.collider.GetComponent(typeof(Building.Building));
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