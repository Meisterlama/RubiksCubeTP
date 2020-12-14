using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Generation : MonoBehaviour
{
    [SerializeField] public GameObject cubePrefab;
    [SerializeField] private int size = 3;

    public int Size
    {
        get => size;
        set => size = value;
    }

    [SerializeField] private int scale = 1;

    private List<GameObject> _spawnedCubes = new List<GameObject>();

    void CreateCube()
    {
        // Create the cubes
        float offset = 0.5f * (size - 1) * scale;
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    if (x == 0 || y == 0 || z == 0 ||
                        x == size - 1 || y == size - 1 || z == size - 1)
                    {
                        Vector3 position = new Vector3(x, y, z);

                        position -= Vector3.one * offset;
                        position *= scale / (float) size;
                        position += gameObject.transform.position;
                        Quaternion rotation = gameObject.transform.rotation;
                        Vector3 cubeScale = Vector3.one / size;
                        GameObject cube = Instantiate(cubePrefab, position, rotation);
                        cube.transform.localScale = cubeScale * scale;
                        cube.transform.parent = gameObject.transform;

                        CubeManager cubeManager = cube.GetComponent<CubeManager>();

                        // CubeColoration
                        if (x == 0)
                        {
                            cubeManager.LoadFaceColors(FaceDirection.Left);
                        }
                        else if (x == size - 1)
                        {
                            cubeManager.LoadFaceColors(FaceDirection.Right);
                        }

                        if (y == 0)
                        {
                            cubeManager.LoadFaceColors(FaceDirection.Down);
                        }
                        else if (y == size - 1)
                        {
                            cubeManager.LoadFaceColors(FaceDirection.Up);
                        }

                        if (z == 0)
                        {
                            cubeManager.LoadFaceColors(FaceDirection.Backward);
                        }
                        else if (z == size - 1)
                        {
                            cubeManager.LoadFaceColors(FaceDirection.Forward);
                        }

                        _spawnedCubes.Add(cube);
                    }
                }
            }
        }
    }

    void DestroyCube()
    {
        foreach (GameObject spawnedCube in _spawnedCubes)
        {
            Destroy(spawnedCube);
        }

        _spawnedCubes.Clear();
    }

    public void Reload()
    {
        DestroyCube();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        CreateCube();
    }

    public void ChangeCubeSize(float newSize)
    {
        size = (int) newSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateCube();
    }

    // Update is called once per frame
    void Update()
    {
    }
}