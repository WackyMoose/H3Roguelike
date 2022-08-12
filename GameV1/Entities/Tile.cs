﻿using MooseEngine.Scenes;
using MooseEngine.Utility;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameV1.Entities
{
    public class Tile : Entity
    {
        public bool Walkable { get; set; }

        public Tile(bool walkable, Coords2D spriteCoords) : base(spriteCoords)
        {
            Walkable = walkable;
        }

        public Tile(bool walkable, Coords2D spriteCoords, Color colorTint) : base(spriteCoords, colorTint)
        {
            Walkable = walkable;
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }
    }
}