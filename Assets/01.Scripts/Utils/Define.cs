using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    private static HitterController hitter = null;
    private static PitcherController pitcher = null;

    public static HitterController Hitter
    {
        get
        {
            if(hitter == null)
            {
                hitter = GameObject.FindObjectOfType<HitterController>();
            }

            return hitter;
        }
    }

    public static PitcherController Pitcher
    {
        get
        {
            if (pitcher == null)
            {
                pitcher = GameObject.FindObjectOfType<PitcherController>();
            }

            return pitcher;
        }
    }
}
