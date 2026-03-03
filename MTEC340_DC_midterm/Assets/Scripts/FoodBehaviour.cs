using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodBehaviour : MonoBehaviour
{
    public BoxCollider2D gridArea;


    private void Start()
    {
        RandomisePosition(); //when game starts the food will begin randomising position 
    }

    private void RandomisePosition()
    {
        Bounds bounds = this.gridArea.bounds; //only operate within the gridArea
        float x = Random.Range(bounds.min.x, bounds.max.x); //generate random point within x axis inside of gridArea
        float y = Random.Range(bounds.min.y, bounds.max.y); //generate random point within y axis inside of gridArea
        this.transform.position = new Vector3 (Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Snake")
        {
            RandomisePosition();
        }
        //if the object tagged "snake" collides with the food, then it will randomise its position again
    }
}
