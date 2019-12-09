using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonoGame
{
    class ScoreHUD
    {
        Texture2D scoreTexture;
        Texture2D scoreNumberTexture;
        ImageDrawer score;
        ImageDrawer scoreNumber;
        int currentScore = 0;


        public ScoreHUD(Texture2D scoreTextureIn, Texture2D scoreNumberTextureIn)
        {
            scoreTexture = scoreTextureIn;
            scoreNumberTexture = scoreNumberTextureIn;

            score = new ImageDrawer(scoreTextureIn, new Vector2(10, 100), new Vector2(200, 70), new Vector2(400, 141));
            scoreNumber = new ImageDrawer(scoreNumberTextureIn, new Vector2(150, 100), new Vector2(64, 64), new Vector2(16,16), new Vector2(currentScore * 16, 0));
        }

        public void Update(int playerScoreIn)
        {
            currentScore = playerScoreIn;
            scoreNumber = new ImageDrawer(scoreNumberTexture, new Vector2(220, 100), new Vector2(64, 64), new Vector2(16, 16), new Vector2(currentScore * 16, 0));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            score.Draw(spriteBatch);
            scoreNumber.Draw(spriteBatch);
        }
    }
}
