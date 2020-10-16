using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public float velocidad = 20;
    public Vector2 direccion;

    public float tiempodeVida = 2;

    public TextController textController;

    // Start is called before the first frame update
    void Start()
    {
        textController = FindObjectOfType<TextController>();
        Destroy(gameObject, tiempodeVida);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movimiento = direccion.normalized * velocidad * Time.deltaTime;

        transform.Translate(movimiento);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Zombie"))
        {
            textController.AddPoints(10);
            Destroy(this.gameObject);
        }
    }
}
