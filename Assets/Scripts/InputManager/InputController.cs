using System.Linq;
using BootManager;
using BuildingPackage;
using Enums;
using UnityEngine;
using PlayerView;

namespace InputManager
{
    public class InputController 
    {
        public float speed = 0.3f;

        private readonly GameObject focusObject;
        private readonly CameraController cameraController;
        private readonly GameObject camera;
        private Ray ray;
        private RaycastHit rayCastHit;
        private float distance;
        private Vector3 focusPoint;
        private static GameObject focusedBuilding;
        public  InputController()
        {
            focusObject = GameObject.Find("Firma");
            focusPoint = focusObject.transform.position;
            camera = Camera.main?.gameObject;
            cameraController = new CameraController();
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
    }
}