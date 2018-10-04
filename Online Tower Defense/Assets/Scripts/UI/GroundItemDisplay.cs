using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundItemDisplay : MonoBehaviour {

    public ScriptableGroundObject GroundObject;
    public Text TextObject;
    public Image ImageObject;

    private void Start()
    {
        TextObject.text = GroundObject.name;
        ImageObject.color = GroundObject.Color;
    }

}
