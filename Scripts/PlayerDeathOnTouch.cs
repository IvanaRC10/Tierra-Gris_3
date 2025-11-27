using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathOnTouch : MonoBehaviour
{
    public GameObject panelDerrota; // Arrastra aquí el panel del Canvas

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("💀 Jugador murió");

            // PAUSAR EL JUEGO
            Time.timeScale = 0f;

            // DESACTIVAR AL JUGADOR
            other.gameObject.SetActive(false);

            // MOSTRAR EL MENSAJE Y EL BOTÓN
            panelDerrota.SetActive(true);
        }
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}