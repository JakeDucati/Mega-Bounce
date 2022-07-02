using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Movement m;
    public Image energyBarImage;
    void Start()
    {
        energyBarImage.fillAmount = 0.5f;
    }
    void Update()
    {
        energyBarImage.fillAmount = Mathf.Clamp(m.energy / m.maxEnergy,0,1);
    }
}
