using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipScr : MonoBehaviour
{
    public bool IsOnlyHorizontal=true;
    public float Speed = 50f;

    public int WeaponTypeIdx=0;
    public Weapon[] Weapons;

  


    //Промежуточные переменные
    #region Vars
    float CurTimeToRealod = 0f;
    Vector2 MousePos;
    GameObject CreatedBulletObj;
    #endregion

    GeneralScr _GeneralScr;
    // Start is called before the first frame update
    void Start()
    {
        _GeneralScr = GameObject.Find("GeneralObj").GetComponent<GeneralScr>();
    }





    // Update is called once per frame
    void Update()
    {

        if (!_GeneralScr.IsGameOver && _GeneralScr.IsStartGame)
        {

            MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsOnlyHorizontal)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Clamp(MousePos.x, -8.15f, 8.15f), transform.position.y, transform.position.x), Speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Clamp(MousePos.x, -8.15f, 8.15f), Mathf.Clamp(MousePos.y, -4f, 4f), transform.position.x), Speed * Time.deltaTime);
            }


            if (CurTimeToRealod <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    CurTimeToRealod = Weapons[WeaponTypeIdx].TimeToRealod;
                    for (int i = 0; i < Weapons[WeaponTypeIdx].BulletsCount; i++)
                    {
                        CreatedBulletObj = GameObject.Instantiate(Weapons[WeaponTypeIdx].BulletObj, transform.position + Weapons[WeaponTypeIdx].DeltaPosV3[i], Quaternion.identity);
                        CreatedBulletObj.GetComponent<PlayerBulletScr>().Dmg = Weapons[WeaponTypeIdx].Dmg;
                        CreatedBulletObj.GetComponent<PlayerBulletScr>().DirectionV3 = Weapons[WeaponTypeIdx].DirectionV3[i];
                    }
                }
            }
            else
            {
                CurTimeToRealod -= Time.deltaTime;
            }
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bonuses")
        {
            WeaponTypeIdx = collision.gameObject.GetComponent<BonusScr>().BonusNumber;
            Destroy(collision.gameObject);
        }
    }




}



[System.Serializable]
public class Weapon{
    public string Name = "Default";
    public GameObject BulletObj;
    public float Dmg;
    public float TimeToRealod;

    public int BulletsCount = 1;
    public Vector3[] DeltaPosV3;
    public Vector3[] DirectionV3;

}

