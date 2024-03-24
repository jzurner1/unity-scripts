public Vector2 CombineVectorsWithWeights(Vector2 vectorA, float weightA, Vector2 vectorB, float weightB)
{
    float totalWeight = weightA + weightB;
    if (totalWeight <= 0)
    {
        return Vector2.zero;
    }

    float normalizedWeightA = weightA / totalWeight;
    float normalizedWeightB = weightB / totalWeight;

    Vector2 result = (vectorA * normalizedWeightA) + (vectorB * normalizedWeightB);

    return result;
}
