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
    int imageCount = 10;
    [SerializeField]
    float circleRad = 1f;
    [SerializeField]
    Vector3 circleAxesMultiplication;
    [SerializeField]
    List<GameObject> imageBlocks = new List<GameObject>(10);
    #endregion

    //Public float for angle change
    public float currentAngle;

    void Update()
    {
        BlocksMoveUpdate();
    }


    private void Start()
    {

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

            imageBlocks.Add(
                Instantiate(imagePrefab, newPrefabPosition, Quaternion.Euler(0, -90, 0)
                ));

            imageBlocks[i].transform.parent = this.transform;
            imageBlocks[i].name = "ImageBlock" + i;
            if (i % 2 == 0)
            {
                imageBlocks[i].GetComponent<SpriteRenderer>().color = color1;
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
        float step = Mathf.PI * 2 / imageCount;


        //Calculation of each coordinate for circular motion
        float y = Mathf.Abs((Mathf.Sin((step / 2 * itemId) + currentAngle / 2))) * circleAxesMultiplication.y;
        float x = Mathf.Cos(step * itemId + currentAngle) * circleRad * circleAxesMultiplication.x;
        float z = Mathf.Sin(step * itemId + currentAngle) * circleAxesMultiplication.z * circleRad;


        Vector3 position = new Vector3(x, y, z) + this.transform.position;

        return position;
    }
    
    private void BlocksMoveUpdate()
    {
        for (int i = 0; i < imageCount; i++)
        {

            imageBlocks[i].transform.position = UpdateImageBlocksPositions(i);

        }
    }



}
