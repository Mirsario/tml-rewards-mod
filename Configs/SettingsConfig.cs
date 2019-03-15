﻿using HamstarHelpers.Components.Config;
using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework;
using Rewards.Items;
using System;
using System.Collections.Generic;


namespace Rewards.Configs {
	public partial class RewardsSettingsConfigData : ConfigurationDataBase {
		public static string ConfigFileName => "Rewards Config.json";


		////////////////

		public string VersionSinceUpdate = "";

		public bool DebugModeInfo = false;
		public bool DebugModePPInfo = true;	// Defaults true
		public bool DebugModeKillInfo = false;
		public bool DebugModeEnableCheats = false;
		public bool DebugModeSaveKillsAsJson = false;

		public bool ShowPoints = true;
		public bool ShowPointsPopups = true;

		public bool PointsDisplayWithoutInventory = false;
		public int PointsDisplayX = 448;
		public int PointsDisplayY = 1;
		public byte[] PointsDisplayColorRGB = new byte[] { Color.YellowGreen.R, Color.YellowGreen.G, Color.YellowGreen.B };

		public bool SharedRewards = true;

		public bool InstantWayfarer = false;
		
		public bool UseUpdatedWorldFileNameConvention = true;

		////

		[PacketProtocolIgnore]
		public string _OBSOLETE_SETTINGS_BELOW_ = "";

		[PacketProtocolIgnore]
		public float GrindKillMultiplier = 0.1f;

		[PacketProtocolIgnore]
		public float GoblinInvasionReward = 15f;
		[PacketProtocolIgnore]
		public float FrostLegionInvasionReward = 15f;
		[PacketProtocolIgnore]
		public float PirateInvasionReward = 25f;
		[PacketProtocolIgnore]
		public float MartianInvasionReward = 200f;
		[PacketProtocolIgnore]
		public float PumpkingMoonWaveReward = 10f;
		[PacketProtocolIgnore]
		public float FrostMoonWaveReward = 10f;

		[PacketProtocolIgnore]
		public IDictionary<string, float> NpcRewards = new Dictionary<string, float>();
		[PacketProtocolIgnore]
		public IDictionary<string, int> NpcRewardTogetherSets = new Dictionary<string, int>();
		[PacketProtocolIgnore]
		public ISet<string> NpcRewardRequiredAsBoss = new HashSet<string>();
		[PacketProtocolIgnore]
		public IDictionary<string, string> NpcRewardNotGivenAfterNpcKilled = new Dictionary<string, string>();

		[PacketProtocolIgnore]
		public IList<ShopPackDefinition> ShopLoadout = new List<ShopPackDefinition>();



		////////////////

		public void SetDefaults() {
			this.PointsDisplayColorRGB = new byte[] { Color.YellowGreen.R, Color.YellowGreen.G, Color.YellowGreen.B };
		}

		////

		public bool CanUpdateVersion( out bool formatChanged ) {
			if( this.VersionSinceUpdate == "" ) {
				formatChanged = false;
				return true;
			}

			var versSince = new Version( this.VersionSinceUpdate );
			bool canUpdate = versSince < RewardsMod.Instance.Version;

			formatChanged = versSince < new Version( 2, 1, 0 );
			return canUpdate;
		}
		
		public void UpdateToLatestVersion() {
			var mymod = RewardsMod.Instance;

			var versSince = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();
		}
	}
}
