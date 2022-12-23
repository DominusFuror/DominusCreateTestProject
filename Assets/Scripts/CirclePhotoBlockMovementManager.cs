using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePhotoBlockMovementManager : MonoBehaviour
{


    #region Variables 
    [SerializeField]
    GameObject imagePrefab;
    [SerializeField]
    Color color1;
    [SerializeField]
    Color color2;
   
   
    [SerializeField]
    float circleRad = 1f;
    [SerializeField]
    Vector3 circleAxesMultiplication;
    [SerializeField]
    List<GameObject> imageBlocks = new List<GameObject>(10);
    [SerializeField]
    float imageBlockAngle = 0;
    #endregion

    //Public float for angle change and image count
    public float currentAngle=0;
    public int imageCount = 10;
    public float step;
    bool isRotate = true;
    void Update()
    {
        BlocksMoveUpdate();
        
        
    }

    public void SettingsUpdate(bool isCountChange, Setts settingsMenu)
    {

        circleAxesMultiplication = new Vector3(
            settingsMenu.circleAxeX,
            settingsMenu.circleAxeY,
            settingsMenu.circleAxeZ
            );
        circleRad = settingsMenu.circleRadius;
        isRotate = settingsMenu.isRotate;
        imageCount = settingsMenu.imageCount;
        if (isCountChange)
        {
            Generation();
        }





    }


    private void OnEnable()
    {
        Generation();
      
    }

    private void Generation()
    {
        step = Mathf.PI * 2 / imageCount;
        currentAngle = 0;

        for (int i = 0; i < imageBlocks.Count; i++)
        {
            Destroy(imageBlocks[i]);
   
        }
        imageBlocks.Clear();
        GeneretImageBlocks();

    }



    /// <summary>
    /// Initial creation of blocks on the stage
    /// </summary>
    public void GeneretImageBlocks()
    {
        for (int i = 0; i < imageCount; i++)
        {

            Vector3 newPrefabPosition = UpdateImageBlocksPositions(i);
            Quaternion newPrefabRot = UpdateImageBlocksRotation(newPrefabPosition);
            imageBlocks.Add(
                 Instantiate(imagePrefab, newPrefabPosition, newPrefabRot
                ));

            imageBlocks[i].transform.parent = this.transform;
            imageBlocks[i].name = "ImageBlock" + i;
            if (i % 2 == 0)
            {
                imageBlocks[i].GetComponentInChildren<UnityEngine.UI.Image>().color = color1;
                if (i == 0)
                {
                    imageBlocks[i].GetComponentInChildren<UnityEngine.UI.Image>().color = Color.white;
                    imageBlocks[i].GetComponentInChildren<UnityEngine.UI.Image>().sprite = ImageLoaderManager.imageUploader.UploadImage();
                }
            }
            else
            {
                imageBlocks[i].GetComponentInChildren<UnityEngine.UI.Image>().color = color2;
            }

           
        }
    }



    /// <summary>
    /// Update the position of blocks depending on their ID
    /// </summary>
    private Vector3 UpdateImageBlocksPositions(int itemId) 
    {

       
        this.transform.position = Vector3.right * -circleRad * circleAxesMultiplication.x;
        // The step is calculated depending on the number of blocks for the same distance between them



        //Calculation of each coordinate for circular motion
        float y = Mathf.Abs((Mathf.Sin((step / 2 * itemId) + currentAngle / 2))) * circleAxesMultiplication.y;
        float x = Mathf.Cos(step * itemId + currentAngle) * circleRad * circleAxesMultiplication.x;
        float z = Mathf.Sin(step * itemId + currentAngle) * circleAxesMultiplication.z * circleRad;


        Vector3 position = new Vector3(x, y, z) + this.transform.position;

        return position;
    }
    private Quaternion UpdateImageBlocksRotation(Vector3 itemPosition)
    {


        if (isRotate)
        {
            float y = 90 - Mathf.Lerp(0, 90, Mathf.Abs(itemPosition.x - circleRad * circleAxesMultiplication.x) / circleRad * circleAxesMultiplication.x);


            if (itemPosition.z > 0 && !(itemPosition.x <= (-circleRad * circleAxesMultiplication.x) * 0.99f))
            {
                y *= -1;
                y += 180;

            }

            if (itemPosition.x <= (-circleRad * circleAxesMultiplication.x) * 0.99f)
            {

                y += 3;
            }

            Quaternion rotate = Quaternion.Euler(0, y, 0);

            return rotate;

        }

        else
        {

            Quaternion rotate = Quaternion.Euler(0, 90, 0);

            return rotate;
        }
        
      
    }
    private void BlocksMoveUpdate()
    {
        for (int i = 0; i < imageCount; i++)
        {

            imageBlocks[i].transform.position = UpdateImageBlocksPositions(i);
            imageBlocks[i].transform.rotation = UpdateImageBlocksRotation(imageBlocks[i].transform.localPosition);
        }
    }



}
