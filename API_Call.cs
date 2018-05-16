﻿using Rewards.Items;
using System;
using Terraria;


namespace Rewards {
	public static partial class RewardsAPI {
		internal static object Call( string call_type, params object[] args ) {
			switch( call_type ) {
			case "GetModSettings":
				return RewardsAPI.GetModSettings();

			case "SaveModSettingsChanges":
				RewardsAPI.SaveModSettingsChanges();
				return null;

			case "GetPoints":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				var player2 = args[0] as Player;
				if( player2 == null ) { throw new Exception( "Invalid parameter player for API call " + call_type ); }

				return RewardsAPI.GetPoints( player2 );

			case "AddPoints":
				if( args.Length < 2 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				var player3 = args[0] as Player;
				if( player3 == null ) { throw new Exception( "Invalid parameter player for API call " + call_type ); }

				if( !( args[1] is float ) ) { throw new Exception( "Invalid parameter points for API call " + call_type ); }
				float points = (int)args[1];

				RewardsAPI.AddPoints( player3, points );
				return null;

			case "ShopClear":
				RewardsAPI.ShopClear();
				return null;

			case "ShopRemoveLastPack":
				return RewardsAPI.ShopRemoveLastPack();

			case "ShopAddPack":
				if( args.Length < 1 ) { throw new Exception( "Insufficient parameters for API call " + call_type ); }

				if( !( args[0] is ShopPackDefinition ) ) { throw new Exception( "Invalid parameter pack for API call " + call_type ); }
				var pack = (ShopPackDefinition)args[0];

				RewardsAPI.ShopAddPack( pack );
				return null;

			case "SpawnWayfarer":
				RewardsAPI.SpawnWayfarer();
				return null;
			}
			
			throw new Exception("No such api call "+call_type);
		}
	}
}
