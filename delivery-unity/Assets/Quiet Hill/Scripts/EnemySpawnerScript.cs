using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    // Birdman spawn & location variables
    public GameObject enemy;
    float randX;
    Vector2 whereToSpawn;

    // spawn rate and limit variables
    public float spawnRate = 2f;
    float nextSpawn = 0.0f;
    public int spawnLimit;
    public int enemies;
    public int deadBirds = 0;

    // Update is called once per frame
    void Update()
    {
        // check number of Birdmen
        enemies = GameObject.FindGameObjectsWithTag("Birdman").Length;
        
        // wait for intro to end
        if (Time.time >= 20)
        {
            //stagger spawns & limit total number & check that spawn time is correct
            if (Time.time > nextSpawn && enemies < spawnLimit && Mathf.Floor(Time.time) % 4 == 0)
            {
                // reset next spawn
                nextSpawn = Time.time + spawnRate;

                // randomize spawn location within horizontal range
                randX = Random.Range (-8f, 4f);

                // set spawn location
                whereToSpawn = new Vector2 (randX, transform.position.y);

                // spawn
                GameObject clone = Instantiate (enemy, whereToSpawn, Quaternion.identity);

                // assign layer
                clone.layer = LayerMask.NameToLayer("Birdman");
            }

            // when Birdman dies, alternate respawn
            if (Time.time > nextSpawn - 2 && deadBirds >= 3 && Mathf.Floor(Time.time) % 2 == 0)
            {
                // randomize spawn location within horizontal range
                randX = Random.Range (-8f, 4f);

                // set spawn location
                whereToSpawn = new Vector2 (randX, transform.position.y);

                // spawn
                GameObject clone = Instantiate (enemy, whereToSpawn, Quaternion.identity);

                // assign layer
                clone.layer = LayerMask.NameToLayer("Birdman");

                // reduce respawn limit
                deadBirds--;
            }

            // increase spawn limit
            if (Time.time >= 140){
                spawnLimit = 12;
            }

            // increase spawn limit again
            if (Time.time >= 200){
                spawnLimit = 12;
            }

            // destroy enemies when player wins
            if (Time.time >= 260){
                GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Birdman");
                for (int i=0; i<enemies; i++){
                    Destroy(allEnemies[i]);
                }
            }
        }

        // return during intro
        else
        {
            return;
        }
    }

    // increase respawn limit
    public void deadBird(){
        deadBirds++;
	}

}
