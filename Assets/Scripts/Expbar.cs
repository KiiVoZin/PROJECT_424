using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Expbar : MonoBehaviour
{

    public Slider slider;
    
    public void setMaxExp(int cap)
    {
        slider.maxValue = cap;
        slider.value = cap;
    }
    
    public void setExp(int expvalue)
    {
        slider.value = expvalue;
    }


}
