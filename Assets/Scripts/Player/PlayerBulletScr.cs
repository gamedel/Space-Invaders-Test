using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScr : MonoBehaviour
{

    public float Dmg =1f;
    public float Speed = 15f;
    public Vector3 DirectionV3=new Vector3(0f,100f,0f);

    // Start is called before the first frame update
    void Start()
    {
     
    }





    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + DirectionV3, Time.deltaTime * Speed);


        if (transform.position.y > 7f)
        {
            Destroy(gameObject);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemies")
        {
            collision.gameObject.GetComponent<EnemyShipScr>().Hp -= Dmg;
            collision.gameObject.GetComponent<EnemyShipScr>().CurBlinkTime = 0.2f;
            Destroy(gameObject);
        }
    }


}
