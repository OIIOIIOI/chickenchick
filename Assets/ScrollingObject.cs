using UnityEngine;
using System.Collections;

public class ScrollingObject : MonoBehaviour
{

    Main main;

    private void Start ()
    {
        main = GameObject.Find("MainScript").GetComponent<Main>();
    }

	void FixedUpdate ()
    {
        this.transform.Translate(0f, main.Speed(), 0f);
	}

}
