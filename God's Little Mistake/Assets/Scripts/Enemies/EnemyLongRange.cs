using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLongRange : GameBehaviour
{


    [Header ("Enemy Navigation")]
    bool projectileShot;
    GameObject firingPoint;
    public GameObject firingPointFront;
    public GameObject firingPointBack;
    public GameObject firingPointLeft;
    public GameObject firingPointRight;
    public GameObject player;
    public NavMeshAgent agent;

    bool animationPlayed;
    bool runAway;
    float runAwaySpeed;
    float normalSpeed;

    public float sightRange = 7;
    public float attackRange;

    bool canAttack;
    bool canSee;

    float projectileRange;

    ////patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    public LayerMask whatIsGround, whatIsPlayer;

    public BaseEnemy enemyStats;
    BaseEnemy baseEnemy;
    Vector3 target;

    [Header("Animation")]
    [SerializeField]
    Animator anim;

    [SerializeField]
    ParticleSystem attackPS;

    [Header("Audio")]
    public AudioSource attackAudio;
    public AudioSource hurtAudio;
    public AudioSource deathAudio;
    public AudioSource stepAudio;

    void Start()
    {
        //enemyRange = _ED.enemies[0].range;
        //enemySightRange = _ED.enemies[0].range + 0.5f;
        //player = GameObject.Find("Player");
        enemyStats = GetComponent<BaseEnemy>();
        baseEnemy = GetComponent<BaseEnemy>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        normalSpeed = enemyStats.stats.speed;
        runAwaySpeed = enemyStats.stats.speed*2;

        attackRange = enemyStats.stats.range;
        target = SearchWalkPoint();
        
        projectileRange = enemyStats.stats.range +2;

        baseEnemy.childAnim = anim;
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.velocity.magnitude > 0.5f)
        {
            anim.SetBool("Walking", true);
            //baseEnemy.walking.Play();
        }
        else anim.SetBool("Walking", false);


        baseEnemy.FlipSprite(agent.destination);

        agent.speed = enemyStats.stats.speed;

        if (_GM.gameState != GameManager.GameState.Dead)
        {
            ////check for the sight and attack range
            if (baseEnemy.enemyState != BaseEnemy.EnemyState.Charmed)
            {
                if (baseEnemy.enemyState != BaseEnemy.EnemyState.Die)
                {
                    canSee = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
                    canAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                    //if cant see player, patrol
                    if (!canSee) baseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;
                    else if (canSee) baseEnemy.enemyState = BaseEnemy.EnemyState.Chase;
                    //else agent.isStopped = tru;
                }


            }
        }
        //just patrol if player is dead
        else baseEnemy.enemyState = BaseEnemy.EnemyState.Patrolling;




        firingPoint = firingPointFront;
        firingPoint.transform.LookAt(player.transform.position);


        switch (baseEnemy.enemyState)
        {
            case BaseEnemy.EnemyState.Patrolling:

                if (!agent.pathPending)
                {
                    if (agent.remainingDistance <= agent.stoppingDistance)
                    {
                        if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                        {
                            // Destination reached
                            target = SearchWalkPoint();
                            //new target
                            
                        }
                    }
                }


                agent.SetDestination(target);

                //change destination


                break;
            case BaseEnemy.EnemyState.Chase:


                if (Vector3.Distance(player.transform.position, gameObject.transform.position) > attackRange*2)
                {
                    //chase player
                    agent.isStopped = false;
                    enemyStats.stats.speed = normalSpeed;

                    runAway = false;

                    //frontAnim.speed = 1;
                    //backAnim.speed = 1;
                    //leftSideAnim.speed = 1;
                    //rightSideAnim.speed = 1;
                    agent.SetDestination(player.transform.position);
                    //change destination

                }
                else if(Vector3.Distance(player.transform.position, gameObject.transform.position) < attackRange  && 
                    Vector3.Distance(player.transform.position, gameObject.transform.position) > 5)
                {
                    if(!runAway)
                    {

                        baseEnemy.FlipSprite(_PC.transform.position);
                        print("shooot");
                        agent.isStopped = true;
                        FireProjectile(enemyStats.stats.projectilePF, enemyStats.stats.projectileSpeed, enemyStats.stats.fireRate, enemyStats.stats.range);

                        transform.LookAt(player.transform.position);
           
                    }

                    

                }
                else if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 3)
                {
                    runAway = true;

                    //frontAnim.speed = 2;
                    //backAnim.speed = 2;
                    //leftSideAnim.speed = 2;
                    //rightSideAnim.speed = 2;

                    enemyStats.stats.speed = runAwaySpeed;

                    //run away from player
                    agent.isStopped = false;
                    Vector3 toPlayer = player.transform.position - transform.position;
                    Vector3 targetPosition = toPlayer.normalized * -10f;

                    agent.SetDestination(targetPosition);
                    //change destination


                }


                break;
            case BaseEnemy.EnemyState.Die:

                deathAudio.Play();
                baseEnemy.Die();

                break;
            case BaseEnemy.EnemyState.Attacking:


                

                break;
        }




    }


    private Vector3 SearchWalkPoint()
    {

        return transform.position + Random.insideUnitSphere * walkPointRange;

    }


    public void FireProjectile(GameObject _prefab, float _projectileSpeed, float _firerate, float _range)
    {
        if (!projectileShot)
        {
            attackPS.Play();
            anim.SetTrigger("Attack");

            baseEnemy.attack.Play();
            //Spawn bullet and apply force in the direction of the mouse
            //Quaternion.LookRotation(flatAimTarget,Vector3.forward);
            GameObject bullet = Instantiate(_prefab, firingPoint.transform.position, firingPoint.transform.rotation);

            bullet.GetComponent<EnemyProjectile>().dmg = enemyStats.stats.dmg;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * _projectileSpeed);


            bullet.GetComponent<RangeDetector>().range = projectileRange;
            bullet.GetComponent<RangeDetector>().positionShotFrom = transform.position;
            bullet.GetComponent<EnemyProjectile>().dmg = enemyStats.stats.dmg;



            Mathf.Clamp(bullet.transform.position.y, 0, 0);

            //This will destroy bullet once it exits the range, aka after a certain amount of time
            //if (Vector3.Distance(transform.position, bullet.transform.position) > _range) Destroy(bullet);

            //Controls the firerate, player can shoot another bullet after a certain amount of time
            projectileShot = true;
            ExecuteAfterSeconds(_firerate, () => projectileShot = false);
        }

    }


}
