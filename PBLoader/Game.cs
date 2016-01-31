using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Planetbase;

namespace PBLoader
{
	/// <summary>
	/// Collection of utility functions for interacting with the game.
	/// </summary>
	public class Game
	{
		/// <summary>
		/// GameStates in Planetbase.
		/// </summary>
		public enum States { Credits, Game, LoadGame, LocationSelection, Logo, Settings, Title, Unknown }

		/// <summary>
		/// Resources in Planetbase.
		/// </summary>
		public enum Resources { Bioplastic, Coins, Gun, Meal, MedicalSupplies, Metal, Ore, Spares, Starch, Vegetables, Vitromeat, None }

		/// <summary>
		/// Return private field from class.
		/// </summary>
		/// <typeparam name="T">The type of field to get.</typeparam>
		/// <param name="instance">The instance of the class to get the field from.</param>
		/// <param name="name">The name of the private field.</param>
		/// <returns>Private field from class.</returns>
		public static T GetPrivateField<T>(object instance, string name)
		{
			FieldInfo var = instance.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
			return (T)var.GetValue(instance);
		}

		/// <summary>
		/// Return resource instance.
		/// </summary>
		/// <param name="res">Resource to return instance for.</param>
		/// <returns>Resource instance.</returns>
		public static ResourceType GetResource(Resources res)
		{
			if (res == Resources.Bioplastic)
			{
				return ResourceTypeList.BioplasticInstance;
			}
			else if (res == Resources.Coins)
			{
				return ResourceTypeList.CoinsInstance;
			}
			else if (res == Resources.Gun)
			{
				return ResourceTypeList.GunInstance;
			}
			else if (res == Resources.Meal)
			{
				return ResourceTypeList.MealInstance;
			}
			else if (res == Resources.MedicalSupplies)
			{
				return ResourceTypeList.MedicalSuppliesInstance;
			}
			else if (res == Resources.Metal)
			{
				return ResourceTypeList.MetalInstance;
			}
			else if (res == Resources.Ore)
			{
				return ResourceTypeList.OreInstance;
			}
			else if (res == Resources.Spares)
			{
				return ResourceTypeList.SparesInstance;
			}
			else if (res == Resources.Starch)
			{
				return ResourceTypeList.StarchInstance;
			}
			else if (res == Resources.Vegetables)
			{
				return ResourceTypeList.VegetablesInstance;
			}
			else if (res == Resources.Vitromeat)
			{
				return ResourceTypeList.VitromeatInstance;
			}

			return null;
		}

		/// <summary>
		/// Sets the game's current state.
		/// </summary>
		/// <param name="state">GameState to change to.</param>
		public static void SetGameState(GameState state)
		{
			GameManager.getInstance().setNewState(state);
		}

		/// <summary>
		/// Gets the current GameState.
		/// </summary>
		/// <returns>Current GameState as States enum; Returns States.Unknown if state is unknown.</returns>
		public static States GetGameState()
		{
			GameState state = GameManager.getInstance().getGameState();

			if (state is GameStateCredits)
			{
				return States.Credits;
			}
			else if (state is GameStateGame)
			{
				return States.Game;
			}
			else if (state is GameStateLoadGame)
			{
				return States.LoadGame;
			}
			else if (state is GameStateLocationSelection)
			{
				return States.LocationSelection;
			}
			else if (state is GameStateLogo)
			{
				return States.Logo;
			}
			else if (state is GameStateSettings)
			{
				return States.Settings;
			}
			else if (state is GameStateTitle)
			{
				return States.Title;
			}

			return States.Unknown;
		}
	}
}
