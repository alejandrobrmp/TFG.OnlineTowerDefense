using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTileController : MonoBehaviour {

    public ScriptableTurret TurretData;
    public GameObject TEMPORAL;
    private GameObject assignedGameobject;

    public void InstantiateGameObject(GameObject gameObject, ScriptableTurret turretData)
    {
        assignedGameobject = Instantiate(gameObject, transform.position, Quaternion.identity, null);
        assignedGameobject.GetComponent<TurretController>().ApplyScriptableTurret(turretData);
        ToogleEnable();
    }

    public void ToogleEnable()
    {
        gameObject.SetActive(gameObject.activeSelf ? false : (assignedGameobject == false));
    }

    private void OnMouseOver()
    {
        
    }

    private void OnMouseDown()
    {
        InstantiateGameObject(TEMPORAL, TurretData);
    }

}
