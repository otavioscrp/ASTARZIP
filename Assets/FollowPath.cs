using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    //determinadas as variaveis de velociade, precisão e rotação
    Transform goal;
    float speed = 5.0f;
    float accuracy = 2f;
    float rotSpeed = 3f;

    //declaração de gameObjects dos waypoints
    public GameObject wpManager;
    GameObject[] wp;
    GameObject currentNode;
    //determinado posição inicial de waypoint
    int currentWP = 0;
    Graph g;


    void Start()
    {
        //determinar componente WPMANAGER
        wp = wpManager.GetComponent<WpManager>().waypoints;
        g = wpManager.GetComponent<WpManager>().graph;

        currentNode = wp[0];
    }


    //determinados METODOS para pontos especificos do mapa, como as ruinas, heliporto e os tanques do cenario
    public void GotoHeliport()
    {
        g.AStar(currentNode, wp[1]);
        currentWP = 0;
    }

    public void GotoRuins()
    {
        g.AStar(currentNode, wp[6]);
        currentWP = 0;
    }

    public void GotoTank()
    {
        g.AStar(currentNode, wp[7]);
        currentWP = 0;
    }


    //reseta o index do trajeto dos waypoints
    private void LateUpdate()
    {

        if (g.getPathLength() == 0 || currentWP == g.getPathLength())
            return;


        currentNode = g.getPathPoint(currentWP);


        //distanciamento de posições
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        { currentWP++; }



        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z); //determinar a direção do player
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed); 


            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}