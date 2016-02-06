using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Planetbase;
using PBLoader;
using UnityEngine;

namespace CheatMod
{
	public class CheatMod : Mod
	{
		public override string ID { get { return "CheatMod"; } }
		public override string Name { get { return "Cheat Mod"; } }
		public override string Author { get { return "sfoy"; } }
		public override string Version { get { return "1.0.0"; } }

		private int menuState = 0;
		private int spawnAmount = 1;
		private bool menuOpen = false;
		private bool spawningResources = false;
		private Vector2 scrollPosition = Vector2.zero;
		private ResourceType selectedResource = null;
		private Rect windowRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 300, 400, 600);

		private Keybind menuKey = new Keybind("Open", "Open menu", KeyCode.P);

		// Draw window
		private void windowManager(int id)
		{
			if (id == 0)
			{
				if (menuState == 0)
				{
					// Main

					if (GUILayout.Button("Set day", GUILayout.Height(30)))
					{
						Indicator ind = GameUtil.GetPrivateField<Indicator>(EnvironmentManager.getInstance(), "mTimeIndicator");
						ind.setValue(0.0f);
					}

					if (GUILayout.Button("Set night", GUILayout.Height(30)))
					{
						Indicator ind = GameUtil.GetPrivateField<Indicator>(EnvironmentManager.getInstance(), "mTimeIndicator");
						ind.setValue(0.5f);
					}

					if (GUILayout.Button("Spawn resources", GUILayout.Height(30)))
					{
						menuState = 1;
					}

					/*
					if (GUILayout.Button("Spawn colonists", GUILayout.Height(30)))
					{
						menuState = 2;
					}
					*/

					GUILayout.Space(20);

					if (GUILayout.Button("Close", GUILayout.Height(30)))
					{
						menuOpen = false;
					}
				}
				else if (menuState == 1)
				{
					// Resource spawning

					scrollPosition = GUILayout.BeginScrollView(scrollPosition);

					foreach (var res in Game.Resources)
					{
						if (GUILayout.Button(res.Key, GUILayout.Height(30)))
						{
							selectedResource = res.Value;
						}
					}

					GUILayout.EndScrollView();

					GUILayout.Label("Selected resource: " + Game.GetResourceName(selectedResource));
					GUILayout.Label("Amount: " + spawnAmount);
					spawnAmount = (int)GUILayout.HorizontalSlider(spawnAmount, 1, 200);

					if (GUILayout.Button("Spawn", GUILayout.Height(30)))
					{
						if (selectedResource != null)
						{
							spawningResources = !spawningResources;
							menuOpen = false;
						}
					}

					GUILayout.Space(20);

					if (GUILayout.Button("Back", GUILayout.Height(30)))
					{
						menuState = 0;
					}
				}
				else if (menuState == 2)
				{
					// Spawn colonists
				}
			}
		}

		// Load keybinds
		public override void OnLoad()
		{
			Keybind.Add(this, menuKey);
		}

		// Draw GUI
		public override void OnGUI()
		{
			if (menuOpen)
			{
				windowRect = GUILayout.Window(0, windowRect, windowManager, "Cheats");
			}

			if (spawningResources)
			{
				GUI.Label(new Rect(10, Screen.height - 60, 300, 50), "Click somewhere to spawn resources");
			}
		}

		// Update
		public override void Update()
		{
			if (menuKey.IsDown())
			{
				if (Game.GetGameState() == Game.States.Game)
				{
					menuOpen = !menuOpen;
					scrollPosition = Vector2.zero;
				}
			}

			if (spawningResources)
			{
				if (Input.GetMouseButtonDown(0))
				{
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

					if (Physics.Raycast(ray, out hit, 1000.0f))
					{
						if (selectedResource != null)
						{
							for (int i = 0; i < spawnAmount; i++)
							{
								Resource.create(selectedResource, hit.point, Location.Exterior);
							}

							spawningResources = false;
						}
					}
				}
			}
		}
	}
}
