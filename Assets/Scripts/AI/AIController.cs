using UnityEngine;

public class AIController : MonoBehaviour
{
    private IAI agent;
    private enum State
    {
        Patrol,
        Chase,
        Attack,
        Look,
    }
    State state;
    void Start()
    {
        agent = GetComponent<IAI>();
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Patrol:
                if (agent.IsInSearchArea())
                {
                    state = State.Chase;
                }
                else
                {
                    agent.Patrol();
                }
            break;

            case State.Chase:
                if (agent.IsInAttackArea())
                {
                    state = State.Attack;
                }
                else if (!agent.IsInSearchArea())
                {
                    state = State.Patrol;
                }
                else
                {
                    agent.Chase();
                }
            break;

            case State.Attack:
                if (agent.CanAttack())
                {
                    agent.Attack();
                }
                else if (!agent.IsInAttackArea())
                {
                    state = State.Chase;
                }
            break;

        }
    }

}
