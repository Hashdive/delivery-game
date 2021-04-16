using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    // item variables
    public GameObject dagger;
    public GameObject bat;
    public GameObject hammer;
    public GameObject gun;
    public GameObject crate;

    // spawn dagger
    public void SpawnDagger()
    {
        Instantiate(dagger, transform.position, Quaternion.identity);
    }

    // spawn bat
    public void SpawnBat()
    {
        Instantiate(bat, transform.position, Quaternion.identity);
    }

    // spawn hammer
    public void SpawnHammer()
    {
        Instantiate(hammer, transform.position, Quaternion.identity);
    }

    // spawn gun
    public void SpawnGun()
    {
        Instantiate(gun, transform.position, Quaternion.identity);
    }

    // spawn crate
    public void SpawnCrate()
    {
        Instantiate(crate, transform.position, Quaternion.identity);
    }

}
