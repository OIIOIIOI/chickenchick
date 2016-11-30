using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    Main main;

    void Start()
    {
        main = GameObject.Find("MainScript").GetComponent<Main>();
    }

    void FixedUpdate()
    {
        this.transform.Translate(0f, main.Speed() * 2f, 0f);

        if (!this.gameObject.GetComponent<SpriteRenderer>().isVisible && this.transform.position.y < 0)
            Destroy(this.gameObject);
    }

}
