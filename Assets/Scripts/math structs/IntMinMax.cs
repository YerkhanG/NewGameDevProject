using UnityEngine;

namespace math_structs
{
    [System.Serializable]
    public class IntMinMax
    {
        public float min;
        public float max;

        public int GetValue()
        {
            return (int)Random.Range(min, max);
        }
    }
}