﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float speed;

    private move1GameManger gameManager;

    /*
     * cache
     */
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<move1GameManger>();
        this.transform.rotation = Quaternion.Euler(0, 30, Random.Range(-70f, 70f));
        speed = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        touch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.bulletCountUp(this.gameObject.GetComponent<bullet>()) ;
            
        } else if (collision.gameObject.name == "LeftCol")
        {
            gameManager.destoryBullet(this.gameObject.GetComponent<bullet>());
        }
    }

    private void move()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }

    private void touch()
    {
        // 마우스 클릭 (터치) 할 경우
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭(터치) 한 포지션을, 2D 월드 포지션으로 변환
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 클릭한 마우스 위치에서 레이캐스트에 걸리는 물체를 hit으로 가져온다
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, transform.forward, 15f);
            if (hit)
            {
                // 자신을 터치한 게 아니면 리턴
                if (hit.transform.gameObject != this.transform.gameObject) return;

                gameManager.bulletCountUp(this.gameObject.GetComponent<bullet>());
            }
        }
    }
}

