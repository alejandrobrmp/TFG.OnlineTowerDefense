using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTileController : MonoBehaviour {
    
    private GameObject assignedGameobject;
    private bool canPlace;

    private void Start()
    {
        GameController.Instance.OnPlayChange += (bool value, bool hasMoreWaves) => ToggleEnable();
        GameController.Instance.OnActionsLeftChanged += EvaluateCanPlace;
        EvaluateCanPlace(GameController.Instance.ActionsLeft);
    }

    private void EvaluateCanPlace(int actions)
    {
        canPlace = actions > 0;
    }

    public void InstantiateGameObject(GameObject gameObject)
    {
        GameController.Instance.ModifyActionsLeft(-1);
        assignedGameobject = Instantiate(gameObject, transform.position, Quaternion.identity, null);
        assignedGameobject.GetComponent<TurretController>().Tile = this;
        ToggleEnable();
    }

    public void ToggleEnable()
    {
        gameObject.SetActive(gameObject.activeSelf ? false : (assignedGameobject == false));
    }

    public void TurretDestroyed()
    {
        assignedGameobject = null;
        EvaluateCanPlace(GameController.Instance.ActionsLeft);
        ToggleEnable();
    }

    private void OnMouseDown()
    {
        if (GameController.Instance.SelectedTurretToPlace != null && canPlace)
        {
            InstantiateGameObject(GameController.Instance.SelectedTurretToPlace);
        }
    }

}
