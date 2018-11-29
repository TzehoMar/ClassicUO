﻿#region license
//  Copyright (C) 2018 ClassicUO Development Community on Github
//
//	This project is an alternative client for the game Ultima Online.
//	The goal of this is to develop a lightweight client considering 
//	new technologies.  
//      
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
#endregion
using System;

namespace ClassicUO.Game.Scenes
{
    public enum ScenesType
    {
        Login,
        Game
    }

    public class SceneManager
    {
        public Scene CurrentScene { get; private set; }

        public void ChangeScene(ScenesType type)
        {
            CurrentScene?.Dispose();
            CurrentScene = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GameLoop game = Service.Get<GameLoop>();

            switch (type)
            {
                case ScenesType.Login:
                    game.WindowWidth = 640;
                    game.WindowHeight = 480;
                    CurrentScene = new LoginScene();

                    break;
                case ScenesType.Game:
                    game.WindowWidth = 1000;
                    game.WindowHeight = 800;
                    CurrentScene = new GameScene();

                    break;
            }

            CurrentScene.Load();
        }

        public T GetScene<T>() where T : Scene
        {
            return CurrentScene?.GetType() == typeof(T) ? (T) CurrentScene : null;
        }
    }
}