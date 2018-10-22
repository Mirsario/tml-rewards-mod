﻿using HamstarHelpers.Services.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;


namespace Rewards.Logic {
	partial class KillData {
		public void AddRewardForPlayer( RewardsMod mymod, Player player, bool is_grind, bool is_expired, float reward ) {
			if( is_expired ) {
				return;
			}

			if( is_grind ) {
				reward *= mymod.Config.GrindKillMultiplier;
			}

			if( Main.netMode != 2 && mymod.Config.ShowPointsPopups ) {
				if( Math.Abs(reward) >= 0.01f ) {
					string msg = "+" + Math.Round( reward, 2 ) + " PP";
					Color color = reward > 0 ?
						is_grind ? Color.DarkGray : Color.GreenYellow :
						Color.Red;

					PlayerMessages.AddPlayerLabel( player, msg, color, 60 * 3, true, false );
				}
			}

			this.ProgressPoints += reward;

			foreach( var hook in mymod.OnPointsGainedHooks ) {
				hook( player, reward );
			}
		}


		////////////////

		public bool Spend( int points ) {
			if( this.ProgressPoints < points ) {
				return false;
			}

			this.ProgressPoints -= points;

			return true;
		}


		////////////////

		public bool CanDrawPoints( RewardsMod mymod ) {
			if( !mymod.Config.ShowPoints ) { return false; }
			if( !mymod.Config.PointsDisplayWithoutInventory ) {
				return Main.playerInventory;
			}
			return true;
		}


		public void DrawPointScore( RewardsMod mymod, SpriteBatch sb ) {
			float pos_x = mymod.Config.PointsDisplayX;
			float pos_y = mymod.Config.PointsDisplayY;
			pos_x = pos_x < 0 ? Main.screenWidth + pos_x : pos_x;
			pos_y = pos_y < 0 ? Main.screenHeight + pos_y : pos_y;
			Vector2 pos = new Vector2( pos_x, pos_y );

			string pp_str = "PP: " + (int)this.ProgressPoints;

			//sb.DrawString( Main.fontMouseText, "PP: " + (int)this.ProgressPoints, pos, mymod.Config.PointsDisplayColor );
			Utils.DrawBorderStringFourWay( sb, Main.fontMouseText, pp_str, pos.X, pos.Y, mymod.Config.PointsDisplayColor, Color.Black, default( Vector2 ), 1f );

			if( Main.mouseX >= pos_x && Main.mouseY >= pos_y && Main.mouseX < (pos_x + 40) && Main.mouseY < (pos_y + 16) ) {
				var mouse_pos = new Vector2( Main.mouseX - 160, Main.mouseY + 10 );
				sb.DrawString( Main.fontMouseText, "Progress Points (see Wayfarer)", mouse_pos, Color.White, 0f, default(Vector2), 0.75f, SpriteEffects.None, 1f );
			}
		}
	}
}
