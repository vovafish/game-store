using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore
{
    public class PCGame : Game
    {
        protected int soldNewPCGames;
        //Constructor
        public PCGame(string console, string gameTitle, string gameDev, DateTime releaseDate, decimal originalPrice,
            Condition condition, string gameType, int soldNewPCGames)
            : base(console, gameTitle, gameDev, releaseDate, originalPrice, condition, gameType)
        {
            this.soldNewPCGames = soldNewPCGames;
        }
        public override decimal CalculateApproximateValue()
        {
            decimal value = 0;
            // we modify the PS's value based on its condition
            if (condition == Condition.mint)
            {
                value = originalPrice * 0.9m; // 90% of original value
            }
            if (condition == Condition.good)
            {
                value = originalPrice * 0.7m; // 70% of original value
            }
            if (condition == Condition.fair)
            {
                value = originalPrice * 0.5m; // 50% of original value
            }
            if (condition == Condition.poor)
            {
                value = originalPrice * 0.4m; // 40% of original value
            }
            // we also take into account the PSGame's age
            int age = CalculateApproximateAgeInYears();

            // the loop below could be re-written as
            // decimal alternativeValue = value * (decimal)Math.Pow(0.8, age); 
            //we loose 20% of value for each year old... i.e. we keep 80% (0.8)
            // we lose another 20% of the value every year - so we keep 80% or 0.8
            for (int i = 0; i < age; i++)
            {
                value *= 0.9m;
            }
            // this loop could be re-written as
            // value = value * (decimal)Math.Pow(0.8, age); 
            // we loose 20% of value for each year old... i.e. we keep 80% (0.8)
            value = Decimal.Round(value, 0);
            // the PS lot rounds this down to the nearest £100 
            value -= (value % 100);
            // and then adds £99
            value += 99;
            return value;
        }

        public decimal OverallIncomeFromSoldGames()
        {
            decimal value = 0;

            value = soldNewPCGames * originalPrice;
            value = Decimal.Round(value, 0);
            return value;
        }

        public override string Description()
        {
            string description = base.Description() + Environment.NewLine + string.Format("Sold OverAll: {0}{1}Overall Income: {2:c}",
               soldNewPCGames,
               Environment.NewLine,
               OverallIncomeFromSoldGames());

            return description;
        }
    }

}
