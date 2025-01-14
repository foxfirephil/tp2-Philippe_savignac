using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrok : MonoBehaviour, IZombie
{
    //public instance
    public int lifebar;
    public GameObject warroksmallprefab;
    //child instance
    Slider lifebarSld;
    Rigidbody[] rbs;
    Animator animator;
    AudioSource deathsound;
    Collider bodyCol;

    // Start is called before the first frame update
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        deathsound = GetComponent<AudioSource>();
        bodyCol = GetComponent<Collider>();
        lifebarSld = GetComponentInChildren<Slider>();

        //d�sactiver le ragdoll quand il spawn
        ToggRagdoll(false);
    }

    // Update is called once per frame
    void Update()
    {
        //changer la valeur de la lifebar
        lifebarSld.value = lifebar / 100;
    }
    public void TakeDamage()
    {
        lifebar -= 1;
        if (lifebar <= 0)
        {
            Die();
            //patcher mon bug quand y meurt
            lifebar = 10000;
        }
    }

    void Die()
    {
        ToggRagdoll(true);
        bodyCol.enabled = false;
        //ajout de l'argent
        Coins.nbCoins += 30;
        deathsound.Play();
        //quand warrok mort spawn 2 petit
        Instantiate(warroksmallprefab, transform.position, Quaternion.identity);
        Instantiate(warroksmallprefab, transform.position, Quaternion.identity);
        //d�truit apres 1 seconde
        Destroy(gameObject, 1f);
    }

    void ToggRagdoll(bool value)
    {
        foreach (var r in rbs)
        {
            r.isKinematic = !value;
        }

        animator.enabled = !value;
    }
}
