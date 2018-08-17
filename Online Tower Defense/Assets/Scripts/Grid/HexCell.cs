using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour {

    public GroundManager GroundManager;

    [SerializeField]
    private GroundData groundData;
    public GroundData GroundData {
        get
        {
            return groundData;
        }
        set
        {
            groundData = value;
            GroundManager.GroundData = value;
        }
    }

    private void Awake()
    {
        GroundManager = GetComponentInChildren<GroundManager>();
    }

}
