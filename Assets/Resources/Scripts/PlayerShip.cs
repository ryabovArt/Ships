using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShip : ShipsBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private float sinkTime;

    void Start()
    {
        path = GameObject.FindGameObjectWithTag("Map").GetComponent<BuildPath>().Path;
    }

    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyShip ship))
        {
            PlayerDeath();
        }
    }

    /// <summary>
    /// јнимаци€ движени€ корабл€
    /// </summary>
    public void PlayerMoveAnimation()
    {
        playerAnimator.SetTrigger("Move");
    }

    /// <summary>
    /// ƒействи€ при столкновении с вражеским кораблем
    /// </summary>
    public void PlayerDeath()
    {
        playerAnimator.SetTrigger("Sink");
        GetAnimationTime();
        StartCoroutine(DestroyPlayerShip());
    }

    IEnumerator DestroyPlayerShip()
    {
        ChangeLevel.instance.RestartLevel();
        GetComponent<PlayerShip>().enabled = false;
        yield return new WaitForSeconds(sinkTime);
        Destroy(gameObject);
        
    }

    /// <summary>
    /// ѕолучаем врем€ проигрывани€ анимационного клипа
    /// </summary>
    public void GetAnimationTime()
    {
        AnimationClip[] clips = playerAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Sinking":
                    sinkTime = clip.length;
                    break;
            }
        }
    }
}
