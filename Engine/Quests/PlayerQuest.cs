using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PlayerQuest
    {
        // Armazena dados de Quests aceitas pelo jogador
        public Quest Detalhes { get; set; }
        public bool IsCompletado { get; set; }

        public PlayerQuest (Quest detalhes)
        {
            Detalhes = detalhes;
            IsCompletado = false;
        }
    }
}
