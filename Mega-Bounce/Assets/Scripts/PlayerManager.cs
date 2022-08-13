using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public TMP_Text LevelCount;
    void Start()
    {
        level = PlayerPrefs.GetInt("PlayerLevel",1);
        Respawn();
        menuPanel.SetActive(false);
    }

    void Update()
    {
        LevelCount.text = "Level: " + level;
        CheckDeath();
        CheckLevel();
        PauseMenu();
    }
    public GameObject menuPanel;
    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPanel.activeSelf)
            {
                menuPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                menuPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    Vector3 spawnPos = new Vector3(0, 2, 0);
    int level = 1;
    int distanceBetweenSpawnpoints = 40;
    void CheckDeath()
    {
        float deathLevel = -2;
        if(transform.position.y < deathLevel)
        {
            Respawn();
        }
    }
    void Respawn()
    {
        spawnPos = new Vector3((level - 1) * distanceBetweenSpawnpoints, 2, 0);
        transform.position = spawnPos;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
    void CheckLevel()
    {
        if (transform.position.x > level * distanceBetweenSpawnpoints - 10)
        {
            level++;
            PlayerPrefs.SetInt("PlayerLevel",level);
            print("Level: " + level);
        }
    }
    public static List<GameObject> pickupList = new List<GameObject>();
    public void ResetLevel()
    {
        foreach(GameObject g in pickupList)
        {
            print(g.name);
            PlayerPrefs.SetInt(g.name, 1);
            g.SetActive(true);
        }
        level = 1;
        PlayerPrefs.SetInt("PlayerLevel", level);
        PlayerPrefs.SetInt("CubesCollected",0);
        PlayerPrefs.SetFloat("energy",5f);
        gameObject.GetComponent<Movement>().SetCountText();
        gameObject.GetComponent<Movement>().OnReset();
        Respawn();
    }
}
