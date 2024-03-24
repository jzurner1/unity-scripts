/*
Remember to tag entities with the "Entity" tag
Also, create the `repulsionRadius`, `repulsionForce`, and `damping` variable

Note that this will cause significant lag with a large number of entities
*/

public void RepelFromOtherEntities()
{
    GameObject[] entities = GameObject.FindGameObjectsWithTag("Entity");

    foreach (GameObject other in entities)
    {
        if (other == gameObject) continue;  // skip self

        Vector2 directionToOther = other.transform.position - transform.position;
        float distanceToOther = directionToOther.magnitude;

        if (distanceToOther < repulsionRadius && distanceToOther != 0)
        {
            Vector2 repulsionDirection = directionToOther.normalized;
            float repulsionMagnitude = (1 - (distanceToOther / repulsionRadius)) * repulsionForce * damping;
            Vector2 repulsionVector = -repulsionDirection * repulsionMagnitude;

            transform.position += (Vector3)repulsionVector * Time.deltaTime;
        }
    }
}
