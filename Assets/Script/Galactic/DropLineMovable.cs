using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using BOTF3D_Core;
//using BOTF3D_Combat;
//using Assets.Script;

namespace Assets.Core
{

    public class DropLineMovable: MonoBehaviour
    {                 
        private LineRenderer lineRenderer;
        private Vector3[] points;

        //private void Awake()
        //{
        //    lineRenderer = GetComponent<LineRenderer>();
        //}
        public void GetLineRenderer()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetUpLine(Vector3[] points)
        {
            lineRenderer.positionCount = points.Length;
            this.points = points;
            if (lineRenderer != null && points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    lineRenderer.SetPosition(i, points[i]);
                }
            }
        }
        //public void SetUpLine(Vector3[] points)
        //{
        //    lineRenderer.positionCount = points.Length;
        //    this.points = points;
        //}
        private void Update()
        {
            if (lineRenderer != null && points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    lineRenderer.SetPosition(i, points[i]);
                }
            }
        }
    }
}
