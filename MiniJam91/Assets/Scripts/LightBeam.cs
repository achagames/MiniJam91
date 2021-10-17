using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightBeam : MonoBehaviour
{

    public float fishFlyingSpeed = 3.5f;

    bool hasCaughtFish = false;
    Transform parent;
    Transform fish;

    private void Start()
    {
        parent = GetComponentInParent<Transform>();
    }
    private void Update()
    {
        if (hasCaughtFish)
        {
            fish.position = Vector3.MoveTowards(fish.position, parent.position, fishFlyingSpeed * Time.deltaTime);
            fish.GetComponent<AIController>().StopSwimming();
        }

        
    }
    public bool GetHasCaughtFish()
    {
        return hasCaughtFish;
    }
    public FishEnum GetFish()
    {
        return fish.GetComponent<AIController>().type;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!hasCaughtFish && collision.gameObject.CompareTag("fish"))
        {
             hasCaughtFish = true;
            fish = collision.gameObject.GetComponent<Transform>();
            
            
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
