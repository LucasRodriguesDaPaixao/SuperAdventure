using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class ItemDeQuest
    {
        // Armazena detalhes de items de quests
        public Item Detalhes { get; set; }
        public int Quantidade { get; set; }

        // Cria construtor de classe
        public ItemDeQuest (Item details, int quantity)
        {
            Detalhes = details;
            Quantidade = quantity;
        }
    }
}
