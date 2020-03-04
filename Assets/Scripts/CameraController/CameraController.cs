using System.Collections;
using BootManager;
using Enums;
using StateMachine;
using UnityEngine;
using Zenject;
using Zenject_Signals;

namespace PlayerView
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

        public CameraController(SignalBus signalBus,  StateController<RunTimeState> runtimeStateController)
        {
            mainCameraGameObject = Camera.main.gameObject;
            mainCamera = Camera.main;
            this.signalBus = signalBus;
            this.runtimeStateController = runtimeStateController;
        }
        public void FocusOn(Vector3 endPosition)
        {
            isArrive = false;
            runtimeStateController.CurrentState = RunTimeState.FOCUS_ON;

            originPosition = mainCameraGameObject.transform.position;
            originRotation = mainCameraGameObject.transform.rotation;

            BootController.BootControllerInstance.monoBehaviour.StartCoroutine(MoveTo(endPosition));
            BootController.BootControllerInstance.monoBehaviour.StartCoroutine(ChangeState());
        }
        public void ToEmptyPos()
        {
            BootController.BootControllerInstance.monoBehaviour.StartCoroutine(MoveTo(originPosition));
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
            signalBus.Fire(new ShowBuildingData{});
        }
    }
}
