using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{

    public float baseSpeed;
    public int maxRoadChance;
    public GameObject[] housesPrefabs;
    public GameObject roadPrefab;
    public GameObject playerPrefab;
    public GameObject chickenPrefab;

    GameObject player;
    Bike bike;
    GameObject town;
    Camera cam;
    AudioSource music;
    AudioDistortionFilter disto;

    float maxYOffset;
    float baseXOffset;
    float basePitch;
    float tick;
    int spawned;
    int roadChance;
    
	void Start ()
    {
        town = GameObject.Find("Town");
        cam = Camera.main;

        float playerY = -cam.orthographicSize * 0.65f;
        player = Instantiate(playerPrefab, new Vector3(0f, playerY, 0f), Quaternion.identity) as GameObject;
        bike = player.GetComponent<Bike>();

        music = this.gameObject.GetComponent<AudioSource>();
        music.panStereo = 0f;
        disto = this.gameObject.GetComponent<AudioDistortionFilter>();
        disto.distortionLevel = 0;
        basePitch = 0.65f;

        tick = 0;
        spawned = 0;
        roadChance = maxRoadChance;
        maxYOffset = 0.05f;
        baseXOffset = -1.7f;
    }

    void FixedUpdate ()
    {
        tick += Speed();
        if (tick <= 0f)
        {
            GameObject newObject;
            if (Random.Range(0, roadChance) > 0)
            {
                newObject = SpawnHouse();
                roadChance--;
            }
            else
            {
                newObject = SpawnRoad();
                roadChance = maxRoadChance;
            }
            tick = newObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        }

        HandleMusic();
    }

    GameObject SpawnHouse ()
    {
        spawned++;
        int rand = Random.Range(0, housesPrefabs.Length);
        float xOffset = Random.Range(-0.2f, 0.2f);
        float yOffset = Random.Range(-maxYOffset, maxYOffset);
        Vector3 pos = new Vector3(baseXOffset + xOffset, cam.orthographicSize + yOffset, spawned/10f);
        GameObject go = Instantiate(housesPrefabs[rand], pos, Quaternion.identity, town.transform) as GameObject;
        return go;
    }

    GameObject SpawnRoad ()
    {
        spawned++;
        Vector3 pos = new Vector3(baseXOffset, cam.orthographicSize, spawned / 10f);
        GameObject go = Instantiate(roadPrefab, pos, Quaternion.identity, town.transform) as GameObject;
        return go;
    }

    public GameObject ThrowChicken ()
    {
        Vector3 pos = bike.transform.position;
        GameObject go = Instantiate(chickenPrefab, pos, Quaternion.identity) as GameObject;
        return go;
    }

    public float Speed ()
    {
        return baseSpeed / -100f * bike.multiplier;
    }

    void HandleMusic ()
    {
        music.pitch = basePitch + 0.1f * bike.multiplier;
        if (disto.distortionLevel > 0f)
            disto.distortionLevel *= 0.99f;
        if (music.panStereo > 0.05f || music.panStereo < -0.05f)
            music.panStereo *= 0.99f;
        else
            music.panStereo = 0f;
        if (Random.Range(0, 200) == 0)
        {
            disto.distortionLevel = 1f;
            music.panStereo = 1f;
        }
    }

}
