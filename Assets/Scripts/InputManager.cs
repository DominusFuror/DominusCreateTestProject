using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    [SerializeField]
    float moveVelocity = 0;


    bool recordVelocity = false;
    float lastMousePositionX;
    [SerializeField]
    CirclePhotoBlockMovementManager CirclePhotoBlockMovementManager;

    [SerializeField]
    float sensetivy = 1;

    int frameSmoothCounter = 0;
    private void Update()
    {
        MouseInputCheck();
    
        CirclePhotoBlockMovementManager.currentAngle +=moveVelocity / 1000 * sensetivy;
    }

    void MouseInputCheck()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (recordVelocity)
            {
             

                float xChange = Input.mousePosition.x - lastMousePositionX;
                if (Mathf.Abs(xChange)>1)
                {

                    moveVelocity += (Input.mousePosition.x - lastMousePositionX) * Time.deltaTime ;


                }
                else
                {
                    frameSmoothCounter += 1;

                    if (frameSmoothCounter > 10)
                    {
                        frameSmoothCounter = 0;
                        moveVelocity = 0;
                    }
                }



            }
            if (!recordVelocity)
            {
                frameSmoothCounter = 0;
                recordVelocity = true;
                moveVelocity = 0;
            }




            lastMousePositionX = Input.mousePosition.x;
        }
        else
        {
            recordVelocity = false;
        }

        moveVelocity *= 1 - 1*Time.deltaTime;

        if ( Mathf.Abs( moveVelocity) < 0.001f )
        {
            moveVelocity = 0;
        }
    }

}
