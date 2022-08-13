using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Expbar : MonoBehaviour
{

    public Slider slider;
    [SerializeField] 
    public Text level;
    
    void Start(){
        GameObject gameManager = GameObject.Find("GM");
        GM  gm = (GM) gameManager.GetComponent(typeof(GM));
        
    }
    
    void Update(){
        GameObject gameManager = GameObject.Find("GM");
        GM  gm = (GM) gameManager.GetComponent(typeof(GM));
        slider.maxValue = gm.xp2Next;
        slider.value = gm.xp2Next;
        slider.value = gm.xp;
        
        level.text = gm.level.ToString();
    }   


}
