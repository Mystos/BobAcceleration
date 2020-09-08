using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace PointAcceleration
{
    class Parachutiste
    {
        private Vector3 v;
        private Vector3 p;
        private Vector3 a;
        private float m;
        private float cd;


        public Parachutiste()
        {
            this.V = new Vector3(0,0,0);
            this.P = new Vector3(0, 0, 0); ;
            this.A = new Vector3(0, 0, 0); ;
            this.M = 0;
            this.Cd = 0f;
        }

        public Vector3 V { get => v; set => v = value; }
        public Vector3 P { get => p; set => p = value; }
        public Vector3 A { get => a; set => a = value; }
        public float M { get => m; set => m = value; }
        public float Cd { get => cd; set => cd = value; }
    }
}
