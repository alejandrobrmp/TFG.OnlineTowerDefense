using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GroundStatus
{
    Normal,
    Selected
}

[RequireComponent(typeof(Renderer))]
public class GroundManager : MonoBehaviour {

    private GroundStatus status = GroundStatus.Normal;
    private GroundData groundData;
    public GroundData GroundData
    {
        get
        {
            return groundData;
        }
        set
        {
            groundData = value;
        }
    }

    private DissolveController dissolveController;
    private Renderer renderer;

    private void Awake()
    {
        dissolveController = GetComponent<DissolveController>();
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        renderer.material = groundData.Material;
        StartCoroutine(dissolveController.FadeIn());
    }

    public void Touched()
    {
        switch (status)
        {
            case GroundStatus.Normal:
                status = GroundStatus.Selected;
                Select();
                break;
            case GroundStatus.Selected:
                status = GroundStatus.Normal;
                Deselect();
                break;
            default:
                break;
        }

    }

    private void Select()
    {
        renderer.material.shader = groundData.touchedShader;
        renderer.material.SetTexture("_Texture", renderer.material.mainTexture);
    }

    private void Deselect()
    {
        renderer.material = groundData.Material;
    }

}
