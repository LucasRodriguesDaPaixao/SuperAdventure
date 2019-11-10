using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Arma : Item
    {
        // Armazena valores de armas
        public int DanoMinimo { get; set; }
        public int DanoMaximo { get; set; }

        // Construtor de classe
        public Arma (int id, string nome, string nomePlural, int danoMinimo, int danoMaximo)
            : base (id, nome, nomePlural) // Redireciona dados para o construtor da classe mãe 
        {
            DanoMinimo = danoMinimo;
            DanoMaximo = danoMaximo;
        }
    }
}
