using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SnakesOnAGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D snakeTexture;
        Texture2D FoodTexture;
        Rectangle currentSquare;

        float snakemovetimer = 0f;
        float snakemovetime = 40f;

         int playerScore = 0;

        Random rand = new Random();
        List <Vector2> snake = new List <Vector2>();
        Vector2 Food = new Vector2(1,2);
        //Vector2 pellet = new Vector2(2, 2);//rand.Next(1, 10),rand.Next(1,10));
        Vector2 velocity = new Vector2(0, -1);

        Color[] colors = new Color[] { Color.Red, Color.PowderBlue, 
        Color.SteelBlue, Color.Tomato, Color.IndianRed, Color.DarkRed,Color.Wheat, Color.White,Color.Yellow,Color.Turquoise,Color.Green};

        Camera camera;

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

            Food = new Vector2(45, 29);
            snake.Add(new Vector2(40, 24));

            camera = new Camera(new Viewport(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height));
            camera.Origin = new Vector2(camera.ViewPort.Width / 2.0f, camera.ViewPort.Height);

            snakeTexture = Content.Load<Texture2D>(@"SQUARE");

            FoodTexture = Content.Load<Texture2D>(@"Food");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            camera.Update(gameTime);

            snakemovetimer += (float)gameTime.ElapsedGameTime.Milliseconds;

            if (snakemovetimer > snakemovetime)
            {

                for (int n = snake.Count - 1; n > 0; n--)
                {
                    snake[n] = snake[n - 1];

                  
                }


                snake[0] += velocity;
                 snakemovetimer = 0f;
            }
                

            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Up) && velocity.Y != 1)
            {
                velocity = new Vector2(0, -1);
            }

            if (kb.IsKeyDown(Keys.Down) && velocity.Y != -1)
            {
                velocity = new Vector2(0, 1);
            }

            if (kb.IsKeyDown(Keys.Left) && velocity.X != 1)
            {
                velocity = new Vector2(-1, 0);
            }

            if (kb.IsKeyDown(Keys.Right) && velocity.X != -1)
            {
                velocity = new Vector2(1, 0);
                
            }


            for (int g = snake.Count - 1; g > 0; g--)
            {
                if (snake[0] == snake[g])
                {
                    snake.Clear();
                    snake.Add(new Vector2(40, 24));

                    break;
                }

            }
           


            if (snake[0] == Food)
            {
                snake.Add(new Vector2(2, 1));
                Food = new Vector2(rand.Next(1, 40), (rand.Next(1, 30)));
                playerScore++;

                camera.Shake(5, 0.25f);

                //Food.Add(new Vector2(rand.Next(1,100), rand.Next(1,100)));
            }

           

            this.Window.Title = "Score : " + playerScore.ToString();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix(Vector2.One));

            for (int i = 0; i < snake.Count; i++)
            {
                spriteBatch.Draw(FoodTexture, Food * 16, Color.White);
                spriteBatch.Draw(snakeTexture, snake[i] * 16, Color.DarkRed);
                
                
               
            }
            spriteBatch.End();


            

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
