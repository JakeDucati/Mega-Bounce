using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    void Start()
    {
        PlayerManager.pickupList.Add(gameObject);
        if (PlayerPrefs.GetInt(gameObject.name,1) == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public void HideCube()
    {
        PlayerPrefs.SetInt(gameObject.name, 0);
        gameObject.SetActive(false);
    }
}
