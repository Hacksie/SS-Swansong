using UnityEngine;

namespace HackedDesign
{
    public class Laser : MonoBehaviour
    {
        [SerializeField]
        public float thrust;

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
            this.gameObject.SetActive(false);
            rigidbody.velocity = Vector2.zero;
        }

        public void Launch(Vector3 start, Vector3 direction, GameObject source, bool hostile)
        {
            Debug.Log(this.name + ": launch!");
            this.gameObject.SetActive(true);
            this.source = source;
            this.type = "Laser";
            this.transform.position = start;
            this.transform.up = direction;
            this.launchTime = Time.time;
            this.hostile = hostile;
        }

        public void UpdateMissile()
        {
            if(source == null)
            {
                return;
            }
            if(Time.time - this.launchTime > this.timeOut)
            {
                Debug.Log(this.name + ": missile timed out");
                Explode();
                return;
            }            

            rigidbody.velocity = transform.up * thrust * Time.fixedDeltaTime;

        }

        public void Explode()
        {
            Debug.Log(this.name + ": explode");
            Reset();
        }
    }
}