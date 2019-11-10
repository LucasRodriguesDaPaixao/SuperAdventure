using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Jogador : LivingCreature
    {
        // Guarda Status do jogador
        public int Ouro { get; set; }
        public int PontosDeStatus { get; set; }
        public int Vigor { get; set; }
        public int Forca { get; set; }
        public int PontosDeExperiencia { get; set; }
        public float ExperienciaParaProximoNivel { get; set; }
        public int Level { get; set; }
        public Localizacao LocalizacaoAtual { get; set; }

        // Cria listas para objetos de Inventory e Quests
        public List<ItemNoInventario> Inventario { get; set; }
        public List<PlayerQuest> Quests { get; set; }

        // Construtor de classe
        public Jogador(int hitPointsAtual, int hitPointMaximo, int ouro, int pontosDeExperiencia, 
                      int pontosDeStatus, int forca, int vigor, float experienciaParaProximoNivel, int level) 
                      : base(hitPointsAtual, hitPointMaximo)
        {
            Vigor = vigor;
            Forca = forca;
            PontosDeStatus = pontosDeStatus;
            Ouro = ouro;
            PontosDeExperiencia = pontosDeExperiencia;
            ExperienciaParaProximoNivel = experienciaParaProximoNivel;
            Level = level;

            // Instancia listas vazias de objetos
            Inventario = new List<ItemNoInventario>();
            Quests = new List<PlayerQuest>();
        }

        public bool TemItemNecessarioParaEntrar(Localizacao localizacao)
        {
            if (localizacao.ItemNecessarioParaEntrar == null)
            {
                // Não precisa de nenhum item para entrar neste lugar então retorna verdadeiro
                return true;
            }

            // Vê se o jogador tem o item no inventário
            foreach (ItemNoInventario ii in Inventario)
            {
                if (ii.Detalhes.ID == localizacao.ItemNecessarioParaEntrar.ID)
                {
                    // O item foi encontrado  então retorna verdadeiro
                    return true;
                }
            }

            // O item não foi encontrado no inventário então retorna falso
            return false;
        }

        public bool TemEstaQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Detalhes.ID == quest.ID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CompletouEstaQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if (playerQuest.Detalhes.ID == quest.ID)
                {
                    return playerQuest.IsCompletado;
                }
            }

            return false;
        }

        public bool TemTodosItensDeQuest(Quest quest)
        {
            // Checa se o jogador tem todos os itens necessários para completar a quest
            foreach (ItemDeQuest idq in quest.ItemDeQuest)
            {
                bool encontrouItemNoInventario = false;

                // Checa cada item no inventário do jogador e confere se ele tem o item e a quantidade necessária
                foreach (ItemNoInventario ii in Inventario)
                {
                    // Confere se a quantidade é suficiente
                    if (ii.Detalhes.ID == idq.Detalhes.ID) 
                    {
                        encontrouItemNoInventario = true;

                        // O jogador não tem nenhum item da quest no inventário
                        if (ii.Quantidade < idq.Quantidade) 
                        {
                            return false;
                        }
                    }
                }

                // O jogador não tem nenhum item da quest no inventário
                if (!encontrouItemNoInventario)
                {
                    return false;
                }
            }

            // O jogador possui todos os itens necessários para concluir a quest
            return true;
        }

        public void RemoveItemDeQuest(Quest quest)
        {
            foreach (ItemDeQuest idq in quest.ItemDeQuest)
            {
                foreach (ItemNoInventario ii in Inventario)
                {
                    if (ii.Detalhes.ID == idq.Detalhes.ID)
                    {
                        // Retira do inventário do jogador o necessário para concluir a quest
                        ii.Quantidade -= idq.Quantidade;
                        break;
                    }
                }
            }
        }

        public void AddItemAoInventario(Item itemParaAdd)
        {
            foreach (ItemNoInventario ii in Inventario)
            {
                if (ii.Detalhes.ID == itemParaAdd.ID)
                {
                    // O jogador tem o item então incrementa o item em um
                    ii.Quantidade++;

                    // Item incrementado então saia da função
                    return;
                }
            }

            // O jogador não tinha o item então adicione o item ao inventário
            Inventario.Add(new ItemNoInventario(itemParaAdd, 1));
        }

        public void MarcaQuestComoConcluida(Quest quest)
        {
            // Encontra a quest na lista de quests do jogador
            foreach (PlayerQuest pq in Quests)
            {
                if (pq.Detalhes.ID == quest.ID)
                {
                    // Marca quest como concluida
                    pq.IsCompletado = true;

                    // Quest encontrada e marcada como concluida então saia da função
                    return; 
                }
            }
        }
    }
}