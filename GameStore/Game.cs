using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using System.IO;

namespace GameStore
{
    // Game is an abstract base class; we will derive PSGame and XboxGame classses fromt this
    public abstract class Game : IComparable
    {
        public enum Condition
        {
            poor,
            fair,
            good,
            mint
        };

        protected string console;
        protected string gameTitle;
        protected string gameDev;
        protected Condition condition;
        protected decimal originalPrice;
        protected DateTime releaseDate;
        protected string gameType;

        public Game(string console, string gameTitle, string gameDev, DateTime releaseDate,
            decimal originalPrice, Condition condition, string gameType)
        {
            this.console = console;
            this.gameTitle = gameTitle;
            this.gameDev = gameDev;
            this.releaseDate = releaseDate;
            this.originalPrice = originalPrice;
            this.condition = condition;
            this.gameType = gameType;
        }

        public string GameTitle
        {
            get { return gameTitle; }
            set { gameTitle = value; }
        }

        public int CalculateApproximateAgeInYears()
        {
            DateTime now = DateTime.Now;
            TimeSpan ageAsTimeSpan = now.Subtract(releaseDate);
            int ageInYears = ageAsTimeSpan.Days / 365;
            return ageInYears;
        }

        // This abstract method has to be implemented in the derived class.
        public abstract decimal CalculateApproximateValue();

        public virtual string Description()
        {

            /*
             Get a string describing the current game condition from the names... 
             in the Condition enumeration. 
             */
            string conditionName = Enum.GetName(typeof(Condition), condition);
            /*
            we can get the enumeration name here eg. good or fair as text as opposed to its value.
             */
            // Builds a string describing the current game.

            string description = string.Format("Platform: {0}{1}Game: {2}{3}Game Developer: {4}{5}Type of Game: {6}{7}Condition:  {8}{9}Current Value: {10:c}",
               console,
               Environment.NewLine,
               gameTitle,
               Environment.NewLine,
               gameDev,
               Environment.NewLine,
               gameType,
               Environment.NewLine,
               conditionName,
               Environment.NewLine,
               CalculateApproximateValue());

            return description;


        }

        // Implement IComparable CompareTo method - provide default sort order.
        // This will be used if we need to sort the games.

        int IComparable.CompareTo(object obj)
        {

            // IComparable returns +1, 0 or -1.
            Game otherGame = (Game)obj;
            decimal ddifferenceInPrice = this.CalculateApproximateValue() - otherGame.CalculateApproximateValue();
            // We want to return +1, 0 or -1
            return Math.Sign(ddifferenceInPrice);
        }

    }

}
