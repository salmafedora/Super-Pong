using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    // Skrip GameManager untuk mengakses skor maksimal
    [SerializeField]
    private GameManager gameManager;

    //nambah skor jika kena dinding
    public PlayerControl player;

    // akan dipanggil jika objek bersentuhan dengan dinding
    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        //jika objek bola
        if(anotherCollider.name == "Ball")
        {
            player.IncrementScore();

            //jika skor belum maks
            if (player.Score < gameManager.maxScore)
            {
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }

    }

}
