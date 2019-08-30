using UnityEngine;

namespace HackedDesign
{
    public class ProxMine : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player") // Ignore the shield
            {
                Game.Instance.GameOverMine();
                return;
            }          
        }        
    }
}