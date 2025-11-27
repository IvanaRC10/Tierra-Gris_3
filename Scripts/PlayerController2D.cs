using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float fuerzaSalto = 10f;

    [Header("Detección de suelo")]
    public Transform groundCheck;
    public float radioSuelo = 0.15f;
    public LayerMask capaSuelo;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool enSuelo;

    [Header("Ataque")]
    public LayerMask monstruoLayer;
    public float rangoGolpe = 1.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.freezeRotation = true;
        rb.centerOfMass = Vector2.zero;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        float movimiento = 0f;

        // ----------------------------
        // CONTROLES DE PC (para pruebas)
        // ----------------------------
        movimiento += Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) Saltar();
        if (Input.GetKeyDown(KeyCode.X)) GolpearMonstruo();

        // ----------------------------
        // CONTROLES TÁCTILES
        // ----------------------------
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            // Mover izquierda
            if (toque.position.x < Screen.width * 0.4f)
            {
                movimiento = -1f;
            }

            // Mover derecha
            else if (toque.position.x > Screen.width * 0.6f)
            {
                movimiento = 1f;
            }

            // Saltar: deslizar hacia arriba
            if (toque.phase == TouchPhase.Moved && toque.deltaPosition.y > 35f)
            {
                Saltar();
            }

            // Golpear: toque rápido
            if (toque.phase == TouchPhase.Ended && toque.deltaTime < 0.15f)
            {
                GolpearMonstruo();
            }
        }

        Mover(movimiento);
    }

    // ---------------------------------------------------

    void Mover(float movimiento)
    {
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

        // Voltear sprite
        if (sr != null)
        {
            if (movimiento > 0.01f) sr.flipX = false;
            else if (movimiento < -0.01f) sr.flipX = true;
        }
    }

    void Saltar()
    {
        enSuelo = Physics2D.OverlapCircle(groundCheck.position, radioSuelo, capaSuelo);

        if (enSuelo)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        }
    }

    void GolpearMonstruo()
    {
        Collider2D monstruo = Physics2D.OverlapCircle(transform.position, rangoGolpe, monstruoLayer);

        if (monstruo != null)
        {
            MonstruoGolpes mg = monstruo.GetComponent<MonstruoGolpes>();
            if (mg != null)
            {
                mg.RecibirGolpe();
            }
        }
    }

    // ---------------------------------------------------

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, radioSuelo);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoGolpe);
    }
}