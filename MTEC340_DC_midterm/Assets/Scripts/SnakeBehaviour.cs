using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SnakeBehaviour : MonoBehaviour
{
    public static SnakeBehaviour Instance;
    private Vector2 _direction; //moves in x and y axis. 
    [SerializeField] private KeyCode _up = KeyCode.W;
    [SerializeField] private KeyCode _down = KeyCode.S;
    [SerializeField] private KeyCode _left = KeyCode.A;
    [SerializeField] private KeyCode _right = KeyCode.D;
    private Rigidbody2D _rb;
    private List<Transform> _bodySegments;
    public Transform segmentPrefab;
    public Utilities.GameState GameMode;

    
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _eatFood;
    [SerializeField] private AudioClip _snakeDie;
    [SerializeField] private AudioClip _powerDown;

    private float _speed = 0.08f;
    //private bool _powerDownActive = false;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Time.fixedDeltaTime = _speed; //this is how "fast" the snake moves bc we're controlling
                                     // open in another computer see if it feels smooth. binding to physics system (mvmt) 
                                     //multiply the unit we have in whatever machine by time fixeddeltatime. refresh rate of monobehaviour.
                                     //maybe even consider speed as a value itself and fixed delta time as a multiplier\
                                     //TEST IN OTHER COMPUTER BC IT COULD BE SHITTY POOPY

        _bodySegments = new List<Transform>(); //initialise the list ya
        _bodySegments.Add(this.transform);
        GameMode = Utilities.GameState.Play;
    }

    private void Update() //this is how you control the snake
    {
        if (GameBehaviour.Instance.GameMode == Utilities.GameState.Play)
        {
            if (Input.GetKeyDown(_up))
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(_down))
            {
                _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(_left))
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(_right))
            {
                _direction = Vector2.right;
            }
        }
        else if (GameBehaviour.Instance.GameMode == Utilities.GameState.Pause)
        {
            _direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        //this is what makes the body segments move with the snake
        for (int i = _bodySegments.Count - 1; i > 0; i--) //a loop that works backwards. 
        {
            _bodySegments[i].position = _bodySegments[i - 1].position;
            //this is slowly shifting the segment body positions so that
            //theyre all following the segment in the front
            //the snake head must be the last one to get updated
        }
        
        
        this.transform.position = new Vector3(
            Mathf.Round(transform.position.x) + _direction.x, 
            Mathf.Round(transform.position.y) + _direction.y,
            0.0f); //this allows us to change snake's position
        //the round function makes sure our numbers are always whole numbers bc we're working on a grid
        //ALSO!!! we're using the framerate to snap to a grid. dont have to make a grid
        
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);//spawn the prefab
        //make the new body segments spawn at the end of the tail
        segment.position = _bodySegments[_bodySegments.Count - 1].position;
        _bodySegments.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food")) 
        {
            Grow();
            GameBehaviour.Instance.Score(); //if we trigger snake body, then initialise Score() function
            _source.resource = _eatFood;
        }
        /* 
        else if (other.CompareTag("Power Down"))
        {
            PowerDown();
            Destroy(other.gameObject);
            FindAnyObjectByType<FoodBehaviour>().RandomisePosition();
            GameBehaviour.Instance.Score();
            Grow();

        }
        //failed to implement the powerup. i really dont know how to do it.
        */
        
        else if (other.CompareTag("Arena Limit"))
        {
            _source.resource = _snakeDie;
            GameBehaviour.Instance.GameMode = Utilities.GameState.GameOver;
            GameOver();
            GameBehaviour.Instance.ResetScore();
            SceneManager.LoadScene("GameOverScreen");

        }
        _source.Play();

        
    }

    private void GameOver()
    {
        for (int i = 1; i < _bodySegments.Count; i++)
        {
            Destroy(_bodySegments[i].gameObject);
        }
        _bodySegments.Clear(); //clear the list we have
        _bodySegments.Add(this.transform); //add back our main head object
        
        this.transform.position  = Vector3.zero; //reset position of snake
        _direction = Vector2.zero; //stop it from moving
    }

    /*
     //thought this would work but it failed, sorry. 
     //i really probably shouldve gone to your office hour. but at least i got the better outcome for my midterm!
     //bc im obsessed with doing things properly i will figureout how to do this over break ok yea byee!!!
    public void PowerDown()
    {
        if (_powerDownActive = true)
        {
            StartCoroutine(DoubleSpeedRoutine());
        }
    }

    private IEnumerator DoubleSpeedRoutine()
    {
        _powerDownActive = true;
        _speed /= 2f;
        yield return new WaitForSeconds(5f);
        
        _speed *= 2f;
        _powerDownActive = false;
    } 
    */
    
}
