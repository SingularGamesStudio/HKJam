using UnityEngine;

namespace Move{
    public class Emove{
        public static Vector3 simulate_move3d(Vector3 p, ref Vector3 dp, Vector3 ddp, float dt){
            return new Vector3(simulate_move(p.x, ref dp.x, ddp.x, dt),
                               simulate_move(p.y, ref dp.y, ddp.y, dt),
                               simulate_move(p.z, ref dp.z, ddp.z, dt));
        }
        public static Vector2 simulate_move2d(Vector2 p, ref Vector2 dp, Vector2 ddp, float dt){
            return new Vector2(simulate_move(p.x, ref dp.x, ddp.x, dt),
                                      simulate_move(p.y, ref dp.y, ddp.y, dt));
        }
        public static float simulate_move(float p, ref float dp, float ddp, float dt){
            ddp -= dp * 15f;
            p = p + dp * dt + ddp * dt * dt * .5f;
            dp = dp + ddp * dt;
            return p;
        }
    }
}