using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{


    public GameObject imagePrefab;

    public Color color1;
    public Color color2;

    public int imageCount = 10;
    public float angularSpeed = 1f;
    public float circleRad = 1f;

    public Vector3 fixedPoint;
    public float currentAngle;

    public List<GameObject> imageBlocks = new List<GameObject>(10);

    void Update()
    {

        for (int i = 0; i < 10; i++)
        {

            imageBlocks[i].transform.position = UpdateImageBlocksPositions(i);

        }
        this.transform.position =  Vector3.right *-circleRad  * multi.x ;
    }

 
    private void Start()
    {
      
        GeneretPrefabs();
        fixedPoint = transform.position;
    }


    public void GeneretPrefabs()
    {
        for (int i = 0; i < imageCount; i++)
        {

            Vector3 newPrefabPosition = UpdateImageBlocksPositions(i);

            imageBlocks.Add(
                Instantiate(imagePrefab, newPrefabPosition, Quaternion.Euler(0, -90, 0)
                ));

            imageBlocks[i].transform.parent = this.transform;
            imageBlocks[i].name = "ImageBlock" +i;
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


    public Vector3 multi;
    private Vector3 UpdateImageBlocksPositions(int itemId) {

     

        float step =  Mathf.PI * 2 / imageCount;

        float y = Mathf.Abs( (Mathf.Sin((step/2  * itemId ) + currentAngle / 2)) ) * multi.y; 
        

        float x = Mathf.Cos(step * itemId + currentAngle) * circleRad * multi.x;

        float z = Mathf.Sin(step * itemId + currentAngle) *multi.z * circleRad;


        Vector3 position = new Vector3(x,  y,z ) +this.transform.position;

        return position;
    }
}
