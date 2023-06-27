﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemy2Behaviour : TopDownEnemyBehaviour
{   
    public float projectileTimer = 2f;
    public GameObject projectile;

    // attacking sprites
    [SerializeField] private List<Sprite> fireSpritesUp = new List<Sprite>(4);
    [SerializeField] private List<Sprite> fireSpritesRight = new List<Sprite>(4);
    [SerializeField] private List<Sprite> fireSpritesDown = new List<Sprite>(4);
    [SerializeField] private List<Sprite> fireSpritesLeft = new List<Sprite>(4);

    // projectile firing
    private float _currentTime;
    private bool _isFiring;
    private float _firingFramesPerSecond = 8f;


    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        _currentTime = projectileTimer;
        _health = 2;
    }

    // Update is called once per frame
    override public void FixedUpdate()
    {
        base.FixedUpdate();
        _currentTime -= Time.deltaTime;
        
        // shoot a bullet if the time is right
        if (_currentTime <= 0 && !_isDying){
            _isFiring = true;
            _currentFrame = 0;
            _currentTime = projectileTimer;

            
        }
    }

    override public Direction pickDirection(){
        if (_isFiring) {return _currDir;}
        return base.pickDirection();
    }

    override public Vector2 getMovement(){
        if (_isFiring) {return Vector2.zero;}
        return base.getMovement();
    }

    override public void handleAnimation(){
        if (_isFiring){
            int lastFrame = Mathf.FloorToInt(_currentFrame);
            _currentFrame = Mathf.Repeat(_currentFrame + Time.deltaTime * _firingFramesPerSecond, 4f); 

            // if it's the right time in the animation, fire the projectile!
            if (lastFrame == 1 && Mathf.FloorToInt(_currentFrame) == 2){
                // fire the projectile!
                GameObject p = Instantiate(projectile) as GameObject;
                TopDownEnemyProjectileBehaviour projScript = (TopDownEnemyProjectileBehaviour)p.GetComponent(typeof(TopDownEnemyProjectileBehaviour));
                projScript.setDirection((int)_currDir); 
                p.transform.position = transform.position;
            }

            // if we are done attacking, allow movement again and play the appropriate animation!
            if (lastFrame == 3 && Mathf.FloorToInt(_currentFrame) == 0){
                _isFiring = false;
            }
            else{
                switch (_currDir){
                    case Direction.North:
                        currentSprite.sprite = fireSpritesUp[Mathf.FloorToInt(_currentFrame)];
                        break;
                    case Direction.East:
                        currentSprite.sprite = fireSpritesRight[Mathf.FloorToInt(_currentFrame)];
                        break;
                    case Direction.South:
                        currentSprite.sprite = fireSpritesDown[Mathf.FloorToInt(_currentFrame)];
                        break;
                    case Direction.West:
                        currentSprite.sprite = fireSpritesLeft[Mathf.FloorToInt(_currentFrame)];
                        break;
                    default:
                        break;
                }
                return;
            }
        }
        base.handleAnimation();
    }
}
