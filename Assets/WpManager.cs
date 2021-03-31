using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Link

{
    //declaração de variaveis de gameobjects
    public enum direction { UNI, BI }//decisão de trajetoria do tanque
    public GameObject node1;
    public GameObject node2;
    public direction dir;

}


public class WpManager : MonoBehaviour
{
    public GameObject[] waypoints;//colocar os waypoints (objetos) no script
    public Link[] links;
    public Graph graph = new Graph();

    void Start()
    {

        if (waypoints.Length > 0)
        {
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }
            foreach (Link i in links)
            {
                graph.AddEdge(i.node1, i.node2);
                if (i.dir == Link.direction.BI)
                {
                    graph.AddEdge(i.node1, i.node2);
                }
            }

        }

    }

    //mostrar o trajeto na cena
    void Update()
    {
        graph.debugDraw();
    }
}
