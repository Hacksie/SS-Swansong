using UnityEngine;

namespace HackedDesign
{
    public class Missile : MonoBehaviour
    {
        [SerializeField]
        public float thrust;

        [SerializeField]
        public float rotateSpeed;        

        [SerializeField]
        public GameObject source;

        [SerializeField]
        public GameObject target;

        [SerializeField]
        public string type;

        [SerializeField]
        public bool hostile;

        [SerializeField]
        public float timeOut;
        public float launchTime;



        private new Rigidbody2D rigidbody = null;

        public void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            if (rigidbody == null)
            {
                Debug.LogError(this.name + ": rigidbody is null");
            }
        }

        public void Reset()
        {
            source = null;
            target = null;
            hostile = false;
            launchTime = 0;
            type = null;
        }

        public void Launch(Vector3 start, Vector3 direction, GameObject source, GameObject target, string type, bool hostile)
        {
            Debug.Log(this.name + ": launch!");
            this.gameObject.SetActive(true);
            this.source = source;
            this.target = target;
            this.type = type;
            this.transform.position = start;
            //float force = thrust;
            this.transform.up = direction;
            //rigidbody.AddRelativeForce(direction, ForceMode2D.Impulse);
            this.launchTime = Time.time;

            this.hostile = hostile;

            //RotateTowardTarget();





        }

        public void UpdateMissile()
        {
            if (target == null)
            {
                rigidbody.velocity = Vector2.zero;
                this.gameObject.SetActive(false);
                return;
            }

            if(Time.time - this.launchTime > this.timeOut)
            {
                Debug.Log(this.name + ": missile timed out");
                Explode();
                return;
            }            

            rigidbody.velocity = transform.up * thrust * Time.fixedDeltaTime;

            Vector3 targetVector = target.transform.position - transform.position;

            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

            rigidbody.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.fixedDeltaTime;


            //RotateTowardTarget();

        }

        void RotateTowardTarget()
        {
            // Slerp this
            Vector2 direction = (target.transform.position - this.transform.position);
            transform.up = direction;
        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            rigidbody.velocity = Vector2.zero;
            target = null;

        }

        // void OnCollisionEnter2D(Collision2D other)
        // {
            
        //     // if (hostile)
        //     // {

        //     // }
        //     // else
        //     // {
        //     //     if(other.gameObject.tag == "Player" || (other.transform.parent != null && other.transform.parent.tag == "Hostile"))
        //     //     {
        //     //         return; // If the missile isn't hostile and the collision is us, ignore the collision
        //     //     }
        //     //     if (other.gameObject.tag == "Hostile" || (other.transform.parent != null && other.transform.parent.tag == "Hostile"))
        //     //     {
        //     //         Debug.Log(this.name + ": explode!");
        //     //     }
        //     // }
        // }
    }
}