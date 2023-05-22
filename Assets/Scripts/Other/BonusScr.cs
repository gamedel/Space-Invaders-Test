using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScr : MonoBehaviour
{

    public int BonusNumber;
    // Start is called before the first frame update
    void Start()
    {
        BonusNumber = Random.Range(1, 4);
        transform.GetChild(0).GetComponent<TMPro.TextMeshPro>().text = BonusNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0f, -100f, 0), 3f * Time.deltaTime);
        if (transform.position.y < -10f) Destroy(gameObject);
    }
}
