
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class Burst : BaseAbility
{
    private bool isBurstActive = false;

    public override void Execute(Player player)
    {
        ParticleSystem burstEffect = GetComponent<ParticleSystem>();

        if (!isBurstActive)
        {
            print("Burst executed for player: " + player.name);
            isBurstActive = true;
            transform.position = player.transform.position;
            burstEffect.Play();
            StartCoroutine(ApplyBurst(player));
        }
    }

    private IEnumerator ApplyBurst(Player player)
    {
        float burstDuration = 5f;
        float burstSpeedMultiplier = 2f;

        float originalSpeed = player.MoveSpeed;
        player.MoveSpeed *= burstSpeedMultiplier;

        yield return new WaitForSeconds(burstDuration);

        player.MoveSpeed = originalSpeed;
        isBurstActive = false;
    }
}
