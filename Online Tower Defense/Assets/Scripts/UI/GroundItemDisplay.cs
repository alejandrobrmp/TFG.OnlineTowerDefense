using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundItemDisplay : MonoBehaviour {

    public MapEditor mapEditor;
    public ScriptableGroundObject GroundObject;
    public Text TextObject;
    public Image ImageObject;

    private void Start()
    {
        TextObject.text = GroundObject.name;
        ImageObject.color = GroundObject.Color;
        GetComponent<Button>().onClick.AddListener(Click);
    }

    public void Click()
    {
        Debug.Log(GroundObject.Object.name);
        mapEditor.SetMaterial(GroundObject.Object);
    }

}
