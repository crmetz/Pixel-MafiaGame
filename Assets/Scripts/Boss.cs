using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public float speed;
    public float chaseDistance;
    public float stopDistance;
    public GameObject target;

    private bool facingRight = true;

    public GameObject bulletPrefab;
    public Transform FirePointBoss;
    public float shootCooldown = 2f;
    private float shootTimer = 0f;

    private float targetDistance;

    public int health = 100;

    public GameObject deathEffect;

    public float shootDistance = 5f;

    void Shoot()
    {
        // Create a new bullet instance
        GameObject bullet = Instantiate(bulletPrefab, FirePointBoss.position, FirePointBoss.rotation);

        // Add any necessary logic to control the bullet's movement and behavior here
    }

    

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Time.timeScale = 0;
    }

    


    private void Update()
    {
        targetDistance = Vector2.Distance(transform.position, target.transform.position);
        if(targetDistance < chaseDistance && targetDistance > stopDistance)
        {
            ChasePlayer();
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

    

    private void StopChasePlayer()
    {
        //Fazer nada
    }

    private void ChasePlayer()
    {
        if(transform.position.x < target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;

            FirePointBoss.transform.Rotate(0, 180, 0);
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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

    }
}
