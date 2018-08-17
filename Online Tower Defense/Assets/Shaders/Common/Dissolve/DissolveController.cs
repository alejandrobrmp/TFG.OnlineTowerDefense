using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DissolveController : MonoBehaviour {

    public Shader dissolveShader;

    private Renderer renderer;

    private bool destroyRequested = false;

    private float DissolveProgress = 0f;

    private void Start()
    {
        this.renderer = GetComponent<Renderer>();
//        StartCoroutine(FadeIn());
    }

    //new void Destroy(UnityEngine.Object obj)
    //{
    //    if (!destroyRequested)
    //    {
    //        destroyRequested = true;
    //        StartCoroutine(FadeOut());
    //    }
    //    else
    //    {
    //        Object.Destroy(obj);
    //    }
    //}

    public IEnumerator FadeIn()
    {
        Shader currentShader = renderer.material.shader;
        renderer.material.shader = dissolveShader;
        renderer.material.SetFloat("_Progress", -1f);
        renderer.material.SetTexture("_Texture", renderer.material.mainTexture);

        DissolveProgress = 0f;
        while (DissolveProgress < 1f)
        {
            renderer.material.SetFloat("_Progress", Mathf.Lerp(1f, -1f, DissolveProgress));
            DissolveProgress += Time.deltaTime;
            yield return null;
        }

        renderer.material.shader = currentShader;
    }

    public IEnumerator FadeOut()
    {
        Shader currentShader = renderer.material.shader;
        renderer.material.shader = dissolveShader;
        renderer.material.SetFloat("_Progress", 1f);
        renderer.material.SetTexture("_Texture", renderer.material.mainTexture);

        DissolveProgress = 0f;
        while (DissolveProgress < 1f)
        {
            renderer.material.SetFloat("_Progress", Mathf.Lerp(-1f, 1f, DissolveProgress));
            DissolveProgress += .2f * Time.deltaTime;
            yield return null;
        }

        renderer.material.shader = currentShader;
        Destroy(gameObject);
    }

}
