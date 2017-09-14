using ServiceMultiGame;
using System;

namespace PingPong_Eduardo
{
    class CallbackHandler : IMultiplayerCallback
    {
        public void GetPosition(double result)
        {
            Console.WriteLine("Result({0})", result);
        }

        public void KeyDown(string token)
        {
            Console.WriteLine("Tecla Abajo " + "usuario " + token);
        }

        public void KeyUp(string token)
        {
            Console.WriteLine("Tecla arriba " + "usuario " + token);
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
