using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private float playerScaleX;
    private float playerScaleY;
    PlayerMovement playerMovement;
    Vector2 SpawnPosition;

    [SerializeField]
    private int rows;

    [SerializeField]
    private int cols;


    [SerializeField]
    private Sprite cellSprite;
    private Vector2 cellSize;
    private Vector2 cellScale;

    private Vector2 gridSize;
    private Vector2 gridOffset;

    private float WorldWidth;
    private float WorldHeight;

    void Start()
    {
        int w = Screen.width;
        int h = Screen.height;
        float a = (float)w / (float)h;
        gridSize = new Vector2(a * 10, 10);         //to make the grid fullscreen on different screen resolutions

        playerScaleX = Mathf.Abs(player.transform.localScale.x);
        playerScaleY = Mathf.Abs(player.transform.localScale.y);

        WorldWidth = Camera.main.ScreenToWorldPoint(new Vector3(w, h)).x;
        WorldHeight = Camera.main.ScreenToWorldPoint(new Vector3(w, h)).y;

        playerMovement = player.GetComponent<PlayerMovement>();

        generateScene();
    }

    void generateScene()
    {
        GameObject cellObject = new GameObject();
        cellObject.AddComponent<SpriteRenderer>().sprite = cellSprite;

        cellSize = cellSprite.bounds.size;

        Vector2 newcellSize = new Vector2(gridSize.x / (float)cols, gridSize.y / (float)rows);

        cellScale.x = newcellSize.x / cellSize.x;
        cellScale.y = newcellSize.y / cellSize.y;

        cellSize = newcellSize;

        cellObject.transform.localScale = new Vector2(cellScale.x, cellScale.y);

        gridOffset.x = -(gridSize.x / 2) + cellSize.x / 2;
        gridOffset.y = -(gridSize.y / 2) + cellSize.y / 2;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 pos = new Vector2(col * cellSize.x + gridOffset.x, row * cellSize.y + gridOffset.y);
                GameObject go = Instantiate(cellObject, pos, Quaternion.identity) as GameObject;
                go.transform.parent = transform;
            }
        }
        Destroy(cellObject);

        SpawnPosition = new Vector2((cols % 2) * (cellSize.x / 2), (rows % 2) * (cellSize.y / 2));
        player.transform.position = SpawnPosition;
    }

    void Update()
    {
        //looping the player across the screen
        if(player.transform.position.x < - WorldWidth - playerScaleX/2)
        {
            player.transform.position = new Vector2(player.transform.position.x + WorldWidth * 2, player.transform.position.y);
        }
        if(player.transform.position.x > WorldWidth + playerScaleX/2)
        {
            player.transform.position = new Vector2(player.transform.position.x - WorldWidth * 2, player.transform.position.y);
        }
        if(player.transform.position.y > WorldHeight + playerScaleY/2)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - WorldHeight * 2);
        }
        if(player.transform.position.y < - WorldHeight - playerScaleY/2)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + WorldHeight * 2);
        }


        //make player able to turn only at the crossing lines
        if (Math.Round((player.transform.localPosition.x - SpawnPosition.x) % cellSize.x, 1) == 0 && Math.Round((player.transform.localPosition.y - SpawnPosition.y) % cellSize.y, 1) == 0)
        {
            playerMovement.moveDir = playerMovement.swipeDir;
        }
    }
}
