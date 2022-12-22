using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Events;
public class GoogleApiNetworkHandler : MonoBehaviour
{


    public UnityEvent completeWebReq;
    class NetworkSettings
    {
        public string placeURL = "https://maps.googleapis.com/maps/api/place/textsearch/json?";
        public string photoURL = "https://maps.googleapis.com/maps/api/place/photo?";
        public string APIToken = "AIzaSyAXb6kOVtVHa1msABSv5AreZ6EOPH6k6dQ";

    }


    NetworkSettings networkSettings;
    void Start()
    {
        networkSettings = new NetworkSettings();


       
       
    }


    public void StartFindingPlace(string place)
    {
        StartCoroutine(GetGooglePlaceJson(place));
    }
    // Start is called before the first frame update
   


    GoogleApiPlaceJSON googleApiPlaceJSON;
    IEnumerator GetGooglePlaceJson(string place)
    {
        UnityWebRequest req = UnityWebRequest.Get(networkSettings.placeURL +"" +
            "query=" + place+
            "&key=" + networkSettings.APIToken);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(req.error);
        }
        else
        {
            try
            {
               
                googleApiPlaceJSON = JsonConvert.DeserializeObject<GoogleApiPlaceJSON>(req.downloadHandler.text);
            }
            catch (System.Exception)
            {
                        
                throw new System.Exception("Can't FromJson google Place JSON");
            }
        
        }
        StartCoroutine(GetGooglePhoto(700));
        yield break;

    }
    IEnumerator GetGooglePhoto( int maxwidth)
    {


            string url = networkSettings.photoURL +
            "maxwidth=" + maxwidth +
            "&photo_reference=" + googleApiPlaceJSON.results[0].photos[0].photo_reference +
            "&key=" + networkSettings.APIToken;
            UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);


        yield return req.SendWebRequest();
        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(req.error);
        }
        else
        {
            try
            {
                Texture2D texture = ((DownloadHandlerTexture)req.downloadHandler).texture;
                var bytes = texture.EncodeToPNG();
                File.WriteAllBytes("photo.png", bytes);
                
            }
            catch (System.Exception)
            {

                throw new System.Exception("Can't donwload google Photo");
            }

        }

        completeWebReq.Invoke();
        yield break;


    }

}


#region JSON Class
public class GoogleApiPlaceJSON
{
    public object[] html_attributions;
    public Result[] results;
    public string status;
}

public class Result
{
    public string formatted_address ;
    public Geometry geometry ;
    public string icon ;
    public string icon_background_color ;
    public string icon_mask_base_uri ;
    public string name ;
    public Photo[] photos ;
    public string place_id ;
    public string reference ;
    public string[] types ;
}

public class Geometry
{
    public Location location ;
    public Viewport viewport ;
}

public class Location
{
    public float lat ;
    public float lng ;
}

public class Viewport
{
    public Northeast northeast ;
    public Southwest southwest ;
}

public class Northeast
{
    public float lat ;
    public float lng ;
}

public class Southwest
{
    public float lat ;
    public float lng ;
}

public class Photo
{
    public int height ;
    public string[] html_attributions ;
    public string photo_reference ;
    public int width ;
}

#endregion