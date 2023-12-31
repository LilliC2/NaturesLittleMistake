using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using DG.Tweening;


public class PlayerEffects : Singleton<PlayerEffects>
{
    public ParticleSystem explosionPS;

    [Header("Player Particle Systems")]
    public ParticleSystem deathExplosionPS;
    public GameObject missyDeathAnim;
    public ParticleSystem missyHitParticle;
    public ParticleSystem landingPS;

    [Header("Vignettes")]
    [SerializeField]
    Animator redVignetteAnim;
    public Animator greenVignetteAnim;

    [Header("Pea Shooter Particle")]
    public ParticleSystem peaShooterPS;

    [Header("Big Eye Particle")]
    public GameObject bigEyePS;

    [Header("Teeth Shotgun Particle")]
    public ParticleSystem teethShotgunPS;

    [Header("Squito Particle")]
    public ParticleSystem squitoPS;
    LineRenderer redDot;
    public GameObject redDotGO;
    float redDotLength;
    bool updateRedDot;

    [Header("Eyeball Particle")]
    public ParticleSystem eyeballPS;

    [Header("Nubs Particle")]
    public ParticleSystem nubsPS;

    [Header("Baby Lob")]
    public ParticleSystem babyPS;

    [Header("Urchin Particle")]
    public ParticleSystem urchinPS;

    [Header("Sabertooth Particle")]
    public ParticleSystem sabertoothPS;

    [Header("Tripod")]
    bool isTrailActive;
    [SerializeField]
    float activeTime = 2f;
    float refreshRate = 0.1f;

    [SerializeField]
    GameObject fader;

    public GameObject[] childrenWithSprites;
    public SpriteRenderer[] spriteRenderers;

    private void Start()
    {




    }

    private void Update()
    {



        if (updateRedDot)
        {
            print("update red dot");
            redDot.SetPosition(1, new Vector3(0, 0, Mathf.Lerp(0, _IM.itemDataBase[4].projectileRange, 1)));
            redDotGO.transform.localEulerAngles = new Vector3(0, _PC.directional.transform.eulerAngles.y, 0);
        }

 
    }


    public void TripodVFX()
    {

        if (!isTrailActive)
        {
            spriteRenderers = childrenWithSprites[0].GetComponentsInChildren<SpriteRenderer>();

            isTrailActive = true;
            StartCoroutine(ActivateTrail(0.3f));
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= refreshRate;



            for (int i = 0; i < spriteRenderers.Length; i++)
            {

                GameObject gObj = Instantiate(fader, new Vector3(childrenWithSprites[0].transform.position.x, childrenWithSprites[0].transform.position.y, childrenWithSprites[0].transform.position.z), childrenWithSprites[0].transform.rotation) as GameObject;

                gObj.transform.localScale = childrenWithSprites[0].transform.localScale;

                gObj.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = spriteRenderers[i].GetComponent<SpriteRenderer>().sprite;

                gObj.transform.GetChild(i).gameObject.transform.position = childrenWithSprites[i + 1].gameObject.transform.position;
                gObj.transform.GetChild(i).gameObject.transform.localScale = childrenWithSprites[i + 1].gameObject.transform.localScale;
            }

            yield return new WaitForSeconds(refreshRate);
        }

        isTrailActive = false;
    }

    public void RedVignetteFade()
    {
        redVignetteAnim.SetTrigger("Hit");
    }
    

    public void SquitoRedDot()
    {
        redDot = redDotGO.GetComponent<LineRenderer>();

        print("play anim");
        //squitoPS.Play();
        updateRedDot = true;


        ExecuteAfterSeconds(0.5f, () => updateRedDot = false);
        ExecuteAfterSeconds(0.5f, () => redDot.SetPosition(1, Vector3.zero));

    }

  
    private Tween RedDotLength(float endValue, float time)
    {
        var redDotTween = DOTween.To(() => redDotLength, (x) => redDotLength = x, endValue, time);
        return redDotTween;
    }
}
