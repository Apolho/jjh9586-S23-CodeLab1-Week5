using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;

public class ASCIILevelLoader : MonoBehaviour
{
    public GameObject //creates public Game Object variables
        player,
        wall,
        obstacle,
        collectible;
        
    private GameObject //creates private Game object variables
        level,
        currentPlayer;

    private int currentLevel = 0; //creates and sets current level

    public int CurrentLevel
    {
        get { return currentLevel; } //when current level changes
        set
        {
            currentLevel = value; //current level turns into what it changed to
            LoadLevel(); //and load level function is called
        }
    }

    public int score = 0; //creates and sets score variable

    public int scoreCap = 1;//sets and creates score cap
    
    //const strings are the folders and files for script to load
    private const string FILE_NAME = "LevelX.txt";
    // X will be replaced by numbers to load levels
    private const string FILE_DIR = "/Levels/";
    private string FILE_PATH;

    public float xOffset; //creates offset variable
    public float yOffset;//creates offset variable
    public Vector2 playerStartPos; //creates a Vector 2 for player start position

    // Start is called before the first frame update
    void Start()
    {
        FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME; //creates a path to the folder for the texts
        
        LoadLevel(); //calls the load level function
    }
    

    bool LoadLevel()
    {
        Destroy(level); //destorys current level
        level = new GameObject("Level"); //creates a level gameObject that will hold all the gameObjects we will instantiate

        string newPath = FILE_PATH.Replace("X", currentLevel + ""); //File path name is chnaged with current level to load correct level

        string[] fileLines = File.ReadAllLines(newPath); //turns lines in level text files into arrays to read

        //createa a for loop to read each line of the arrqay
        for (int yPos = 0; yPos < fileLines.Length; yPos++)
        {
            //gets each line in the array
            string lineText = fileLines[yPos];
            
            //turns current line in the array into an array of characters
            char[] lineChars = lineText.ToCharArray();
            
            //for loop for each character
            for (int xPos = 0; xPos < lineChars.Length; xPos++)
            {
                //grabs current character
                char c = lineChars[xPos];

                //make a variable for a new Game Object
                GameObject newObj = null;
                
                
                switch (c)
                {
                    case 'p': //if char == p
                        //set startposition as the position of the player that is instantiated, adding offset to center it with the level
                        playerStartPos = new Vector2(xPos + xOffset ,yOffset - yPos); 
                        //create a player game object
                        newObj = Instantiate<GameObject>(player);
                        //turn the player game object as the current player
                        currentPlayer = newObj;
                        break;
                    case 'w': //if char == w
                        newObj = Instantiate<GameObject>(wall); //create wall
                        break;
                    case 'o': //if char == o
                        newObj = Instantiate<GameObject>(obstacle); //create obstacle
                        break;
                    case '0': //if char == 0
                        newObj = Instantiate<GameObject>(obstacle); //create obstacle
                        newObj.GetComponent<Obstacles>().side = false; //set side bool to false
                        break;
                    case 'h': //if char == h
                        newObj = Instantiate<GameObject>(collectible); //create collectible
                        break;
                    default:
                        newObj = null; //turn the rest to null and create nothing
                        break;
                        
                }

                if (newObj != null) //objects that are not null
                {
                    newObj.transform.position =
                        new Vector2(xOffset + xPos,
                                            yOffset - yPos);
                    //are positioned to the location of the text file with the offset to center on screen

                    newObj.transform.parent = level.transform; 
                    //turns the objects into children of the level game object
                }
            }
        }

        return false;
    }

    public void ResetPlayer()
    {
        currentPlayer.transform.position = playerStartPos;
        //moves current player to the start position that was set
    }

    public void Scoring()
    {
        score++; //score goes up
        if (score == scoreCap) //if the score == the Cap
        {
            CurrentLevel++; //current level will go up
            scoreCap++; //Cap goes up
            score = 0; // score goes back down to 0
        }
    }
}
