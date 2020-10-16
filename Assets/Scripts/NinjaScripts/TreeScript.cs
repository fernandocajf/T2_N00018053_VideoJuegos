using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public BoxCollider2D treeBoxCollider;


    // Start is called before the first frame update
    void Start()
    {
        treeBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("ClimbDetector"))
        {
            treeBoxCollider.isTrigger = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ninja"))
        {
            treeBoxCollider.isTrigger = true;
        }
    }


}
