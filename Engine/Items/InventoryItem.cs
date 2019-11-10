using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class ItemNoInventario
    {
        // Armazena items do inventário do jogador
        public Item Detalhes { get; set; }
        public int Quantidade { get; set; }

        // Construtor de classe
        public ItemNoInventario (Item detalhes, int quantidade)
        {
            Detalhes = detalhes;
            Quantidade = quantidade;
        }
    }
}
