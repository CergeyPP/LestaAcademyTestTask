using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Transform _transformToFollow;

    private void Update()
    {
        transform.position = _transformToFollow.position;
    }
}
