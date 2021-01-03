using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject RubikCube;
    [SerializeField] private Text CubeSizeText;
    [SerializeField] private Slider CubeSizeSlider;

    private Generation _rubikCubeGeneration;

    // Start is called before the first frame update
    void Start()
    {
        _rubikCubeGeneration = RubikCube.GetComponent<Generation>();
        CubeSizeSlider.value = _rubikCubeGeneration.Size;
        CubeSizeText.text = "Cube Size: " + _rubikCubeGeneration.Size;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadCube()
    {
        _rubikCubeGeneration.Reload();
    }

    public void ChangeCubeSize(float newSize)
    {
        _rubikCubeGeneration.Size = (int)newSize;
        CubeSizeText.text = "Cube Size: " + newSize;
    }
}
