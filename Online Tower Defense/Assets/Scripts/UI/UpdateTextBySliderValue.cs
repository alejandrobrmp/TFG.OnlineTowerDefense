using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UpdateTextBySliderValue : MonoBehaviour {

    public string format = "0";
    public Slider slider;

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(float value)
    {
        text.text = value.ToString(format);
    }

}
