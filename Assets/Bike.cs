using UnityEngine;
using System.Collections;

public class Bike : MonoBehaviour
{

    int tick;
    float direction;
    float speedX;
    int cooldown;

    Main main;
    AudioSource music;

    [HideInInspector]
    public float multiplier;

    void Start ()
    {
        tick = 0;
        multiplier = 3f;
        speedX = 0.06f;
        cooldown = 0;

        main = GameObject.Find("MainScript").GetComponent<Main>();
        music = this.gameObject.GetComponent<AudioSource>();
    }

    void Update ()
    {
        if (!main.gameOver && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (tick < 10)      multiplier = 4f;
            else if (tick < 15) multiplier = 3f;
            else if (tick < 20) multiplier = 2f;
            else                multiplier = 1f;

            tick = 0;
        }

        int newDir = 0;
        if (!main.gameOver && Input.GetKey(KeyCode.LeftArrow))    newDir--;
        if (!main.gameOver && Input.GetKey(KeyCode.RightArrow))   newDir++;
        if (newDir != 0)    direction = newDir;
        else                direction *= 0.95f;

        if (Input.GetKeyUp(KeyCode.Space) && cooldown <= 0)
        {
            main.ThrowChicken();
            cooldown = 30 + Random.Range(0, 20);
        }
    }

    void FixedUpdate ()
    {
        tick++;
        if (tick % 25 == 0)
        {
            if (multiplier > 1f) multiplier -= 1f;
            multiplier -= 0.1f;
            if (multiplier < 0f) multiplier = 0f;
        }

        cooldown--;

        this.gameObject.transform.Translate(speedX * direction, 0f, 0f);

        HandleMusic();
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag != "Chicken")
        {
            main.gameOver = true;
            main.gameOverText.text = "YOUR DEAD";
            main.gameOverUI.SetActive(true);
        }
    }

    void HandleMusic ()
    {
        music.pitch = 1f + 0.1f * multiplier;
    }
}
