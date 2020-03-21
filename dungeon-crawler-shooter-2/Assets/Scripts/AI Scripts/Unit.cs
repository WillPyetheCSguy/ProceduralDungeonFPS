using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{


    public Transform target;
    float speed = 3;
    Vector3[] path;
    int targetIndex;
    destroythis Entrance;
    public bool isFollowing;
    EnemyController thisZombie;

    void Start()
    {
        thisZombie = GetComponent<EnemyController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Entrance = GameObject.FindGameObjectWithTag("Player").GetComponent<destroythis>();
        isFollowing = false;
    }
    private void Update()
    {
        if (Entrance.start && !isFollowing)
        {
            StartCoroutine(startPath());
        }
    }
    IEnumerator startPath()
    {
        isFollowing = true;
        while (!thisZombie.isDead)
        {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            yield return new WaitForSeconds(0.3f);
        }
       
        
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.LookAt(currentWaypoint);
            if(!thisZombie.isDead)
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}