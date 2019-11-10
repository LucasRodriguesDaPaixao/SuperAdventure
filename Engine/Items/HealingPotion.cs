using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PocaoDeCura : Item
    {
        // Guarda status de poção de cura
        public int QuantidadeDeCura { get; set; }

        // Construtor de classe
        public PocaoDeCura (int id, string nome, string nomePlural, int quantidadeDeCura) 
            : base (id, nome, nomePlural) // Redireciona dados para o construtor da classe mãe 
        {
            QuantidadeDeCura = quantidadeDeCura;
        }
    }
}
