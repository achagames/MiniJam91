using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamController : MonoBehaviour
{
    public Transform beamPrefab;
    public Transform beamPos;
    public float beamSpeed = 10f;
    public float beamReturnSpeed = 10f;

    bool beamIsSpawned;
    Transform lightBeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!beamIsSpawned)
            {
                lightBeam = Instantiate(beamPrefab, beamPos.position, Quaternion.identity);
                lightBeam.parent = beamPos;
                beamIsSpawned = true;
            }
        }

        if (beamIsSpawned && !GetComponentInChildren<LightBeam>().GetCaughtFish())
        {
            lightBeam.localScale = new Vector3(1,lightBeam.localScale.y + Time.deltaTime * beamSpeed,1);
        }
        else if(beamIsSpawned && GetComponentInChildren<LightBeam>().GetCaughtFish() && lightBeam.localScale.y > 0.2f)
        {
            lightBeam.localScale = new Vector3(1, lightBeam.localScale.y - Time.deltaTime * beamReturnSpeed, 1);
        }

        if (beamIsSpawned && GetComponentInChildren<LightBeam>().GetCaughtFish() && lightBeam.localScale.y <= 0.2f)
        {
            lightBeam.GetComponent<LightBeam>().DespawnBeam();
            beamIsSpawned = false;
        }
    }

    
}
