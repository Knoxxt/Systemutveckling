using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game21.Utilities;

namespace Game21
{
    class Interface
    {
        private Panel panel, panel1, exitBattle, panel2;
        private Label heroHp, heroMp, monsterHp, round, level;
        private Button startGame, exitGame, attack, exit, spells, spell1, spell2, spell3, spell4;
        private bool doThisOnce;

        Game1 g;
        CombatManager com;
        ReadManager rm;
        public Interface( ReadManager rm, Game1 g, CombatManager com)
        {
            this.g = g;
            this.rm = rm;
            this.com = com;
            doThisOnce = false;
        }
        
        public void Menu() // HUD for menu. Created by Anton Persson.
        {
            if(g.state == GameState.Menu)
            {
                panel = new Panel(new Vector2(g.Width / 2, g.Height / 2), PanelSkin.Simple);
                UserInterface.Active.AddEntity(panel);
                panel.FillColor = Color.DimGray;
                panel.ShadowColor = Color.Black;
                startGame = new Button("Start Game", ButtonSkin.Default, Anchor.TopCenter, new Vector2(g.Width / 4, g.Height / 8));
                panel.AddChild(startGame);
                startGame.FillColor = Color.DarkRed;
                startGame.ShadowColor = Color.Black;
                exitGame = new Button("Exit Game", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(g.Width / 4, g.Height / 8));
                panel.AddChild(exitGame);
                exitGame.FillColor = Color.DarkRed;
                exitGame.ShadowColor = Color.Black;

                startGame.OnClick = (Entity start) =>
                {
                    g.state = GameState.Game;
                    panel.Visible = false;
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
                    panel1 = new Panel(new Vector2(g.Width, (g.Height / 4) + 4), PanelSkin.Simple, Anchor.BottomCenter);
                    UserInterface.Active.AddEntity(panel1);
                    panel1.FillColor = Color.DimGray;
                    panel1.ShadowColor = Color.Black;
                    attack = new Button("Attack", ButtonSkin.Default, Anchor.TopCenter, new Vector2(g.Width / 6, g.Height / 10));
                    panel1.AddChild(attack);
                    attack.FillColor = Color.DarkRed;
                    attack.ShadowColor = Color.Black;
                    heroHp = new Label("Health: " + h.health, Anchor.TopLeft, new Vector2(200, 10));
                    panel1.AddChild(heroHp);
                    heroMp = new Label("Mana: " + h.mana, Anchor.TopLeft, new Vector2(200, 10), new Vector2(0, 25));
                    panel1.AddChild(heroMp);
                    monsterHp = new Label("Health: " + m.Health, Anchor.TopRight, new Vector2(100, 10));
                    panel1.AddChild(monsterHp);
                    round = new Label("Round: " + h.Round, Anchor.TopCenter, new Vector2(100, 10), new Vector2(0, -25));
                    panel1.AddChild(round);
                    spells = new Button("Spells", ButtonSkin.Default, Anchor.BottomCenter, new Vector2(g.Width / 6, g.Height / 10));
                    panel1.AddChild(spells);
                    spells.FillColor = Color.DarkRed;
                    spells.ShadowColor = Color.Black;
                    spell1 = new Button(h.spell1Name, ButtonSkin.Default, Anchor.TopLeft, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    panel1.AddChild(spell1);
                    spell1.FillColor = Color.DarkRed;
                    spell1.ShadowColor = Color.Black;
                    spell1.Visible = false;
                    spell2 = new Button(h.spell2Name, ButtonSkin.Default, Anchor.BottomLeft, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    panel1.AddChild(spell2);
                    spell2.FillColor = Color.DarkRed;
                    spell2.ShadowColor = Color.Black;
                    spell2.Visible = false;
                    spell3 = new Button(h.spell3Name, ButtonSkin.Default, Anchor.TopRight, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    panel1.AddChild(spell3);
                    spell3.FillColor = Color.DarkRed;
                    spell3.ShadowColor = Color.Black;
                    spell3.Visible = false;
                    spell4 = new Button(h.spell4Name, ButtonSkin.Default, Anchor.BottomRight, new Vector2(g.Width / 6, g.Height / 10), new Vector2(150, 0));
                    panel1.AddChild(spell4);
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

        public void Game()
        {
            foreach(Monster m in rm.monstersInCombat)
            {
                if (!m.IsAlive)
                {
                    panel1.Visible = false;
                    exitBattle.Visible = false;
                }
            }
            panel2 = new Panel(new Vector2(g.Width / 6, (g.Height / 12) + 4), PanelSkin.Simple, Anchor.TopLeft);
            UserInterface.Active.AddEntity(panel2);
            foreach (Hero h in rm.heroes)
            {
                level = new Label("level: " + h.agility, Anchor.AutoCenter, new Vector2(200, 10));
                panel2.AddChild(level);
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

                    if (!m.IsAlive)
                    {
                        if (!doThisOnce)
                        {
                            exitBattle = new Panel(new Vector2(g.Width / 4, g.Height / 4), PanelSkin.Simple, Anchor.Center);
                            UserInterface.Active.AddEntity(exitBattle);
                            exitBattle.FillColor = Color.DimGray;
                            exitBattle.ShadowColor = Color.Black;
                            exit = new Button("Exit", ButtonSkin.Default, Anchor.Center, new Vector2(g.Width / 8, g.Height / 8));
                            exitBattle.AddChild(exit);
                            exit.FillColor = Color.DarkRed;
                            exit.ShadowColor = Color.Black;
                            doThisOnce = true;
                        }

                        exit.OnClick = (Entity start) =>
                        {
                            //attack.Disabled = false;
                            //spells.Disabled = false;
                            panel1.Visible = false;
                            exitBattle.Visible = false;
                            doThisOnce = true;
                            h.currentExp += m.ExpGiven;
                            rm.monstersInCombat.Remove(m);
                            g.state = GameState.Game;
                        };
                    }
                }
            }
        }
    }
}
