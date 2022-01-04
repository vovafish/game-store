using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameStore
{
    class Shop
    {
        // The GameStore Class contains a list of vehicles (PSGame and XBoxGame).
        // Using polymorphism, we inherit from the abstract base class game
        // and provide a method that differs between the two game types to calculate
        // their approximate value based on their age and condition

        private List<Game> gameStock;

        private int gameCurrentlyDisplayed = 0;

        public Shop()
        {
            gameStock = new List<Game>();
           
        }
        //Get method for GameCurrentlyDisplayed.
        public int GameCurrentlyDisplayed
        {
            get { return gameCurrentlyDisplayed; }
        }
        //Get method to return the number of game in stock
        public int NumberOfGames
        {
            get { return gameStock.Count; }
            
        }
         
        

        //Method to return the available games' description
        public string DescribeCurrentGame()
        {
            string description;

            //if there are any games, its description will be displayed 
            if (gameStock.Count > 0)
            {
                description = gameStock[gameCurrentlyDisplayed].Description();
            }
            else
            {
                description = "No game in stock";
            }
            return description;
        }
        // Method to add vehicle to the list. We are not currently using this in the form.
        public void AddGame(Game game)
        {
            gameStock.Add(game);
        }
        public void SortGame() 
        {
            gameStock.Sort();
        }

        public void RemoveGameAt(int index)
        {
            if (index < gameStock.Count)
            {
                gameStock.RemoveAt(index);
                //Ensure gameCurrentlyDisplayed is either zero or pointing at an existing vehicle
                LegaliseGameCurentlyDisplayed();
            }

        }
        // We ensure that gameCurrentlyDisplayed indexes a game that exists
        // (if there are any)
        private void LegaliseGameCurentlyDisplayed()
        {
            if (gameCurrentlyDisplayed > (gameStock.Count - 1))
            {
                gameCurrentlyDisplayed = gameStock.Count - 1;
                //this will -1 if stock is zero
                if (gameCurrentlyDisplayed < 0)
                {
                    gameCurrentlyDisplayed = 0; //make sure its legal or zero...
                }
            }
        }
        public bool IsPreviousGame()
        {
            if (gameCurrentlyDisplayed > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            // We could write this in one line
            //return (gameCurrentlyDisplayed > 0);
        }
        //Method to check if there is a previous game in the list.
        public bool IsNextGame()
        {
            if (gameCurrentlyDisplayed < gameStock.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StepToPreviousGame() //Method to move to the previous game in the list
        {
            if (IsPreviousGame())
            {
                gameCurrentlyDisplayed--;
            }

        }
        public void StepToNextGame()
        {
            if (IsNextGame())
            {
                gameCurrentlyDisplayed++;
            }
        }

    }
}
