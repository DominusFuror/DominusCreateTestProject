using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBlockClick : MonoBehaviour
{


    Vector3 mouseInputPosition;
    private void OnMouseDown()
    {


        mouseInputPosition = Input.mousePosition;
          

      
       


    }
    private void OnMouseUp()
    {

        if(mouseInputPosition== Input.mousePosition)
        {
            InfoBlockAnimationManager.infoBlockAnimationManager.ChangeState();
        }
       
    }



}
