using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class HpBar : MonoBehaviour
{

    public Slider slider;
     
    public void AlterHealth(float health)
    {
        if (slider != null)
        {
            slider.value = health;
        }
        else
        {
            Debug.LogError("A refer�ncia para slider em HpBar � nula.");
        }
    }

}
