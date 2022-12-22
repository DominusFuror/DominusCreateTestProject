using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    [SerializeField]
    float moveVelocity = 0;



    float lastMousePositionX;
    [SerializeField]
    CirclePhotoBlockMovementManager CirclePhotoBlockMovementManager;

    [SerializeField]
    float sensetivy = 1;

    int frameSmoothCounter = 0;


    Coroutine moveFinisher;



    bool moveByButton = false;
    bool recordVelocity = false;


    private void Update()
    {
        MouseInputCheck();
        CircleMovement();
       
    }



    /// <summary>
    /// Mouse movement fucntion
    /// </summary>
    void CircleMovement()
    {


        if (Mathf.Abs(moveVelocity) < 1f && !Input.GetKey(KeyCode.Mouse0))
        {

            if (moveVelocity != 0 || Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (moveFinisher == null && !moveByButton )
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


    // All mouse detection
    void MouseInputCheck()
    {

        // Check click and remove bottom side of screen for mouse movement
        if (Input.GetKey(KeyCode.Mouse0) && Input.mousePosition.y > Screen.width/8)
        {

            if (recordVelocity)
            {
                StopAllCoroutines();
                moveByButton = false;
                
                if (moveFinisher != null)
                {

                  
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

        //Friction
        moveVelocity *= 1 - 1*Time.deltaTime;

      
    }



    public void OnLeftButtonClick()
    {

        OnButtonClick(-1);
    }

    public void OnRightButtonClick()
    {
        OnButtonClick(1);

    }

    // Direction 1 = Right -1 = Left
    public void OnButtonClick(int direction)
    {

        clicks++;
   


        // Stop slider Movement Finisher when start movement by buttons
        if (moveFinisher != null)
        {
            StopCoroutine(moveFinisher);
        }


        // Chech will we continue button movement, or only start new one
        if (moveByButton)
        {
            moveIteration -= moveIteration / 2;
            nextPosition += CirclePhotoBlockMovementManager.step * -direction;
       
        }
        else
        {
            moveIteration = 0;
            moveByButton = true;
            
            nextPosition = CirclePhotoBlockMovementManager.currentAngle / CirclePhotoBlockMovementManager.step;

            nextPosition = Mathf.Round(nextPosition);

            nextPosition = nextPosition * CirclePhotoBlockMovementManager.step;


            nextPosition += CirclePhotoBlockMovementManager.step * -direction;
            StartCoroutine(MoveByButton());

        }
      

    }

    /// <summary>
    ///  Movement Finisher
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveFinisher()
    {
        moveIteration = 0;
        float neasrestPosition=  CirclePhotoBlockMovementManager.currentAngle / CirclePhotoBlockMovementManager.step;

        neasrestPosition = Mathf.Round(neasrestPosition);

        neasrestPosition = neasrestPosition * CirclePhotoBlockMovementManager.step;


        while (Mathf.Abs(neasrestPosition-CirclePhotoBlockMovementManager.currentAngle)>0.001f)
        {
            moveIteration += Time.deltaTime / 10;
            CirclePhotoBlockMovementManager.currentAngle = Mathf.Lerp(CirclePhotoBlockMovementManager.currentAngle,neasrestPosition, moveIteration);
            yield return new WaitForEndOfFrame();

        }

        CirclePhotoBlockMovementManager.currentAngle = neasrestPosition;
        yield break;
    }
    float nextPosition = 0;



    //Clicks and moveIteration vars for more smooth movement for multiplay clicks
    float moveIteration = 0; 
    int clicks = 0;


    // Fucntion for movement by buttons click
    IEnumerator MoveByButton()
    {
      


            

        while (Mathf.Abs(nextPosition - CirclePhotoBlockMovementManager.currentAngle) > 0.005f)
        {
            moveIteration += Time.deltaTime/20 *clicks; 
            CirclePhotoBlockMovementManager.currentAngle = Mathf.Lerp(CirclePhotoBlockMovementManager.currentAngle, nextPosition, moveIteration/clicks);
            yield return new WaitForEndOfFrame();
        }
        CirclePhotoBlockMovementManager.currentAngle = nextPosition;
        moveByButton = false;
        moveIteration = 0;
        clicks = 0;
        yield break;
    }
    
}
