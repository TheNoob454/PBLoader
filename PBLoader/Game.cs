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
		public static Dictionary<string, ResourceType> Resources = new Dictionary<string, ResourceType>()
		{
			{ "Bioplastic", ResourceTypeList.BioplasticInstance },
			{ "Coins", ResourceTypeList.CoinsInstance },
			{ "Gun", ResourceTypeList.GunInstance },
			{ "Meal", ResourceTypeList.MealInstance },
			{ "MedicalSupplies", ResourceTypeList.MealInstance },
			{ "Metal", ResourceTypeList.MetalInstance },
			{ "Ore", ResourceTypeList.OreInstance },
			{ "Spares", ResourceTypeList.SparesInstance },
			{ "Starch", ResourceTypeList.StarchInstance },
			{ "Vegetables", ResourceTypeList.VegetablesInstance },
			{ "Vitromeat", ResourceTypeList.VitromeatInstance }
		};

		/// <summary>
		/// Returns resource with name
		/// </summary>
		/// <param name="name">Name of the resource</param>
		/// <returns>Resource with name provided</returns>
		public static ResourceType GetResource(string name)
		{
			ResourceType val = null;

			if (Resources.TryGetValue(name, out val))
			{
				return val;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Returns resource name (If it's registered)
		/// </summary>
		/// <param name="resType">The resource</param>
		/// <returns>The name of the resource</returns>
		public static string GetResourceName(ResourceType resType)
		{
			var res = Resources.First(x => x.Value == resType);

			if (!res.Equals(new KeyValuePair<string, ResourceType>()))
			{
				return res.Key;
			}

			return "None";
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
