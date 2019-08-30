using UnityEngine;

namespace HackedDesign
{
    public class Targeting : MonoBehaviour
    {


        // void OnTriggerEnter2D(Collider2D other)
        // {
        //     if(other.tag == "Respawn") // Ignore the shield
        //     {
        //         return;
        //     }

        //     Game.Instance.CurrentTarget = other.gameObject;
        // }

        void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Respawn") // Ignore the shield
            {
                return;
            }

            Game.Instance.CurrentTarget = other.gameObject;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Game.Instance.CurrentTarget = null;
        }
    }
}