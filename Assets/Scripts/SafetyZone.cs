using UnityEngine;

namespace HackedDesign
{
    public class SafetyZone : MonoBehaviour
    { 
       
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Scanner") // Ignore the scanner
            {
                return;
            }
            Debug.Log("Trigger enter");
            Game.Instance.player.Velocity = 0;
        }
    }
}