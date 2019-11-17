using System.Linq;
using BootManager;
using BuildingPackage;
using Enums;
using UnityEngine;
using PlayerView;
using TMPro;
using UIPackage.UIBuildingContent;

namespace InputManager
{
    public class InputController 
    {
        public float speed = 1f;

        private readonly GameObject focusObject;
        private readonly CameraController cameraController;
        private readonly GameObject camera;
        private Camera main;
        private Ray ray;
        private RaycastHit rayCastHit;
        private float distance;
        private Vector3 focusPoint;
        private static GameObject focusedBuilding;
        private UiElements uiElements = new UiElements();
        private TextMeshProUGUI buildingLabel;
        public  InputController()
        {
            focusObject = GameObject.Find("Company");
            focusPoint = focusObject.transform.position;
            camera = Camera.main?.gameObject;
            cameraController = new CameraController();
            buildingLabel = uiElements.GetCanvas("");
            buildingLabel.gameObject.SetActive(false);
            main = Camera.main;
        }

        public void Do()
        {
            Vector3 middleDirection = (camera.transform.up + camera.transform.forward) / 2;
            distance = Vector3.Distance(camera.transform.position, focusObject.transform.position);
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
                cameraController.Move( -(camera.transform.right * speed));
            }
            if (Input.GetKey(KeyCode.D))
            {
                cameraController.Move( camera.transform.right * speed);
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
                if (Boot.runtimeStateController.CurrentState != RunTimeState.FOCUS_ON &&
                    Boot.runtimeStateController.CurrentState != RunTimeState.BUILDING_INFO)
                {
                    ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                    
                    if (Physics.Raycast(ray,out rayCastHit))
                    {
                        if(isBuilding(rayCastHit.collider.gameObject))
                        {
                            
                            focusPoint = rayCastHit.point;
                            focusedBuilding = rayCastHit.collider.gameObject;
                            cameraController.FocusOn(rayCastHit.point);
                        }
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(Boot.runtimeStateController.CurrentState == RunTimeState.FOCUS_ON || 
                   Boot.runtimeStateController.CurrentState == RunTimeState.BUILDING_INFO || 
                   Boot.runtimeStateController.CurrentState == RunTimeState.GAME_MENU)
                {
                    cameraController.ToEmptyPos();

                    Boot.runtimeStateController.CurrentState = RunTimeState.PLAYING;
                    focusPoint = focusObject.transform.position;
                }
            }

            ShowNameOffice();
        }
        /// <summary>
        /// Gibt den gecklikten Gebäude zurrück.
        /// </summary>
        public static GameObject FocusedBuilding
        {
            get => focusedBuilding;
            private set => focusedBuilding = value;
        }

        private bool isBuilding(GameObject targetObject)
        {
            Component[] buildingComponent = targetObject.GetComponents(typeof(Building));

            return buildingComponent.Length > 0;
        }

        private void ShowNameOffice()
        {
            ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
              
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
                    rectTransform.rotation = Quaternion.LookRotation(main.transform.forward);
                }
                else
                {
                    buildingLabel.gameObject.SetActive(false);     
                }
            }
        }
    }
}