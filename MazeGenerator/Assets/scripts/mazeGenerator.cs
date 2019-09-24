using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mazeGenerator : MonoBehaviour
{

    //hier word de Cell aangemaakt waar later de muren in koment te zitten
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north;
        public GameObject east;
        public GameObject south;
        public GameObject west;
    }

    public GameObject wall;
    public GameObject floor;
    public GameObject player;
    public GameObject treasure;
    public float wallength = 1.0f;
    public int xSize = 0;
    public int ySize = 0;
    public static int staticXSize = 0;
    public static int staticYSize = 0;
    private Vector3 initialPosition;
    private GameObject wallHolder;
    private Cell[] cells;
    public int currentCell = 0;
    private int totalCells;
    private int visitedCells = 0;
    private bool startedBuilding = false;
    private int currentNeighbour = 0;
    private int randomYValue = 0;
    private int randomXValue = 0;
    private List<int> lastCell;
    private int backUp = 0;
    private int wallBreak = 0;
    private bool PlayerAlreadyMade = false;
    private bool noTreasure = true;
    public static bool FPS = false;
    private GameObject activateUI;
    public static bool cameraTurned = false;



    [SerializeField]
    GameObject mazeDestroy;
    void Start()
    {
        activateUI = GameObject.Find("UIParent");
    }

    //when this function is called the walls will be made
    public void createWalls()
    {
        //this will remove the last maze
        if (wallHolder != null)
            Destroy(wallHolder);

        wallength = 1.0f;
        currentCell = 0;
        visitedCells = 0;
        startedBuilding = false;
        currentNeighbour = 0;
        backUp = 0;
        wallBreak = 0;
        GameObject tempWall;
        randomXValue = Random.Range(0, xSize);
        randomYValue = Random.Range(0, ySize);
        //this will make the parent object for the walls
        wallHolder = new GameObject();
        wallHolder.name = "Maze";
        wallHolder.AddComponent(typeof(MeshRenderer));

        initialPosition = new Vector3((-xSize / 2) + wallength / 2, 0.0f, (-ySize / 2) + wallength / 2);
        Vector3 myPosition = initialPosition;

        //this will make the east and westside walls
        for (int i = 0; i < ySize; i++)
        {
            for (int k = 0; k <= xSize; k++)
            {
                myPosition = new Vector3(initialPosition.x + (k * wallength) - wallength / 2, 0.0f, initialPosition.z + (i * wallength) - wallength / 2);
                tempWall = Instantiate(wall, myPosition, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }
        //this will create the north and southside walls
        for (int i = 0; i <= ySize; i++)
        {
            for (int k = 0; k < xSize; k++)
            {
                myPosition = new Vector3(initialPosition.x + (k * wallength), 0.0f, initialPosition.z + (i * wallength) - wallength);
                tempWall = Instantiate(wall, myPosition, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }
        //this will create the floor of the maze
        for (int i = 0; i <= ySize; i++)
        {
            for (int k = 0; k < xSize; k++)
            {
                myPosition = new Vector3(initialPosition.x + (k * wallength), 0.0f, initialPosition.z + (i * wallength) - wallength);
                tempWall = Instantiate(floor, myPosition, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }
        //this will create the player and put it in the under left corner
        if (!PlayerAlreadyMade)
        {
            myPosition = new Vector3(initialPosition.x + (wallength) - wallength / 2 - 0.5f, 0.5f, initialPosition.z + (wallength) - wallength / 2);
            tempWall = Instantiate(player, myPosition, Quaternion.identity) as GameObject;
            PlayerAlreadyMade = true;
        }

        //this will make the treasure on a random space
        if (noTreasure == true)
        {

            myPosition = new Vector3(initialPosition.x + (randomXValue * wallength) - wallength / 2 - 0.5f, 0.5f, initialPosition.z + (randomYValue * wallength) - wallength / 2);
            tempWall = Instantiate(treasure, myPosition, Quaternion.identity) as GameObject;
            PlayerAlreadyMade = true;
            noTreasure = false;
        }
        createCells();
    }
    //this will create cells and fill them with walls
    void createCells()
    {
        lastCell = new List<int>();
        lastCell.Clear();
        totalCells = xSize * ySize;
        GameObject[] allWalls;
        int children = wallHolder.transform.childCount;
        allWalls = new GameObject[children];
        cells = new Cell[xSize * ySize];
        int eastWestProcess = 0;
        int childProcess = 0;
        int termCount = 0;


        //this gets all the children in a array
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }
        //this will create cell and put the walls into them
        for (int cellProcess = 0; cellProcess < cells.Length; cellProcess++)
        {
            //this will make sure that wall are correctly assineged to the correct cell after a row
            if (termCount == xSize)
            {
                eastWestProcess++;
                termCount = 0;
            }

            //this is process of creating the cells and putting walls in them
            cells[cellProcess] = new Cell();
            cells[cellProcess].east = allWalls[eastWestProcess];
            cells[cellProcess].south = allWalls[childProcess + (xSize + 1) * ySize];

            eastWestProcess++;
            termCount++;
            childProcess++;
            cells[cellProcess].west = allWalls[eastWestProcess];
            cells[cellProcess].north = allWalls[(childProcess + (xSize + 1) * ySize) + xSize - 1];
        }
        createMaze();

    }
    //this will define the walls that need to be destroyed
    void createMaze()
    {
        //this will keep going until the all cells are visited
        while (visitedCells < totalCells)
        {
            //this will look if the first cell has been visited
            if (startedBuilding)
            {
                findNeighbour();
                //this will check if the neighbour has been visited
                if (cells[currentNeighbour].visited == false && cells[currentCell].visited == true)
                {
                    breakWall();
                    cells[currentNeighbour].visited = true;
                    visitedCells++;
                    lastCell.Add(currentCell);
                    currentCell = currentNeighbour;
                    //this will assign who will be the backup
                    if (lastCell.Count > 0)
                    {
                        backUp = lastCell.Count - 1;
                    }
                }
            }
            //this will select a random cell to begin the maze
            else
            {
                currentCell = Random.Range(0, totalCells);
                cells[currentCell].visited = true;
                visitedCells++;
                startedBuilding = true;

            }


        }
    }
    //this will destroy the walls
    void breakWall()
    {
        switch (wallBreak)
        {
            case 1: Destroy(cells[currentCell].north); break;
            case 2: Destroy(cells[currentCell].east); break;
            case 3: Destroy(cells[currentCell].south); break;
            case 4: Destroy(cells[currentCell].west); break;
        }
    }
    //this will find the neighbouring cells and walls
    void findNeighbour()
    {
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWall = new int[4];
        //these checks are here so that you can't get the border walls
        int check = 0;
        check = ((currentCell + 1) / xSize);
        check -= 1;
        check *= xSize;
        check += xSize;
        //this is check if there is a west wall
        if (currentCell + 1 < totalCells && (currentCell + 1) != check)
        {
            //this will check if that cell has been visited
            if (cells[currentCell + 1].visited == false)
            {
                neighbours[length] = currentCell + 1;
                connectingWall[length] = 4;
                length++;
            }
        }
        //this is check if there is a east wall
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            //this will check if that cell has been visited
            if (cells[currentCell - 1].visited == false)
            {
                neighbours[length] = currentCell - 1;
                connectingWall[length] = 2;
                length++;
            }
        }
        //this is check if there is a north wall
        if (currentCell + xSize < totalCells)
        {
            //this will check if that cell has been visited
            if (cells[currentCell + xSize].visited == false)
            {
                neighbours[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }
        //this is check if there is a south wall
        if (currentCell - xSize >= 0)
        {
            //this will check if that cell has been visited
            if (cells[currentCell - xSize].visited == false)
            {
                neighbours[length] = currentCell - xSize;
                connectingWall[length] = 3;
                length++;
            }
        }
        //this will check if a neighbour is found
        if (length != 0)
        {
            int firstOne = Random.Range(0, length);
            currentNeighbour = neighbours[firstOne];
            wallBreak = connectingWall[firstOne];
        }
        //this will backup one cell if there are no non visited neighbours 
        else if (backUp > 0)
        {
            currentCell = lastCell[backUp];
            backUp--;
        }

    }


    //this will change the X value with using the X slider
    public void XsliderChanger(float value)
    {
        xSize = Mathf.RoundToInt(value);
    }
    //this will change the Y value with using the Y slider
    public void YsliderChanger(float value)
    {
        ySize = Mathf.RoundToInt(value);
    }

    //this will control which camera is used
    public void cameraChoose()
    {
        FPS = true;
    }
    void Update()
    {
        staticXSize = xSize;
        staticYSize = ySize;
    }

    public void winFunctionGen()
    {
        Destroy(wallHolder);
        activateUI.SetActive(true);
    }



}
