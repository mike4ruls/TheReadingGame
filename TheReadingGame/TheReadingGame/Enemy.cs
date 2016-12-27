using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheReadingGame
{
    class Enemy : Character
    {
        private int health;
        private int totalHealth;
        private int money;
        private int level;
        private string word;
        private string family;
        ARfamily arFam;
        ERfamily erFam;
        Rectangle enemyBox;
        Rectangle enemyHealthBox;
        Rectangle enemyHealthBGBox;
        Rectangle enemyHealthBG2Box;
        Texture2D enemyTexture;
        Texture2D enemyHealth;
        Texture2D enemyHealthBG;
        Texture2D enemyHealthBG2;

        public Enemy(int hlt, int mny, int lvl, string fam, Texture2D eI, Texture2D eh, Texture2D bg, Texture2D bg2, SpriteBatch sb, SpriteFont rf)
        {
            health = hlt;
            totalHealth = hlt;
            money = mny;
            level = lvl;
            family = fam;
            enemyHealth = eh;
            enemyHealthBG = bg;
            enemyHealthBG2 = bg2;
            enemyTexture = eI;


            switch (family)
            {
                case "AR":
                    {
                        arFam = new ARfamily(sb, rf);
                        break;
                    }
                case "ER":
                    {
                        erFam = new ERfamily(sb, rf);
                        break;
                    }
            }

            enemyBox = new Rectangle(170, 60, 70, 70);
            enemyHealthBox = new Rectangle(enemyBox.X, (enemyBox.Height + enemyBox.Y) + 15, enemyBox.Width, 10);
            enemyHealthBGBox = new Rectangle(enemyHealthBox.X - 3, enemyHealthBox.Y - 3, enemyHealthBox.Width + 6, enemyHealthBox.Height + 6);
            enemyHealthBG2Box = enemyHealthBox;

            GenerateWord();
        }


        #region Properties

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
            }
        }

        public int Money
        {
            get { return money; }
        }

        public int Level
        {
            get { return level; }
        }

        public string Family
        {
            get { return family; }
        }

        public string Word
        {
            get { return word; }
        }

        public Rectangle EnemyBox
        {
            get{ return enemyBox; }
            set{ enemyBox = value; }
        }

        public Rectangle EnemyHealthBox
        {
            get { return enemyHealthBox; }
            set { enemyHealthBox = value; }
        }
        public Rectangle EnemyHealthBGBox
        {
            get { return enemyHealthBGBox; }
            set { enemyHealthBGBox = value; }
        }

        public Rectangle EnemyHealthBG2Box
        {
            get { return enemyHealthBG2Box; }
            set { enemyHealthBG2Box = value; }
        }

        public Texture2D EnemyHealth
        {
            get { return enemyHealth; }
        }

        public Texture2D EnemyTexture
        {
            get { return enemyTexture; }
        }
        public Texture2D EnemyHealthBG
        {
            get { return enemyHealthBG; }
        }

        public Texture2D EnemyHealthBG2
        {
            get { return enemyHealthBG2; }
        }

        #endregion

        public void TakeDamage(int damage)
        {
            health -= damage;
            enemyHealthBox.Width = (int)(enemyHealthBox.Width * ((double)health/(double)totalHealth));

        }

        public void GenerateWord()
        {
            word = "";

            if (arFam != null)
            {
                word = arFam.GetWord(level);
            }
            else if (erFam != null)
            {
                word = erFam.GetWord(level);
            }
        }


    }
}
