using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public class MineBaseData
    {
        public int MaxWorkerAmount;
        public int CurrentWorkerAmount;
        public int DiamondCapacity;
        public int CurrentDiamondAmount;
        public int MineCartCapacity;
        public float GemCollectionOffset=5f;
        
        public List<Transform> MinePlaces;
        public List<Transform> CartPlaces;
    }
}