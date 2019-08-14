using UnityEngine;

namespace CameraManager
{
    public class Controller : MonoBehaviour
    {
        private Rigidbody rigidbody;

        private Camera mainCamera;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
            Rotation();
            Debug.DrawRay(rigidbody.position, transform.forward*5f, Color.red);
        }

        void Movement()
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.velocity += transform.forward * 0.3f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                rigidbody.velocity -= transform.forward * 0.3f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.velocity -= transform.right * 0.3f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.velocity += transform.right * 0.3f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody.velocity = transform.up * 3f;
            }
        }

        void Rotation()
        {
            rigidbody.rotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X"));
            mainCamera.transform.rotation *= Quaternion.Euler(Vector3.left * Input.GetAxis("Mouse Y"));
        }
    }
}