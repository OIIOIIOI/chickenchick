using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour
{
    
	void Start ()
    {
        //this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
	}
	
	void FixedUpdate ()
    {
        this.gameObject.transform.Translate(-0.05f, 0f, 0f);
        //this.gameObject.transform.Rotate(Vector3.forward, 10f, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            Invoke("DestroyMe", 0.1f);
        }
    }

    void DestroyMe ()
    {
        Destroy(this.gameObject);
    }

}
