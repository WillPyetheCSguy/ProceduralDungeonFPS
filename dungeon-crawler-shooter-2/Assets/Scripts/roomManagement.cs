using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomManagement : MonoBehaviour
{
    public int x = 0;
    public int n;
    public GameObject currentRoom;
    public List<GameObject> Rooms = new List<GameObject>();
    public int numEnemies;
    public GameObject Astar;

    // Start is called before the first frame update
    public List<GameObject> RoomList()
    {
        return Rooms;
    }
    public int NumRooms()
    {
        return Rooms.Count;
    }
    private void Awake()
    {
        n = 2;
    }
    private void Start()
    {
        numEnemies = 0;
    }

    public void AddRoom(GameObject room)
    {
        Rooms.Add(room);
        currentRoom = room;
        Astar.transform.position = currentRoom.GetComponent<RoomCreation>().middleTransform.position;
        Astar.GetComponent<Grid>().CreateGrid();
        n++;
        x++;
    }
    public int numEnemiesToSpawn()
    {
        
        numEnemies = Random.Range(n, n + 3);
        if (numEnemies > 18)
            return 18;
        return numEnemies;
    }
    public bool checkExisting(GameObject middle)
    {
        foreach (GameObject room in Rooms)
        {
            if (middle.transform.position == room.transform.position)
            {
                Debug.Log("checkexist = false");
                return false;
            }
        }
        Debug.Log("checkexist = true");
        return true;
    }

}
