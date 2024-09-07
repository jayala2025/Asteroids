using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 moveAdjustment = Vector3.zero;

        if (viewportPosition.x < 0)
        {
            moveAdjustment.x += 1;
        }
        if (viewportPosition.x > 1)
        {
            moveAdjustment.x -= 1;
        }
        if (viewportPosition.y < 0)
        {
            moveAdjustment.y += 1;
        }
        if (viewportPosition.y > 1)
        {
            moveAdjustment.y -= 1;
        }
        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjustment);
    }
}
