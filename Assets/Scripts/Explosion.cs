using UnityEngine;

namespace HackedDesign
{
    public class Explosion : MonoBehaviour
    {
        Animator animator;
        bool playing = false;
        

        void Start()
        {
            animator = GetComponent<Animator>();

            if(animator == null)
            {
                Debug.LogError(this.name + ": animator is not set");
            }
        }

        void Reset()
        {
            animator.StopPlayback();

        }

        public void Play()
        {
            //animator.SetBool("Play", true);
            animator.Play("Explosion", -1, 0);

        }

        void Update()
        {
        }


    }
}