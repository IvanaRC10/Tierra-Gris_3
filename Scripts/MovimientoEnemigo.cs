using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    // Velocidad a la que se mueve el enemigo
    public float velocidadMovimiento = 2f;

    // Distancia total de movimiento (por ejemplo, 4 unidades a cada lado)
    public float distanciaMovimiento = 4f;

    private Vector3 puntoInicial;
    private int direccion = 1; // 1 para derecha, -1 para izquierda

    void Start()
    {
        // Guardamos la posición inicial del monstruo
        puntoInicial = transform.position;
    }

    void Update()
    {
        // 1. Movimiento
        transform.Translate(Vector2.right * velocidadMovimiento * direccion * Time.deltaTime);

        // 2. Cálculo de distancia y cambio de dirección
        float distanciaRecorrida = transform.position.x - puntoInicial.x;

        // Si se mueve hacia la derecha y ha llegado al límite
        if (direccion == 1 && distanciaRecorrida >= distanciaMovimiento)
        {
            direccion = -1;
            VoltearSprite();
        }
        // Si se mueve hacia la izquierda y ha llegado al límite
        else if (direccion == -1 && distanciaRecorrida <= -distanciaMovimiento)
        {
            direccion = 1;
            VoltearSprite();
        }
    }

    // Función para voltear el sprite (gira el objeto)
    void VoltearSprite()
    {
        Vector3 escalaActual = transform.localScale;
        escalaActual.x *= -1; // Invierte el eje X para voltear
        transform.localScale = escalaActual;
    }

    // NOTA: No necesita OnTriggerEnter2D porque JugadorVida.cs ya detecta al enemigo.
}