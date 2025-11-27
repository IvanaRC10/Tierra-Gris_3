using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class Temporizador : MonoBehaviour
{
    [Header("⏱️ Configuración de tiempo")]
    public float tiempoTotal = 40f; // Tiempo límite del juego
    private float tiempoRestante;

    [Header("🎮 Referencias UI")]
    public TextMeshProUGUI textoTiempo; // Texto que muestra el tiempo
    public TextMeshProUGUI textoFin;    // Texto que mostrará "Fin del juego"

    [Header("🧴 Botellas recolectadas")]
    public int botellasRecolectadas = 0;
    public int botellasNecesarias = 4;

    private bool juegoTerminado = false;

    void Start()
    {
        tiempoRestante = tiempoTotal;

        // CORRECCIÓN 1: Comprobar si 'textoFin' está asignado antes de usarlo.
        if (textoFin != null)
        {
            textoFin.gameObject.SetActive(false);
        }
        else
        {
            // Advertencia para el desarrollador si se olvida de asignar.
            
        }
    }

    void Update()
    {
        if (juegoTerminado) return;

        // Resta el tiempo
        tiempoRestante -= Time.deltaTime;

        // CORRECCIÓN 1: Comprobar si 'textoTiempo' está asignado antes de usarlo.
        if (textoTiempo != null)
        {
            // Actualiza el texto del temporizador
            textoTiempo.text = "⏰ Tiempo: " + Mathf.CeilToInt(tiempoRestante).ToString();
        }

        // Si el tiempo llega a cero
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            FinDelJuego();
        }
    }

    void FinDelJuego()
    {
        juegoTerminado = true;

        // Si el texto de fin no está asignado, no podemos continuar.
        if (textoFin == null) return;

        textoFin.gameObject.SetActive(true);

        // Verificamos si recolectó todas las botellas
        if (botellasRecolectadas >= botellasNecesarias)
        {
            textoFin.text = "¡Felicidades! Has ganado";
        }
        else
        {
            // CORRECCIÓN 2: Lógica y texto corregido
            textoFin.text = "Lo siento, solo recolectaste " + botellasRecolectadas +
                            " botellas. Necesitas " + botellasNecesarias + " para ganar.";
        }

        // Espera un poco antes de pausar el juego
        Invoke(nameof(PausarJuego), 0.2f);
    }

    void PausarJuego()
    {
        Time.timeScale = 0f;
    }

    // ✅ FUNCIÓN para sumar botellas
    public void SumarBotella()
    {
        botellasRecolectadas++;
    }
}