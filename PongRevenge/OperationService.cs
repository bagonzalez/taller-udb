using ServiceMultiGame;
using System;

namespace PongRevenge
{
	class OperationService : IMultiplayerCallback
	{
		OperationService game;
		string flagToken, tokenPlayer_2;

		public OperationService()
		{
			

		}

		public void GetPosition(double result)
		{
			Console.WriteLine("Result({0})", result);
		}

		public void KeyDown(string token)
		{
			Console.WriteLine("Tecla Abajo " + "usuario " + token);
			flagToken = GameMultiplayerWindow.player1Token;
			if (!(flagToken.Equals(token)))
			{
				GameMultiplayerWindow.visitanteY = 1;
			}
		}

		public void KeyUp(string token)
		{
			Console.WriteLine("Tecla arriba " + "usuario " + token);
			flagToken = GameMultiplayerWindow.player1Token;
			if (!(flagToken.Equals(token)))
			{
				GameMultiplayerWindow.visitanteY = -1;
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
