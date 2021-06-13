using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    /// <summary>
    /// События при соприкосновении с сундуком
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerShip ship))
        {
            ChangeLevel.instance.EndGame();
            particleSystem.Play();
            StartCoroutine(DestroyChest());
        }
    }

    IEnumerator DestroyChest()
    {
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
