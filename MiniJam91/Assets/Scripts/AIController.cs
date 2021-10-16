using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public float swimmingSpeed = 10f;
    public Transform[] swimPoints;

    bool facingRight = false;
    Transform nextPos;
    

    // Start is called before the first frame update
    void Start()
    {
        nextPos = swimPoints[Random.Range(0, swimPoints.Length)];
    }

    // Update is called once per frame
    void Update()
    {

        Swim(nextPos);
        if (nextPos == swimPoints[0] && facingRight)
        {
            Flip();
            facingRight = false;
        }
        else if (nextPos == swimPoints[1] && !facingRight)
        {
            Flip();
            facingRight = true;
        }

        if (transform.position == nextPos.position)
        {
            nextPos = swimPoints[Random.Range(0, swimPoints.Length)];
        }        
            //transform.Translate(Vector2.right * swimmingSpeed * Time.deltaTime, Space.World);
    }
  
    void Swim(Transform target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, swimmingSpeed * Time.deltaTime);
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
      
    }
    public void StopSwimming()
    {
        swimmingSpeed = 0f;
    }
}
