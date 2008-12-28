using System.Linq;
using System.Data.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Coloretto.Game;

namespace Coloretto.Services.Data
{
	partial class Game
	{
		public ColorettoGame ColorettoGame
		{
			get
			{
				using (ColorettaDataContext context = new ColorettaDataContext())
				{
					Setting directorySetting = context.Settings.Where(setting => setting.Name == "GamesDir" && setting.Context == "Default").Single();
					bool directoryExists = Directory.Exists(directorySetting.Value);
					bool fileExists = File.Exists(Path.Combine(directorySetting.Value, GameId.ToString()));
					if (!directoryExists || !fileExists)
					{
						return null;
					}
					else
					{
						BinaryFormatter formatter = new BinaryFormatter();
						using (Stream fileStream = File.OpenRead(Path.Combine(directorySetting.Value, GameId.ToString())))
						{
							ColorettoGame game = formatter.Deserialize(fileStream) as ColorettoGame;
							return game;
						}
					}
				}
			}
		}
	}

	partial class ColorettaDataContext
	{
	}
}
