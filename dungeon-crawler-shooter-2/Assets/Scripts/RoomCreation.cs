using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomCreation : MonoBehaviour
{

    Vector3 right,left,forw,back;
    public GameObject Room;
    public Transform middleTransform;
    public bool currentPlane;
    public GameObject currentPlaneObj;
    public GameObject player;
    public float scaleMultiplier;
    public int numEnemies;
    Vector3 worldBottomLeft;
    Vector3 worldBottomRight;
    Vector3 worldTopLeft;
    Vector3 worldTopRight;
    public Text text;
    private float spawnbuffer;
    public GameObject enemy;
    public GameObject doorDestroy;
    roomManagement roomManager;
    doorDetection doorDetection;
    public int spawnBossRoom;
    public GameObject roomWayPoint;
    private waypointManager waypointManager;
    public Vector3 frontpoint, backpoint;
    public float doorRespawn;
    public GameObject door;
    public bool startTimer;



    void Awake()
    {
        door = (GameObject)Resources.Load("Door_C");
        
        SetUpRoom();
    }
    
    void Start()
    {
        SpawnObjects();
        SpawnEnemies();
    }
   

    public void SetUpRoom()
    {
        roomWayPoint = (GameObject)Resources.Load("waypoint");
        waypointManager = GameObject.Find("RoomManager").GetComponent<waypointManager>();
        
        text = GameObject.FindGameObjectWithTag("numEnemies").GetComponent<Text>();
        Room = Resources.Load<GameObject>("Room");
        spawnBossRoom = 0;
        spawnbuffer = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        middleTransform = GameObject.FindGameObjectWithTag("Middle").transform;
        doorDetection = player.gameObject.GetComponent<doorDetection>();
        currentPlaneObj = gameObject;
        enemy = (GameObject)Resources.Load("Zombie");
        scaleMultiplier = transform.localScale.x * 10;
        roomManager = GameObject.FindGameObjectWithTag("RoomManager").GetComponent<roomManagement>();
        roomManager.AddRoom(gameObject);
        currentPlane = true;
        worldBottomLeft = middleTransform.position - Vector3.right * (transform.localScale.x * scaleMultiplier) / 2 - Vector3.forward * (transform.localScale.x * scaleMultiplier) / 2;
        worldBottomRight = middleTransform.position - Vector3.left * (transform.localScale.x * scaleMultiplier) / 2 - Vector3.forward * (transform.localScale.x * scaleMultiplier) / 2;
        worldTopLeft = middleTransform.position - Vector3.right * (transform.localScale.x * scaleMultiplier) / 2 - Vector3.back * (transform.localScale.x * scaleMultiplier) / 2;
        worldTopRight = middleTransform.position - Vector3.left * (transform.localScale.x * scaleMultiplier) / 2 - Vector3.back * (transform.localScale.x * scaleMultiplier) / 2;
        left = currentPlaneObj.transform.position + Vector3.left * scaleMultiplier;
        right = currentPlaneObj.transform.position + Vector3.right * scaleMultiplier;
        forw = currentPlaneObj.transform.position + Vector3.forward * scaleMultiplier;
        back = currentPlaneObj.transform.position + Vector3.back * scaleMultiplier;
        
    }
    public Vector3 RandomSpawnPoint()
    {
        Vector3 spawnpoint = new Vector3(Random.Range(transform.position.x - (scaleMultiplier / 2) + spawnbuffer, transform.position.x + (scaleMultiplier / 2) - spawnbuffer), 0.05f, Random.Range(transform.position.z - (scaleMultiplier / 2) + spawnbuffer, transform.position.z + (scaleMultiplier / 2) - spawnbuffer));
        return spawnpoint;
    }
    public void SpawnEnemies()
    {
        int i = 0;
        int numSpawn = roomManager.numEnemiesToSpawn();
        while (i < numSpawn)
        {
            Vector3 spawn = RandomSpawnPoint();
            GameObject zombie = Instantiate(enemy, spawn, Quaternion.identity);
            i++;
        }
    }
    public void UpdateNumEnemies()
    {
        int i = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            i++;
        }
        numEnemies = i;
        text.text = i + " hostiles remaining";
    }
    void GeneratePlane()
    {
        
            if (currentPlane == true)
            {
                doorRespawn = 0;
                if (doorDetection.doorLeft == true)
                {

                    if (roomManager.NumRooms() > 4)
                    {

                        spawnBossRoom = Random.Range(0, 10);
                        Debug.Log(spawnBossRoom);
                    }
                    if (spawnBossRoom > 6 && spawnBossRoom <= 9 || roomManager.NumRooms() > 7)
                    {
                        SceneManager.LoadScene("bossfight");
                    }

                    Vector3 temp = middleTransform.position;
                    middleTransform.position = left;
                    if (roomManager.checkExisting(middleTransform.gameObject))
                    {
                        GameObject SpawnedRoom = Instantiate(Room, left, Quaternion.identity);
                        SpawnedRoom.name = "Room" + roomManager.NumRooms();
                        doorDestroy = SpawnedRoom.transform.GetChild(3).gameObject;
                        doorDetection.SetBoolsFalse();
                        StartCoroutine(InactiveToActive(doorDestroy));
                        doorRespawn += Time.deltaTime;
                        if (doorRespawn > 3)
                        {
                            doorDestroy.SetActive(true);
                            doorRespawn = 0;
                        }
                        currentPlane = false;
                    }
                    else
                    {
                        middleTransform.position = temp;
                    }
                }
                if (doorDetection.doorUp == true)
                {

                    if (roomManager.NumRooms() > 3)
                    {

                        spawnBossRoom = Random.Range(0, 10);
                        Debug.Log(spawnBossRoom);
                    }
                    if (spawnBossRoom >= 6 && spawnBossRoom <= 9)
                    {
                        SceneManager.LoadScene("bossfight");
                    }
                    Vector3 temp = middleTransform.position;
                    middleTransform.position = forw;
                    if (roomManager.checkExisting(middleTransform.gameObject))
                    {
                        GameObject SpawnedRoom = Instantiate(Room, forw, Quaternion.identity);
                        SpawnedRoom.name = "Room" + roomManager.NumRooms();
                        doorDestroy = SpawnedRoom.transform.GetChild(1).gameObject;
                        doorDetection.SetBoolsFalse();
                        StartCoroutine(InactiveToActive(doorDestroy));
                        doorRespawn += Time.deltaTime;
                        if (doorRespawn > 3)
                        {
                            doorDestroy.SetActive(true);
                            doorRespawn = 0;
                        }
                        currentPlane = false;
                    }
                    else
                    {
                        middleTransform.position = temp;
                    }
                }
                if (doorDetection.doorRight == true)
                {
                    doorRespawn += Time.deltaTime;
                    if (roomManager.NumRooms() > 3)
                    {

                        spawnBossRoom = Random.Range(0, 10);
                        Debug.Log(spawnBossRoom);
                    }
                    if (spawnBossRoom >= 6 && spawnBossRoom <= 9)
                    {
                        SceneManager.LoadScene("bossfight");
                    }
                    Vector3 temp = middleTransform.position;
                    middleTransform.position = right;
                    if (roomManager.checkExisting(middleTransform.gameObject))
                    {
                        GameObject SpawnedRoom = Instantiate(Room, right, Quaternion.identity);
                        SpawnedRoom.name = "Room" + roomManager.NumRooms();
                        doorDestroy = SpawnedRoom.transform.GetChild(2).gameObject;
                        doorDetection.SetBoolsFalse();
                        StartCoroutine(InactiveToActive(doorDestroy));

                    if (doorRespawn > 3)
                        {
                            Debug.Log(doorRespawn);
                            doorDestroy.SetActive(true);
                            doorRespawn = 0;
                        }
                        currentPlane = false;
                    }
                    else
                    {
                        middleTransform.position = temp;
                    }
                }
                if (doorDetection.doorDown == true)
                {
                    doorRespawn += Time.deltaTime;
                    if (roomManager.NumRooms() > 3)
                    {

                        spawnBossRoom = Random.Range(0, 10);
                        Debug.Log(spawnBossRoom);
                    }
                    if (spawnBossRoom >= 6 && spawnBossRoom <= 9)
                    {
                        SceneManager.LoadScene("bossfight");
                    }
                    Vector3 temp = middleTransform.position;
                    middleTransform.position = back;
                    if (roomManager.checkExisting(middleTransform.gameObject))
                    {
                        GameObject SpawnedRoom = Instantiate(Room, back, Quaternion.identity);
                        SpawnedRoom.name = "Room" + roomManager.NumRooms();
                        doorDestroy = SpawnedRoom.transform.GetChild(0).gameObject;
                        doorDetection.SetBoolsFalse();
                        StartCoroutine(InactiveToActive(doorDestroy));

                        if (doorRespawn > 3)
                        {
                            Debug.Log(doorRespawn);
                            doorDestroy.SetActive(true);
                            doorRespawn = 0;
                        }
                        currentPlane = false;


                    }
                    else
                    {
                        middleTransform.position = temp;
                    }
                }


            
            
        }
        if (roomManager.NumRooms() > 1)
        {

            Transform roomToDelete = roomManager.RoomList()[roomManager.NumRooms()-2].transform;
            for (int i = 0; i < roomToDelete.childCount; i++)
            {
                if (roomToDelete.GetChild(i).gameObject.tag != "persist" )
                {
                    roomToDelete.GetChild(i).tag = "unpassable";
                    roomToDelete.GetChild(i).GetComponent<MeshCollider>().isTrigger = false;

                }
                     
            }
            //roomManager.RoomList().RemoveAt(0);
        }
       
        
    }

    private void SpawnObjects()
    {
        Transform room = transform.Find("Persisting Room");
        Transform props = room.Find("Props");
        var allChildren = props.gameObject.GetComponentInChildren<Transform>();
        foreach (Transform t in allChildren)
        {
            if (t.CompareTag("roomassets") == true)
            {
                float p = Random.Range(0,100);
                if(p < 35)
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
    }
    IEnumerator InactiveToActive(GameObject door)
    {
        door.SetActive(false);
        yield return new WaitForSeconds(1);
        door.SetActive(true);
    }

 
    void Update()
    {
        if (startTimer)
        {
            doorRespawn += Time.deltaTime;
            
        }
        GeneratePlane();
        UpdateNumEnemies();
    }
    //private void OnDrawGizmos()
    //{

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawCube(left, Vector3.one);
    //    Gizmos.DrawCube(right, Vector3.one);
    //    Gizmos.DrawCube(forw, Vector3.one);
    //    Gizmos.DrawCube(back, Vector3.one);
    //}
}
