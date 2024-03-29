using UnityEngine;
using System.Linq;

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
            if (rigidbody != null)
            {
                rigidbody.velocity = Vector2.zero;
            }
            this.gameObject.SetActive(false);
        }

        public void Launch(Vector3 start, Vector3 direction, GameObject source, GameObject target, string type, bool hostile)
        {
            Debug.Log(this.name + ": launch!");
            MissileDescription missile = Game.Instance.missileDescriptions.FirstOrDefault(e => e.name == type);

            if(missile != null)
            {
                this.thrust = missile.thrust;
                this.rotateSpeed = missile.rotationSpeed;
                this.timeOut = missile.timeOut;
            }

            this.gameObject.SetActive(true);
            this.source = source;
            this.target = target;
            this.type = type;
            this.transform.position = start;
            this.transform.up = direction;
            this.launchTime = Time.time;
            this.hostile = hostile;
        }

        public void UpdateMissile()
        {
            if (target == null)
            {
                this.gameObject.SetActive(false);

                if (rigidbody != null)
                {
                    rigidbody.velocity = Vector2.zero;
                }
                return;
            }

            if (Time.time - this.launchTime > this.timeOut)
            {
                Debug.Log(this.name + ": missile timed out");
                Explode();
                return;
            }

            rigidbody.velocity = transform.up * thrust * Time.fixedDeltaTime;

            Vector3 targetVector = target.transform.position - transform.position;

            float rotatingIndex = Vector3.Cross(targetVector, transform.up).z;

            rigidbody.angularVelocity = -1 * rotatingIndex * rotateSpeed * Time.fixedDeltaTime;
        }

        public void Explode()
        {
            if(type == "ES-23 Harpoon" || type == "RM-44 Rook") 
            {
                Game.Instance.EMPExplosion(this.transform.position);
            }
            else 
            {
                Game.Instance.Explosion(this.transform.position);
            }
            Debug.Log(this.name + ": explode");
            Reset();
        }
    }
}