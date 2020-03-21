using System.Collections;
using Enums;
using StateManager;
using UnityEngine;
using Zenject;
using Zenject.SceneContext.Signals;

namespace CameraController
{
    public class CameraController
    {
        public readonly GameObject mainCameraGameObject;
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
        /// 
        /// </summary>
        /// <param name="endPosition"></param>
        public void FocusOn(Vector3 endPosition)
        {
            isArrive = false;
            runtimeStateController.CurrentState = RunTimeState.FOCUS_ON;

            originPosition = mainCameraGameObject.transform.position;
            originRotation = mainCameraGameObject.transform.rotation;

            monoBehaviour.StartCoroutine(MoveTo(endPosition));
            monoBehaviour.StartCoroutine(ChangeState());
        }
        public void ToEmptyPos()
        {
            monoBehaviour.StartCoroutine(MoveTo(originPosition));
            mainCameraGameObject.transform.rotation = originRotation;

            runtimeStateController.SwitchToLastState();
        }

        public void Move(Vector3 direction)
        {
            mainCameraGameObject.transform.position += direction;
        }

        public void Rotate(Vector3 direction, Vector3 focusPoint,float speed)
        {
            mainCameraGameObject.transform.RotateAround(focusPoint,direction,speed);
        }
        private IEnumerator MoveTo(Vector3 endPosition)
        {
            float stepSpeed = 0f;
            var cameraDistance = Vector3.Distance(originPosition, endPosition) * 0.5f;
            endPosition = endPosition + (-mainCameraGameObject.transform.forward*cameraDistance);
            
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
            while (isArrive == false) yield return null;
            
            runtimeStateController.CurrentState = RunTimeState.BUILDING_INFO;
            // the signal bus is to early instantiate as the ShowBuildingData in Game scene new declarated
            signalBus.Fire(new ShowBuildingData{});
        }
    }
}
