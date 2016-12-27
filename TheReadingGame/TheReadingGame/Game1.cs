using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheReadingGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont regFont;
        KeyboardState kbState;
        KeyboardState previousState;
        Texture2D backgroundTexture1;
        //Texture2D backgroundTexture2;
        //Texture2D backgroundTexture3;
        Texture2D gameOverTexture;
        TileBuild levelGen;
        Color enemyText;
        Color playerText;
        Camera camera;
        Player p1;
        Enemy arGrunt;
        Vector2 camPos;
        string word;
        bool blink;
        bool collideFall;
        bool collideWall;
        bool death;
        //int loopBackground;
        int lvlTiles;
        int correctTimer;
        int timerBlink;
        int timer;
        int gravityTimer;
        int gravityDelay;
        int platformSpeed;


        public enum GameStates
        {
            StartMenu,
            WorldMap,
            Game,
            GamePause
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Loading all content

            regFont = Content.Load<SpriteFont>("Calibri_15");
            

            #endregion


            blink = true;
            collideFall = false;
            collideWall = false;
            death = false;
            //loopBackground = 0;
            lvlTiles = 400;
            timerBlink = 0;
            correctTimer = 0;
            timer = 0;
            gravityTimer = 0;
            gravityDelay = 0;
            platformSpeed = 1;
            enemyText = Color.White;
            playerText = Color.Green;
            word = "";
            camera = new Camera();
            camPos = new Vector2(0, 0);
            backgroundTexture1 = Content.Load<Texture2D>("city.jpg");
            //backgroundTexture2 = Content.Load<Texture2D>("city.jpg");
            //backgroundTexture3 = Content.Load<Texture2D>("city.jpg");
            gameOverTexture = Content.Load<Texture2D>("gameover.png");
            levelGen = new TileBuild(Content.Load<Texture2D>("redblock.jpg"), Content.Load<Texture2D>("greenblock.jpg"), Content.Load<Texture2D>("greyblock.jpg"), graphics);
            arGrunt = new Enemy(100, 10, 2, "AR", Content.Load<Texture2D>("gunguy.png"), Content.Load<Texture2D>("redblock.jpg"), Content.Load<Texture2D>("whiteblock.jpg"), Content.Load<Texture2D>("greyblock.jpg"), spriteBatch, regFont);
            p1 = new Player(100, 0, 0, "Guy", Content.Load<Texture2D>("pizza.png"), Content.Load<Texture2D>("greenblock.jpg"), Content.Load<Texture2D>("whiteblock.jpg"), Content.Load<Texture2D>("greyblock.jpg"));

            levelGen.BuildLevel(lvlTiles);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            kbState = Keyboard.GetState();

            Keys[] letters = kbState.GetPressedKeys();

            Gravity(p1);

            p1.PlayerHealthBox = new Rectangle(p1.PlayerBox.X, (p1.PlayerBox.Height + p1.PlayerBox.Y) + 15, p1.PlayerHealthBox.Width, 10);
            p1.PlayerHealthBGBox = new Rectangle(p1.PlayerHealthBox.X - 3, p1.PlayerHealthBox.Y - 3, p1.PlayerHealthBGBox.Width, p1.PlayerHealthBGBox.Height);
            p1.PlayerHealthBG2Box = new Rectangle(p1.PlayerHealthBox.X, p1.PlayerHealthBox.Y, p1.PlayerHealthBG2Box.Width, p1.PlayerHealthBG2Box.Height);

            if (death != true)
            {
                if (kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.Down) || kbState.IsKeyDown(Keys.Space))
                {
                    Move(kbState);
                }
            }
            

            if (correctTimer == 0)
            {
                if (timer == 0)
                {
                    blink = true;
                    enemyText = Color.White;
                    playerText = Color.Green;

                    if (kbState != previousState)
                    {
                        if (kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.Up) || kbState.IsKeyDown(Keys.Down) || kbState.IsKeyDown(Keys.Space) || kbState.IsKeyDown(Keys.LeftShift))
                        {

                        }
                        else
                        {
                            for (int i = 0; i < letters.Length; i++)
                            {
                                word += letters[i];
                                word = word.ToLower();
                            }
                            for (int i = 0; i < word.Length; i++)
                            {
                                if (word.Length > arGrunt.Word.Length || word[i] != arGrunt.Word[i])
                                {
                                    playerText = Color.Red;
                                    if (timer != 0)
                                    {

                                    }
                                    else
                                    {
                                        p1.TakeDamage(arGrunt.Level + 10);
                                    }

                                    timerBlink = 20;
                                    timer = 120;

                                }
                                if (word.Length == arGrunt.Word.Length)
                                {
                                    enemyText = Color.Green;
                                    if (correctTimer != 0)
                                    {

                                    }
                                    else
                                    {
                                        arGrunt.TakeDamage(10);
                                    }
                                    correctTimer = 40;
                                }
                            }
                        }     
                    }
                }
                else
                {
                    if (timerBlink == 0)
                    {
                        if (blink)
                        {
                            blink = false;
                            timerBlink = 20;
                        }
                        else
                        {
                            blink = true;
                            timerBlink = 20;
                        }
                    }

                    timerBlink -= 1;
                    timer -= 1;
                }
                if (timer == 2)
                {
                    word = "";
                }
            
            }
            else
            {                
                correctTimer -= 1;
                if (correctTimer == 0)
                {
                    
                    word = "";
                    arGrunt.GenerateWord();
                }
            }

            if (p1.PlayerBox.Y > GraphicsDevice.Viewport.Height + 30)
            {
                p1.Health = 0;
            }

            if (p1.Health == 0)
            {
                death = true;
            }

            camPos = camera.Update(p1.PlayerBox.X, p1.PlayerBox.Y);

           MoveEnviornment();

            previousState = kbState;


                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);

            for (int i = 0; i < 20; i++)
            {
                spriteBatch.Draw(backgroundTexture1, new Rectangle(GraphicsDevice.Viewport.Width * i, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            }
                
            
            if (death)
            {
                spriteBatch.Draw(gameOverTexture, new Rectangle((int)camPos.X + (GraphicsDevice.Viewport.Width / 14), GraphicsDevice.Viewport.Height / 7, GraphicsDevice.Viewport.Width - 100, GraphicsDevice.Viewport.Height - 150), Color.White);
            }
            else
            {
                spriteBatch.Draw(gameOverTexture, new Rectangle((int)camPos.X + (GraphicsDevice.Viewport.Width / 14), GraphicsDevice.Viewport.Height / 7, GraphicsDevice.Viewport.Width - 100, GraphicsDevice.Viewport.Height - 150), Color.Transparent);
            }
            

            for (int i = 0; i < levelGen.Build.Count; i++) 
            {
                spriteBatch.Draw(levelGen.Build[i].TileTexture, levelGen.Build[i].TileBox, Color.White);
            }


                if (blink)
                {
                    spriteBatch.DrawString(regFont, word, new Vector2(arGrunt.EnemyBox.X + ((arGrunt.EnemyBox.Width - (word.Length * 9)) / 2), arGrunt.EnemyBox.Y - 32), playerText);
                }            
            spriteBatch.DrawString(regFont, arGrunt.Word, new Vector2 (arGrunt.EnemyBox.X + ((arGrunt.EnemyBox.Width - (arGrunt.Word.Length * 9)) / 2), arGrunt.EnemyBox.Y - 17), enemyText);
            spriteBatch.Draw(arGrunt.EnemyTexture, arGrunt.EnemyBox, Color.White);
            spriteBatch.Draw(arGrunt.EnemyHealthBG, arGrunt.EnemyHealthBGBox, Color.White);
            spriteBatch.Draw(arGrunt.EnemyHealthBG2, arGrunt.EnemyHealthBG2Box, Color.White);
            spriteBatch.Draw(arGrunt.EnemyHealth, arGrunt.EnemyHealthBox, Color.White);

            spriteBatch.DrawString(regFont, p1.Name, new Vector2(p1.PlayerBox.X + ((p1.PlayerBox.Width - (p1.Name.Length *9)) / 2), p1.PlayerBox.Y - 17), Color.White);
            spriteBatch.Draw(p1.PlayerImage, p1.PlayerBox, Color.White);
            spriteBatch.Draw(p1.PlayerHealthBG, p1.PlayerHealthBGBox, Color.White);
            spriteBatch.Draw(p1.PlayerHealthBG2, p1.PlayerHealthBG2Box, Color.White);
            spriteBatch.Draw(p1.PlayerHealth, p1.PlayerHealthBox, Color.White);
            



            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void Move(KeyboardState kbstate)
        {
            int run = 1;

            if (kbstate.IsKeyDown(Keys.LeftShift))
            {
                run = 2;
            }
            if (kbstate.IsKeyDown(Keys.Left))
            {
                p1.PlayerBox = new Rectangle (p1.PlayerBox.X - (3*run), p1.PlayerBox.Y, p1.PlayerBox.Width, p1.PlayerBox.Height);
            }
            if (kbstate.IsKeyDown(Keys.Right))
            {
                p1.PlayerBox = new Rectangle(p1.PlayerBox.X + (3*run), p1.PlayerBox.Y, p1.PlayerBox.Width, p1.PlayerBox.Height);
            }
            if (kbstate.IsKeyDown(Keys.Up))
            {
                /*
                p1.PlayerBox = new Rectangle(p1.PlayerBox.X, p1.PlayerBox.Y - 3, p1.PlayerBox.Width, p1.PlayerBox.Height);
                 */
            }
            if (kbstate.IsKeyDown(Keys.Down))
            {
                /*
                p1.PlayerBox = new Rectangle(p1.PlayerBox.X, p1.PlayerBox.Y + 3, p1.PlayerBox.Width, p1.PlayerBox.Height);
                */
            }

            if (collideFall)
            {
                if (kbstate.IsKeyDown(Keys.Space))
                {
                    p1.PlayerBox = new Rectangle(p1.PlayerBox.X, p1.PlayerBox.Y - 5, p1.PlayerBox.Width, p1.PlayerBox.Height);
                    gravityTimer = -3;
                    gravityDelay = 8;
                }
            }
            
        }

        public void Gravity(Player person)
        {
            Collision(person);
            if (collideFall)
            {
                gravityTimer = 0;
            }
            else
            {
                if (gravityDelay == 0)
                {
                    gravityTimer += 1;
                    gravityDelay = 8;
                }
                else
                {
                    gravityDelay -= 1;
                }
                    int fall;
                    if (gravityTimer < 0)
                    {
                        fall = (int)person.PlayerBox.Y + (gravityTimer * gravityTimer) * -1;
                    }
                    else
                    {
                        fall = person.PlayerBox.Y + (gravityTimer * gravityTimer);
                    }
                    person.PlayerBox = new Rectangle(person.PlayerBox.X, fall, person.PlayerBox.Width, person.PlayerBox.Height);
                    
                
                
                
            }

        }

         public void Collision(Player prson)
        {
            collideFall = false;
            collideWall = false;

            for(int i = 0; i < levelGen.Build.Count; i++)
            {
                //collide = prson.PlayerBox.Intersects(levelGen.Build[i].TileBox);
                if ((prson.PlayerBox.Y + prson.PlayerBox.Height) >= levelGen.Build[i].TileBox.Y)
                {
                    for (int j = levelGen.Build[i].TileBox.X; j <= levelGen.Build[i].TileBox.X + levelGen.Build[i].TileBox.Width; j++)
                    {
                        if (prson.PlayerBox.X + (prson.PlayerBox.Width / 2) == j)
                        {
                            if (prson.PlayerBox.Y < levelGen.Build[i].TileBox.Y)
                            {
                                prson.PlayerBox = new Rectangle(prson.PlayerBox.X, levelGen.Build[i].TileBox.Y - prson.PlayerBox.Height, prson.PlayerBox.Width, prson.PlayerBox.Height);
                                collideFall = true;
                                switch (levelGen.Build[i].Type)
                                {
                                    case "normal":
                                        {
                                            break;
                                        }
                                    case "bounce":
                                        {
                                            gravityTimer = -5;
                                            collideFall = false;
                                            break;
                                        }
                                    case "platform":
                                        {
                                            if (levelGen.Build[i].MoveRight)
                                            {
                                                prson.PlayerBox = new Rectangle(prson.PlayerBox.X + platformSpeed, prson.PlayerBox.Y, prson.PlayerBox.Width, prson.PlayerBox.Height);
                                            }
                                            else
                                            {
                                                prson.PlayerBox = new Rectangle(prson.PlayerBox.X - platformSpeed, prson.PlayerBox.Y, prson.PlayerBox.Width, prson.PlayerBox.Height);
                                            }
                                            if (levelGen.Build[i].MoveUp)
                                            {
                                                prson.PlayerBox = new Rectangle(prson.PlayerBox.X, prson.PlayerBox.Y - platformSpeed, prson.PlayerBox.Width, prson.PlayerBox.Height);
                                    
                                            }
                                            else
                                            {

                                            }
                                       
                                            break;
                                        }
                                       
                                }
                                break;
                            }
                            if (prson.PlayerBox.Y < levelGen.Build[i].TileBox.Y + levelGen.Build[i].TileBox.Height && prson.PlayerBox.Y > levelGen.Build[i].TileBox.Y + (levelGen.Build[i].TileBox.Height/2))
                            {
                                prson.PlayerBox = new Rectangle(prson.PlayerBox.X, levelGen.Build[i].TileBox.Y + levelGen.Build[i].TileBox.Height, prson.PlayerBox.Width, prson.PlayerBox.Height);
                                gravityTimer = 0;
                            }
                            
                        }
                    }   
                }
                for (int j = levelGen.Build[i].TileBox.Y; j <= levelGen.Build[i].TileBox.Y + levelGen.Build[i].TileBox.Height; j++)
                {
                    if (prson.PlayerBox.Y + (prson.PlayerBox.Height - 2) == j)
                    {
                        if (prson.PlayerBox.X + prson.PlayerBox.Width >= levelGen.Build[i].TileBox.X && prson.PlayerBox.X + prson.PlayerBox.Width <= levelGen.Build[i].TileBox.X + 20)
                        {
                            prson.PlayerBox = new Rectangle(levelGen.Build[i].TileBox.X - prson.PlayerBox.Width, prson.PlayerBox.Y, prson.PlayerBox.Width, prson.PlayerBox.Height);
                            collideWall = true;
                        }
                        else if (prson.PlayerBox.X <= levelGen.Build[i].TileBox.X + levelGen.Build[i].TileBox.Width && prson.PlayerBox.X >= levelGen.Build[i].TileBox.X + (levelGen.Build[i].TileBox.Width - 20))
                        {
                            prson.PlayerBox = new Rectangle(levelGen.Build[i].TileBox.X + levelGen.Build[i].TileBox.Width, prson.PlayerBox.Y, prson.PlayerBox.Width, prson.PlayerBox.Height);
                            collideWall = true;
                        }

                    }
                    
                }
            }
        }

         public void MoveEnviornment()
         {
             for (int i = 0; i < levelGen.Build.Count; i++)
             {
                 string type = levelGen.Build[i].Type;
                 switch (type)
                 {
                     case "normal":
                         {
                             break;
                         }
                     case "bounce":
                         {
                             break;
                         }
                     case "platform":
                         {
                             if (levelGen.Build[i].MoveRight)
                             {
                                 levelGen.Build[i].TileBox = new Rectangle(levelGen.Build[i].TileBox.X + platformSpeed, levelGen.Build[i].TileBox.Y, levelGen.Build[i].TileBox.Width, levelGen.Build[i].TileBox.Height);
                                 for(int j = 0; j < levelGen.Build.Count; j++)
                                 {
                                     if (levelGen.Build[i].TileBox == levelGen.Build[j].TileBox)
                                     {

                                     }
                                     else if (levelGen.Build[i].TileBox.Intersects(levelGen.Build[j].TileBox))
                                     {
                                         if (levelGen.Build[j].Type != "blank")
                                         {
                                             levelGen.Build[i].MoveRight = false;
                                         } 
                                     }
                                 }
                                 
                             }
                             else
                             {
                                 levelGen.Build[i].TileBox = new Rectangle(levelGen.Build[i].TileBox.X - platformSpeed, levelGen.Build[i].TileBox.Y, levelGen.Build[i].TileBox.Width, levelGen.Build[i].TileBox.Height);
                                 for (int j = 0; j < levelGen.Build.Count; j++)
                                 {
                                     if (levelGen.Build[i].TileBox == levelGen.Build[j].TileBox)
                                     {

                                     }
                                     else if (levelGen.Build[i].TileBox.Intersects(levelGen.Build[j].TileBox))
                                     {
                                         if (levelGen.Build[j].Type != "blank")
                                         {
                                             levelGen.Build[i].MoveRight = true;
                                         }          
                                     }
                                 }
                             }
                             
                             break;
                         }
                 }
             }
         }

    }
}
