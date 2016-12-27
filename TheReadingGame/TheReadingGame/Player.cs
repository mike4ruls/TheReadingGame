using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheReadingGame
{
    public class Player
    {
        private int health;
        private int totalHealth;
        private int money;
        private int level;
        private double exp;
        private string name;
        Texture2D playerImage;
        Texture2D playerHealth;
        Texture2D playerHealthBG;
        Texture2D playerHealthBG2;
        Rectangle playerBox;
        Rectangle playerHealthBox;
        Rectangle playerHealthBGBox;
        Rectangle playerHealthBG2Box;


        public Player(int hlt, int mny, int lvl, string nm, Texture2D pi, Texture2D ph, Texture2D bg, Texture2D bg2)
        {

            health = hlt;
            totalHealth = hlt;
            money = mny;
            level = lvl;
            exp = 0.0;
            name = nm;
            playerImage = pi;
            playerHealth = ph;
            playerHealthBG = bg;
            playerHealthBG2 = bg2;


            playerBox = new Rectangle(5, 60, 70, 70);
            playerHealthBox = new Rectangle(playerBox.X, (playerBox.Height + playerBox.Y) + 15, playerBox.Width, 10);
            playerHealthBGBox = new Rectangle(playerHealthBox.X - 3, playerHealthBox.Y - 3, playerHealthBox.Width + 6, playerHealthBox.Height + 6);
            playerHealthBG2Box = playerHealthBox;
        }

        #region Properties
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public double Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public string Name
        {
            get { return name; }
        }

        public Texture2D PlayerImage
        {
            get { return playerImage; }
        }

        public Texture2D PlayerHealth
        {
            get { return playerHealth; }
        }

        public Texture2D PlayerHealthBG
        {
            get { return playerHealthBG; }
        }

        public Texture2D PlayerHealthBG2
        {
            get { return playerHealthBG2; }
        }

        public Rectangle PlayerBox
        {
            get { return playerBox; }
            set { playerBox = value; }
        }

        public Rectangle PlayerHealthBox
        {
            get { return playerHealthBox; }
            set { playerHealthBox = value; }
        }

        public Rectangle PlayerHealthBGBox
        {
            get { return playerHealthBGBox; }
            set { playerHealthBGBox = value; }
        }

        public Rectangle PlayerHealthBG2Box
        {
            get { return playerHealthBG2Box; }
            set { playerHealthBG2Box = value; }
        }


        #endregion

        public void TakeDamage(int damage)
        {
            health -= damage;
            playerHealthBox.Width = (int)(playerHealthBox.Width * ((double)health/(double)totalHealth));

        }
    }
}
