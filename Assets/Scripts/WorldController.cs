using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController _w;
    public int PlayerPos = -1;
    public float TurnTime = 0;
    public int MapLen = 0;
    public float CellLen = 0;
    public GameObject plr;
    float CurTimeout = 0;
    System.Random rnd = new System.Random();
    public enum CellType
    {
        mine,
        wall,
        turret,
        spike
    }
    [System.Serializable]
    public class cell
    {
        public string Name;
        public int num;
        public CellType type;
        public bool Empty;
        public bool DangerousFwd;
        public bool DangerousUp;
        public int Perc;
        public GameObject pref;
    }
    public class cellInst
    {
        public int num;
        public bool repaired;
        public GameObject Instance;
        public cellInst(cell c, GameObject I)
        {
            num = c.num;
            repaired = false;
            Instance = I;
        }
    }
    public List<cellInst> map = new List<cellInst>();
    public List<cell> Cells = new List<cell>();
    void Awake()
    {
        _w = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Step._step.new_game_ojbect(plr);
        GameObject g = Instantiate(Cells[0].pref);
        g.transform.position = Vector3.zero;
        map.Add(new cellInst(Cells[0], g));
        nextTurn();
        genMap();
    }

    // Update is called once per frame
    void Update()
    {
        CurTimeout += Time.deltaTime;
        TurnLoad._t.set(CurTimeout/TurnTime);
        if (CurTimeout < TurnTime) {
            bool ok = true;
            if (Input.GetKeyDown(KeyCode.D)) {
                //attack right
            } else if (Input.GetKeyDown(KeyCode.A)) {
                //attack left
            } else if (Input.GetKeyDown(KeyCode.S)) {
                //attack Down
            } else if (Input.GetKeyDown(KeyCode.W)) {
                Step._step.up();
            } else ok = false;
            if (ok) {
                nextTurn();
                genMap();
            }
        } else {
            Step._step.forward();
            nextTurn();
            genMap();
        }
    }
    public void repair(int num)
    {

    }
    void nextTurn()
    {
        PlayerPos++;
        TurnLoad._t.set(0);
        CurTimeout = 0;
    }
    void genMap()
    {
        while(map.Count - PlayerPos < MapLen) {
            if (Cells[map[map.Count - 1].num].DangerousFwd) {
                List<cell> ok = new List<cell>();
                int maxcnt = 0;
                for (int i = 0; i < Cells.Count; i++) {
                    if (!Cells[i].DangerousUp) {
                        ok.Add(Cells[i]);
                        maxcnt += Cells[i].Perc;
                    }
                }
                int k = rnd.Next(maxcnt);
                for (int i = 0; i < ok.Count; i++) {
                    k -= ok[i].Perc;
                    if (k <= 0) {
                        GameObject g = Instantiate(ok[i].pref);
                        g.transform.position = map[map.Count - 1].Instance.transform.position + new Vector3(CellLen, 0);
                        map.Add(new cellInst(ok[i], g));
                        break;
                    }
                }
            } else {
                int maxcnt = 0;
                for (int i = 0; i < Cells.Count; i++) {
                    maxcnt += Cells[i].Perc;
                }
                int k = rnd.Next(maxcnt);
                for (int i = 0; i < Cells.Count; i++) {
                    k -= Cells[i].Perc;
                    if (k <= 0) {
                        GameObject g = Instantiate(Cells[i].pref);
                        g.transform.position = map[map.Count - 1].Instance.transform.position + new Vector3(CellLen, 0);
                        map.Add(new cellInst(Cells[i], g));
                        break;
                    }
                }
            }
        }
    }
}
