using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public int PlayerPos = 0;
    public float TurnTime = 0;
    public int MapLen = 0;
    float CurTimeout = 0;
    System.Random rnd = new System.Random();
    [System.Serializable]
    public class cell
    {
        public string Name;
        public int num;
        public bool Empty;
        public bool DangerousFwd;
        public bool DangerousUp;
        public int Perc;
    }
    public List<cell> map = new List<cell>();
    public List<cell> Cells = new List<cell>();
    // Start is called before the first frame update
    void Start()
    {
        map.Add(Cells[0]);
        genMap();
    }

    // Update is called once per frame
    void Update()
    {
        CurTimeout += Time.deltaTime;
        if (CurTimeout < TurnTime) {
            bool ok = true;
            if (Input.GetKeyDown(KeyCode.D)) {
                //attack right
            } else if (Input.GetKeyDown(KeyCode.A)) {
                //attack left
            } else if (Input.GetKeyDown(KeyCode.S)) {
                //attack Down
            } else if (Input.GetKeyDown(KeyCode.W)) {
                //jump
            } else ok = false;
            if (ok) {
                nextTurn();
                genMap();
            }
        } else {
            //go forward or fall from jump
            nextTurn();
            genMap();
        }
    }
    void nextTurn()
    {
        CurTimeout = 0;
    }
    void genMap()
    {
        while(map.Count - PlayerPos < MapLen) {
            if (map[map.Count - 1].DangerousFwd) {
                List<cell> ok = new List<cell>();
                int maxcnt = 0;
                for (int i = 0; i < Cells.Count; i++) {
                    if (!Cells[i].DangerousUp) {
                        ok.Add(Cells[i]);
                        maxcnt += Cells[i].Perc;
                    }
                }

            } else {

            }
        }
    }
}
