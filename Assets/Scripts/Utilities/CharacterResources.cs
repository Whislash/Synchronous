﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace KWY
{
    class CharacterResources 
    {
        // [Type]_[CID]
        public static string Flappy_1 = "Prefabs/Characters/Flappy";
        public static string Flappy2_2 = "Prefabs/Characters/Flappy2";
        public static string Knight_3 = "Prefabs/Characters/Knight";

        public static GameObject LoadCharacter(CID cid)
        {
            return cid switch
            {
                CID.Flappy => Resources.Load<GameObject>(Flappy_1),
                CID.Flappy2 => Resources.Load<GameObject>(Flappy2_2),
                CID.Knight => Resources.Load<GameObject>(Knight_3),
                _ => null,
            };
        }
    }
}
