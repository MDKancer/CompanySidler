using System.Collections;
using BootManager;
using Constants;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace PlayerView
{
    public class CameraController
    {
        private GameObject mainCamera;
        private Vector3 originPosition;
        private Quaternion originRotation;
        private bool isArrivate = false;

        public CameraController()
        {
            mainCamera = Camera.main?.gameObject;
        }
        public void FocusOn(Vector3 endPosition)
        {
            isArrivate = false;
            Boot.runtimeStateController.CurrentState = RunTimeState.FOCUS_ON;

            originPosition = mainCamera.transform.position;
            originRotation = mainCamera.transform.rotation;

            Boot.monobehaviour.StartCoroutine(MoveTo(endPosition));
            Boot.monobehaviour.StartCoroutine(ChangeState());
        }
        public void ToEmptyPos()
        {
            Boot.monobehaviour.StartCoroutine(MoveTo(originPosition));
            mainCamera.transform.rotation = originRotation;

            Boot.runtimeStateController.CurrentState = RunTimeState.PLAYING;
        }

        public void Move(Vector3 direction)
        {
            mainCamera.transform.position += direction;
        }

        public void Rotate(Vector3 direction, Vector3 focusPoint,float speed)
        {
            mainCamera.transform.RotateAround(focusPoint,direction,speed);
        }
        private IEnumerator MoveTo(Vector3 endPosition)
        {
            float stepSpeed = 0f;
            var cameraDistance = Vector3.Distance(originPosition, endPosition) * 0.5f;
            endPosition = endPosition + (-mainCamera.transform.forward*cameraDistance);
            
            while (mainCamera.transform.position != endPosition)
            {
                stepSpeed += Time.deltaTime;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, endPosition , stepSpeed);   
                yield return null;
            }

            isArrivate = true;
        }

        private IEnumerator ChangeState()
        {
            while (isArrivate == false) yield return null;

            Boot.runtimeStateController.CurrentState = RunTimeState.GAME_MENU;
        }
    }
}
