﻿using GameV1;
using GameV1.Entities.Factory;
using MooseEngine.Core;

Engine.Start<TestGame>(builder =>
{
    builder.RegisterFactory<EntityFactory>();
    
    //builder.Register<IEntityFactory, EntityFactory>();
});



