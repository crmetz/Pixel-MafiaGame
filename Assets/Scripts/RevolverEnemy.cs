using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RevolverEnemy : MonoBehaviour
{

        public float speed;
        public float chaseDistance;
        public float stopDistance;
        public GameObject target;

        private bool facingRight = true;

        public GameObject bulletPrefab;
        public Transform FirePointRevolver;
        public float shootCooldown = 2f;
        private float shootTimer = 0f;

        private float targetDistance;

        public int health = 50;

        public GameObject deathEffect;

        private Animator animator;
        private int movementHash = Animator.StringToHash("movement");
        float horizontalMove;
        float verticalMove;

        public float shootDistance = 5f;

    private void Awake()
    {
        //rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }

        }

        void Die()
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }


        private void Update()
        {
            targetDistance = Vector2.Distance(transform.position, target.transform.position);
            if (targetDistance < chaseDistance && targetDistance > stopDistance)
            {
                ChasePlayer();
                horizontalMove = Input.GetAxis("Horizontal");
                verticalMove = Input.GetAxis("Vertical");

                animator.SetBool(movementHash, horizontalMove != 0);

            }
            else
            {
                StopChasePlayer();
            }

            shootTimer += Time.deltaTime;

        // Check if it's time to shoot
            if (shootTimer >= shootCooldown && targetDistance < shootDistance)
            {
                Shoot();
                shootTimer = 0f; // Reset the timer
            }
    
    }

        void Shoot()
        {
            // Create a new bullet instance
            GameObject bullet = Instantiate(bulletPrefab, FirePointRevolver.position, FirePointRevolver.rotation);

            // Add any necessary logic to control the bullet's movement and behavior here
        }

        private void StopChasePlayer()
        {
            //Fazer nada
        }

        private void ChasePlayer()
        {
            if (transform.position.x < target.transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;

                FirePointRevolver.transform.Rotate(0, 180, 0);
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;


            }

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        private void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    
}
