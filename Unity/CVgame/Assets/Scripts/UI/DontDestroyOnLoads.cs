using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoads : MonoBehaviour
{
  void Awake()
  {
    DontDestroyOnLoad(gameObject);
  }
}

