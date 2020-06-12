using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLoad : MonoBehaviour
{
    public static TurnLoad _t;
    public RectTransform LoadBackground;
    public RectTransform LoadingBar;
    // Start is called before the first frame update
    void Awake()
    {
        _t = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set(float f)
    {
        LoadingBar.sizeDelta = new Vector3(LoadBackground.sizeDelta.x * f, LoadBackground.sizeDelta.y);
        
    }
}
