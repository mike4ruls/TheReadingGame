using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheReadingGame
{
    class TileBuild
    {
        Random rgen;
        int tileSize;
        List<Tile> build;
        Texture2D tile1;
        Texture2D tile2;
        Texture2D tile3;
        GraphicsDeviceManager graphics;
        public TileBuild(Texture2D t1, Texture2D t2, Texture2D t3, GraphicsDeviceManager gp)
        {
            tile1 = t1;
            tile2 = t2;
            tile3 = t3;
            tileSize = 50;
            rgen = new Random();
            build = new List<Tile>();
            graphics = gp;

        }

        public List<Tile> Build
        {
            get { return build; }
        }

        public void BuildLevel(int tiles)
        {
            for (int i = 0; i < tiles; i++)
            {

                int ranBlank = rgen.Next(0, 1000);

                if (i <= 1)
                {
                    build.Add(new Tile(tile1, new Rectangle(0, graphics.GraphicsDevice.Viewport.Height - tileSize, tileSize, tileSize), "normal"));
                }
                else if (ranBlank < 900)
                {

                    int ranTile = rgen.Next(0, 15001);
                    int ranSize = rgen.Next(0, 801);
                    int size = 1;

                    if (ranSize > 750)
                    {
                        size = 3;
                    }

                    if (ranTile < 13000)
                    {                        
                        build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height - (tileSize * size), tileSize, tileSize * size), "normal"));                        
                    }

                    else if (ranTile < 14400)
                    {                       
                        build.Add(new Tile(tile2, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height - tileSize, tileSize, tileSize), "bounce"));                      
                    }

                    else if (ranTile < 15001)
                    {
                        int ranSpace = rgen.Next(5, 7);

                        build.Add(new Tile(tile3, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height - (tileSize / 2), tileSize + (tileSize / 2), tileSize / 3), "platform"));
                        
                        for (int j = 0; j < ranSpace; j++)
                        {
                            i += 1;
                            build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height + 100, tileSize, tileSize), "blank"));                       
                        }

                    }
                }
                else
                {

                    int ranNumBlank = rgen.Next(0, 101);
                    if (ranNumBlank < 20)
                    {
                        build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height + 100, tileSize, tileSize), "blank"));
                    }
                    else if (ranNumBlank < 80)
                    {
                        build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height + 100, tileSize, tileSize), "blank"));
                        i += 1;
                        build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height + 100, tileSize, tileSize), "blank"));
                    }
                    else if (ranNumBlank < 101)
                    {
                        build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height + 100, tileSize, tileSize), "blank"));
                        i += 1;
                        build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height + 100, tileSize, tileSize), "blank"));
                        i += 1;
                        build.Add(new Tile(tile1, new Rectangle(build[i - 1].TileBox.X + tileSize, graphics.GraphicsDevice.Viewport.Height + 100, tileSize, tileSize), "blank"));
                    }
                }

                    

                

                    






                
            }
        }
     }
 }

