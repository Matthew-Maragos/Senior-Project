using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;


public class MemoryGameSetup : MonoBehaviour
{
    public static MemoryGameSetup Instance;
    
    public Sprite[] numberSprites;
    public List<Sprite> imageSprites;
    [SerializeField] private Vector2 _initialPositions;
    private int colLength = 5;

    //X and Y spacing for the boxes
    [SerializeField] private Vector2 _spacing;

    //X: MinWidth, Y: MinHeight, Z: MaxWidth, W: MaxHeight
    [SerializeField] private Vector4 _minAndMaxWidthsAndHeights;

    //Variables for expanding and contraction of the image
    [SerializeField] private float expansionMultiplier = 1.5f;
    private float expansionDuration = 0.5f;
    private float contractionDuration = 0.5f;

    private int numBoxes = 20;

    // Going to keep track of the boxes with this list
    private List<GameObject> spawnedBoxes = new List<GameObject>();

    private int index = 0;

    // This will control how fast players can scroll with the controller
    private float moveCooldown = 0.2f;

    private float nextMoveTime = 0f;

    // These will monitor the player logic as well as player points
    public int numberOfPlayers = 2;
    private int currentPlayer = 0;
    private int[] playerScores;
    public int playerIndex;
    private PlayerInput playerInput;
    private GameManager instance;
    public TMP_Text turnText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        SpawnBoxes();
        // Highlighting the first box at the start
        HighlightBoxes(index);
        // How players scores are going to be monitored
        numberOfPlayers = MainGameManager.Instance.GetPlayerCount();
        playerScores = new int[numberOfPlayers];
        UpdateUI();
        MainGameManager.Instance.minigameType = MainGameManager.MinigameType.MemoryGame;


    }

    //Update is called once per frame
    void Update()
    {
        int activePlayer = GameManager.Instance.GetCurrentPlayer();
        // Controller input
        // Getting player specific input
        float moveX = Input.GetAxisRaw("Horizontal_P" + (activePlayer + 1));
        float moveY = Input.GetAxisRaw("Vertical_P" + (activePlayer + 1));
        if (Time.time >= nextMoveTime)
        {
            if (moveX > 0.5f)
            {
                // Moving Right
                moveRight();
            }
    
            if (moveX < -0.5f)
            {
                // Moving Left
                moveLeft();
            }
    
            if (moveY > 0.5f)
            {
                // Moving Up
                moveUp();
            }
    
            if (moveY < -0.5f)
            {
                // Moving Down
                moveDown();
            }
        }
    
        if (Input.GetButtonDown("Submit_P" + (activePlayer + 1)))
        {
            if (spawnedBoxes[index] != null)
            {
                spawnedBoxes[index].GetComponent<BoxHandler>().OnMouseDown();
                
            }
        }
    }

    public void SpawnBoxes()
    {
        //Get boxes from the pool instead of instantiating them
        for (int i = 0; i < numBoxes; i++)
        {
            GameObject newBox = ObjectPooler.Instance.GetPooledObject();
            spawnedBoxes.Add(newBox);
            if (newBox == null)
            {
                Debug.LogWarning("No pooled object avaliable");
                continue;
            }

            //Set the box's position in the grid
            newBox.transform.position =
                new Vector3(_initialPositions.x + (_spacing.x * (i % colLength)),
                    _initialPositions.y + (-_spacing.y * (i / colLength)), 0);
            newBox.SetActive(true); //Activate the box

            //Set the numbers for the front of the boxes
            SpriteRenderer boxSpriteRenderer = newBox.GetComponent<SpriteRenderer>();
            boxSpriteRenderer.sprite = numberSprites[i];

            //Set the images for the back of the boxes
            SpriteRenderer image = newBox.transform.GetChild(0).GetComponent<SpriteRenderer>();
            int randIndx = Random.Range(0, imageSprites.Count);
            image.sprite = imageSprites[randIndx];
            image.sortingOrder = -2;
            imageSprites.RemoveAt(randIndx);

            //Calculate the sprite's original dimensions (in world units)
            float spriteWidth = image.sprite.bounds.size.x;
            float spriteHeight = image.sprite.bounds.size.y;

            //Determine the maximum scale factor so that the image doesn't exceed maxwidth and maxheight
            float maxScale = Mathf.Min(_minAndMaxWidthsAndHeights.z / spriteWidth,
                _minAndMaxWidthsAndHeights.w / spriteHeight);

            //Determine the minimum scale factor so that the image meets minwidth and minheight
            float minScale = Mathf.Max(_minAndMaxWidthsAndHeights.x / spriteWidth,
                _minAndMaxWidthsAndHeights.y / spriteHeight);

            //The image's original scale is 1. So if that scale is within our minScale and maxScale, keep it. Otherwise, clamp it to the appropriate boundary
            float scaleFactor = Mathf.Clamp(1f, minScale, maxScale);

            //Apply the uniform scale so that we preserve the sprite's aspect ratio
            image.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);

            BoxHandler bh = newBox.GetComponent<BoxHandler>();
            if (bh != null)
            {
                bh.ResetAvaliability();
            }
        }
    }

    //All the getters are below. First one is returning the number of boxes.
    public int GetNumBoxes()
    {
        return numBoxes;
    }

    public float GetExpansionDuration()
    {
        return expansionDuration;
    }

    public float GetExpansionMultiplier()
    {
        return expansionMultiplier;
    }

    public float GetContractionDuration()
    {
        return contractionDuration;
    }

    void HighlightBoxes(int index)
    {
        for (int i = 0; i < spawnedBoxes.Count; i++)
        {
            // Example: Change color to highlight selected card
            Renderer rend = spawnedBoxes[i].GetComponent<Renderer>();
            rend.material.color = (i == index) ? Color.green : Color.white;
        }
    }

    void MoveSelection(int direction)
    {
        int newIndex = index + direction;
        if (newIndex >= 0 && newIndex < spawnedBoxes.Count) // Ensure it's within bounds
        {
            index = newIndex;
            HighlightBoxes(index);
        }
    }

    public void UpdateUI()
    { 
        // Get the current player's score
        // Get current player's index
        StringBuilder turnTextBuilder = new StringBuilder();
        int currentPlayer = GameManager.Instance.GetCurrentPlayer();

        for (int i = 0; i < numberOfPlayers; i++) 
        {
            int playerScore = GameManager.Instance.getPlayerScore(i); // Ensure it's fetching per player

            if (i == currentPlayer)
            {
                turnTextBuilder.Append($"<b><color=#32CD32>Player {i + 1}: {playerScore}</color></b>  ");
            }
            else
            {
                turnTextBuilder.Append($"Player {i + 1}: {playerScore}  ");
            }
        }

        turnText.text = turnTextBuilder.ToString();
    }

    public void moveLeft()
    {
        MoveSelection(-1);
        nextMoveTime = Time.time + moveCooldown;
    }
    public void moveRight()
    {
        MoveSelection(1);
        nextMoveTime = Time.time + moveCooldown;
    }
    public void moveUp()
    {
        MoveSelection(colLength);
        nextMoveTime = Time.time + moveCooldown;
    }
    public void moveDown()
    {
        MoveSelection(-colLength);
        nextMoveTime = Time.time + moveCooldown;
    }

    public int getNumberOfPlayers()
    {
        return numberOfPlayers;
    }
}


