﻿namespace uTinyRipper.Classes.Fonts
{
	public struct CharacterInfo : IAssetReadable
	{
		/// <summary>
		/// 1.6.0 and greater
		/// </summary>
		public static bool IsReadIndex(Version version)
		{
			return version.IsGreaterEqual(1, 6);
		}
		/// <summary>
		/// 1.6.0 to 5.3.0 exclusive
		/// </summary>
		public static bool IsReadWidth(Version version)
		{
			return version.IsGreaterEqual(1, 6) && version.IsLess(5, 3);
		}
		/// <summary>
		/// 5.3.0 and greater 
		/// </summary>
		public static bool IsReadAdvance(Version version)
		{
			return version.IsGreaterEqual(5, 3);
		}
		/// <summary>
		/// 4.0.0 and greater
		/// </summary>
		public static bool IsReadFlipped(Version version)
		{
			return version.IsGreaterEqual(4);
		}

		private static int GetSerializedVersion(Version version)
		{
			if (Config.IsExportTopmostSerializedVersion)
			{
				return 2;
			}

			if (version.IsGreaterEqual(1, 6))
			{
				return 2;
			}
			return 1;
		}

		public void Read(AssetReader reader)
		{
			if (IsReadIndex(reader.Version))
			{
				Index = reader.ReadInt32();
			}
			UV.Read(reader);
			Vert.Read(reader);
			
			if (IsReadWidth(reader.Version))
			{
				Width = reader.ReadSingle();
			}
			if (IsReadAdvance(reader.Version))
			{
				Advance = reader.ReadSingle();
			}
			if (IsReadFlipped(reader.Version))
			{
				Flipped = reader.ReadBoolean();
				reader.AlignStream(AlignType.Align4);
			}
		}

		public int Index { get; private set; }
		public float Width  { get; private set; }
		public float Advance { get; private set; }
		public bool Flipped { get; private set; }

		public Rectf UV;
		public Rectf Vert;
	}
}
