using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScr : MonoBehaviour
{
    public float Hp = 1f;
    public float Speed = 3f;
    public Vector3 DirectionV3 = new Vector3(0f, -100f, 0f);
    public bool IsWaveMoving = false;
    public float WaveSize = 2f;

    public float DeltaX;

    public float CurBlinkTime = 0f;


    bool IsMoveRight = true;

    GeneralScr _GeneralScr;
    // Start is called before the first frame update
    void Start()
    {
        _GeneralScr = GameObject.Find("GeneralObj").GetComponent<GeneralScr>();
       
    }


    void Die()
    {
        _GeneralScr.Score += 10 * (_GeneralScr.RoundIdx + 1);
        if (Random.Range(0f, 100f) < 20f)
        {
            GameObject.Instantiate(_GeneralScr.BonusToCreate, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
      //  transform.position = Vector3.MoveTowards(transform.position, transform.position + DirectionV3, Time.deltaTime * Speed);

        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }


        if (Hp <= 0f) Die();

        if (CurBlinkTime <= 0f)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
          }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            CurBlinkTime -= Time.deltaTime;
        }


        if (!_GeneralScr.IsGameOver && _GeneralScr.IsStartGame)
        {
            if (IsMoveRight)
            {
                if (transform.position.x + (_GeneralScr.WidthEnemies - DeltaX) >= 8f)
                {

                    IsMoveRight = false;
                    transform.position -= new Vector3(0f, _GeneralScr.DistBetweenY / 4f, 0f);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(100f, 0f, 0f), Time.deltaTime * (Speed+ _GeneralScr.RoundIdx));
                }
            }
            else
            {


                if (transform.position.x - DeltaX <= -8f)
                {
                    IsMoveRight = true;
                    transform.position -= new Vector3(0f, _GeneralScr.DistBetweenY / 4f, 0f);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-100f, 0f, 0f), Time.deltaTime * (Speed+ _GeneralScr.RoundIdx));
                }
            }

        }



        if (transform.position.y < -4)
        {
            _GeneralScr.GameOver();
        }

    }







}
