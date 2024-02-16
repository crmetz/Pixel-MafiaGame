using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, player.position, 0.1f);
    }

    public float minY = 0.0f; // A posi��o m�nima no eixo Y que voc� deseja permitir para a c�mera.

    void Update()
    {
        // Obter a posi��o atual da c�mera.
        Vector3 currentPosition = transform.position;

        // Se a posi��o da c�mera estiver abaixo do limite m�nimo no eixo Y, ajuste para o limite m�nimo.
        if (currentPosition.y < minY)
        {
            currentPosition.y = minY;
            transform.position = currentPosition;
        }
    }

}
