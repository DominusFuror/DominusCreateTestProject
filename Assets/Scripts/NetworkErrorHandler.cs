using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkErrorHandler : MonoBehaviour
{


    [SerializeField]
    GameObject ErrorWindow;
    [SerializeField]
    InputPlaceNameManager inputPlaceNameManager;
    [SerializeField]
    GoogleApiNetworkHandler googleApiNetworkHandler;
    public void LoadLocalData()
    {
        inputPlaceNameManager.ShowPhotoCircle();
        ErrorWindow.SetActive(false);

    }


    public void ShowErrorWindow()
    {
        ErrorWindow.SetActive(true);
    }


    private void Start()
    {
        googleApiNetworkHandler.internetError.AddListener(ShowErrorWindow);
    }



}
