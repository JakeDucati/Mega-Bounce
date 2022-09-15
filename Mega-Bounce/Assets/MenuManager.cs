using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public TextMeshProUGUI menu, sprint;

    public GameObject KeybindsPanel;

    public void KeybindsShown()
    {

        if (KeybindsPanel.activeSelf)
        {
            KeybindsPanel.SetActive(false);
        }
        else
        {
            KeybindsPanel.SetActive(true);
        }
    }

    private void Awake()
    {
        keys.Add("Menu", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Menu", "Escape")));
        keys.Add("Sprint", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift")));
        menu.text = keys["Menu"].ToString();
        sprint.text = keys["Sprint"].ToString();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void Quit()
    {
        SceneManager.LoadScene("Start Screen");
    }
    private GameObject currentKey;
    Color32 normal = new Color32(137, 137, 137, 255);
    Color32 selected = new Color32(239, 116, 36, 255);
    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                PlayerPrefs.SetString(currentKey.name, e.keyCode.ToString());
                currentKey.GetComponentInChildren<TMP_Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }
}
