using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NinjaScripts : MonoBehaviour
{
    public float velocidadNinja = 18f;
    public Vector2 direccionNinja;

    public Rigidbody2D rigidbodyNinja;
    public Animator animatorNinja;
    public SpriteRenderer spriteNinja;
    public Transform transformNinja;
    public BoxCollider2D boxColliderNinja;

    public GameObject Kunai;

    private Vector3 _firePoint;

    private const int animatorNinjaQuieto = 0;
    private const int animatorNinjaCorrer = 1;
    private const int animatorNinjaSaltar = 2;
    private const int animatorNinjaAtack = 3;
    private const int animatorNinjaMorir = 4;
    private const int animatorNinjaDeslizar = 5;
    private const int animatorNinjaEscalar = 6;
    private const int animatorNinjaPlanear = 7;

    private bool muertoNinja = false;

    public TextController textController;
    public ClimbDetector climbDetector;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyNinja = GetComponent<Rigidbody2D>();
        animatorNinja = GetComponent<Animator>();
        spriteNinja = GetComponent<SpriteRenderer>();
        transformNinja = GetComponent<Transform>();
        climbDetector = FindObjectOfType<ClimbDetector>();
        boxColliderNinja = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (muertoNinja != true)
        {
            rigidbodyNinja.velocity = new Vector2(0, rigidbodyNinja.velocity.y);
            animatorNinja.SetInteger("Estado", animatorNinjaQuieto);

            if (Input.GetKeyDown(KeyCode.W))
            {
                animatorNinja.SetInteger("Estado", animatorNinjaAtack);
                disparar(_firePoint);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                spriteNinja.flipX = false;
                animatorNinja.SetInteger("Estado", animatorNinjaCorrer);

                //rigidbodyNinja.velocity = new Vector2(10, rigidbodyNinja.velocity.y);
                direccionNinja = Vector2.right;
                Vector2 movimiento = direccionNinja.normalized * velocidadNinja * Time.deltaTime;
                transformNinja.Translate(movimiento);

                _firePoint = new Vector3(transformNinja.position.x + 1.64f, transformNinja.position.y - 0.049f, 0);

                if (Input.GetKeyDown(KeyCode.W))
                {
                    animatorNinja.SetInteger("Estado", animatorNinjaAtack);
                    disparar(_firePoint);
                }

            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                animatorNinja.SetInteger("Estado", animatorNinjaCorrer);
                spriteNinja.flipX = true;

                //rigidbodyNinja.velocity = new Vector2(-10, rigidbodyNinja.velocity.y);
                direccionNinja = Vector2.left;
                Vector2 movimiento = direccionNinja.normalized * velocidadNinja * Time.deltaTime;
                transformNinja.Translate(movimiento);

                _firePoint = new Vector3(transformNinja.position.x - 1.64f, transformNinja.position.y - 0.049f, 0);
                if (Input.GetKeyDown(KeyCode.W))
                {
                    animatorNinja.SetInteger("Estado", animatorNinjaAtack);
                    disparar(_firePoint);
                }

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbodyNinja.AddForce(new Vector2(0, 50), ForceMode2D.Impulse);

                animatorNinja.SetInteger("Estado", animatorNinjaSaltar);
            }

            if (Input.GetKey(KeyCode.A))
            {
                animatorNinja.SetInteger("Estado", animatorNinjaDeslizar);
            }
            if (climbDetector.escalar)
            {
                rigidbodyNinja.gravityScale = 0;
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    direccionNinja = Vector2.up;
                    Vector2 movimiento = direccionNinja.normalized * velocidadNinja * Time.deltaTime;
                    transformNinja.Translate(movimiento);
                    animatorNinja.SetInteger("Estado", animatorNinjaEscalar);
                    
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    direccionNinja = Vector2.down;
                    Vector2 movimiento = direccionNinja.normalized * velocidadNinja * Time.deltaTime;
                    transformNinja.Translate(movimiento);
                    animatorNinja.SetInteger("Estado", animatorNinjaEscalar);
                }
            }
            if (!climbDetector.escalar)
            {
                rigidbodyNinja.gravityScale = 10;
            }

            if (rigidbodyNinja.velocity.y < 0 && Input.GetKeyDown(KeyCode.S))
            {
                animatorNinja.SetInteger("Estado", animatorNinjaPlanear);
            }

        }
 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Zombie"))
        {
            if (textController.GetVidas() <= 0)
            {
                muertoNinja = true;
                animatorNinja.SetInteger("Estado", animatorNinjaMorir);
                
            }
        }

    }

    void disparar(Vector3 _firePoint)
    {
        if (Kunai != null && _firePoint != null && this.gameObject != null)
        {
            GameObject myKunai = Instantiate(Kunai, _firePoint, Quaternion.identity) as GameObject;

            Kunai kunaiComponent = myKunai.GetComponent<Kunai>();
            SpriteRenderer kunaiSr = myKunai.GetComponent<SpriteRenderer>();

            if (spriteNinja.flipX == true)
            {
                kunaiSr.flipX = true;
                kunaiComponent.direccion = Vector2.left;
            }
            if (spriteNinja.flipX == false)
            {
                kunaiSr.flipX = false;
                kunaiComponent.direccion = Vector2.right;
            }
        }
    }

}
