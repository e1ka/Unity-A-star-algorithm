using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathRenderer
{
    public static void GenerateRoadLine(List<Tile> road, LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = road.Count();
        for(int i = 0; i < road.Count(); i++)
        {
            lineRenderer.SetPosition(i, new Vector3(road[i].Pos.x, road[i].Pos.y + 0.3f, road[i].Pos.z));
        }
    }

    public static void ClearRoadLine(LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = 0;
    }

}
