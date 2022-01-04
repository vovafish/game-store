using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameStore
{
    public class XboxGame : Game
    {
        protected string hasSSD;
        public XboxGame(string console, string gameTitle, string gameDev, DateTime releaseDate, decimal originalPrice,
            Condition condition, string gameType, string hasSSD)
            : base(console, gameTitle, gameDev, releaseDate, originalPrice, condition, gameType)
        {
            this.hasSSD = hasSSD;
        }
        // This calculation is overridden in both derived classes
        public override decimal CalculateApproximateValue()
        {
            decimal value = 0;
            // we modify the Xbox's value based on its condition
            if (condition == Condition.mint)
            {
                value = originalPrice * 0.9m; // 90% of original value
            }
            if (condition == Condition.good)
            {
                value = originalPrice * 0.8m; // 80% of original value
            }
            if (condition == Condition.fair)
            {
                value = originalPrice * 0.7m; // 70% of original value
            }
            if (condition == Condition.poor)
            {
                value = originalPrice * 0.5m; // 50% of original value
            }
            // We also take into account the Xbox’s age
            int age = CalculateApproximateAgeInYears();

            //the loop below could be re-written
            // value = value * (decimal)Math.Pow(0.9, age); 
            // we loose 10% of value for each year old... i.e. we keep 90% (0.9)
            for (int i = 0; i < age; i++)
            {
                value = value * 0.90m;
            }
            // Debug.Assert(value == alternativeValue); - was a check on alternative 
            //calculation.
            value = Decimal.Round(value, 0); // Round to the nearest pound. 
                                             // The car lot rounds this down to the nearest £100.
            value = value - (value % 100);
            // and then adds £99
            value = value + 99;
            return value;
        }

        public override string Description()
        {
            string description = base.Description() + Environment.NewLine +
            string.Format("SSD Size: {0}", hasSSD);
            return description;
        }

    }

}
