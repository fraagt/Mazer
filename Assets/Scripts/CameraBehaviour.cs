using System;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
  [SerializeField] private Vector3 offset;
  
  private Transform _target;

  public void SetTarget(Transform target) => _target = target;

  private void LateUpdate()
  {
    if (_target != null)
      transform.position = _target.position + offset;
  }
}
