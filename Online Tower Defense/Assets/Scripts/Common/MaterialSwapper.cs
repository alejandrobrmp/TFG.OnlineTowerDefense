using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialSwapper : MonoBehaviour {

    private Material defaultMaterial;

    /// <summary>
    /// Materials for the object identified by name
    /// </summary>
    public List<Material> materials;

    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        defaultMaterial = renderer.material;
    }

    public Material GetCurrent()
    {
        return renderer.material;
    }

    public Material SelectMaterial(string name)
    {
        Material newMaterial = materials.Find(m => m.name.Equals(name));
        if (newMaterial == null)
        {
            Debug.LogError(gameObject.name + "_MaterialSwapper.SelectMaterial: Key not found.");
        }
        else
        {
            return SetMaterial(newMaterial);
        }
        return null;
    }

    public Material Reset()
    {
        return SetMaterial(defaultMaterial);
    }

    private Material SetMaterial(Material material)
    {
        return renderer.material = material;
    }

}
