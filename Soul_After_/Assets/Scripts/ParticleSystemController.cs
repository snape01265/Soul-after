using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public void ParticleSystemFadeIn(ParticleSystem pt, float maxRate = 10, float speed = 1)
    {
        StartCoroutine(ParticleFadeIn(pt, maxRate, speed));
    }

    public void ParticleSystemFadeOut(ParticleSystem pt, float minRate = 0, float speed = 1)
    {
        StartCoroutine(ParticleFadeOut(pt, minRate, speed));
    }

    IEnumerator ParticleFadeIn(ParticleSystem particle, float maxRate = 10, float speed = 1)
    {
        particle.Play();
        var emit = particle.emission.rateOverTime;
        emit.constant = 0;
        while (emit.constant <= maxRate)
        {
            emit.constant += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ParticleFadeOut(ParticleSystem particle, float minRate = 0, float speed = 1)
    {
        var emit = particle.emission.rateOverTime;
        while (emit.constant >= minRate)
        {
            emit.constant -= Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        }
        particle.Stop();
    }
}
