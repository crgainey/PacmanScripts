using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    public GameObject redEnemy;
    public GameObject pinkEnemy;
    public GameObject blueEnemy;
    public GameObject orangeEnemy;
    
    //public Transform player;

    public float speed;
    public float NextWaypointDistance;

    public float scatterLength = 5f;
    public float timeScatter;
    public float timeRun;


    //used for Chase
    Path path;
    int _currentWaypoint = 0;
    bool _reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    //for scatter mode
    public Transform[] redWaypoints;
    public Transform[] pinkWaypoints;
    public Transform[] blueWaypoints;
    public Transform[] orangeWaypoints;

    public int waypointIndex;

    GameController _gameController;

    enum State
    {
        Scatter,
        Roam,
        Chase,
        Runaway
    }

    State state;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = GetComponent<GameController>();

        //sets the name of the enemys
        redEnemy.name = "Red";
        pinkEnemy.name = "Pink";
        blueEnemy.name = "blue";
        orangeEnemy.name = "Orange";

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Fixedpdate()
    {
        switch (state)
        {
            case State.Scatter:
                Scatter();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Runaway:
                Runaway();
                break;
        }
    }

    //The position in the box
    Vector2 StartPoint()
    {
        switch (gameObject.name)
        {
            case "Red":
                return new Vector2(0f, 0f);

            case "Pink":
                return new Vector2(0f, 0f);

            case "Blue":
                return new Vector2(0f, 0f);

            case "Orange":
                return new Vector2(0f, 0f);
        }
        return new Vector2(); 
    }

    //the start area the ghosts go to
    void Scatter()
    {
        if (gameObject.CompareTag("Red"))
        {
            transform.position = Vector2.MoveTowards(transform.position, redWaypoints[waypointIndex].position, speed * Time.deltaTime);
            waypointIndex++;
        }

        else if (gameObject.CompareTag("Pink"))
        {
            transform.position = Vector2.MoveTowards(transform.position, pinkWaypoints[waypointIndex].position, speed * Time.deltaTime);
            waypointIndex++;
        }

        else if (gameObject.CompareTag("Blue"))
        {
            transform.position = Vector2.MoveTowards(transform.position, blueWaypoints[waypointIndex].position, speed * Time.deltaTime);
            waypointIndex++;
        }

        else if (gameObject.CompareTag("Orange"))
        {
            transform.position = Vector2.MoveTowards(transform.position, orangeWaypoints[waypointIndex].position, speed * Time.deltaTime);
            waypointIndex++;
        }
    }

    //chase the player
    void Chase()
    {
        if (path == null)
        {
            return;
        }

        if (_currentWaypoint >= path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[_currentWaypoint]);

        if (distance < NextWaypointDistance)
        {
            _currentWaypoint++;
        }
    }

    //after the player eats them
    void Runaway()
    {
        Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(state == State.Runaway)
            {
                _gameController.UpdateScore();
            }

            else
            {
                _gameController.UpdateLives();
            }
        }
    }

}
