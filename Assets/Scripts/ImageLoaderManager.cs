using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ImageLoaderManager : MonoBehaviour
{

    public static ImageLoaderManager imageUploader;


    public ImageLoaderManager()
    {

        if (imageUploader == null)
        {


            imageUploader = this;
        }
    }



    Texture2D Phototexture;
    public Sprite UploadImage()
    {
        var bytes = File.ReadAllBytes("photo.png");
        Phototexture = new Texture2D(500,500);
;
        ImageConversion.LoadImage(Phototexture, bytes);




        return Sprite.Create(Phototexture, new Rect(0, 0, Phototexture.width, Phototexture.height), Vector2.zero);
    }
}
