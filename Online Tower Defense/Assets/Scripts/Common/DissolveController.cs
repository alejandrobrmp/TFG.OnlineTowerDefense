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

    /// <summary>
    /// Animates an object creation by using a shader "Dissolve"
    /// </summary>
    /// <param name="speedRatio">Speed ratio 1-0 (Clamped)(0 will do nothing and return)</param>
    /// <returns><see cref="IEnumerator"/></returns>
    public IEnumerator FadeIn(float speedRatio = 1f)
    {
        if (speedRatio != 0f)
        {
            speedRatio = Mathf.Clamp(speedRatio, 0f, 1f);
            Material dissolveMaterial = materialSwapper.SelectMaterial("Dissolve");
            if (dissolveMaterial != null)
            {
                DissolveProgress = 0f;
                while (DissolveProgress < 1f)
                {
                    dissolveMaterial.SetFloat("_Progress", Mathf.Lerp(1f, -1f, DissolveProgress));
                    DissolveProgress += Time.deltaTime * speedRatio;
                    yield return null;
                }
                materialSwapper.Reset();
            }
        }
    }

    /// <summary>
    /// Animates an object destruction by using a shader "Dissolve"
    /// </summary>
    /// <param name="speedRatio">Speed ratio 1-0 (Clamped)(0 will do nothing and return)</param>
    /// <returns><see cref="IEnumerator"/></returns>
    public IEnumerator FadeOut(float speedRatio = 1f)
    {
        if (speedRatio != 0f)
        {
            speedRatio = Mathf.Clamp(speedRatio, 0f, 1f);
            Material dissolveMaterial = materialSwapper.SelectMaterial("Dissolve");
            if (dissolveMaterial != null)
            {
                DissolveProgress = 0f;
                while (DissolveProgress < 1f)
                {
                    dissolveMaterial.SetFloat("_Progress", Mathf.Lerp(-1f, 1f, DissolveProgress));
                    DissolveProgress += .2f * Time.deltaTime * speedRatio;
                    yield return null;
                }
                materialSwapper.Reset();
            }
        }
    }

}
