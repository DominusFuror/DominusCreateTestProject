using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPlaceNameManager : MonoBehaviour
{

    public GameObject inputBlock;
    public TMP_InputField inputField;
    public GameObject circle;
    public GameObject circleUI;
    public GoogleApiNetworkHandler googleApiNetworkHandler;


    private void Start()
    {
        googleApiNetworkHandler.completeWebReq.AddListener(ShowPhotoCircle);
    }

    public void OnButtonClick()
    {

        googleApiNetworkHandler.StartFindingPlace(inputField.text);
        googleApiNetworkHandler.completeWebReq.AddListener(ShowPhotoCircle);
    }


    public void ShowPhotoCircle()
    {

        circle.SetActive(true);
        circleUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
