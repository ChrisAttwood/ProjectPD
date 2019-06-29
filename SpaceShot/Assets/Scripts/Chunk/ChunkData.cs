using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkData 
{
    [System.Serializable]
    public struct Row
    {
        public int[] row;
    }

    public Row[] rows = new Row[8];
}
