using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    public class ToolTipSimple : MonoBehaviour, IToolTip
    {
        [TextArea]
        public string tip;

        public string getToolTipMessage()
        {
            return tip;
        }
    }
}