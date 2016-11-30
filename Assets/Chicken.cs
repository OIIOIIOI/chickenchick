using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour
{

    Main main;

    void Start ()
    {
        main = GameObject.Find("MainScript").GetComponent<Main>();
        //this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }
	
	void FixedUpdate ()
    {
        this.gameObject.transform.Translate(-0.05f, 0f, 0f);

        if (!this.gameObject.GetComponent<SpriteRenderer>().isVisible)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Invoke("DestroyMe", 0.05f);
            if (collision.gameObject.GetComponent<House>() != null)
            {
                House house = collision.gameObject.GetComponent<House>();
                if (house.isCustomer)
                {
                    main.ChickenHouse(true);
                }
                else
                {
                    main.ChickenHouse(false);
                }
            }
        }
    }

    void DestroyMe ()
    {
        Destroy(this.gameObject);
    }

}
