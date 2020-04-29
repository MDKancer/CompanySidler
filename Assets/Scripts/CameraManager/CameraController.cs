using System.Collections;
using Enums;
using StateManager;
using UnityEngine;
using Zenject;
using Zenject.SceneContext.Signals;

namespace CameraManager
{
    public class CameraController
    {
        private readonly GameObject mainCameraGameObject;
        public readonly Camera mainCamera;
        private Vector3 originPosition;
        private Quaternion originRotation;
        private bool isArrive;
        private SignalBus signalBus;
        private StateController<RunTimeState> runtimeStateController;
        private MonoBehaviour monoBehaviour;

        public CameraController(SignalBus signalBus,MonoBehaviour monoBehaviour,StateController<RunTimeState> runtimeStateController)
        {
            mainCamera = Camera.main;
            mainCameraGameObject = mainCamera.gameObject;
            this.signalBus = signalBus;
            this.runtimeStateController = runtimeStateController;
            this.monoBehaviour = monoBehaviour;
        }
        /// <summary>
        /// this Funktion moved the Camera to the fokused building.
        /// and set the runtimeState to FocusOn.
        /// </summary>
        /// <param name="endPosition"> it ist the position of the building.</param>
        public void FocusOn(Vector3 endPosition)
        {
            isArrive = false;
            runtimeStateController.CurrentState = RunTimeState.FOCUS_ON;

            originPosition = mainCameraGameObject.transform.position;
            originRotation = mainCameraGameObject.transform.rotation;

            monoBehaviour.StartCoroutine(MoveTo(endPosition));
            monoBehaviour.StartCoroutine(ChangeState());
        }
        /// <summary>
        /// Move the camera to the last position, that before the building was focused.
        /// and switched to the last state, what was before the BuildingInfo.
        /// </summary>
        public void ToEmptyPos()
        {
            monoBehaviour.StartCoroutine(MoveTo(originPosition));
            mainCameraGameObject.transform.rotation = originRotation;

            runtimeStateController.SwitchToLastState();
        }

        /// <summary>
        /// Moved the camera to direction.
        /// </summary>
        public void Move(Vector3 direction)
        {
            mainCameraGameObject.transform.position += direction;
        }
        /// <summary>
        /// rotate the camera to direction. 
        /// </summary>
        /// <param name="focusPoint">any object</param>
        public void Rotate(Vector3 direction, Vector3 focusPoint,float speed)
        {
            mainCameraGameObject.transform.RotateAround(focusPoint,direction,speed);
        }
        private IEnumerator MoveTo(Vector3 endPosition)
        {
            float stepSpeed = 0f;
            // why multiply to 0.5
            // end position is the building position
            // and i need to see the building not the end position.
            var cameraDistance = Vector3.Distance(originPosition, endPosition) * 0.5f;
            endPosition += (-mainCameraGameObject.transform.forward * cameraDistance);
            
            while (mainCameraGameObject.transform.position != endPosition)
            {
                stepSpeed += Time.deltaTime;
                mainCameraGameObject.transform.position = Vector3.Lerp(mainCameraGameObject.transform.position, endPosition , stepSpeed);   
                yield return null;
            }

            isArrive = true;
        }

        private IEnumerator ChangeState()
        {
            //it need to wait until the camera have arrived to the end position
            while (isArrive == false) yield return null;
            
            // when the camera have arrived to the end position
            //than set the runTime State to the BuildingInfo
            runtimeStateController.CurrentState = RunTimeState.BUILDING_INFO;
            // the signal bus is to early instantiate as the ShowBuildingData in Game scene new declarated
            signalBus.Fire(new ShowBuildingData{});
        }
    }
}
