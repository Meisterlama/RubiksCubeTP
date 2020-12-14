using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FaceDirection
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

    public void LoadFaceColors(FaceDirection faceDirection)
    {
        for (int i = 0; i < faceList.Count; i++)
        {
            if (i == (int)faceDirection)
            {
                faceList[i].GetComponent<MeshRenderer>().material = faceMaterials[i];
            }
        }
    }
}