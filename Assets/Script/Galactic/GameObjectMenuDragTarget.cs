using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Core;

public class GameObjectMenuDragTarget : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FleetController objectMover = (FleetController)target;

        // Display a field to drag a GameObject reference
        objectMover.targetTrans = (Transform)EditorGUILayout.ObjectField("Target Object", objectMover.targetTrans, typeof(Transform), true);
    }


}
