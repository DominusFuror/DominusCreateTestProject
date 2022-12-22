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
    public float currentAngle;
    public int imageCount = 10;
    public float step;
    void Update()
    {
        BlocksMoveUpdate();
        
        
    }


    private void Start()
    {
        step = Mathf.PI * 2 / imageCount;
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
                imageBlocks[i].GetComponent<SpriteRenderer>().color = color1;
                if (i == 0)
                {

                    imageBlocks[i].GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
            else
            {
                imageBlocks[i].GetComponent<SpriteRenderer>().color = color2;
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


       
        
        float y = 90 - Mathf.Lerp(0, 90, Mathf.Abs( itemPosition.x- circleRad * circleAxesMultiplication.x) / circleRad*circleAxesMultiplication.x);


        if (itemPosition.z > 0)
        {
            y *= -1;

        }

        if(itemPosition.x <= (-circleRad * circleAxesMultiplication.x)*0.99f)
        {

            y += 3;
        }
        
        Quaternion rotate = Quaternion.Euler(0, y, 0);

        return rotate;
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
