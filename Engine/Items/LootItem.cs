using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class ItemDeLoot
    {
        // Armazena items de loot
        public Item Detalhes { get; set; }
        public int PorcentagemDeDrop { get; set; }
        public bool IsItemDefault { get; set; }

        // Cria construtor de classe
        public ItemDeLoot (Item detalhes, int porcentagemDeDrop, bool isItemDefault)
        {
            Detalhes = detalhes;
            PorcentagemDeDrop = porcentagemDeDrop;
            IsItemDefault = isItemDefault;
        }
    }
}
