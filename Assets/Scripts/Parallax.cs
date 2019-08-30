/*
Built with help from: https://www.youtube.com/watch?v=zit45k6CUMk
*/

using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Vector2 bounds, startpos;

    [SerializeField]
    private Camera cam = null;

    [SerializeField]
    private float parallaxEffect = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(cam == null)
        {
            Debug.LogError(this.name + ": cam not set");
        }
        startpos = transform.position;
        bounds = GetComponent<SpriteRenderer>().bounds.size;       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 temp = (cam.transform.position * (1 - parallaxEffect));
        Vector2 dist = cam.transform.position * parallaxEffect;

        transform.position = startpos + dist;

        if(temp.x > (startpos.x + bounds.x))
            startpos.x += bounds.x;
        if(temp.x < (startpos.x - bounds.x))
            startpos.x -= bounds.x;            
        if(temp.y > (startpos.y + bounds.y))
            startpos.y += bounds.y;        
        if(temp.y < (startpos.y - bounds.y))
            startpos.y -= bounds.y;            
    }
}
