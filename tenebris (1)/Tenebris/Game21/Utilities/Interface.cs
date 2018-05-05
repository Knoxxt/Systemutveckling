using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21.Utilities;
using Microsoft.Xna.Framework.Input;
using Game21.Managers;
using Game21.Objects;

namespace Game21
{
    class Interface
    {
        private Panel menuPanel, combatPanel, exitBattle, statsPanel, inventoryPanel, gameOverPanel;
        private Label heroHp, heroMp, monsterHp, round, level, health, mana, gameOver, sHealth, sMana, sStr, sInt, sAgi, attribute, skill, rank1, rank2, rank3, rank4;
        private Button startGame, exitGame, attack, exit, spells, spell1, spell2, spell3, spell4, inventoryButton, warrior, mage, exitgoButton;
        private Header iStats, iEq, iInv;
        private HorizontalLine iHL;
        public SelectList inventoryItems, equipmentItems;
        public bool doThisOnce;

        Game1 g;
        CombatManager com;
        ReadManager rm;
        Enums enums;
        public Interface( ReadManager rm, Game1 g, CombatManager com, Enums enums)
        {
            this.g = g;
            this.rm = rm;
            this.com = com;
            this.enums = enums;
            doThisOnce = false;
        }
        
        public void Menu() // HUD for menu. Created by Anton Persson.
        {
            if(enums.gState == GameState.Menu)
            {
                menuPanel = new Panel(new Vector2(g.Width / 2, g.Height / 2), PanelSkin.Simple);
                UserInterface.Active.AddEntity(menuPanel);
                menuPanel.FillColor = Color.DimGray;
                menuPanel.ShadowColor = Color.Black;
                startGame = new Button("Start Game", ButtonSkin.Default, Anchor.TopCenter, new Vector2(g.Width / 4, g.Height / 8));
                menuPanel.AddChild(startGame);
                startGame.FillColor = Color.DarkRed;
                startGame.ShadowColor = Color.Black;
                warrior = new Button("Warrior", ButtonSkin.Default, Anchor.TopCenter, new Vector2(g.Width / 4, g.Height / 8));
                menuPanel.AddChild(warrior);
                warrior.FillColor = Color.DarkRed;
                warrior.ShadowColor = Color.Black;
                warrior.Visible = false;
                exitGame = new Button("Exit Game", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(g.Width / 4, g.Height / 8));
                menuPanel.AddChild(exitGame);
                exitGame.FillColor = Color.DarkRed;
                exitGame.ShadowColor = Color.Black;
                mage = new Button("Mage", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(g.Width / 4, g.Height / 8));
                menuPanel.AddChild(mage);
                mage.FillColor = Color.DarkRed;
                mage.ShadowColor = Color.Black;
                mage.Visible = false;

                startGame.OnClick = (Entity start) =>
                {
                    startGame.Visible = false;
                    exitGame.Visible = false;
                    warrior.Visible = true;
                    mage.Visible = true;
                };
                warrior.OnClick = (Entity start) =>
                {
                    enums.pClass = PlayerClass.Warrior;
                    enums.gState = GameState.Game;
                    menuPanel.Visible = false;
                };
                mage.OnClick = (Entity start) =>
                {
                    enums.pClass = PlayerClass.Mage;
                    enums.gState = GameState.Game;
                    menuPanel.Visible = false;
                };

                exitGame.OnClick = (Entity exit) =>
                {
                    g.Exit();
                };
            }
        }

        public void Combat()
        {
            foreach (Hero h in rm.heroes)
            {

                foreach (Monster m in rm.monstersInCombat)
                {
                    exitBattle = new Panel(new Vector2(g.Width / 4, g.Height / 4), PanelSkin.Simple, Anchor.Center);
                    UserInterface.Active.AddEntity(exitBattle);
                    exitBattle.FillColor = Color.DimGray;
                    exitBattle.ShadowColor = Color.Black;
                    exit = new Button("Exit", ButtonSkin.Default, Anchor.Center, new Vector2(g.Width / 8, g.Height / 8));
                    exitBattle.AddChild(exit);
                    exit.FillColor = Color.DarkRed;
                    exit.ShadowColor = Color.Black;
                    exitBattle.Visible = false;
                    combatPanel = new Panel(new Vector2(g.Width, (g.Height / 4) + 4), PanelSkin.Simple, Anchor.BottomCenter);
                    UserInterface.Active.AddEntity(combatPanel);
                    combatPanel.FillColor = Color.DimGray;
                    combatPanel.ShadowColor = Color.Black;
                    attack = new Button("Attack", ButtonSkin.Default, Anchor.TopCenter, new Vector2(g.Width / 6, g.Height / 10));
                    combatPanel.AddChild(attack);
                    attack.FillColor = Color.DarkRed;
                    attack.ShadowColor = Color.Black;
                    heroHp = new Label("Health: " + h.health, Anchor.TopLeft, new Vector2(200, 10));
                    combatPanel.AddChild(heroHp);
                    heroMp = new Label("Mana: " + h.mana, Anchor.TopLeft, new Vector2(200, 10), new Vector2(0, 25));
                    combatPanel.AddChild(heroMp);
                    monsterHp = new Label("Health: " + m.Health, Anchor.TopRight, new Vector2(100, 10));
                    combatPanel.AddChild(monsterHp);
                    round = new Label("Round: " + h.Round, Anchor.TopCenter, new Vector2(100, 10), new Vector2(0, -25));
                    combatPanel.AddChild(round);
                    spells = new Button("Spells", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(g.Width / 6, g.Height / 10));
                    combatPanel.AddChild(spells);
                    spells.FillColor = Color.DarkRed;
                    spells.ShadowColor = Color.Black;
                    spell1 = new Button(h.spell1Name, ButtonSkin.Default, Anchor.TopLeft, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    combatPanel.AddChild(spell1);
                    spell1.FillColor = Color.DarkRed;
                    spell1.ShadowColor = Color.Black;
                    spell1.Visible = false;
                    spell2 = new Button(h.spell2Name, ButtonSkin.Default, Anchor.BottomLeft, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    combatPanel.AddChild(spell2);
                    spell2.FillColor = Color.DarkRed;
                    spell2.ShadowColor = Color.Black;
                    spell2.Visible = false;
                    spell3 = new Button(h.spell3Name, ButtonSkin.Default, Anchor.TopRight, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    combatPanel.AddChild(spell3);
                    spell3.FillColor = Color.DarkRed;
                    spell3.ShadowColor = Color.Black;
                    spell3.Visible = false;
                    spell4 = new Button(h.spell4Name, ButtonSkin.Default, Anchor.BottomRight, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    combatPanel.AddChild(spell4);
                    spell4.FillColor = Color.DarkRed;
                    spell4.ShadowColor = Color.Black;
                    spell4.Visible = false;
                }


                attack.OnClick = (Entity start) =>
                {
                    if (h.yourTurn)
                    {
                        h.ManaCostOnce = true;
                        h.attack = true;
                        if (com.DoThisOnce1)
                        {
                            h.Round += 1;
                            com.DoThisOnce1 = false;
                        }
                    }

                };

                spells.OnClick = (Entity start) =>
                {
                    if (h.yourTurn)
                    {
                        spell1.Visible = true;
                        spell2.Visible = true;
                        spell3.Visible = true;
                        spell4.Visible = true;
                    }

                };

                spell1.OnClick = (Entity start) =>
                {
                    if (h.yourTurn)
                    {
                        h.ManaCostOnce = true;
                        h.spell1 = true;
                        if (com.DoThisOnce1)
                        {
                            h.Round += 1;
                            com.DoThisOnce1 = false;
                        }
                    }

                };

                spell2.OnClick = (Entity start) =>
                {
                    if (h.yourTurn)
                    {
                        h.ManaCostOnce = true;
                        h.spell2 = true;
                        if (com.DoThisOnce1)
                        {
                            h.Round += 1;
                            com.DoThisOnce1 = false;
                        }
                    }

                };

                spell3.OnClick = (Entity start) =>
                {
                    if (h.yourTurn)
                    {
                        h.ManaCostOnce = true;
                        h.spell3 = true;
                        if (com.DoThisOnce1)
                        {
                            h.Round += 1;
                            com.DoThisOnce1 = false;
                        }
                    }

                };

                spell4.OnClick = (Entity start) =>
                {
                    if (h.yourTurn)
                    {
                        h.ManaCostOnce = true;
                        h.spell4 = true;
                        if (com.DoThisOnce1)
                        {
                            h.Round += 1;
                            com.DoThisOnce1 = false;
                        }
                    }

                };
            }
        }

        public void Game(InventoryManager im)
        {
            foreach(Monster m in rm.monstersInCombat)
            {
                if(!m.isAlive)
                {
                    combatPanel.Visible = false;
                    exitBattle.Visible = false;
                }
            }
            // Stats Panel
            statsPanel = new Panel(new Vector2(g.Width / 6, (g.Height / 9) + 4), PanelSkin.Simple, Anchor.TopLeft);
            UserInterface.Active.AddEntity(statsPanel);
            statsPanel.FillColor = Color.DimGray;
            statsPanel.ShadowColor = Color.Black;
            // Inventory Panel
            inventoryButton = new Button("(I)nventory", ButtonSkin.Default, Anchor.TopCenter, new Vector2(g.Width / 6, g.Height / 16));
            UserInterface.Active.AddEntity(inventoryButton);
            inventoryButton.FillColor = Color.DarkRed;
            inventoryButton.ShadowColor = Color.Black;
            inventoryPanel = new Panel(new Vector2(g.Width / 2, g.Height / 2), PanelSkin.Default, Anchor.AutoCenter);
            UserInterface.Active.AddEntity(inventoryPanel);
            inventoryPanel.FillColor = Color.DimGray;
            inventoryPanel.ShadowColor = Color.Black;
            inventoryPanel.Visible = false;
            inventoryPanel.Draggable = true;
            // Death Screen Panel
            gameOverPanel = new Panel(new Vector2(g.Width / 4, (g.Height / 4)), PanelSkin.Simple, Anchor.AutoCenter);
            UserInterface.Active.AddEntity(gameOverPanel);
            gameOver = new Label("Game Over!", Anchor.TopCenter, new Vector2(200, 100));
            gameOverPanel.AddChild(gameOver);
            exitgoButton = new Button("Exit", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(200, 100));
            gameOverPanel.AddChild(exitgoButton);
            gameOverPanel.Visible = false;

            // Stats Panel Extras
            foreach (Hero h in rm.heroes)
            {
                level = new Label("Level: " + h.level, Anchor.TopCenter);
                statsPanel.AddChild(level);
                health = new Label("Health: " + h.health, Anchor.Center);
                statsPanel.AddChild(health);
                mana = new Label("Mana: " + h.mana, Anchor.BottomCenter);
                statsPanel.AddChild(mana);
            }

            // Inventory Panel Extras
            iStats = new Header("Stats", Anchor.TopLeft, new Vector2(-350, 0));
            iStats.FillColor = Color.DarkRed;
            iStats.ShadowColor = Color.Black;
            inventoryPanel.AddChild(iStats);
            iEq = new Header("Equipment", Anchor.TopCenter);
            iEq.FillColor = Color.DarkRed;
            iEq.ShadowColor = Color.Black;
            inventoryPanel.AddChild(iEq);
            iInv = new Header("Inventory", Anchor.TopRight, new Vector2(-300, 0));
            iInv.FillColor = Color.DarkRed;
            iInv.ShadowColor = Color.Black;
            inventoryPanel.AddChild(iInv);
            iHL = new HorizontalLine(Anchor.TopCenter, new Vector2(0, 50));
            inventoryPanel.AddChild(iHL);
            inventoryItems = new SelectList(new Vector2(300, 300), Anchor.TopRight, new Vector2(0, 100));
            inventoryItems.FillColor = Color.DimGray;
            inventoryItems.ShadowColor = Color.Black;
            inventoryPanel.AddChild(inventoryItems);
            equipmentItems = new SelectList(new Vector2(300, 300), Anchor.TopCenter, new Vector2(0, 100));
            equipmentItems.FillColor = Color.DimGray;
            equipmentItems.ShadowColor = Color.Black;
            inventoryPanel.AddChild(equipmentItems);
            foreach (Hero h in rm.heroes)
            {
                sHealth = new Label("Health: " + h.health + "/" + h.maxHealth, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 100));
                sMana = new Label("Mana: " + h.mana + "/" + h.maxMana, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 120));
                sStr = new Label("Strength: " + h.strength, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 140));
                sInt = new Label("Intelligence: " + h.intelligence, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 160));
                sAgi = new Label("Agility: " + h.agility, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 180));
                attribute = new Label("Attribute points: " + h.attributePoint, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 200));
                rank1 = new Label(h.spell1Name + ": " + h.spell1Rank, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 270));
                rank2 = new Label(h.spell2Name + ": " + h.spell2Rank, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 290));
                rank3 = new Label(h.spell3Name + ": " + h.spell3Rank, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 310));
                rank4 = new Label(h.spell4Name + ": " + h.spell4Rank, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 330));
                skill = new Label("Skill points: " + h.skillPoint, Anchor.TopLeft, new Vector2(250, 20), new Vector2(0, 350));
            }
            inventoryPanel.AddChild(sHealth);
            inventoryPanel.AddChild(sMana);
            inventoryPanel.AddChild(sStr);
            inventoryPanel.AddChild(sInt);
            inventoryPanel.AddChild(sAgi);
            inventoryPanel.AddChild(attribute);
            inventoryPanel.AddChild(rank1);
            inventoryPanel.AddChild(rank2);
            inventoryPanel.AddChild(rank3);
            inventoryPanel.AddChild(rank4);
            inventoryPanel.AddChild(skill);


            // Inventory Panel Events
            inventoryButton.OnClick = (Entity start) =>
            {
                if (inventoryPanel.Visible == true)
                {
                    inventoryPanel.Visible = false;
                }
                else
                {
                    inventoryPanel.Visible = true;
                }
            };
            foreach(Hero h in rm.heroes)
            {
                sHealth.OnClick = (Entity entity) =>
                {
                    if(h.attributePoint > 0)
                    {
                        h.health += 10;
                        h.maxHealth += 10;
                        h.attributePoint -= 1;
                    }
                };

                sMana.OnClick = (Entity entity) =>
                {
                    if (h.attributePoint > 0)
                    {
                        h.mana += 10;
                        h.maxMana += 10;
                        h.attributePoint -= 1;
                    }
                };

                sStr.OnClick = (Entity entity) =>
                {
                    if (h.attributePoint > 0)
                    {
                        h.strength += 1;
                        h.attributePoint -= 1;
                    }
                };

                sInt.OnClick = (Entity entity) =>
                {
                    if (h.attributePoint > 0)
                    {
                        h.intelligence += 1;
                        h.attributePoint -= 1;
                    }
                };

                sAgi.OnClick = (Entity entity) =>
                {
                    if (h.attributePoint > 0)
                    {
                        h.agility += 1;
                        h.attributePoint -= 1;
                    }
                };

                rank1.OnClick = (Entity entity) =>
                {
                    if (h.skillPoint > 0)
                    {
                        h.spell1Rank += 1;
                        h.skillPoint -= 1;
                    }
                };

                rank2.OnClick = (Entity entity) =>
                {
                    if (h.skillPoint > 0)
                    {
                        h.spell2Rank += 1;
                        h.skillPoint -= 1;
                    }
                };

                rank3.OnClick = (Entity entity) =>
                {
                    if (h.skillPoint > 0)
                    {
                        h.spell3Rank += 1;
                        h.skillPoint -= 1;
                    }
                };

                rank4.OnClick = (Entity entity) =>
                {
                    if (h.skillPoint > 0)
                    {
                        h.spell4Rank += 1;
                        h.skillPoint -= 1;
                    }
                };
            }

            //Game Over Screen Events
            exitgoButton.OnClick = (Entity start) =>
            {
                g.Exit();
            };

        }

        public void UpdateGame(GameTime gameTime, InventoryManager im)
        {
            foreach (Hero h in rm.heroes)
            {
                level.Text = "Level: " + h.level;
                health.Text = "Health: " + h.health;
                mana.Text = "Mana: " + h.mana;
                if (g.gameOver)
                {
                    gameOverPanel.Visible = true;
                }
            }
        }

        public void GlobalUpdate(GameTime gameTime, InventoryManager im)
        {
            foreach (Item i in im.itemsEquipped)
            {
                if (equipmentItems.SelectedValue == i.name)
                {
                    equipmentItems.SelectedValue = i.desc;
                }
            }
            if (KeymouseReader.KeyPressed2(Keys.I))
            {
                if (inventoryPanel.Visible == true)
                {
                    inventoryPanel.Visible = false;
                }
                else
                {
                    inventoryPanel.Visible = true;
                }
            }
            foreach (Hero h in rm.heroes)
            {
                if (sHealth != null)
                {
                    sHealth.Text = "Health: " + h.health + "/" + h.maxHealth;
                    sMana.Text = "Mana: " + h.mana + "/" + h.maxMana;
                    sStr.Text = "Strength: " + h.strength;
                    sInt.Text = "Intelligence: " + h.intelligence;
                    sAgi.Text = "Agility: " + h.agility;
                    attribute.Text = "Attribute points: " + h.attributePoint;
                    rank1.Text = h.spell1Name + ": " + h.spell1Rank;
                    rank2.Text = h.spell2Name + ": " + h.spell2Rank;
                    rank3.Text = h.spell3Name + ": " + h.spell3Rank;
                    rank4.Text = h.spell4Name + ": " + h.spell4Rank;
                    skill.Text = "Skill points: " + h.skillPoint;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Hero h in rm.heroes)
            {
                heroHp.Text = "Health: " + h.health;
                heroMp.Text = "Mana: " + h.mana;
                round.Text = "Round: " + h.Round;
                if (h.yourTurn)
                {
                    attack.Disabled = false;
                    spells.Disabled = false;
                    if (h.mana >= h.manaCost1)
                    {
                        spell1.Disabled = false;
                    }
                    if (h.mana >= h.manaCost2)
                    {
                        spell2.Disabled = false;
                    }
                    if (h.mana >= h.manaCost3)
                    {
                        spell3.Disabled = false;
                    }
                    if (h.mana >= h.manaCost4)
                    {
                        spell4.Disabled = false;
                    }
                }
                else
                {
                    attack.Disabled = true;
                    spells.Disabled = true;
                    spell1.Disabled = true;
                    spell2.Disabled = true;
                    spell3.Disabled = true;
                    spell4.Disabled = true;
                }

                foreach (Monster m in rm.monstersInCombat)
                {
                    monsterHp.Text = "Health: " + m.Health;

                    if (!m.IsAlive && rm.monstersInCombat.Count != 0)
                    {
                        if (!doThisOnce)
                        {
                            exitBattle.Visible = true;
                            doThisOnce = true;
                        }

                        exit.OnClick = (Entity start) =>
                        {
                            //attack.Disabled = false;
                            //spells.Disabled = false;
                            combatPanel.Visible = false;
                            exitBattle.Visible = false;
                            doThisOnce = false;
                            h.currentExp += m.ExpGiven;
                            rm.monstersInCombat.Remove(m);
                            enums.gState = GameState.Game;
                            h.position = h.positionBeforeCombat;
                        };
                    }
                }
            }
        }
    }
}
