using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject FinalScreen;
    public GameObject WinText;
    public GameObject LooseText;

    private void Start()
    {
        if (PlayerPrefs.GetInt("EndScreen") == 1)
        {
            PlayerPrefs.SetInt("EndScreen", 0);
            FinalScreen.SetActive(true);
            if (PlayerPrefs.GetInt("Win") == 1)
            {
                WinText.SetActive(true);
                LooseText.SetActive(false);
            }
            else
            {
                LooseText.SetActive(true);
                WinText.SetActive(false);
            }
        }
        else
        {
            MainMenu.SetActive(true);
        }
    }

}
