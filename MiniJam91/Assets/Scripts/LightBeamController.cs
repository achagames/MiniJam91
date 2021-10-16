using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightBeamController : MonoBehaviour
{
    public Transform beamPrefab;
    public Transform beamPos;
    public float beamSpeed = 10f;
    public float beamReturnSpeed = 10f;
    public Transform cam;
    public Light2D lighting;

    Vector3 camOriginalPos;
    float originalLightRadius;
    bool isAscending;
    bool beamIsSpawned;
    Transform lightBeam;

    // Start is called before the first frame update
    void Start()
    {
        camOriginalPos = cam.transform.position;
        originalLightRadius = lighting.pointLightOuterRadius;
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

            if (cam.position.y != camOriginalPos.y)
            {
                cam.position = Vector3.MoveTowards(cam.position, new Vector3(cam.position.x, camOriginalPos.y, cam.position.z), Time.deltaTime * (beamReturnSpeed + 2));
                //cam.transform.position += Vector3.up * Time.deltaTime * (beamReturnSpeed);
            }
            if (lighting.pointLightOuterRadius > originalLightRadius)
            {
                lighting.pointLightOuterRadius -= Time.deltaTime * 4f;
            }
            
        }
    }

    public void DescendLightBeam()
    {
        if (beamIsSpawned)
        {
            isAscending = false;
            lightBeam.localScale = new Vector3(1, lightBeam.localScale.y + Time.deltaTime * beamSpeed, 1);
            cam.position -= Vector3.up * Time.deltaTime * beamSpeed;
            lighting.pointLightOuterRadius += Time.deltaTime * 1.5f;
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
