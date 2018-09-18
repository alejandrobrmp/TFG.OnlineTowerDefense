using UnityEngine;
using UnityEngine.UI;
 
using UnityEditor;

// ----------
// String => Material
// ----------
[UnityEditor.CustomPropertyDrawer(typeof(StringMaterialDictionary))]
public class StringMaterialDictionaryDrawer: SerializableDictionaryDrawer<string, Material>
{
    protected override SerializableKeyValueTemplate<string, Material> GetTemplate()
    {
        return GetGenericTemplate<SerializableStringMaterialTemplate>();
    }
}
internal class SerializableStringMaterialTemplate : SerializableKeyValueTemplate<string, Material> {};
