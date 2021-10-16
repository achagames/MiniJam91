using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    public float fishFlyingSpeed = 3.5f;

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
            fish.position = Vector3.MoveTowards(fish.position, parent.position, fishFlyingSpeed * Time.deltaTime);
            fish.GetComponent<AIController>().StopSwimming();
        }

        
    }
    public bool GetCaughtFish()
    {
        return caughtFish;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!caughtFish && collision.gameObject.CompareTag("fish"))
        {
             caughtFish = true;
            fish = collision.gameObject.GetComponent<Transform>();
            print("Touched" + collision.gameObject.tag);
            
        }

        //if (!caughtFish && collision.gameObject.CompareTag("surface"))
        //{
        //    Transform cover = collision.gameObject.transform;
        //    cover.position = new Vector3(cover.position.x,cover.position.y,1);
        //}
    }

    public void DespawnBeam()
    {
            Destroy(parent.gameObject);
        if (fish != null)
        {
            Destroy(fish.gameObject);
        }
           
    }
}
