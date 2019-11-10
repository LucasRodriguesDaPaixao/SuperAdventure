using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        // Armazena valores de quests
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int RecompensaDeExperiencia { get; set; }
        public int RecompensaDeOuro { get; set; }
        public Item RecompensaDeItem { get; set; }

        // Cria lista de Items de quests
        public List<ItemDeQuest> ItemDeQuest { get; set; }
        
        // Construtor de classe
        public Quest (int id, string nome, string descricao, int recompensaDeExperiencia, int recompensaDeOuro)
        {
            ID = id;
            Nome = nome;
            Descricao = descricao;
            RecompensaDeExperiencia = recompensaDeExperiencia;
            RecompensaDeOuro = recompensaDeOuro;

            // Instancia listas vazias de objetos
            ItemDeQuest = new List<ItemDeQuest>();
        }
    }
}
