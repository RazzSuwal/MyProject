using System;
using System.Collections.Generic;
using System.Linq;

public class CourseUtility
{
    // Method to calculate cosine similarity
    public static double CosineSimilarity(List<string> features1, List<string> features2)
    {
        var commonFeatures = features1.Intersect(features2).ToList();
        double dotProduct = commonFeatures.Count;

        double magnitude1 = Math.Sqrt(features1.Count);
        double magnitude2 = Math.Sqrt(features2.Count);

        if (magnitude1 == 0 || magnitude2 == 0)
        {
            return 0; // To avoid division by zero
        }

        return dotProduct / (magnitude1 * magnitude2);
    }
}
