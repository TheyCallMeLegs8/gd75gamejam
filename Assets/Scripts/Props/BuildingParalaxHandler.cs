using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class BuildingParalaxHandler : MonoBehaviour
{
    [SerializeField] private float _vanishZValue;
    [SerializeField] private float _speed;

    [SerializeField] private List<GameObject> _buildingPrefabs = new List<GameObject>();


}
