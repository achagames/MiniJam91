using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : MonoBehaviour
{
    public Transform beamPrefab;
    public Transform beamPos;
    public float beamSpeed = 10f;
    public float beamReturnSpeed = 10f;

    bool isAscending;
    bool beamIsSpawned;
    Transform lightBeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SpawnLightBeam();

        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            AscendLightBeam();


        }
        if (lightBeam != null)
        {
            if (beamIsSpawned && GetComponentInChildren<LightBeam>().GetCaughtFish())
            {
                isAscending = true;
            }
        
        
        if (!isAscending)
        {
            DescendLightBeam();
        }
        else if (isAscending && lightBeam.localScale.y > 0.2f)
        {
            AscendLightBeam();
        }

        if ( isAscending && lightBeam.localScale.y <= 0.2f)
        {
            DestroyLightBeam();
        }
        }
    }

    private void DestroyLightBeam()
    {
        lightBeam.GetComponent<LightBeam>().DespawnBeam();
        beamIsSpawned = false;
        isAscending = false;
    }

    public void AscendLightBeam()
    {
        if (beamIsSpawned)
        {
            isAscending = true;
            lightBeam.localScale = new Vector3(1, lightBeam.localScale.y - Time.deltaTime * beamReturnSpeed, 1);
        }
        
    }

    public void DescendLightBeam()
    {
        if (beamIsSpawned)
        {
            isAscending = false;
            lightBeam.localScale = new Vector3(1, lightBeam.localScale.y + Time.deltaTime * beamSpeed, 1);
        }
        
    }
    public void SpawnLightBeam()
    {
        if (!beamIsSpawned)
        {
            lightBeam = Instantiate(beamPrefab, beamPos.position, Quaternion.identity);
            lightBeam.parent = beamPos;
            beamIsSpawned = true;
            isAscending = false;
        }
    }

}
