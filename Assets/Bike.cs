using UnityEngine;
using System.Collections;

public class Bike : MonoBehaviour
{

    int tick;
    float direction;
    float speedX;

    Main main;

    [HideInInspector]
    public float multiplier;

    void Start ()
    {
        tick = 0;
        multiplier = 1f;
        speedX = 0.06f;

        main = GameObject.Find("MainScript").GetComponent<Main>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (tick < 10)      multiplier = 4f;
            else if (tick < 15) multiplier = 3f;
            else if (tick < 20) multiplier = 2f;
            else                multiplier = 1f;

            tick = 0;
        }

        int newDir = 0;
        if (Input.GetKey(KeyCode.LeftArrow))    newDir--;
        if (Input.GetKey(KeyCode.RightArrow))   newDir++;
        if (newDir != 0)    direction = newDir;
        else                direction *= 0.8f;

        if (Input.GetKeyUp(KeyCode.Space))
            main.ThrowChicken();
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

        this.gameObject.transform.Translate(speedX * direction, 0f, 0f);
    }
}
