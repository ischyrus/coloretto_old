using System;
using System.Collections.Generic;
using System.Text;

namespace CardManagement.Coloretto
{
    public class Constants
    {
        public const string ColorettoDeckProviderName = "ColorettoDeckProvider";
        public const int DefaultLastCycleCard = 1;
        public const int DefaultNumberOfWilds = 3;
        public const int DefaultNumberOfPlus2 = 10;
        public const int DefaultNumberOfEachColor = 9;
        public const int DefaultNumberOfCardsOnEndBeforeLastCycleCard = 15;

        /// <summary>
        /// The default max put on the number of points a person can get from a single color
        /// </summary>
        public const int DefaultMaxNumberOfScoredCardsPerColor = 6;
    }
}
