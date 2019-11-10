using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LivingCreature
    {
        // Armazena valores de criaturas vivas
        public int HitPointsMaximo { get; set; }
        public int HitPointsAtual { get; set; }

        // Construtor da classe base LivingCreature
        public LivingCreature (int hitPointsMaximo, int hitPointsAtual)
        {
            HitPointsMaximo = hitPointsMaximo;
            HitPointsAtual = hitPointsAtual;
        }
    }
}
