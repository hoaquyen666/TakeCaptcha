using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Assembly_CSharp.Mod
{
    internal class CustomGraphics
    {
        public static void paintMobSquare(mGraphics g, Mob mob)
        {
            if (mob == null) return;

            if (mob.hp <= 0 || mob.status == 0 || mob.status == 1)
            {
                return;
            }

            g.setColor(0xFF0000);
            int newWidth = mob.w * 2 / 3;
            int newHeight = mob.h * 2 / 3;

            int x = mob.x - newWidth / 2;
            int y = mob.y - mob.w + (mob.w - newHeight);

            g.drawRect(x - 1, y, newWidth, newHeight);

            string hpText = mob.hp + "/" + mob.maxHp;
            mFont.tahoma_7_yellow.drawString(g, "HP:" + hpText, mob.x, mob.y - mob.w - 5, 2);

            string mobName = "Unknown";
            if (Mob.arrMobTemplate != null &&
                    mob.templateId >= 0 &&
                    mob.templateId < Mob.arrMobTemplate.Length &&
                    Mob.arrMobTemplate[mob.templateId] != null &&
                    Mob.arrMobTemplate[mob.templateId].name != null)
            {
                mobName = Mob.arrMobTemplate[mob.templateId].name;
            }

            mFont.tahoma_7_red.drawString(g, mobName, mob.x, mob.y - mob.w - 15, 2);
        }

        public static void paintCharSquare(mGraphics g, Char @char)
        {
            if (@char == null) return;

            g.setColor(0xFFFFFF);
            int newWidth = @char.cw * 2 / 3;
            int newHeight = @char.ch * 2 / 3;
            int x = @char.cx - newWidth / 2;
            int y = @char.cy - @char.cw + (@char.cw - newHeight);

            g.drawRect(x - 1, y, newWidth, newHeight);
            string charname = @char.cName + "\n" + @char.charID;
            //string charid = $"{@char.charID}";
            mFont.tahoma_7b_green.drawString(g, charname, @char.cx, @char.cy - @char.cw - 20, 2);
        }

        public static void paintNPCSquare(mGraphics g, Npc npc)
        {
            if (npc == null) return;
            g.setColor(0xFF00FF);
            int newWidth = npc.cw * 2 / 3;
            int newHeight = npc.ch * 2 / 3;
            int x = npc.cx - newWidth / 2;
            int y = npc.cy - npc.cw + (npc.cw - newHeight);

            g.drawRect(x - 1, y, newWidth, newHeight);
        }

        public static void paintGridTerrain(mGraphics g)
        {
            g.setColor(0x00FFFF);
            for (int n = GameScr.gssx; n < GameScr.gssxe; n++)
            {
                for (int m = GameScr.gssy; m < GameScr.gssye; m++)
                {
                    bool flag = TileMap.maps[m * TileMap.tmw + n] != 0 &&
                        ((!TileMap.tileTypeAt(n * 24, (m + 1) * 24, 2) &&
                        !TileMap.tileTypeAt(n * 24, (m + 2) * 24, 2) &&
                        !TileMap.tileTypeAt(n * 24, m * 24, 2)) || TileMap.tileTypeAt(n * 24, m * 24, 2));
                    if (flag)
                    {
                        bool flag2 = n > 0;
                        if (flag2)
                        {
                            g.drawRect(n * TileMap.size, m * (int)TileMap.size + 8, 24, 24);
                        }
                        else
                        {
                            g.drawRect(n * TileMap.size, m * (int)TileMap.size + 8, 24, 24);
                        }
                    }

                }
            }
        }
    }
}
