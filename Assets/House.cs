using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

    [HideInInspector]
    public bool isCustomer;

    Main main;

    void Start()
    {
        main = GameObject.Find("MainScript").GetComponent<Main>();
        isCustomer = Random.Range(0, 2) == 0;
        if (isCustomer)
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0.9f, 0f);
    }

    void FixedUpdate()
    {
        this.transform.Translate(0f, main.Speed(), 0f);

        if (!this.gameObject.GetComponent<SpriteRenderer>().isVisible && this.transform.position.y < 0)
            Destroy(this.gameObject);
    }

}
