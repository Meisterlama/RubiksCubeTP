using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FaceDirection
{
    Up = 0,
    Down,
    Forward,
    Backward,
    Left,
    Right
}

public class CubeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> faceList = new List<GameObject>(6);

    [SerializeField] private List<Material> faceMaterials = new List<Material>(6); 
    // Start is called before the first frame update

    void LoadFaceColors()
    {
        for (int i =0; i < faceList.Count; i++)
        {
            faceList[i].GetComponent<MeshRenderer>().material = faceMaterials[i];
        }
    }

    void Start()
    {
        LoadFaceColors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
