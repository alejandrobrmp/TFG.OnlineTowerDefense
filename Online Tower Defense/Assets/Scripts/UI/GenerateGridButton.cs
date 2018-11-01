using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GenerateGridButton : MonoBehaviour {

    public MapEditor mapEditor;
    public Slider widthSlider;
    public Slider heightSlider;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    private void Click()
    {
        mapEditor.MakeGrid(new Vector2(widthSlider.value, heightSlider.value));
    }
}
