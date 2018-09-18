using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DissolveController))]
[RequireComponent(typeof(MaterialSwapper))]
public class GroundVisualsController : MonoBehaviour {

    private DissolveController dissolveController;
    private MaterialSwapper materialSwapper;

    private void Start()
    {
        dissolveController = GetComponent<DissolveController>();
        materialSwapper = GetComponent<MaterialSwapper>();
        StartCoroutine(dissolveController.FadeIn());
    }
    private void OnRenderObject()
    {
        
    }
}
