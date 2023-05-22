using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralScr : MonoBehaviour
{


    public float StartEnemiesSpeed = 2f;

    public GameObject EnemyToCreate;
    public GameObject BonusToCreate;

    public float SpawnTimer = 2f;
    public float CurSpawnTimer;


    public Vector3 StartKoord = new Vector3(0f,0f,0f);
    public int CountXEnemies = 5;
    public int CountYEnemies = 3;
    public float DistBetweenX = 1f;
    public float DistBetweenY = 1f;
    public int RoundIdx = 0;


    public bool IsGameOver = false;
    public bool IsStartGame = false;


    public GameObject GameOverTxObj;
    public GameObject MenuObj;
    public GameObject ScoreTxObj;
    public GameObject RoundTxObj;

    public int Score = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }



    GameObject[] EnemiesObjArray;
    float CurEnemiesSpeed;
    // Update is called once per frame
    void Update()
    {
        EnemiesObjArray = GameObject.FindGameObjectsWithTag("Enemies");
        CurEnemiesSpeed = StartEnemiesSpeed + (1f - ((float)EnemiesObjArray.Length / (float)(CountXEnemies * CountYEnemies))) * 5f;
        for (int i = 0; i < EnemiesObjArray.Length; i++)
        {
            EnemiesObjArray[i].GetComponent<EnemyShipScr>().Speed = CurEnemiesSpeed;
        }

        ScoreTxObj.GetComponent<TMPro.TextMeshProUGUI>().text = Score.ToString();
        RoundTxObj.GetComponent<TMPro.TextMeshProUGUI>().text = "Round "+(RoundIdx+1).ToString();

        if (EnemiesObjArray.Length == 0 && !IsGameOver && IsStartGame)
        {
            RoundIdx++;
            EnemiesSpawn();
        }


    }


    GameObject CreatedEnemyObj;

    public float WidthEnemies, HeightEnemies;
    void EnemiesSpawn()
    {
        WidthEnemies = DistBetweenX * (float)CountXEnemies;
        HeightEnemies = DistBetweenY* (float)CountYEnemies;


        for (int yy = 0; yy < CountYEnemies; yy++)
        {
            for (int xx = 0; xx < CountXEnemies; xx++)
            {

                CreatedEnemyObj = GameObject.Instantiate(EnemyToCreate, StartKoord + new Vector3(xx * DistBetweenX- WidthEnemies/2, HeightEnemies/2- yy * DistBetweenY), Quaternion.identity);
                CreatedEnemyObj.GetComponent<EnemyShipScr>().DeltaX = (DistBetweenX * (float)xx)+ DistBetweenX/2f;
            }
        }
    }

    public void StartGame()
    {
        EnemiesObjArray = GameObject.FindGameObjectsWithTag("Enemies");
        for (int i = 0; i < EnemiesObjArray.Length; i++)
        {
           Destroy(EnemiesObjArray[i]);
        }
        GameObject.Find("PlayerShipObj").GetComponent<PlayerShipScr>().WeaponTypeIdx = 0;


        Score = 0;
        EnemiesSpawn();
        MenuObj.gameObject.SetActive(false);
        IsStartGame = true;
        IsGameOver = false;
    }

    public void GameOver()
    {
        if (!IsGameOver)
        {
            IsGameOver = true;
            IsStartGame = false;
            MenuObj.SetActive(true);
            GameOverTxObj.SetActive(true);
            GameOverTxObj.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Score.ToString();
        }
    }


}
