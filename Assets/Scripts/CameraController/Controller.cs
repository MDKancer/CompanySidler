using UnityEngine;

namespace CameraManager
{
    public class Controller : MonoBehaviour
    {
        private Rigidbody rigidBody;

        private Camera mainCamera;
        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            Movement();
            Rotation();
            Debug.DrawRay(rigidBody.position, transform.forward*5f, Color.red);
        }

        void Movement()
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigidBody.velocity += transform.forward * 0.3f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                rigidBody.velocity -= transform.forward * 0.3f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rigidBody.velocity -= transform.right * 0.3f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidBody.velocity += transform.right * 0.3f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody.velocity = transform.up * 3f;
            }
        }

        void Rotation()
        {
            rigidBody.rotation *= Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X"));
            mainCamera.transform.rotation *= Quaternion.Euler(Vector3.left * Input.GetAxis("Mouse Y"));
        }
    }
}