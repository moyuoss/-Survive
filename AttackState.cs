using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float movetimer;
    private float losePlayerTimer;
    private float shotTimer;
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())//player can be seen
        {
            //lock thr lose player timer and increment the move and shot timers.
            losePlayerTimer = 0;
            movetimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            //if shot timer > fireRate
            if(shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            //move the enemy to a rando, position after a random time.
            if (movetimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                movetimer = 0;
            }
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else //lost sight of player.
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //Change to the search state.
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
    public void Shoot()
    {
        //store referance to the gun barrel.
        Transform gunbarrel = enemy.gunBarrel;

        //instantiate a new bullet.
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        //calculate the direction to the player.
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //add force rigidbody of the bullet.
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up) * shootDirection * 40;
        Debug.Log("Shoot");
        shotTimer = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
