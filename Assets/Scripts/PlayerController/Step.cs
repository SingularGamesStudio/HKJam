using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Move;

public class Step : MonoBehaviour
{
    Vector2 dp;
    GameObject obj;
    public static Step _step;
    Vector2 position;
    enum type
    {
        Up,
        Down,
        Forward,
        Back,
        Null,
    }
    type Vertical, Horizontal;

    // const variables
    const float cell_len = 3.2f;
    const float speed = 700f;

    void Awake()
    {
        _step = this;
        Vertical = Horizontal = type.Null;
    }

    public void simple_forward()
    {
        dp = Vector2.zero;
        Horizontal = type.Forward;
        position.x = obj.transform.position.x + cell_len;
    }
    public void simple_up()
    {
        dp = Vector2.zero;
        Vertical = type.Up;
        position.y = obj.transform.position.y + cell_len;
    }
    public void simple_down()
    {
        dp = Vector2.zero;
        Vertical = type.Down;
        position.y = obj.transform.position.y - cell_len;
    }
    public void forward()
    {
        if (obj.transform.position.y > 0)
        { // мы находимся наверху
            simple_down();
        }
        simple_forward();
    }
    public void up()
    {
        simple_forward();
        simple_up();
    }

    public void new_game_ojbect(GameObject Game_object)
    {
        obj = Game_object;
    }

    float update_position(ref float position, float p, ref float dp, ref type Move)
    {
        if (Move != type.Null)
        {
            {
                float ddp = 0f;
                ddp = (Move == type.Up || Move == type.Forward ? speed : -speed);
                p = Emove.simulate_move(p, ref dp, ddp, Time.deltaTime);
            }
            if (Move == type.Down || Move == type.Back)
            {
                if (p < position)
                {
                    p = position;
                    Move = type.Null;
                }
            }
            else if (Move == type.Up || Move == type.Forward)
            {
                if (p > position)
                {
                    p = position;
                    Move = type.Null;
                }
            }
        }
        return p;
    }

    void Update()
    {
        if (Vertical == type.Down) {
            Debug.Log("hhh");
        }
        float TempY = update_position(ref position.y, obj.transform.position.y, ref dp.y, ref Vertical);
        float TempX = update_position(ref position.x, obj.transform.position.x, ref dp.x, ref Horizontal);
        obj.transform.position = new Vector2(TempX, TempY);     
    }
}