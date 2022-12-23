using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBlockClick : MonoBehaviour
{


    Vector3 mouseInputPosition;
    private bool isSettingsOn;

    private void OnMouseDown()
    {


        mouseInputPosition = Input.mousePosition;
          

      
       


    }
    private void OnMouseUp()
    {
        if (SettingsMenu.isSettingsBlockHidden)
        {
            if (mouseInputPosition == Input.mousePosition)
            {
                InfoBlockAnimationManager.infoBlockAnimationManager.ChangeState();
            }
          
        }

      
       
    }



}
