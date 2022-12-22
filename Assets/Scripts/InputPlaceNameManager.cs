using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPlaceNameManager : MonoBehaviour
{

    public GameObject inputBlock;
    public TMP_InputField inputField;

    public GoogleApiNetworkHandler googleApiNetworkHandler;


    public void OnButtonClick()
    {

        googleApiNetworkHandler.StartFindingPlace(inputField.text);

    }
}
