using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GoldenDragonCup
{
    public partial class FightViewComponent : Component
    {
        public FightViewComponent()
        {
            InitializeComponent();
        }

        public FightViewComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
