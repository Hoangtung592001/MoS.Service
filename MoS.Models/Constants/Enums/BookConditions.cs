using System.Collections.Generic;

namespace MoS.Models.Constants.Enums
{
    public static class BookConditions
    {
        public static List<int> BookConditionValues = new List<int>
        {
            1,
            2,
            3
        };

        public enum BookConditionIDs
        {
            Fine = 1,
            New = 2,
            Old = 3
        }
    }
}
