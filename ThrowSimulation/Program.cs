using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThrowSimulation.Loop;

namespace ThrowSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            InitLoop loop = new InitLoop(500, 500, "throw symulation");
            loop.RUN();
        }
    }
}
