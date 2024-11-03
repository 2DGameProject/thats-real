using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public GameObject Player;
    public GameObject ColliderTop;
    public GameObject ColliderBottom;


    void Start()
    {
        ColliderBottom.SetActive(true);
        ColliderTop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if(Player.transform.position.x >= ColliderTop.transform.position.x - 0.5 && Player.transform.position.x <= ColliderBottom.transform.position.x + 0.5)
        {
            if(Player.transform.position.y > ColliderTop.transform.position.y)
            {
                ColliderTop.SetActive(false);
                ColliderBottom.SetActive(true);
                Player.GetComponent<SpriteRenderer>().sortingOrder = 0;

            }
            else
            {
                ColliderTop.SetActive(true);
                ColliderBottom.SetActive(false);
                Player.GetComponent<SpriteRenderer>().sortingOrder = 5;

            }
        }
    }
}
