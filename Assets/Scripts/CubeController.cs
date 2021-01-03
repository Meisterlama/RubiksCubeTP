using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private Vector3 _oldMousePosition;
    private Vector3 _oldMousePositionTurn;

    [SerializeField] public float zoomSensitivity = 0.2f;

    [SerializeField] public Camera TargetCamera;

    [SerializeField] public GameObject RubiksCube;

    private List<GameObject> _planes = new List<GameObject>();

    void ResetMovement()
    {
        _oldMousePositionTurn = Input.mousePosition;
        _planes.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        _oldMousePosition = Input.mousePosition;
        _oldMousePositionTurn = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (_planes.Count > 0)
        {
            if (Input.GetMouseButtonUp(0))
            {
                ResetMovement();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 movement = _oldMousePositionTurn - Input.mousePosition;
                //Debug.Log(temp.magnitude.ToString());

                //L'utilisateur a choisis une direction dans laquelle tourner
                if (movement.magnitude > 25.0f)
                {
                    movement.x = movement.x > movement.y ? movement.x : 0;
                    movement.y = movement.y > movement.x ? movement.y : 0;

                    Vector3 vec = RubiksCube.transform.rotation * movement;
                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cube.transform.rotation

                    float f1 = new Vector2(Vector3.Dot(vec,_planes[0].transform.right ),Vector3.Dot(vec,_planes[0].transform.up)).magnitude;
                    float f2 = new Vector2(Vector3.Dot(vec,_planes[1].transform.right),Vector3.Dot( vec,_planes[1].transform.right)).magnitude;

                    GameObject finalPlan = f1 > f2 ? _planes[0] : _planes[1];
                    finalPlan.GetComponent<PlaneController>().Turn(Mathf.Clamp(movement.magnitude,-1,1));

                    //Debug.Log(temp.magnitude.ToString());
                    ResetMovement();
                }
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = TargetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f))
            {
                GameObject hitTarget = hit.transform.gameObject;

                if (!hitTarget.CompareTag("Face"))
                    return;

                Collider[] colliders = Physics.OverlapSphere(hitTarget.transform.position,
                    hitTarget.transform.localScale.magnitude / 10);

                //GameObject debugsphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //debugsphere.transform.position = hitTarget.transform.position;
                //debugsphere.transform.localScale = Vector3.one* hitTarget.transform.localScale.magnitude/10;

                foreach (var obj in colliders)
                {
                    if (obj.CompareTag("Plane"))
                    {
                        _planes.Add(obj.gameObject);
                        //Debug.Log(obj.gameObject.name);
                    }
                }

                if (_planes.Count == 0)
                    return;

                _oldMousePositionTurn = Input.mousePosition;
            }
            else
            {
                ResetMovement();
            }

            return;
        }


        if (Input.GetMouseButton(1))
        {
            Vector3 mouseDeltaAxis = _oldMousePosition - Input.mousePosition;
            mouseDeltaAxis.y *= -1;

            float temp = mouseDeltaAxis.y;
            mouseDeltaAxis.y = mouseDeltaAxis.x;
            mouseDeltaAxis.x = temp;
            
            transform.MyRotateAround(Vector3.zero, mouseDeltaAxis, mouseDeltaAxis.magnitude);
        }

        ResetMovement();

        // Camera Zoom
        Transform targetCameraTransform = TargetCamera.transform;

        Vector3 newCameraPosition = targetCameraTransform.position +
                                    Input.mouseScrollDelta.y * zoomSensitivity * targetCameraTransform.forward;

        newCameraPosition.z = Mathf.Clamp(newCameraPosition.z, -5f, -2f);

        targetCameraTransform.position = newCameraPosition;

        _oldMousePosition = Input.mousePosition;
    }
}