using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MaterialSwapper))]
public class DissolveController : MonoBehaviour {

    private MaterialSwapper materialSwapper;
    private float DissolveProgress = 0f;

    private void Start()
    {
        materialSwapper = GetComponent<MaterialSwapper>();
    }

    public IEnumerator FadeIn()
    {
        Material dissolveMaterial = materialSwapper.SelectMaterial("Dissolve");
        if (dissolveMaterial != null)
        {
            DissolveProgress = 0f;
            while (DissolveProgress < 1f)
            {
                dissolveMaterial.SetFloat("_Progress", Mathf.Lerp(1f, -1f, DissolveProgress));
                DissolveProgress += Time.deltaTime;
                yield return null;
            }
            materialSwapper.Reset();
        }
    }

    public IEnumerator FadeOut()
    {
        Material dissolveMaterial = materialSwapper.SelectMaterial("Dissolve");
        if (dissolveMaterial != null)
        {
            DissolveProgress = 0f;
            while (DissolveProgress < 1f)
            {
                dissolveMaterial.SetFloat("_Progress", Mathf.Lerp(-1f, 1f, DissolveProgress));
                DissolveProgress += .2f * Time.deltaTime;
                yield return null;
            }
            materialSwapper.Reset();
        }
    }

}
