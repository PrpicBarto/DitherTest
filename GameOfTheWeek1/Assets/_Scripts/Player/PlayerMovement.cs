using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void FixedUpdate()
    {
        playerRigidbody.linearVelocity = Vector3.zero;

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        // Move in world space (top-down)
        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();
            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }

    private void RotateTowardsMouse()
    {
        Ray ray;
        RenderTexture renderTexture = camera.targetTexture;
        Vector3 mousePos = Input.mousePosition;

        float x = mousePos.x * renderTexture.width / Screen.width;
        float y = mousePos.y * renderTexture.height / Screen.height;
        Vector3 viewportPoint = new Vector3(x / renderTexture.width, y / renderTexture.height, 0);
        ray = camera.ViewportPointToRay(viewportPoint);

        Plane plane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 lookDir = hitPoint - transform.position;
            lookDir.y = 0;
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}