using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlugLegs : GameBehaviour
{
    float alpha = 1;
    [SerializeField]
    float destroyTime;
    [SerializeField]
    float damage = 1f;
    [SerializeField]
    LayerMask enemy;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindEnemies", 1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, alpha);

        TweenValue(0, destroyTime);
        //get sprite render

        //will add a fade away later
        ExecuteAfterSeconds(destroyTime, () => Destroy(this.gameObject));
    }

    void FindEnemies()
    {
        var col = Physics.OverlapSphere(transform.position, 0.5f, enemy);
        if (col.Length != 0)
        {
            foreach (var enemy in col)
            {
                //print("HItting enemy " + enemy.name);
                enemy.gameObject.GetComponent<BaseEnemy>().Hit(damage);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            print("speed uup");
            _PC.legItem.movementSpeed = 10;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _PC.legItem.movementSpeed = 3;

        }
    }

    private Tween TweenValue(float endValue, float time)
    {
        var speedTween = DOTween.To(() => alpha, (x) => alpha = x, endValue, time);
        return speedTween;
    }
}
