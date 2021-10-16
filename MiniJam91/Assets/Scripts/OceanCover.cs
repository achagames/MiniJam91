using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanCover : MonoBehaviour
{
    public Transform cover;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        cover.position = new Vector3(cover.position.x, cover.position.y, -2f);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("player enter");
        if (other.gameObject.CompareTag("Player"))
        {
            
            cover.position = new Vector3(cover.position.x, cover.position.y, 2f);

        }
    }
}
