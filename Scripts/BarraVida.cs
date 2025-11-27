// BarraVida.cs (Versión simplificada para usar SOLO Image)
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    // Solo dejamos la variable que SÍ estás usando.
    [SerializeField] private Image rellenoImagen;

    private float vidaActualNormalizada;
    private float vidaMaxima = 1.0f;

    public void EstablecerVidaMaxima()
    {
        vidaActualNormalizada = vidaMaxima;

        // ¡Usamos la imagen directamente!
        if (rellenoImagen != null)
        {
            rellenoImagen.fillAmount = vidaActualNormalizada;
        }
    }

    public float ReducirVida(float cantidadDaño)
    {
        vidaActualNormalizada -= cantidadDaño;
        vidaActualNormalizada = Mathf.Clamp(vidaActualNormalizada, 0f, vidaMaxima);
        ActualizarBarraVisual();
        return vidaActualNormalizada;
    }

    public float AumentarVida(float cantidadCuracion)
    {
        vidaActualNormalizada += cantidadCuracion;
        vidaActualNormalizada = Mathf.Clamp(vidaActualNormalizada, 0f, vidaMaxima);
        ActualizarBarraVisual();
        return vidaActualNormalizada;
    }

    private void ActualizarBarraVisual()
    {
        if (rellenoImagen != null)
        {
            rellenoImagen.fillAmount = vidaActualNormalizada;
        }
        else
        {
            // Este error ya no aparecerá si el campo 'Relleno Imagen' está conectado.
            Debug.LogError("❌ ERROR: El campo 'Relleno Imagen' no está conectado en el Inspector.");    
        }
    }
}