using System;
using Assets.src.g;

namespace Assets.src.f
{
	internal class Controller2
	{
		public static void readMessage(Message msg)
		{
			try
			{
				switch (msg.command)
				{
				case sbyte.MinValue:
					readInfoEffChar(msg);
					break;
				case sbyte.MaxValue:
					readInfoRada(msg);
					break;
				case 114:
					try
					{
						string text2 = msg.reader().readUTF();
						mSystem.curINAPP = msg.reader().readByte();
						mSystem.maxINAPP = msg.reader().readByte();
						break;
					}
					catch (Exception)
					{
						break;
					}
				case 113:
				{
					int loop = 0;
					int layer = 0;
					int id = 0;
					short x = 0;
					short y = 0;
					short loopCount = -1;
					try
					{
						loop = msg.reader().readByte();
						layer = msg.reader().readByte();
						id = msg.reader().readUnsignedByte();
						x = msg.reader().readShort();
						y = msg.reader().readShort();
						loopCount = msg.reader().readShort();
					}
					catch (Exception)
					{
					}
					EffecMn.addEff(new Effect(id, x, y, layer, loop, loopCount));
					break;
				}
				case 48:
				{
					sbyte b8 = msg.reader().readByte();
					ServerListScreen.ipSelect = b8;
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					Session_ME.gI().close();
					GameCanvas.endDlg();
					ServerListScreen.waitToLogin = true;
					break;
				}
				case 31:
				{
					int num18 = msg.reader().readInt();
					sbyte b15 = msg.reader().readByte();
					if (b15 == 1)
					{
						short smallID = msg.reader().readShort();
						sbyte b16 = -1;
						int[] array = null;
						short wimg = 0;
						short himg = 0;
						try
						{
							b16 = msg.reader().readByte();
							if (b16 > 0)
							{
								sbyte b17 = msg.reader().readByte();
								array = new int[b17];
								for (int num19 = 0; num19 < b17; num19++)
								{
									array[num19] = msg.reader().readByte();
								}
								wimg = msg.reader().readShort();
								himg = msg.reader().readShort();
							}
						}
						catch (Exception)
						{
						}
						if (num18 == Char.myCharz().charID)
						{
							Char.myCharz().petFollow = new PetFollow();
							Char.myCharz().petFollow.smallID = smallID;
							if (b16 > 0)
							{
								Char.myCharz().petFollow.SetImg(b16, array, wimg, himg);
							}
							break;
						}
						Char obj3 = GameScr.findCharInMap(num18);
						obj3.petFollow = new PetFollow();
						obj3.petFollow.smallID = smallID;
						if (b16 > 0)
						{
							obj3.petFollow.SetImg(b16, array, wimg, himg);
						}
					}
					else if (num18 == Char.myCharz().charID)
					{
						Char.myCharz().petFollow.remove();
						Char.myCharz().petFollow = null;
					}
					else
					{
						Char obj4 = GameScr.findCharInMap(num18);
						obj4.petFollow.remove();
						obj4.petFollow = null;
					}
					break;
				}
				case -89:
					GameCanvas.open3Hour = msg.reader().readByte() == 1;
					break;
				case 42:
				{
					GameCanvas.endDlg();
					LoginScr.isContinueToLogin = false;
					Char.isLoadingMap = false;
					sbyte haveName = msg.reader().readByte();
					if (GameCanvas.registerScr == null)
					{
						GameCanvas.registerScr = new RegisterScreen(haveName);
					}
					GameCanvas.registerScr.switchToMe();
					break;
				}
				case 52:
				{
					sbyte b21 = msg.reader().readByte();
					if (b21 == 1)
					{
						int num26 = msg.reader().readInt();
						if (num26 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(true);
							Char.myCharz().cx = msg.reader().readShort();
							Char.myCharz().cy = msg.reader().readShort();
						}
						else
						{
							Char obj5 = GameScr.findCharInMap(num26);
							if (obj5 != null)
							{
								obj5.setMabuHold(true);
								obj5.cx = msg.reader().readShort();
								obj5.cy = msg.reader().readShort();
							}
						}
					}
					if (b21 == 0)
					{
						int num27 = msg.reader().readInt();
						if (num27 == Char.myCharz().charID)
						{
							Char.myCharz().setMabuHold(false);
						}
						else
						{
							Char obj6 = GameScr.findCharInMap(num27);
							if (obj6 != null)
							{
								obj6.setMabuHold(false);
							}
						}
					}
					if (b21 == 2)
					{
						int charId2 = msg.reader().readInt();
						int id3 = msg.reader().readInt();
						Mabu mabu2 = (Mabu)GameScr.findCharInMap(charId2);
						mabu2.eat(id3);
					}
					if (b21 == 3)
					{
						GameScr.mabuPercent = msg.reader().readByte();
					}
					break;
				}
				case 51:
				{
					int charId = msg.reader().readInt();
					Mabu mabu = (Mabu)GameScr.findCharInMap(charId);
					sbyte id2 = msg.reader().readByte();
					short x2 = msg.reader().readShort();
					short y2 = msg.reader().readShort();
					sbyte b18 = msg.reader().readByte();
					Char[] array2 = new Char[b18];
					int[] array3 = new int[b18];
					for (int num20 = 0; num20 < b18; num20++)
					{
						int num21 = msg.reader().readInt();
						Res.outz("char ID=" + num21);
						array2[num20] = null;
						if (num21 != Char.myCharz().charID)
						{
							array2[num20] = GameScr.findCharInMap(num21);
						}
						else
						{
							array2[num20] = Char.myCharz();
						}
						array3[num20] = msg.reader().readInt();
					}
					mabu.setSkill(id2, x2, y2, array2, array3);
					break;
				}
				case -127:
					readLuckyRound(msg);
					break;
				case -126:
				{
					sbyte b27 = msg.reader().readByte();
					Res.outz("type quay= " + b27);
					if (b27 == 1)
					{
						sbyte b28 = msg.reader().readByte();
						string num41 = msg.reader().readUTF();
						string finish = msg.reader().readUTF();
						GameScr.gI().showWinNumber(num41, finish);
					}
					if (b27 == 0)
					{
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					}
					break;
				}
				case -122:
				{
					short id4 = msg.reader().readShort();
					Npc npc2 = GameScr.findNPCInMap(id4);
					sbyte b26 = msg.reader().readByte();
					npc2.duahau = new int[b26];
					Res.outz("N DUA HAU= " + b26);
					for (int num40 = 0; num40 < b26; num40++)
					{
						npc2.duahau[num40] = msg.reader().readShort();
					}
					npc2.setStatus(msg.reader().readByte(), msg.reader().readInt());
					break;
				}
				case 102:
				{
					sbyte b22 = msg.reader().readByte();
					if (b22 == 0 || b22 == 1 || b22 == 2 || b22 == 6)
					{
						BigBoss2 bigBoss2 = Mob.getBigBoss2();
						if (bigBoss2 == null)
						{
							break;
						}
						if (b22 == 6)
						{
							bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
							break;
						}
						sbyte b23 = msg.reader().readByte();
						Char[] array7 = new Char[b23];
						int[] array8 = new int[b23];
						for (int num33 = 0; num33 < b23; num33++)
						{
							int num34 = msg.reader().readInt();
							array7[num33] = null;
							if (num34 != Char.myCharz().charID)
							{
								array7[num33] = GameScr.findCharInMap(num34);
							}
							else
							{
								array7[num33] = Char.myCharz();
							}
							array8[num33] = msg.reader().readInt();
						}
						bigBoss2.setAttack(array7, array8, b22);
					}
					if (b22 == 3 || b22 == 4 || b22 == 5 || b22 == 7)
					{
						BachTuoc bachTuoc = Mob.getBachTuoc();
						if (bachTuoc == null)
						{
							break;
						}
						if (b22 == 7)
						{
							bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
							break;
						}
						if (b22 == 3 || b22 == 4)
						{
							sbyte b24 = msg.reader().readByte();
							Char[] array9 = new Char[b24];
							int[] array10 = new int[b24];
							for (int num35 = 0; num35 < b24; num35++)
							{
								int num36 = msg.reader().readInt();
								array9[num35] = null;
								if (num36 != Char.myCharz().charID)
								{
									array9[num35] = GameScr.findCharInMap(num36);
								}
								else
								{
									array9[num35] = Char.myCharz();
								}
								array10[num35] = msg.reader().readInt();
							}
							bachTuoc.setAttack(array9, array10, b22);
						}
						if (b22 == 5)
						{
							short xMoveTo = msg.reader().readShort();
							bachTuoc.move(xMoveTo);
						}
					}
					if (b22 > 9 && b22 < 30)
					{
						readActionBoss(msg, b22);
					}
					break;
				}
				case 101:
				{
					Res.outz("big boss--------------------------------------------------");
					BigBoss bigBoss = Mob.getBigBoss();
					if (bigBoss == null)
					{
						break;
					}
					sbyte b19 = msg.reader().readByte();
					if (b19 == 0 || b19 == 1 || b19 == 2 || b19 == 4 || b19 == 3)
					{
						if (b19 == 3)
						{
							bigBoss.xTo = (bigBoss.xFirst = msg.reader().readShort());
							bigBoss.yTo = (bigBoss.yFirst = msg.reader().readShort());
							bigBoss.setFly();
						}
						else
						{
							sbyte b20 = msg.reader().readByte();
							Res.outz("CHUONG nChar= " + b20);
							Char[] array4 = new Char[b20];
							int[] array5 = new int[b20];
							for (int num22 = 0; num22 < b20; num22++)
							{
								int num23 = msg.reader().readInt();
								Res.outz("char ID=" + num23);
								array4[num22] = null;
								if (num23 != Char.myCharz().charID)
								{
									array4[num22] = GameScr.findCharInMap(num23);
								}
								else
								{
									array4[num22] = Char.myCharz();
								}
								array5[num22] = msg.reader().readInt();
							}
							bigBoss.setAttack(array4, array5, b19);
						}
					}
					if (b19 == 5)
					{
						bigBoss.haftBody = true;
						bigBoss.status = 2;
					}
					if (b19 == 6)
					{
						bigBoss.getDataB2();
						bigBoss.x = msg.reader().readShort();
						bigBoss.y = msg.reader().readShort();
					}
					if (b19 == 7)
					{
						bigBoss.setAttack(null, null, b19);
					}
					if (b19 == 8)
					{
						bigBoss.xTo = (bigBoss.xFirst = msg.reader().readShort());
						bigBoss.yTo = (bigBoss.yFirst = msg.reader().readShort());
						bigBoss.status = 2;
					}
					if (b19 == 9)
					{
						bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
					}
					break;
				}
				case -120:
				{
					long num25 = mSystem.currentTimeMillis();
					Service.logController = num25 - Service.curCheckController;
					Service.gI().sendCheckController();
					break;
				}
				case -121:
				{
					long num28 = mSystem.currentTimeMillis();
					Service.logMap = num28 - Service.curCheckMap;
					Service.gI().sendCheckMap();
					break;
				}
				case 100:
				{
					sbyte b29 = msg.reader().readByte();
					sbyte b30 = msg.reader().readByte();
					Item item2 = null;
					if (b29 == 0)
					{
						item2 = Char.myCharz().arrItemBody[b30];
					}
					if (b29 == 1)
					{
						item2 = Char.myCharz().arrItemBag[b30];
					}
					short num42 = msg.reader().readShort();
					if (num42 == -1)
					{
						break;
					}
					item2.template = ItemTemplates.get(num42);
					item2.quantity = msg.reader().readInt();
					item2.info = msg.reader().readUTF();
					item2.content = msg.reader().readUTF();
					sbyte b31 = msg.reader().readByte();
					if (b31 != 0)
					{
						item2.itemOption = new ItemOption[b31];
						for (int num43 = 0; num43 < item2.itemOption.Length; num43++)
						{
							int num44 = msg.reader().readUnsignedByte();
							Res.outz("id o= " + num44);
							int param3 = msg.reader().readUnsignedShort();
							if (num44 != -1)
							{
								item2.itemOption[num43] = new ItemOption(num44, param3);
							}
						}
					}
					if (item2.quantity <= 0)
					{
						item2 = null;
					}
					break;
				}
				case -123:
				{
					int charId3 = msg.reader().readInt();
					if (GameScr.findCharInMap(charId3) != null)
					{
						GameScr.findCharInMap(charId3).perCentMp = msg.reader().readByte();
					}
					break;
				}
				case -119:
					Char.myCharz().rank = msg.reader().readInt();
					break;
				case -117:
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					if (GameScr.gI().percentMabu == 100)
					{
						GameScr.gI().mabuEff = true;
					}
					if (GameScr.gI().percentMabu == 101)
					{
						Npc.mabuEff = true;
					}
					break;
				case -116:
					GameScr.canAutoPlay = msg.reader().readByte() == 1;
					break;
				case -115:
					Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					break;
				case -113:
				{
					sbyte[] array6 = new sbyte[10];
					for (int num30 = 0; num30 < 10; num30++)
					{
						array6[num30] = msg.reader().readByte();
						Res.outz("vlue i= " + array6[num30]);
					}
					GameScr.gI().onKSkill(array6);
					GameScr.gI().onOSkill(array6);
					GameScr.gI().onCSkill(array6);
					break;
				}
				case -111:
				{
					short num11 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int k = 0; k < num11; k++)
					{
						string iD = msg.reader().readUTF();
						sbyte version = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(iD, version));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					break;
				}
				case 125:
				{
					sbyte fusion = msg.reader().readByte();
					int num12 = msg.reader().readInt();
					if (num12 == Char.myCharz().charID)
					{
						Char.myCharz().setFusion(fusion);
					}
					else if (GameScr.findCharInMap(num12) != null)
					{
						GameScr.findCharInMap(num12).setFusion(fusion);
					}
					break;
				}
				case 124:
				{
					short num24 = msg.reader().readShort();
					string text3 = msg.reader().readUTF();
					Res.outz("noi chuyen = " + text3 + "npc ID= " + num24);
					Npc npc = GameScr.findNPCInMap(num24);
					if (npc != null)
					{
						npc.addInfo(text3);
					}
					break;
				}
				case 123:
				{
					Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
					int num10 = msg.reader().readInt();
					short xPos = msg.reader().readShort();
					short yPos = msg.reader().readShort();
					sbyte b7 = msg.reader().readByte();
					Char obj = null;
					if (num10 == Char.myCharz().charID)
					{
						obj = Char.myCharz();
					}
					else if (GameScr.findCharInMap(num10) != null)
					{
						obj = GameScr.findCharInMap(num10);
					}
					if (obj != null)
					{
						ServerEffect.addServerEffect((b7 != 0) ? 173 : 60, obj, 1);
						obj.setPos(xPos, yPos, b7);
					}
					break;
				}
				case 122:
				{
					short num29 = msg.reader().readShort();
					Res.outz("second login = " + num29);
					LoginScr.timeLogin = num29;
					LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
					GameCanvas.endDlg();
					break;
				}
				case 121:
					mSystem.publicID = msg.reader().readUTF();
					mSystem.strAdmob = msg.reader().readUTF();
					Res.outz("SHOW AD public ID= " + mSystem.publicID);
					mSystem.createAdmob();
					break;
				case -124:
				{
					sbyte b4 = msg.reader().readByte();
					sbyte b5 = msg.reader().readByte();
					if (b5 == 0)
					{
						if (b4 == 2)
						{
							int num4 = msg.reader().readInt();
							if (num4 == Char.myCharz().charID)
							{
								Char.myCharz().removeEffect();
							}
							else if (GameScr.findCharInMap(num4) != null)
							{
								GameScr.findCharInMap(num4).removeEffect();
							}
						}
						int num5 = msg.reader().readUnsignedByte();
						int num6 = msg.reader().readInt();
						if (num5 == 32)
						{
							if (b4 == 1)
							{
								int num7 = msg.reader().readInt();
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().holdEffID = num5;
									GameScr.findCharInMap(num7).setHoldChar(Char.myCharz());
								}
								else if (GameScr.findCharInMap(num6) != null && num7 != Char.myCharz().charID)
								{
									GameScr.findCharInMap(num6).holdEffID = num5;
									GameScr.findCharInMap(num7).setHoldChar(GameScr.findCharInMap(num6));
								}
								else if (GameScr.findCharInMap(num6) != null && num7 == Char.myCharz().charID)
								{
									GameScr.findCharInMap(num6).holdEffID = num5;
									Char.myCharz().setHoldChar(GameScr.findCharInMap(num6));
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeHoleEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeHoleEff();
							}
						}
						if (num5 == 33)
						{
							if (b4 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().protectEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).protectEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeProtectEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeProtectEff();
							}
						}
						if (num5 == 39)
						{
							if (b4 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().huytSao = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).huytSao = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeHuytSao();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeHuytSao();
							}
						}
						if (num5 == 40)
						{
							if (b4 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().blindEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).blindEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeBlindEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeBlindEff();
							}
						}
						if (num5 == 41)
						{
							if (b4 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().sleepEff = true;
								}
								else if (GameScr.findCharInMap(num6) != null)
								{
									GameScr.findCharInMap(num6).sleepEff = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().removeSleepEff();
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findCharInMap(num6).removeSleepEff();
							}
						}
						if (num5 == 42)
						{
							if (b4 == 1)
							{
								if (num6 == Char.myCharz().charID)
								{
									Char.myCharz().stone = true;
								}
							}
							else if (num6 == Char.myCharz().charID)
							{
								Char.myCharz().stone = false;
							}
						}
					}
					if (b5 != 1)
					{
						break;
					}
					int num8 = msg.reader().readUnsignedByte();
					sbyte b6 = msg.reader().readByte();
					Res.outz("modbHoldID= " + b6 + " skillID= " + num8 + "eff ID= " + b4);
					if (num8 == 32)
					{
						if (b4 == 1)
						{
							int num9 = msg.reader().readInt();
							if (num9 == Char.myCharz().charID)
							{
								GameScr.findMobInMap(b6).holdEffID = num8;
								Char.myCharz().setHoldMob(GameScr.findMobInMap(b6));
							}
							else if (GameScr.findCharInMap(num9) != null)
							{
								GameScr.findMobInMap(b6).holdEffID = num8;
								GameScr.findCharInMap(num9).setHoldMob(GameScr.findMobInMap(b6));
							}
						}
						else
						{
							GameScr.findMobInMap(b6).removeHoldEff();
						}
					}
					if (num8 == 40)
					{
						if (b4 == 1)
						{
							GameScr.findMobInMap(b6).blindEff = true;
						}
						else
						{
							GameScr.findMobInMap(b6).removeBlindEff();
						}
					}
					if (num8 == 41)
					{
						if (b4 == 1)
						{
							GameScr.findMobInMap(b6).sleepEff = true;
						}
						else
						{
							GameScr.findMobInMap(b6).removeSleepEff();
						}
					}
					break;
				}
				case -125:
				{
					ChatTextField.gI().isShow = false;
					string text4 = msg.reader().readUTF();
					Res.outz("titile= " + text4);
					sbyte b32 = msg.reader().readByte();
					ClientInput.gI().setInput(b32, text4);
					for (int num45 = 0; num45 < b32; num45++)
					{
						ClientInput.gI().tf[num45].name = msg.reader().readUTF();
						sbyte b33 = msg.reader().readByte();
						if (b33 == 0)
						{
							ClientInput.gI().tf[num45].setIputType(TField.INPUT_TYPE_NUMERIC);
						}
						if (b33 == 1)
						{
							ClientInput.gI().tf[num45].setIputType(TField.INPUT_TYPE_ANY);
						}
						if (b33 == 2)
						{
							ClientInput.gI().tf[num45].setIputType(TField.INPUT_TYPE_PASSWORD);
						}
					}
					break;
				}
				case -110:
				{
					sbyte b25 = msg.reader().readByte();
					if (b25 == 1)
					{
						int num37 = msg.reader().readInt();
						sbyte[] array11 = Rms.loadRMS(num37 + string.Empty);
						if (array11 == null)
						{
							Service.gI().sendServerData(1, -1, null);
						}
						else
						{
							Service.gI().sendServerData(1, num37, array11);
						}
					}
					if (b25 == 0)
					{
						int num38 = msg.reader().readInt();
						short num39 = msg.reader().readShort();
						sbyte[] data = new sbyte[num39];
						msg.reader().read(ref data, 0, num39);
						Rms.saveRMS(num38 + string.Empty, data);
					}
					break;
				}
				case 93:
				{
					string str = msg.reader().readUTF();
					str = Res.changeString(str);
					GameScr.gI().chatVip(str);
					break;
				}
				case -106:
				{
					short num31 = msg.reader().readShort();
					int num32 = msg.reader().readShort();
					if (ItemTime.isExistItem(num31))
					{
						ItemTime.getItemById(num31).initTime(num32);
						break;
					}
					ItemTime o = new ItemTime(num31, num32);
					Char.vItemTime.addElement(o);
					break;
				}
				case -105:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					break;
				case -103:
				{
					sbyte b10 = msg.reader().readByte();
					if (b10 == 0)
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b11 = msg.reader().readByte();
						for (int l = 0; l < b11; l++)
						{
							Item item = new Item();
							short num13 = msg.reader().readShort();
							if (num13 != -1)
							{
								item.template = ItemTemplates.get(num13);
								sbyte b12 = msg.reader().readByte();
								if (b12 != -1)
								{
									item.itemOption = new ItemOption[b12];
									for (int m = 0; m < item.itemOption.Length; m++)
									{
										int num14 = msg.reader().readUnsignedByte();
										int param2 = msg.reader().readUnsignedShort();
										if (num14 != -1)
										{
											item.itemOption[m] = new ItemOption(num14, param2);
										}
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
					}
					else if (b10 == 1)
					{
						int num15 = msg.reader().readInt();
						sbyte b13 = msg.reader().readByte();
						Res.outz("---------------actionFlag1:  " + num15 + " : " + b13);
						if (num15 == Char.myCharz().charID)
						{
							Char.myCharz().cFlag = b13;
						}
						else if (GameScr.findCharInMap(num15) != null)
						{
							GameScr.findCharInMap(num15).cFlag = b13;
						}
						GameScr.gI().getFlagImage(num15, b13);
					}
					else
					{
						if (b10 != 2)
						{
							break;
						}
						sbyte b14 = msg.reader().readByte();
						int num16 = msg.reader().readShort();
						PKFlag pKFlag = new PKFlag();
						pKFlag.cflag = b14;
						pKFlag.IDimageFlag = num16;
						GameScr.vFlag.addElement(pKFlag);
						for (int n = 0; n < GameScr.vFlag.size(); n++)
						{
							PKFlag pKFlag2 = (PKFlag)GameScr.vFlag.elementAt(n);
							Res.outz("i: " + n + "  cflag: " + pKFlag2.cflag + "   IDimageFlag: " + pKFlag2.IDimageFlag);
						}
						for (int num17 = 0; num17 < GameScr.vCharInMap.size(); num17++)
						{
							Char obj2 = (Char)GameScr.vCharInMap.elementAt(num17);
							if (obj2 != null && obj2.cFlag == b14)
							{
								obj2.flagImage = num16;
							}
						}
						if (Char.myCharz().cFlag == b14)
						{
							Char.myCharz().flagImage = num16;
						}
					}
					break;
				}
				case -102:
				{
					sbyte b9 = msg.reader().readByte();
					if (b9 != 0 && b9 == 1)
					{
						GameCanvas.loginScr.isLogin2 = false;
						Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
						LoginScr.isLoggingIn = true;
					}
					break;
				}
				case -101:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text = msg.reader().readUTF();
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect, text);
					Service.gI().setClientType();
					Service.gI().login(text, string.Empty, GameMidlet.VERSION, 1);
					break;
				}
				case -100:
				{
					InfoDlg.hide();
					bool flag = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						flag = true;
					}
					sbyte b = msg.reader().readByte();
					Res.outz("t Indxe= " + b);
					GameCanvas.panel.maxPageShop[b] = msg.reader().readByte();
					GameCanvas.panel.currPageShop[b] = msg.reader().readByte();
					Res.outz("max page= " + GameCanvas.panel.maxPageShop[b] + " curr page= " + GameCanvas.panel.currPageShop[b]);
					int num = msg.reader().readUnsignedByte();
					Char.myCharz().arrItemShop[b] = new Item[num];
					for (int i = 0; i < num; i++)
					{
						short num2 = msg.reader().readShort();
						if (num2 == -1)
						{
							continue;
						}
						Res.outz("template id= " + num2);
						Char.myCharz().arrItemShop[b][i] = new Item();
						Char.myCharz().arrItemShop[b][i].template = ItemTemplates.get(num2);
						Char.myCharz().arrItemShop[b][i].itemId = msg.reader().readShort();
						Char.myCharz().arrItemShop[b][i].buyCoin = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].buyGold = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].buyType = msg.reader().readByte();
						Char.myCharz().arrItemShop[b][i].quantity = msg.reader().readInt();
						Char.myCharz().arrItemShop[b][i].isMe = msg.reader().readByte();
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
						sbyte b2 = msg.reader().readByte();
						if (b2 != -1)
						{
							Char.myCharz().arrItemShop[b][i].itemOption = new ItemOption[b2];
							for (int j = 0; j < Char.myCharz().arrItemShop[b][i].itemOption.Length; j++)
							{
								int num3 = msg.reader().readUnsignedByte();
								int param = msg.reader().readUnsignedShort();
								if (num3 != -1)
								{
									Char.myCharz().arrItemShop[b][i].itemOption[j] = new ItemOption(num3, param);
									Char.myCharz().arrItemShop[b][i].compare = GameCanvas.panel.getCompare(Char.myCharz().arrItemShop[b][i]);
								}
							}
						}
						sbyte b3 = msg.reader().readByte();
						if (b3 == 1)
						{
							int headTemp = msg.reader().readShort();
							int bodyTemp = msg.reader().readShort();
							int legTemp = msg.reader().readShort();
							int bagTemp = msg.reader().readShort();
							Char.myCharz().arrItemShop[b][i].setPartTemp(headTemp, bodyTemp, legTemp, bagTemp);
						}
						if (GameMidlet.intVERSION >= 237)
						{
							Char.myCharz().arrItemShop[b][i].nameNguoiKyGui = msg.reader().readUTF();
							Res.err("nguoi ki gui  " + Char.myCharz().arrItemShop[b][i].nameNguoiKyGui);
						}
					}
					if (flag)
					{
						GameCanvas.panel2.setTabKiGui();
					}
					GameCanvas.panel.setTabShop();
					GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
					break;
				}
				}
			}
			catch (Exception ex4)
			{
				Res.outz("=====> Controller2 " + ex4.StackTrace);
			}
		}

		private static void readLuckyRound(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					sbyte b2 = msg.reader().readByte();
					short[] array = new short[b2];
					for (int i = 0; i < b2; i++)
					{
						array[i] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int price = msg.reader().readInt();
					short idTicket = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array, (byte)b3, price, idTicket);
				}
				else if (b == 1)
				{
					sbyte b4 = msg.reader().readByte();
					short[] array2 = new short[b4];
					for (int j = 0; j < b4; j++)
					{
						array2[j] = msg.reader().readShort();
					}
					CrackBallScr.gI().DoneCrackBallScr(array2);
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoRada(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num = msg.reader().readShort();
					int num2 = 0;
					for (int i = 0; i < num; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int id = msg.reader().readShort();
						int no = i + 1;
						int idIcon = msg.reader().readShort();
						sbyte rank = msg.reader().readByte();
						sbyte amount = msg.reader().readByte();
						sbyte max_amount = msg.reader().readByte();
						short templateId = -1;
						Char charInfo = null;
						sbyte b2 = msg.reader().readByte();
						if (b2 == 0)
						{
							templateId = msg.reader().readShort();
						}
						else
						{
							int head = msg.reader().readShort();
							int body = msg.reader().readShort();
							int leg = msg.reader().readShort();
							int bag = msg.reader().readShort();
							charInfo = Info_RadaScr.SetCharInfo(head, body, leg, bag);
						}
						string name = msg.reader().readUTF();
						string info = msg.reader().readUTF();
						sbyte b3 = msg.reader().readByte();
						sbyte use = msg.reader().readByte();
						sbyte b4 = msg.reader().readByte();
						ItemOption[] array = null;
						if (b4 != 0)
						{
							array = new ItemOption[b4];
							for (int j = 0; j < array.Length; j++)
							{
								int num3 = msg.reader().readUnsignedByte();
								int param = msg.reader().readUnsignedShort();
								sbyte activeCard = msg.reader().readByte();
								if (num3 != -1)
								{
									array[j] = new ItemOption(num3, param);
									array[j].activeCard = activeCard;
								}
							}
						}
						info_RadaScr.SetInfo(id, no, idIcon, rank, b2, templateId, name, info, charInfo, array);
						info_RadaScr.SetLevel(b3);
						info_RadaScr.SetUse(use);
						info_RadaScr.SetAmount(amount, max_amount);
						myVector.addElement(info_RadaScr);
						if (b3 > 0)
						{
							num2++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num2, num);
					RadarScr.gI().switchToMe();
				}
				else if (b == 1)
				{
					int id2 = msg.reader().readShort();
					sbyte use2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id2) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id2).SetUse(use2);
					}
					RadarScr.SetListUse();
				}
				else if (b == 2)
				{
					int num4 = msg.reader().readShort();
					sbyte level = msg.reader().readByte();
					int num5 = 0;
					for (int k = 0; k < RadarScr.list.size(); k++)
					{
						Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
						if (info_RadaScr2 != null)
						{
							if (info_RadaScr2.id == num4)
							{
								info_RadaScr2.SetLevel(level);
							}
							if (info_RadaScr2.level > 0)
							{
								num5++;
							}
						}
					}
					RadarScr.SetNum(num5, RadarScr.list.size());
					if (Info_RadaScr.GetInfo(RadarScr.listUse, num4) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, num4).SetLevel(level);
					}
				}
				else if (b == 3)
				{
					int id3 = msg.reader().readShort();
					sbyte amount2 = msg.reader().readByte();
					sbyte max_amount2 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, id3).SetAmount(amount2, max_amount2);
					}
					if (Info_RadaScr.GetInfo(RadarScr.listUse, id3) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, id3).SetAmount(amount2, max_amount2);
					}
				}
				else if (b == 4)
				{
					int num6 = msg.reader().readInt();
					short idAuraEff = msg.reader().readShort();
					Char obj = null;
					obj = ((num6 != Char.myCharz().charID) ? GameScr.findCharInMap(num6) : Char.myCharz());
					if (obj != null)
					{
						obj.idAuraEff = idAuraEff;
						obj.idEff_Set_Item = msg.reader().readByte();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				int num = msg.reader().readInt();
				Char obj = null;
				obj = ((num != Char.myCharz().charID) ? GameScr.findCharInMap(num) : Char.myCharz());
				if (b == 0)
				{
					int id = msg.reader().readShort();
					int layer = msg.reader().readByte();
					int loop = msg.reader().readByte();
					short loopCount = msg.reader().readShort();
					sbyte isStand = msg.reader().readByte();
					if (obj != null)
					{
						obj.addEffChar(new Effect(id, obj, layer, loop, loopCount, isStand));
					}
				}
				else if (b == 1)
				{
					int id2 = msg.reader().readShort();
					if (obj != null)
					{
						obj.removeEffChar(0, id2);
					}
				}
				else if (b == 2 && obj != null)
				{
					obj.removeEffChar(-1, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		private static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				sbyte idBoss = msg.reader().readByte();
				NewBoss newBoss = Mob.getNewBoss(idBoss);
				if (newBoss == null)
				{
					return;
				}
				if (actionBoss == 10)
				{
					short xMoveTo = msg.reader().readShort();
					short yMoveTo = msg.reader().readShort();
					newBoss.move(xMoveTo, yMoveTo);
				}
				if (actionBoss >= 11 && actionBoss <= 20)
				{
					sbyte b = msg.reader().readByte();
					Char[] array = new Char[b];
					int[] array2 = new int[b];
					for (int i = 0; i < b; i++)
					{
						int num = msg.reader().readInt();
						array[i] = null;
						if (num != Char.myCharz().charID)
						{
							array[i] = GameScr.findCharInMap(num);
						}
						else
						{
							array[i] = Char.myCharz();
						}
						array2[i] = msg.reader().readInt();
					}
					sbyte dir = msg.reader().readByte();
					newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), dir);
				}
				if (actionBoss == 21)
				{
					newBoss.xTo = msg.reader().readShort();
					newBoss.yTo = msg.reader().readShort();
					newBoss.setFly();
				}
				if (actionBoss == 22)
				{
				}
				if (actionBoss == 23)
				{
					newBoss.setDie();
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
