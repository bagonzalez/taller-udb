using ServiceMultiGame;
using System;

namespace PingPong_Eduardo
{
    class CallbackHandler : IMultiplayerCallback
    {
        GameMultiplayer game;
        string flagToken;

        public CallbackHandler(GameMultiplayer game) {
            this.game = game;            
           
        }
        
        public void GetPosition(double result)
        {
            Console.WriteLine("Result({0})", result);
        }

        public void KeyDown(string token)
        {
            Console.WriteLine("Tecla Abajo " + "usuario " + token);
            flagToken = game.playerOne;
            if (!(flagToken.Equals(token))) {
                game.Player2_moveDown();
            }
        }

        public void KeyUp(string token)
        {
            Console.WriteLine("Tecla arriba " + "usuario " + token);
            flagToken = game.playerOne;
            if (!(flagToken.Equals(token)))
            {
                game.Player2_moveUp();
            }
        }

        public void ResultMsg(string eqn)
        {
            Console.WriteLine("MSJ({0})", eqn);
        }

        public void ResultScore(double result, string token)
        {
            Console.WriteLine("Result({0} token {1})", result, token);
        }
    }
}
