using System;
using System.Collections.Generic;
using System.Text;

namespace StarCitizen
{
    public abstract class Entity
    {
        public string Id { get; set; }
    }

    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}
