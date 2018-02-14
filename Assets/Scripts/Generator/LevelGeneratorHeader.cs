using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    public class LevelGeneratorHeader : MonoBehaviour
    {
        public static LevelGeneratorHeader singleton;
        
        void Awake()
        {
            singleton = this;
        }

    }
}
