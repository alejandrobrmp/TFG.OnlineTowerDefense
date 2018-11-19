using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour {

    public GameObject Turret;
    public Image Image;

    private void Start()
    {
        
    }

    public void SetSelected(bool value)
    {
        Image.GetComponent<Image>().color = value ? new Color(.57f, .9f, .48f) : Color.white;
    }

}
