using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBlockAnimationManager : MonoBehaviour
{
    // Start is called before the first frame update


    public static InfoBlockAnimationManager infoBlockAnimationManager;


    InfoBlockAnimationManager()
    {
        if (infoBlockAnimationManager == null)
        {

            infoBlockAnimationManager = this;
        }

    }

    [SerializeField]
    Animator infoBlockAnimator;
    public void ChangeState()
    {

        infoBlockAnimator.SetBool("IsHide", !infoBlockAnimator.GetBool("IsHide"));
    }

    public void HideBlock()
    {

        infoBlockAnimator.SetBool("IsHide", true);
    }
    // Update is called once per frame
    private void Start()
    {

     


    }
}
