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


    Coroutine moveFinisher;
    private void Update()
    {
        MouseInputCheck();
        CircleMovement();
       
    }


    void CircleMovement()
    {


        if (Mathf.Abs(moveVelocity) < 0.1f && !Input.GetKey(KeyCode.Mouse0))
        {

            if (moveVelocity != 0)
            {
                if (moveFinisher == null)
                {
                    moveFinisher = StartCoroutine(MoveFinisher());
                }
            }
            moveVelocity = 0;

        }
        else
        {

            CirclePhotoBlockMovementManager.currentAngle += moveVelocity / 1000 * sensetivy;
        }
    }
    void MouseInputCheck()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {

            if (recordVelocity)
            {
                if (moveFinisher != null)
                {

                    StopCoroutine(moveFinisher);
                    moveFinisher = null;
                }


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

      
    }


    IEnumerator MoveFinisher()
    {
        
        float neasrestPosition=  CirclePhotoBlockMovementManager.currentAngle / CirclePhotoBlockMovementManager.step;

        neasrestPosition = Mathf.Round(neasrestPosition);

        neasrestPosition = neasrestPosition * CirclePhotoBlockMovementManager.step;
        print(neasrestPosition);

        while (Mathf.Abs(neasrestPosition-CirclePhotoBlockMovementManager.currentAngle)>0.001f)
        {

            CirclePhotoBlockMovementManager.currentAngle = Mathf.Lerp(CirclePhotoBlockMovementManager.currentAngle,neasrestPosition,Time.deltaTime);
            yield return new WaitForEndOfFrame();

        }


        yield break;
    }
}
