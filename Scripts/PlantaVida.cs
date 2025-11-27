// PlantaVida.cs
using UnityEngine;

public class PlantaVida : MonoBehaviour
{
    // Define cuánta vida dará esta planta (ej: 0.5 = 50% de la barra)
    public float CantidadCuracion = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Busca la barra de vida DIRECTAMENTE como hacía la botella
            BarraVida barraVida = FindObjectOfType<BarraVida>();

            if (barraVida != null)
            {
                // 💥 ¡Aquí es donde ocurre la curación!
                float vidaActual = barraVida.AumentarVida(CantidadCuracion);

                Debug.Log("🌱 Planta 'Vida' consumida. Vida actual: " + vidaActual);

                Destroy(gameObject); // La planta se consume
            }
            else
            {
                Debug.LogError("🔴 ERROR: No se encontró el script 'BarraVida' para curar al jugador.");
            }
        }
    }
}