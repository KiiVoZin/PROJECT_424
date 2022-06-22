using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static float bpm;
    [SerializeField] float bpmHelper;
    // Start is called before the first frame update
    void Awake()
    {
        bpm = bpmHelper;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
