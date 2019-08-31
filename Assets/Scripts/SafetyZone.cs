using UnityEngine;

namespace HackedDesign
{
    public class SafetyZone : MonoBehaviour
    { 
       
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag != "Player") // Ignore the scanner
            {
                return;
            }
            Debug.Log(this.name + ": trigger enter");
            Game.Instance.player.Velocity = 0;
        }
    }
}