﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public float baseSpeed;
    public int maxRoadChance;
    public GameObject[] housesPrefabs;
    public GameObject roadPrefab;
    public GameObject playerPrefab;
    public GameObject chickenPrefab;
    public GameObject carPrefab;
    public AudioClip throwSound;
    public AudioClip goodSound;
    public AudioClip badSound;
    public AudioClip fallSound;
    public AudioClip collisionSound;
    public Button restartButton;
    public GameObject gameOverUI;
    public Text gameOverText;
    public Image fellImg;
    public Text scoreText;

    [HideInInspector]
    public bool gameOver;

    GameObject player;
    Bike bike;
    GameObject town;
    Camera cam;
    AudioSource music;
    AudioSource sfx;
    AudioDistortionFilter disto;

    float maxYOffset;
    float baseXOffset;
    float basePitch;
    float tick;
    int spawned;
    int roadChance;
    int score;
    
	void Start ()
    {
        gameOver = false;
        gameOverUI.SetActive(false);
        //fellImg.color = Color.clear;

        town = GameObject.Find("Town");
        cam = Camera.main;

        float playerY = -cam.orthographicSize * 0.65f;
        player = Instantiate(playerPrefab, new Vector3(0f, playerY, 0f), Quaternion.identity) as GameObject;
        bike = player.GetComponent<Bike>();

        sfx = GameObject.Find("SFXTrack").GetComponent<AudioSource>();

        music = GameObject.Find("MusicTrack").GetComponent<AudioSource>();
        music.panStereo = 0f;
        disto = GameObject.Find("MusicTrack").GetComponent<AudioDistortionFilter>();
        disto.distortionLevel = 0;
        basePitch = 0.65f;

        tick = 0;
        spawned = 0;
        roadChance = maxRoadChance;
        maxYOffset = 0.05f;
        baseXOffset = -1.3f;
        score = 0;
    }

    public void RestartGame ()
    {
        SceneManager.LoadScene(1);
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

        if (Random.Range(0, 200) == 0)
            SpawnCar();

        HandleMusic();
        
        if (Time.timeSinceLevelLoad > 1f && Speed() > -0.01f && !gameOver)
        {
            sfx.PlayOneShot(fallSound);
            gameOver = true;
            gameOverText.text = "YOU FELL...";
            fellImg.color = Color.black;
            gameOverUI.SetActive(true);
        }
    }

    GameObject SpawnCar ()
    {
        float xOffset = Random.Range(-1f, 1f);
        float yOffset = Random.Range(-maxYOffset, maxYOffset);
        Vector3 pos = new Vector3(xOffset, cam.orthographicSize + yOffset, 0f);
        GameObject go = Instantiate(carPrefab, pos, Quaternion.identity) as GameObject;
        return go;
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

    public void PlayCollision ()
    {
        sfx.PlayOneShot(collisionSound);
    }

    public GameObject ThrowChicken ()
    {
        sfx.PlayOneShot(throwSound);
        Vector3 pos = bike.transform.position;
        GameObject go = Instantiate(chickenPrefab, pos, Quaternion.identity) as GameObject;
        return go;
    }

    public void ChickenHouse (bool good)
    {
        if (good)
        {
            sfx.PlayOneShot(goodSound);
            score += 76 + Random.Range(0, 18);
        }
        else
        {
            sfx.PlayOneShot(badSound);
            score -= 39 + Random.Range(0, 23);
        }
        scoreText.text = score + " Pts";
    }

    public float Speed ()
    {
        return baseSpeed / -100f * bike.multiplier;
    }

    void HandleMusic ()
    {
        music.pitch = basePitch + 0.025f * bike.multiplier;
        if (disto.distortionLevel > 0f)
            disto.distortionLevel *= 0.95f;
        if (music.panStereo > 0.05f || music.panStereo < -0.05f)
            music.panStereo *= 0.95f;
        else
            music.panStereo = 0f;
        if (Random.Range(0, 400) == 0)
        {
            disto.distortionLevel = 1f;
            music.panStereo = 1f;
        }
    }

}
