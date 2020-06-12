using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject obj;
    float CurTimeout, TurnTime;
    // Start is called before the first frame update
    void Awake(){
        TurnTime = 4f;
        CurTimeout = 0f;
        Step._step.new_game_ojbect(obj);
    }

    bool first = true;
    
    // Update is called once per frame
    void Update(){
         Step._step.forward();
    }
}
