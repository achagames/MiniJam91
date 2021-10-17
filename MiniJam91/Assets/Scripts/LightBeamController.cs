using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class LightBeamController : MonoBehaviour
{
    public Text[] fishObjectives;
    public Transform beamPrefab;
    public Transform beamPos;
    public float beamSpeed = 10f;
    public float beamReturnSpeed = 10f;
    public float lightsSpeed = 2.3f;
    public float lightsReturnSpeed = 6f;
    public float beamMaxDepth = 20f;
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
            if (beamIsSpawned && GetComponentInChildren<LightBeam>().GetHasCaughtFish())
            {
                isAscending = true;

                switch (GetComponentInChildren<LightBeam>().GetFish())
                {
                    case FishEnum.SWORDFISH:
                        fishObjectives[0].color = Color.yellow;
                        break;
                    case FishEnum.INCOGNITO_FISH:
                        fishObjectives[1].color = Color.yellow;
                        break;
                    case FishEnum.GLOWING_FISH:
                        fishObjectives[2].color = Color.yellow;
                        break;
                    case FishEnum.CLOWNFISH:
                        fishObjectives[3].color = Color.yellow;
                        break;
                    case FishEnum.GOLDFISH:
                        fishObjectives[4].color = Color.yellow;
                        break;
                    case FishEnum.JELLYFISH:
                        fishObjectives[5].color = Color.yellow;
                        break;
                    default:
                        break;
                }
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
                lighting.pointLightOuterRadius -= Time.deltaTime * lightsReturnSpeed;
            }
            
        }
    }

    public void DescendLightBeam()
    {
        if (beamIsSpawned && lightBeam.localScale.y < beamMaxDepth)
        {
            isAscending = false;
            lightBeam.localScale = new Vector3(1, lightBeam.localScale.y + Time.deltaTime * beamSpeed, 1);
            cam.position -= Vector3.up * Time.deltaTime * beamSpeed;
            lighting.pointLightOuterRadius += Time.deltaTime * lightsSpeed;
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
