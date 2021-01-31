using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidbody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce;
    public float yInitialForce;

    //Titik asal lintasan bola sekarang
    private Vector2 trajectoryOrigin;

    // Skrip GameManager untuk mengakses skor maksimal
    [SerializeField]
    private GameManager gameManager;

    //player
    public PlayerControl Player1;
    public PlayerControl Player2;

    public BallControl ball;

    //Reset Ball
    void ResetFireball()
    {
        //reset posisi ke (0,0)
        transform.position = Vector2.zero;

        //reset kecepatan ke (0,0)
        rigidbody2D.velocity = Vector2.zero;
    }

    void PushFireball()
    {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);

        //nilai acak antara 0 dan 2
        float randomDirection = Random.Range(0, 2);

        //menentukan gerakan roda berdasarkan nilai acak
        if (randomDirection < 1.0f)
        {
            rigidbody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce).normalized * xInitialForce);

        }
        else
        {
            rigidbody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce).normalized * xInitialForce);
        }
    }

    //setiap game diulang
    void RestartGame()
    {

        ResetFireball();
        Invoke("PushFireball", 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        RestartGame();
        trajectoryOrigin = transform.position;
    }

    //---------------- merekam tumbukan bola --------------------
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    //---------------- mengakses informasi titik awal lintasan --------------------
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }

    private void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        print(anotherCollider.gameObject.name);

        //jika objek player1
        if (anotherCollider.name == "Player1")
        {

            Player2.IncrementScore();

            //jika skor belum maks
            if (Player2.Score < gameManager.maxScore)
            {
                ball.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
        else if (anotherCollider.name == "Player2")
        {
            Player1.IncrementScore();

            //jika skor belum maks
            if (Player1.Score < gameManager.maxScore)
            {
                ball.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
    

}
