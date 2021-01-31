using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Gerak ke atas
    public KeyCode upButton = KeyCode.W;

    // Gerak ke bawah
    public KeyCode downButton = KeyCode.S;

    // Kecepatan gerak
    public float speed = 10.0f;

    // Batas atas dan bawah game scene
    public float yBoundary = 9.0f;

    // Rigidbody 2D
    private Rigidbody2D rigidBody2D;

    // Skor pemain
    private int score;

    // Titik tumbukan terakhir dengan bola untuk menampilkan variabel fisika
    private ContactPoint2D lastContactPoint;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //---------------- Gerakan Raket --------------------
        // Dapatkan kecepatan raket sekarang.
        Vector2 velocity = rigidBody2D.velocity;

        // Pemain menekan tombol atas
        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }

        // Pemain menekan tombol bawah
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }

        // ga neken apa2
        else
        {
            velocity.y = 0.0f;
        }

        // Masukkan kembali kecepatannya ke rigidBody2D.
        rigidBody2D.velocity = velocity;

        //---------------- Boundary --------------------
        //Posisi raket sekarang
        Vector3 position = transform.position;

        //Mengembalikan ke boundary atas
        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }

        //Mengembalikan ke boundary bawah
        if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        //Memasukkan kembali ke transform
        transform.position = position;

        
    }
    //---------------- Skor --------------------
    //Tambah skor
    public void IncrementScore()
    {
        score++;
    }

    //Reset skor
    public void ResetScore()
    {
        score = 0;
    }

    public void Expand()
    {
        transform.localScale = new Vector3(1, 2, 1);
    }

    public void Contract()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }



    //Mendapatkan skor
    public int Score
    {
        get { return score; }
    }

    //---------------- akses informasi titik kontak kelas lain --------------------
    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    //---------------- merekam titik kontak dengan bola --------------------
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }



}
