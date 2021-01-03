using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    private float dir;
    private float angle;
    private float speed = 50f;
    private bool turn;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Turn(float direction)
    {
        dir = direction;
        Debug.Log("tourne");
        Vector3 size = new Vector3(1f,0.001f,1f);
        Collider[] colliders = Physics.OverlapBox(transform.position,size,transform.rotation);
        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.position = transform.position;
        // cube.transform.rotation = transform.rotation;
        // cube.transform.localScale = size;
        

        foreach (Collider col in colliders)
        {
            if (col.gameObject.CompareTag("Cube"))
            {
                //col.gameObject.transform.parent = this.transform;
                col.gameObject.transform.RotateAround(transform.position,transform.up,90/*0Time.deltaTime *speed *direction*/);
                //StartCoroutine("Rotate");
            }
        }
        
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
}