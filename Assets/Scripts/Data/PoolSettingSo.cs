using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolSetting", menuName = "Config/PoolSetting")]

public class PoolSettingSo : ScriptableObject
{
    public List<PoolSetting> poolSettings = new List<PoolSetting>();
}

[Serializable]
public class PoolSetting
{
    public GameObject prefab;
    public int quantity = 20;
}
