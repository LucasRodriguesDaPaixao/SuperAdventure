using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Localizacao
    {
        // Armazena valores de lugares
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        // Armazena valores específicos de uma localização
        public Item ItemNecessarioParaEntrar { get; set; }
        public Quest QuestDisponivelAqui { get; set; }
        public Monstro MonstroMorandoAqui { get; set; }
        public Localizacao LocalizacaoAoNorte { get; set; }
        public Localizacao LocalizacaoAoLeste { get; set; }
        public Localizacao LocalizacaoAoSul { get; set; }
        public Localizacao LocalizacaoAoOeste { get; set; }

        // Construtor de classe
        public Localizacao(int id, string nome, string descricao,
            Item itemNecessarioParaEntrar = null,
                Quest questDisponivelAqui = null,
                    Monstro monstrosMorandoAqui = null)
        {
            ID = id;
            Nome = nome;
            Descricao = descricao;
            ItemNecessarioParaEntrar = itemNecessarioParaEntrar;
            QuestDisponivelAqui = questDisponivelAqui;
            MonstroMorandoAqui = monstrosMorandoAqui;
        }
    }
}
