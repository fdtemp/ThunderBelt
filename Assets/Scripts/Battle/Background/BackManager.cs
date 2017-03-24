using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackManager : MonoBehaviour
{
    public GameObject sourceDeco; // Decorate source.
    public int count = 400; // count of generated decos.
    public float minSize = 0.2f;
    public float maxSize = 1f;
    // Max speed is determined by the flySpeed defines by Global in the completed game.
    public float maxSpeed = 10f;
    public float maxScale = 1f;
    public float topLimit = 15f;
    public float bottomLimit = -15f;
    public float leftLimit = -12f;
    public float rightLimit = 12f;

    void Start()
    {
        if(Global.flySpeed != 0f) maxSpeed = Global.flySpeed;

        for (int i = 0; i < count; i++)
        {
            float rate = Mathf.Sqrt(Random.Range(minSize, maxSize));
            float size = rate * maxScale;
            float speed = (1f - rate) * maxSpeed;
            GameObject deco = Instantiate(sourceDeco, this.gameObject.transform);
            BackRolling ro = deco.GetComponent<BackRolling>();
            SpriteRenderer rd = deco.GetComponent<SpriteRenderer>();
            if (ro == null || rd == null) { Debug.Log("WARNING: deco not valid!"); break; }
            ro.speed = speed;
            ro.transform.localScale *= size;
            ro.topEdge = topLimit;
            ro.bottomEdge = bottomLimit;
            Vector2 loc = new Vector2(Random.Range(leftLimit, rightLimit),
                Random.Range(topLimit, bottomLimit));
            deco.transform.position = loc;
            rd.color = new Color(Random.Range(0.5f, 0.8f), 
                Random.Range(0.5f, 0.8f), Random.Range(0.4f, 1.0f), 1.0f);

        }
    }
}
