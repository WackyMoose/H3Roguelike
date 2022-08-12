
using MooseEngine.Utilities;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooseEngine.Scenes
{
    public class Camera : Entity
    {

        public Camera(string name, Coords2D spriteCoords) : base(name, spriteCoords)
        {
        }

        public Camera(string name, Coords2D spriteCoords, Color colorTint) : base(name, spriteCoords, colorTint)
        {
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
