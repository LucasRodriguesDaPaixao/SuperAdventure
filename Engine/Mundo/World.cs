using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Mundo
    {
        // Cria lista de items, Monsters, Quests e Locations
        public static readonly List<Item> Itens = new List<Item>();
        public static readonly List<Monstro> Monstros = new List<Monstro>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Localizacao> Localizacoes = new List<Localizacao>();

        // Atribui a variáveis constantes IDs de objetos presentes no mundo (Constantes não podem ter seu valor mudado)
        public const int ITEM_ID_ESPADA_VELHA = 1;
        public const int ITEM_ID_RABO_DE_RATO = 2;
        public const int ITEM_ID_PEDACO_DE_PELO = 3;
        public const int ITEM_ID_PRESA_DE_COBRA = 4;
        public const int ITEM_ID_PELE_DE_COBRA = 5;
        public const int ITEM_ID_CLAVA = 6;
        public const int ITEM_ID_POCAO_DE_CURA = 7;
        public const int ITEM_ID_PRESA_DE_ARANHA = 8;
        public const int ITEM_ID_TEIA_DE_ARANHA = 9;
        public const int ITEM_ID_PASSE_DE_AVENTUREIRO = 10;

        public const int MONSTRO_ID_RATO = 1;
        public const int MONSTRO_ID_COBRA = 2;
        public const int MONSTRO_ID_ARANHA_GIGANTE = 3;

        public const int QUEST_ID_LIMPAR_JARDIM_DO_ALQUIMISTA = 1;
        public const int QUEST_ID_LIMPAR_CAMPO = 2;

        public const int LOCALIZACAO_ID_CASA = 1;
        public const int LOCALIZACAO_ID_CENTRO_DA_CIDADE = 2;
        public const int LOCALIZACAO_ID_POSTO_DO_GUARDA = 3;
        public const int LOCALIZACAO_ID_CABANA_DO_ALQUIMISTA = 4;
        public const int LOCALIZACAO_ID_JARDIM_DO_ALQUIMISTA = 5;
        public const int LOCALIZACAO_ID_CASA_NO_CAMPO = 6;
        public const int LOCALIZACAO_ID_CAMPO = 7;
        public const int LOCALIZACAO_ID_PONTE = 8;
        public const int LOCALIZACAO_ID_CAMPO_DE_ARANHAS = 9;

        // Construtor da classe mundo, irá instanciar todos os elementos do mundo quando chamado e popular as listas atuais.
        static Mundo()
        {
            InstanciaItens();
            InstanciaMonstros();
            InstanciaQuests();
            InstanciaLocalizacoes();
        }
        
        // Métodos que ao serem chamados popularão as listas atuais
        private static void InstanciaItens ()
        {
            Itens.Add(new Arma(ITEM_ID_ESPADA_VELHA, "Espada velha", "Espadas velhas", 0, 10));
            Itens.Add(new Item(ITEM_ID_RABO_DE_RATO, "Cauda de rato", "Caudas de rato"));
            Itens.Add(new Item(ITEM_ID_PEDACO_DE_PELO, "Pedaço de pelo", "Pedaços de pelo"));
            Itens.Add(new Item(ITEM_ID_PRESA_DE_COBRA, "Presa de cobra", "Presas de cobra"));
            Itens.Add(new Item(ITEM_ID_PELE_DE_COBRA, "Pele de cobra", "Peles de cobra"));
            Itens.Add(new Arma(ITEM_ID_CLAVA, "Clava", "Clavas", 3, 10));
            Itens.Add(new PocaoDeCura(ITEM_ID_POCAO_DE_CURA, "Poção de cura", "Poções de cura", 5));
            Itens.Add(new Item(ITEM_ID_PRESA_DE_ARANHA, "Presa de aranha", "Presas de aranha"));
            Itens.Add(new Item(ITEM_ID_TEIA_DE_ARANHA, "Teia de aranha", "Teias de aranha"));
            Itens.Add(new Item(ITEM_ID_PASSE_DE_AVENTUREIRO, "Cartão de aventureiro", "Cartões de aventureiro"));
        }

        private static void InstanciaMonstros()
        {
            Monstro rato = new Monstro(MONSTRO_ID_RATO, "O Rato", 5, 7, 10, 3, 3);
            rato.tabelaDeLoot.Add(new ItemDeLoot(ItemPorID(ITEM_ID_RABO_DE_RATO), 75, false)); 
            rato.tabelaDeLoot.Add(new ItemDeLoot(ItemPorID(ITEM_ID_PEDACO_DE_PELO), 75, true));

            Monstro cobra = new Monstro(MONSTRO_ID_COBRA, "A Cobra", 5, 12, 10, 4, 4);
            cobra.tabelaDeLoot.Add(new ItemDeLoot(ItemPorID(ITEM_ID_PRESA_DE_COBRA), 75, false));
            cobra.tabelaDeLoot.Add(new ItemDeLoot(ItemPorID(ITEM_ID_PELE_DE_COBRA), 75, true));

            Monstro aranhaGigante = new Monstro(MONSTRO_ID_ARANHA_GIGANTE, "A Aranha gigante", 20, 17, 40, 10, 10);
            aranhaGigante.tabelaDeLoot.Add(new ItemDeLoot(ItemPorID(ITEM_ID_PRESA_DE_ARANHA), 75, true));
            aranhaGigante.tabelaDeLoot.Add(new ItemDeLoot(ItemPorID(ITEM_ID_TEIA_DE_ARANHA), 75, false));

            Monstros.Add(rato);
            Monstros.Add(cobra);
            Monstros.Add(aranhaGigante);
        }

        private static void InstanciaQuests()
        {
            Quest limparJardimDoAlquimista = new Quest(QUEST_ID_LIMPAR_JARDIM_DO_ALQUIMISTA, 
            "Limpe o jardim do alquimista", 
            "Mate os ratos no jardim do alquimista e traga 3 caudas de rato. Você receberá uma poção de cura e 10 ouro.", 20, 10);

            limparJardimDoAlquimista.ItemDeQuest.Add(new ItemDeQuest(ItemPorID(ITEM_ID_RABO_DE_RATO), 3));

            limparJardimDoAlquimista.RecompensaDeItem = ItemPorID(ITEM_ID_POCAO_DE_CURA);

            Quest limparCampo = new Quest(QUEST_ID_LIMPAR_CAMPO,
            "Limpe a fazenda",
            "Mate as cobras na fazenda e traga 3 presas de cobra. Você receberá um passe de aventureiro e 20 ouro", 20, 20);

            limparCampo.ItemDeQuest.Add(new ItemDeQuest(ItemPorID(ITEM_ID_PRESA_DE_COBRA), 3));

            limparCampo.RecompensaDeItem = ItemPorID(ITEM_ID_PASSE_DE_AVENTUREIRO);

            Quests.Add(limparJardimDoAlquimista);
            Quests.Add(limparCampo);
        }

        private static void InstanciaLocalizacoes()
        {
            Localizacao casa = new Localizacao(LOCALIZACAO_ID_CASA, "Casa", "Sua casa. Você realmente precisa limpar este lugar.");

            Localizacao centroDaCidade = new Localizacao(LOCALIZACAO_ID_CENTRO_DA_CIDADE, "Praça da cidade", "Você vê uma fonte no centro.");

            Localizacao cabanaDoAlquimista = new Localizacao(LOCALIZACAO_ID_CABANA_DO_ALQUIMISTA, "Cabana do alquimista", 
            "Há muitas plantas estranhas nas prateleiras");
            cabanaDoAlquimista.QuestDisponivelAqui = QuestPorID(QUEST_ID_LIMPAR_JARDIM_DO_ALQUIMISTA);

            Localizacao jardimDoAlquimista = new Localizacao(LOCALIZACAO_ID_JARDIM_DO_ALQUIMISTA, "Jardim do alquimista", 
            "Muitas plantas estão crescem aqui");
            jardimDoAlquimista.MonstroMorandoAqui = MonstroPorID(MONSTRO_ID_RATO);

            Localizacao casaDeCampo = new Localizacao(LOCALIZACAO_ID_CASA_NO_CAMPO, "Fazenda", "Uma pequena fazenda. Há um fazendeiro na frente.");
            casaDeCampo.QuestDisponivelAqui = QuestPorID(QUEST_ID_LIMPAR_CAMPO);

            Localizacao campo = new Localizacao(LOCALIZACAO_ID_CAMPO, "Campo", "Você vê colunas de vegetais crescendo aqui.");
            campo.MonstroMorandoAqui = MonstroPorID(MONSTRO_ID_COBRA);

            Localizacao postoDoGuarda = new Localizacao(LOCALIZACAO_ID_POSTO_DO_GUARDA, "Posto do guarda", "Tem um guarda grande e intimidador aqui.", 
            ItemPorID(ITEM_ID_PASSE_DE_AVENTUREIRO));

            Localizacao ponte = new Localizacao(LOCALIZACAO_ID_PONTE, "Ponte", "Uma ponte de pedra cruza um grande rio.");

            Localizacao campoDeAranhas = new Localizacao(LOCALIZACAO_ID_CAMPO_DE_ARANHAS, "Floresta", 
            "Você vê teias de aranha cobrindo as árvores desta floresta.");
            campoDeAranhas.MonstroMorandoAqui = MonstroPorID(MONSTRO_ID_ARANHA_GIGANTE);

            casa.LocalizacaoAoNorte = centroDaCidade;

            centroDaCidade.LocalizacaoAoNorte = cabanaDoAlquimista;
            centroDaCidade.LocalizacaoAoSul = casa;
            centroDaCidade.LocalizacaoAoLeste = postoDoGuarda;
            centroDaCidade.LocalizacaoAoOeste = casaDeCampo;

            casaDeCampo.LocalizacaoAoLeste = centroDaCidade;
            casaDeCampo.LocalizacaoAoOeste = campo;

            campo.LocalizacaoAoLeste = casaDeCampo;

            cabanaDoAlquimista.LocalizacaoAoSul = centroDaCidade;
            cabanaDoAlquimista.LocalizacaoAoNorte = jardimDoAlquimista;

            jardimDoAlquimista.LocalizacaoAoSul = cabanaDoAlquimista;

            postoDoGuarda.LocalizacaoAoLeste = ponte;
            postoDoGuarda.LocalizacaoAoOeste = centroDaCidade;

            ponte.LocalizacaoAoOeste = postoDoGuarda;
            ponte.LocalizacaoAoLeste = campoDeAranhas;

            campoDeAranhas.LocalizacaoAoOeste = ponte;

            Localizacoes.Add(casa);
            Localizacoes.Add(centroDaCidade);
            Localizacoes.Add(postoDoGuarda);
            Localizacoes.Add(cabanaDoAlquimista);
            Localizacoes.Add(jardimDoAlquimista);
            Localizacoes.Add(casaDeCampo);
            Localizacoes.Add(campo);
            Localizacoes.Add(ponte);
            Localizacoes.Add(campoDeAranhas);
        }

        // Método de laço que checa lista pelo ID do objeto desejado e o retorna.
        public static Item ItemPorID(int id)
        {
            foreach(Item item in Itens)
            {
                if(item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public static Monstro MonstroPorID(int id)
        {
            foreach(Monstro monstro in Monstros)
            {
                if(monstro.ID == id)
                {
                    return monstro;
                }
            }
            return null;
        }

        public static Quest QuestPorID(int id)
        {
            foreach(Quest quest in Quests)
            {
                if(quest.ID == id)
                {
                    return quest;
                }
            }
            return null;
        }

        public static Localizacao LocalizacaoPorID(int id)
        {
            foreach(Localizacao localizacao in Localizacoes)
            {
                if(localizacao.ID == id)
                {
                    return localizacao;
                }
            }
            return null;
        }
    }
}
