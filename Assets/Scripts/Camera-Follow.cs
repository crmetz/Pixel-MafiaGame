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

    public float minY = 0.0f; // A posição mínima no eixo Y que você deseja permitir para a câmera.

    void Update()
    {
        // Obter a posição atual da câmera.
        Vector3 currentPosition = transform.position;

        // Se a posição da câmera estiver abaixo do limite mínimo no eixo Y, ajuste para o limite mínimo.
        if (currentPosition.y < minY)
        {
            currentPosition.y = minY;
            transform.position = currentPosition;
        }
    }

}
