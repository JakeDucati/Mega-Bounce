using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public TMP_Text LevelCount;
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("PlayerLevel",1);
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        LevelCount.text = "Level: " + level;
        CheckDeath();
        CheckLevel();
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
    public void ResetLevel()
    {
        level = 1;
        PlayerPrefs.SetInt("PlayerLevel", level);
        Respawn();
    }
}
