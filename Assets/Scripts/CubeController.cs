using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Vector3 _oldMousePosition;

    [SerializeField] public float zoomSensitivity = 0.2f;

    [SerializeField]
    public Camera TargetCamera;

    // Start is called before the first frame update
    void Start()
    {
        _oldMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Cube Rotation
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDeltaAxis = _oldMousePosition - Input.mousePosition;
            mouseDeltaAxis.y *= -1;

            float temp = mouseDeltaAxis.y;
            mouseDeltaAxis.y = mouseDeltaAxis.x;
            mouseDeltaAxis.x = temp;


            //TODO: Replace with self made RotateAround
            transform.RotateAround(Vector3.zero, mouseDeltaAxis, mouseDeltaAxis.magnitude);
        }

        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = TargetCamera.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 50f))
            {
                GameObject cube = hit.transform.gameObject;
                cube.SetActive(false);
            }    
        }

        // Camera Zoom
        Transform targetCameraTransform = TargetCamera.transform;
        
        Vector3 newCameraPosition = targetCameraTransform.position + Input.mouseScrollDelta.y * zoomSensitivity * targetCameraTransform.forward;
        newCameraPosition.z = Mathf.Clamp(newCameraPosition.z, -5f, -2f);

        targetCameraTransform.position = newCameraPosition;

        _oldMousePosition = Input.mousePosition;
    }
}