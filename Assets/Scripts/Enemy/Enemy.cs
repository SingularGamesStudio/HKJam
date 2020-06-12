using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
    System.Random rnd = new System.Random();

    int it; // позиция в клетке
    Vector2 pos;
    bool up;

    void Awake(){
        gameObject.GetComponent<Step>().new_game_ojbect(gameObject);
    }

    bool Chance(int percent)
    {
        int random = rnd.Next(0, 101);
        return random > percent;
    }
    // return true if enemy dead else false
    bool simulate_Enemy(ref List<WorldController.cellInst> map, ref List<WorldController.cell> Cells)
    { // вызывается каждый шаг
        if (up)
        {
            gameObject.GetComponent<Step>().forward();
            return false;
        }
        bool is_dead = false;
        // simulate it in cells
        {
            WorldController.cell cell = Cells[map[it].num]; // взяли клетку
            WorldController.cellInst cellInst = map[it];

            if (cellInst.repaired)
            {
                if (cell.type == WorldController.CellType.mine) // Если мы стоим в клетке с миной
                {
                    // Мы взорвались
                    cell.type = WorldController.CellType.none;
                    is_dead = true;
                }
                else if (cell.type == WorldController.CellType.spike) // Если мы стоим в клетке с пишами
                {
                    cell.type = WorldController.CellType.none;
                    is_dead = true;
                }
            }

            Cells[map[it].num] = cell;
            map[it] = cellInst;
        }
        up = false;
        // simulate it + 1 in cells
        {
            WorldController.cell cell = Cells[map[it + 1].num]; // взяли клетку
            WorldController.cellInst cellInst = map[it + 1];
            
            if (cellInst.repaired)
            {
                if (cell.type == WorldController.CellType.wall) // Если перед нами стена
                {
                    // разрушить ее
                }
                else if (cell.type == WorldController.CellType.mine) // перед нами мина
                {
                    if (Chance(60)) // можем ее перепрыгнуть
                    {
                        // перепрыгиваем
                        gameObject.GetComponent<Step>().up();
                        up = true;
                    }
                    else // идем вперед
                    {
                        gameObject.GetComponent<Step>().forward();
                    }
                }
                else if (cell.type == WorldController.CellType.spike)
                {
                    if (Chance(60)) // можем ее перепрыгнуть
                    {
                        // перепрыгиваем
                        gameObject.GetComponent<Step>().up();
                        up = true;
                    }
                    else // идем вперед
                    {
                        gameObject.GetComponent<Step>().forward();
                    }
                }
            }

            Cells[map[it].num] = cell;
            map[it] = cellInst;
        }
        return is_dead;
    }
}