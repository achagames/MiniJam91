using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    bool caughtFish = false;
    Transform parent;
    Transform fish;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
    }
    private void Update()
    {
        if (caughtFish)
        {
            fish.position = Vector3.MoveTowards(fish.position, parent.position, 3.5f * Time.deltaTime);
            print(transform.localScale.y);
            
        }

        
    }
    public bool GetCaughtFish()
    {
        return caughtFish;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("fish"))
        {
             caughtFish = true;
            fish = collision.gameObject.GetComponent<Transform>();
            print("Touched" + collision.gameObject.tag);
            
        }
    }

    public void DespawnBeam()
    {
            Destroy(parent.gameObject);
            Destroy(fish.gameObject);
    }
}
