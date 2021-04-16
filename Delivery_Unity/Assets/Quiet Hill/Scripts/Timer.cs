using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    public float time;
    public TMP_Text textBox;
    WeaponSpawn spawner;


    // Start is called before the first frame update
    void Start()
    {
        // start time at 5:00
        time = 18000;

        // grab weapon spawner
        spawner =  GameObject.FindGameObjectWithTag("WeaponSpawn").GetComponent<WeaponSpawn>();
        
        // spawn dagger
        spawner.Invoke("SpawnDagger", 20f);

        // spawn bat
        spawner.Invoke("SpawnBat", 80f);

        // spawn hammer
        spawner.Invoke("SpawnHammer", 140f);

        // spawn gun
        spawner.Invoke("SpawnGun", 200f);

        // spawn crate
        spawner.Invoke("SpawnCrate", 260f);
    }

    // Update is called once per frame
    void Update()
    {
        // speed time up by 180 (turn 12 hours into 4 minutes)
        time += Time.deltaTime*180;

        //clock
        DisplayTime();

        // reset to 0 at midnight
        if (time >= 43200)
        {
            time = 0;
        }
    }

    // digital clock
    void DisplayTime()
    {
        int hours = Mathf.FloorToInt(time / 3600.0f);
        float minutes = Mathf.FloorToInt((time - hours * 3600) / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        textBox.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }

}
