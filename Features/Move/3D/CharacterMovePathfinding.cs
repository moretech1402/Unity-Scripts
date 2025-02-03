using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovePathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public void MoveTo(Vector3 target){
        navMeshAgent.SetDestination(target);
    }

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
}
