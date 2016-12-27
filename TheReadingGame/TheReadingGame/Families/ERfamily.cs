using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheReadingGame
{
    class ERfamily
    {
        SpriteBatch spritebatch;
        SpriteFont regFont;
        Random rgen;
        List<string> lv1;
        List<string> lv2;
        List<string> lv3;
        List<string> boss;

        public ERfamily(SpriteBatch sb, SpriteFont rf)
        {
            spritebatch = sb;
            regFont = rf;
            rgen = new Random();

            lv1 = new List<string>();
            lv2 = new List<string>();
            lv3 = new List<string>();
            boss = new List<string>();

            LoadWords();
        }
        public string GetWord(int lvl)
        {
            string word = "";

            switch (lvl)
            {
                case 1:
                    {
                        int pos = rgen.Next(0, lv1.Count);
                        word = lv1[pos];
                        break;
                    }
                case 2:
                    {
                        int pos = rgen.Next(0, lv2.Count);
                        word = lv2[pos];
                        break;
                    }
                case 3:
                    {
                        int pos = rgen.Next(0, lv3.Count);
                        word = lv3[pos];
                        break;
                    }
                case 4:
                    {
                        int pos = rgen.Next(0, boss.Count);
                        word = boss[pos];
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
            return word;
        }


        public void LoadWords()
        {
            string line = null;
            StreamReader input = new StreamReader("Families/ER.txt");
            try
            {
                //input = new StreamReader("Families/AR.txt");

                while ((line = input.ReadLine()) != null)
                {
                    if (line.Length == 3 || line.Length == 4)
                    {
                        lv1.Add(line);
                    }
                    else if (line.Length == 5 || line.Length == 6)
                    {
                        lv2.Add(line);
                    }
                    else if (line.Length == 7 || line.Length == 8)
                    {
                        lv3.Add(line);
                    }
                    else if (line.Length >= 9)
                    {
                        boss.Add(line);
                    }
                }
            }
           
            catch(Exception e)
            {
                spritebatch.DrawString(regFont,"Error with file: " + e.Message,  new Vector2(0,0), Color.White);
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
            }

        }
    }
    
}
