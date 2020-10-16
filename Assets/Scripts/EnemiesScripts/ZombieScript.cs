using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Rigidbody2D rigidbodyZombie;
    public Animator animatorZombie;
    public SpriteRenderer spriteZombie;
    public BoxCollider2D boxCollZombie;

    private const int animatorZombieCaminar = 1;
    private const int animatorZombieAtacar = 2;
    private const int animatorZombieMorir = 3;

    private bool zombieAtacar = false;
    private bool chocarKunai = false;
    public float tiempodeVida = 1f;

    public TextController textController;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyZombie = GetComponent<Rigidbody2D>();
        animatorZombie = GetComponent<Animator>();
        spriteZombie = GetComponent<SpriteRenderer>();
        textController = FindObjectOfType<TextController>();
        boxCollZombie = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (zombieAtacar)
        {
            animatorZombie.SetInteger("Estado", animatorZombieAtacar);
            rigidbodyZombie.velocity = new Vector2(0, rigidbodyZombie.velocity.y);
            Destroy(gameObject, tiempodeVida);
        }
        else
        {
            rigidbodyZombie.velocity = new Vector2(-5, rigidbodyZombie.velocity.y);
            animatorZombie.SetInteger("Estado", animatorZombieCaminar);

            if (chocarKunai)
            {
                rigidbodyZombie.velocity = new Vector2(0, rigidbodyZombie.velocity.y);
                animatorZombie.SetInteger("Estado", animatorZombieMorir);
                Destroy(this.gameObject, tiempodeVida);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.Equals("Ninja"))
        {
            zombieAtacar = true;

            if (textController.GetVidas() > 0)
            {
                textController.QuitarVidas(1);
            }
            rigidbodyZombie.velocity = new Vector2(0,0);
            rigidbodyZombie.gravityScale = 0;
            boxCollZombie.enabled = false;

            Destroy(this.gameObject,1.5f);
        }
        if (collision.gameObject.tag.Equals("Kunai"))
        {
            chocarKunai = true;
        }

    }
}
