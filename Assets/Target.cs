using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static int amount = 1;
    private int prvateamount=1;
    private Queue<Vector3> playerqueue=null;
    private Vector3 lastknown;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerqueue == null)
        {
            prvateamount = amount;
            amount++;
            
            playerqueue = new Queue<Vector3>(0 + (20 * prvateamount));
        }

        if (lastknown==Playercontroller.player.transform.position)
        {
            return;
        }
        if (playerqueue.Count== 0 + (20 * prvateamount))
        {
            transform.position = playerqueue.Dequeue();
        }
        playerqueue.Enqueue(Playercontroller.player.transform.position);
        lastknown = Playercontroller.player.transform.position;
    }
}
