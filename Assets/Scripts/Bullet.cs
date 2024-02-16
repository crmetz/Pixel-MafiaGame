using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 5;
    public Rigidbody2D rb;

    public int health = 100;
    

    public HpBar bar;
    public GameObject hpbar;

    public float duration = 10000f;
    public int originalDamage;

    // Barra do pickup hp
    public GameObject cooldownBar; // Referência para a barra de cooldown na cena
    private float cooldownTimer; // Temporizador para controlar a duração do efeito


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        originalDamage = damage; // Salva o dano original ao iniciar
    }



    // Função para duplicar o dano por um determinado tempo
    public IEnumerator DuplicateDamageForDuration()
    {
        // Duplica o damage
        damage *= 100;
        Debug.Log("Dano duplicado: " + damage);

        // Aguarda pelo tempo especificado
        yield return new WaitForSeconds(duration);

        // Configura o temporizador de cooldown
        cooldownTimer = duration;

        // Ativa a barra de cooldown
        if (cooldownBar != null)
            cooldownBar.SetActive(true);

        while (cooldownTimer > 0)
        {
            // Atualiza a barra de cooldown (ajustando o tamanho conforme o tempo)
            if (cooldownBar != null)
                cooldownBar.transform.localScale = new Vector3(cooldownTimer / duration, 1, 1);

            // Aguarda um pequeno intervalo antes de decrementar o temporizador
            yield return new WaitForSeconds(0.1f);

            // Decrementa o temporizador
            cooldownTimer -= 0.1f;
        }


        // Restaura o valor original do damage após o tempo especificado
        damage = originalDamage;

        Debug.Log("Dano restaurado: " + damage);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        RevolverEnemy revolverEnemy = hitInfo.GetComponent<RevolverEnemy>();
        Boss boss = hitInfo.GetComponent<Boss>();
        PlayerScript player = hitInfo.GetComponent<PlayerScript>();

        // Evita que o jogador seja destruído pelo próprio tiro
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(50);
                bar.AlterHealth(health - 50);
            }
        }
        else
        {
            // Verifica se atingiu um inimigo (Boss ou RevolverEnemy) e causa dano
            if (boss != null)
            {
                Debug.Log("Boss levou " + damage + " de dano.");
                boss.TakeDamage(damage);
            }

            if (revolverEnemy != null)
            {
                Debug.Log("RevolverE levou " + damage + " de dano.");
                revolverEnemy.TakeDamage(damage);
            }

            
        }

        //Destroy(gameObject);

        if (gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }


}
 