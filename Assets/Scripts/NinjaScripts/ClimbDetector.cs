using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbDetector : MonoBehaviour
{

    public bool escalar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Tree"))
        {
            escalar = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Tree"))
        {
            escalar = false;

        }
    }
}
