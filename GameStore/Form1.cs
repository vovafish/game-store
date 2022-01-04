using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;        // for Dierctory.GetCurrentDirectiory()

using System.Diagnostics;

namespace GameStore
{

    public partial class Form1 : Form
    {

        // A shop contains the list of games and a current index
        Shop shop;
        public Form1()
        {
            InitializeComponent();
        }

        private void txtGame_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            shop.StepToPreviousGame();
            DisplayGame();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            shop.StepToNextGame();
            DisplayGame();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            shop = new Shop();
            // Add some vehicles here...
            // In the real world, we would pick this data up from a database

            PSGame psgame = new PSGame("PlayStation 4", "The Witcher 3 Wild Hunt", "CD Projekt", new DateTime(2015, 04, 15),
               80, Game.Condition.mint, "RPG", "Yes");
            PSGame psgame1 = new PSGame("PlayStation 4", "Red Dead Redemption", "Rockstar", new DateTime(2018, 08, 21),
                122, Game.Condition.poor, "RPG", "No");
            PSGame psgame2 = new PSGame("PlayStation", "Sniper Elite V2 Remastered", "Rebellion", new DateTime(2012, 04, 02),
                122, Game.Condition.poor, "Action", "No");
            XboxGame xboxgame = new XboxGame("Xbox One", "The Sims 4", "Rebellion", new DateTime(2018, 04, 02),
                2022, Game.Condition.good, "Builder", "500G4B");
            XboxGame xboxgame1 = new XboxGame("Xbox 360", "LEGO City Undercover", "Dales", new DateTime(2008, 09, 12),
               2022, Game.Condition.good, "RPG", "256GB");
            XboxGame xboxgame2 = new XboxGame("Xbox One", "Team Sonic Racing", "Deep Inc.", new DateTime(2015, 09, 26),
               2022, Game.Condition.good, "Shooter", "500GB");
            PCGame pcgame = new PCGame("Computer", "Cunter Strike Global Offensive", "Valve", new DateTime(2012, 01, 26),
                25, Game.Condition.good, "Action", 1023020);
            shop.AddGame(xboxgame);
            shop.AddGame(xboxgame1);
            shop.AddGame(xboxgame2);
            shop.AddGame(psgame);
            shop.AddGame(psgame1);
            shop.AddGame(psgame2);
            shop.AddGame(pcgame);
            shop.DescribeCurrentGame();
            DisplayGame();
            
        }
        //Method to a display currently-viewed vehicle.
        private void DisplayGame()
        {
            lblCurrentGame.Text = string.Format("Viewing {0} of {1}",
            shop.GameCurrentlyDisplayed + 1, shop.NumberOfGames);
            txtGame.Text = shop.DescribeCurrentGame();
            
        }
       
        private void btnSort_Click(object sender, EventArgs e)
        {
            shop.SortGame();
            DisplayGame();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //shop.RemoveGameAt(shop.GameCurrentlyDisplayed);

            if (MessageBox.Show("Are you sure want to delete this game?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                shop.RemoveGameAt(shop.GameCurrentlyDisplayed);
            }
            DisplayGame();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGameList();
        }
        private void SaveGameList()
        {
            //create the save dialog and give it sensible default values

            string saveFilename = null;
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "*.txt";
            saveDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveDialog.Filter = "Game list files (text)|*.txt";
            saveDialog.FileName = "game_data.txt";

            DialogResult dr = saveDialog.ShowDialog();


            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                // this could fail so we need a try catch block around it


                try
                {
                    saveFilename = saveDialog.FileName;

                    //this is the net recipe for saving an list of serializable objects
                    //serializable means able to be sent to a filestream

                    System.IO.FileStream s = new System.IO.FileStream(saveFilename, System.IO.FileMode.Create);
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter f = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    f.Serialize(s, shop.GameCurrentlyDisplayed);
                    s.Close();


                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message, "File Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

    }
}
