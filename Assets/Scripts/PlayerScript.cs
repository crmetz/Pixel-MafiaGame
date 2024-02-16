using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update

    
    [SerializeField] private float hSpeed = 10.0f;
    [SerializeField] private float vSpeed = 6.0f;
    private Rigidbody2D rb2D;
    [SerializeField] private bool canMove = true;

    private bool facingRight = true;

    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;
    private Vector3 velocity = Vector3.zero;

    private Animator animator;

    public int health = 100;

    private int movementHash = Animator.StringToHash("Movement");

    float horizontalMove;
    float verticalMove;

    public GameObject LostPanel;

    public HpBar bar;
    public GameObject hpbar;


    public Slider slider;

    public Bullet bulletScript;
    public float duration = 5f;

   
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //bulletScript = GetComponent<Bullet>();
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        slider.value = health;

        if (health <= 0)
        {
            Die();
            LostPanel.SetActive(true);
            Time.timeScale = 0;
        }

    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    public void Move(float hMove, float vMove, bool jump)
    {
        if (canMove)
        {
            Vector3 targetVelocity = new Vector2(hMove * hSpeed, vMove * vSpeed);

            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, movementSmooth);

            //rotate our character if we're facing the wrong way
            if (hMove>0 && !facingRight)
            {
                flip();
            } else if (hMove<0 && facingRight)
            {
                flip();
            }

        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
   
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        animator.SetBool(movementHash, horizontalMove != 0);

        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);
        //transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Move(horizontalMove, verticalMove, false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            if (collision.gameObject != gameObject)
            {
                //Destroi o coin
                Destroy(collision.gameObject);
            }
            
            // Chama a função para duplicar o dano
            StartCoroutine(bulletScript.DuplicateDamageForDuration());

        }

        if (collision.CompareTag("Rubi"))
        {
            if (collision.gameObject != gameObject)
            {
                //Destroi o coin
                Destroy(collision.gameObject);
            }
            Debug.Log("Vida Atual: " + health);

            health += 20;
            slider.value = health;
            Debug.Log("Depois: " + health);

        }
    }

    
}
