using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using BootManager;
using Constants;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public float speed = 0.3f;
    
    private float cameraDistance = 20f;
    
    public GameObject focusObjekt;

    private Ray ray;
    private RaycastHit raycastHit;
    private Vector3 originPosition;
    private Quaternion originRotation;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 middleDirection = (transform.up + transform.forward) / 2; 
       float distance = Vector3.Distance(transform.position, focusObjekt.transform.position);
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += middleDirection * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= middleDirection * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed;
        }
        if (Input.GetKey(KeyCode.E))
        {
           
                transform.RotateAround(transform.TransformDirection(Vector3.forward) * distance,Vector3.down,speed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
                transform.RotateAround(transform.TransformDirection(Vector3.forward) * distance,Vector3.up,speed);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray,out raycastHit))
            {
                Boot.runtimeStateController.CurrentState = RunTimeState.FOCUS_ON;
                StartCoroutine(FocusOn(raycastHit.point));
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(FocusOn(originPosition));
            transform.rotation = originRotation;
        }
    }
    /// <summary>
    /// TODO: die Funktion  ändern / erweitern.
    /// </summary>
    /// <param name="endPosition"></param>
    /// <returns></returns>
    private IEnumerator FocusOn(Vector3 endPosition)
    {
        //TODO: Es kann später ein Problem sein, wenn man 2 mal geklickt hat.
        // Dann kann er nicht wieder zum EmptyPosition wechseln.
        originPosition = transform.position;
        originRotation = transform.rotation;
        //////////////////////////////////////////////////////////////////
        
        float stepSpeed = 0f;
        endPosition = endPosition + (-transform.forward*cameraDistance);
        
        while (transform.position != endPosition)
        {
            stepSpeed += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, endPosition , stepSpeed);   
            yield return null;
        }
    }
}
