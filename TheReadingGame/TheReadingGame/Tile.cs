 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheReadingGame
{
    class Tile
    {
        string type;
        bool moveRight;
        bool moveUp;
        Texture2D tileTexture;
        Rectangle tileBox;

        public Tile(Texture2D tt, Rectangle tb, string ty)
        {
            type = ty;
            tileTexture = tt;
            tileBox = tb;

            if (ty == "platform")
            {
                moveRight = true;
                moveUp = false;
            }
        }

        public Rectangle TileBox
        {
            get { return tileBox; }
            set { tileBox = value; }
        }
        public Texture2D TileTexture
        {
            get { return tileTexture; }
        }
        public string Type
        {
            get { return type; }
        }
        public bool MoveRight
        {
            get { return moveRight; }
            set { moveRight = value; }
        }
        public bool MoveUp
        {
            get { return moveUp; }
            set { moveUp = value; }
        }

    }
}
