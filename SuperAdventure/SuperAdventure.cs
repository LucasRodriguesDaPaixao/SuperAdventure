using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;

namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private Jogador _jogador;
        private Monstro _monstroAtual;

        public SuperAdventure()
        {
            InitializeComponent();

            _jogador = new Jogador(10, 10, 20, 0, 5, 3, 3, 100, 1);
            MovePara(Mundo.LocalizacaoPorID(Mundo.LOCALIZACAO_ID_CASA));
            _jogador.Inventario.Add(new ItemNoInventario(Mundo.ItemPorID(Mundo.ITEM_ID_ESPADA_VELHA), 1));

            lblHitPoints.Text = _jogador.HitPointsAtual.ToString();
            lblGold.Text = _jogador.Ouro.ToString();
            lblExperience.Text = _jogador.PontosDeExperiencia.ToString();
            lblLevel.Text = _jogador.Level.ToString();
        }
        
        DataGridView dgvInventario = new DataGridView();
        DataGridView dgvQuests = new DataGridView();

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MovePara(_jogador.LocalizacaoAtual.LocalizacaoAoNorte);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MovePara(_jogador.LocalizacaoAtual.LocalizacaoAoLeste);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MovePara(_jogador.LocalizacaoAtual.LocalizacaoAoSul);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MovePara(_jogador.LocalizacaoAtual.LocalizacaoAoOeste);
        }

        private void MovePara(Localizacao localizacao)
        {
            ChecaPorItemNecessario(localizacao);

            AtualizaInterface(localizacao);

            // cura Jogador
            _jogador.HitPointsAtual = _jogador.HitPointsMaximo;

            // Atualiza hitpoints na UI
            lblHitPoints.Text = _jogador.HitPointsAtual.ToString();

            ChecaPorQuest(localizacao);

            ChecaPorMonstros(localizacao);
            
            AtualizaInventarioNaUI();
            AtualizaQuestNaUI();
            AtualizaArmaNaUI();
            AtualizaPocaoNaUI();
        }

        /* Começo do método MovePara */
        private void ChecaPorItemNecessario(Localizacao localizacao)
        {
            //Checa se o lugar precisa de items para entrar
            if (!_jogador.TemItemNecessarioParaEntrar(localizacao))
            {
                rtbMessages.Text += $"Você precisa ter {localizacao.ItemNecessarioParaEntrar.Nome} para entrar neste lugar.{Environment.NewLine}";
                return;
            }
        }
        private void AtualizaInterface(Localizacao localizacao)
        {
            // Atualiza a posição do jogador
            _jogador.LocalizacaoAtual = localizacao;

            // Mostra/Esconde botões de movimentação
            btnNorth.Visible = (localizacao.LocalizacaoAoNorte != null);
            btnEast.Visible = (localizacao.LocalizacaoAoLeste != null);
            btnSouth.Visible = (localizacao.LocalizacaoAoSul != null);
            btnWest.Visible = (localizacao.LocalizacaoAoOeste != null);

            // Atualiza nome e descrição do lugar
            rtbLocation.Text = localizacao.Nome + Environment.NewLine;
            rtbLocation.Text += localizacao.Descricao + Environment.NewLine;
        }
        private void ChecaPorQuest(Localizacao localizacao)
        {
            // Checa se a localização possui quest
            if (localizacao.QuestDisponivelAqui != null)
            {
                // Checa se o jogador já tem a quest e se ele já completou ela
                bool jogadorJaTemQuest = _jogador.TemEstaQuest(localizacao.QuestDisponivelAqui);
                bool jogadorJaCompletouQuest = _jogador.CompletouEstaQuest(localizacao.QuestDisponivelAqui);

                // Checa se o jogador tem a quest
                if (jogadorJaTemQuest)
                {
                    // Se o jogador não completou a quest
                    if (!jogadorJaCompletouQuest)
                    {
                        // Checa se o jogador tem todos os itens de quest
                        bool jogadorTemTodosItemsDeQuest = _jogador.TemTodosItensDeQuest(localizacao.QuestDisponivelAqui);

                        ChecaSeJogadorTemItensDeQuest(jogadorTemTodosItemsDeQuest, localizacao);
                    }
                    JogadorNaoTemQuest(localizacao);
                }
            }
        }
        private void ChecaSeJogadorTemItensDeQuest(bool temItemQuest, Localizacao localizacao)
        {
            // Jogador tem todos os itens necessários da quest
            if (temItemQuest)
            {
                // Apresenta mensagem
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += $"Você completou a quest {localizacao.QuestDisponivelAqui.Nome}.{Environment.NewLine}";

                // Remove item da quest do inventário
                _jogador.RemoveItemDeQuest(localizacao.QuestDisponivelAqui);

                // Entrega recompensa de quest
                rtbMessages.Text += $"Você recebeu {Environment.NewLine}";

                rtbMessages.Text += $"{localizacao.QuestDisponivelAqui.RecompensaDeExperiencia.ToString()} pontos de experiência{Environment.NewLine}";

                rtbMessages.Text += $"{localizacao.QuestDisponivelAqui.RecompensaDeOuro.ToString()} Ouro{Environment.NewLine}";

                rtbMessages.Text += $"{localizacao.QuestDisponivelAqui.RecompensaDeItem.Nome}{Environment.NewLine}";

                rtbMessages.Text += Environment.NewLine;

                _jogador.PontosDeExperiencia += localizacao.QuestDisponivelAqui.RecompensaDeExperiencia;
                _jogador.Ouro += localizacao.QuestDisponivelAqui.RecompensaDeOuro;

                // Adiciona recompensa ao inventário do jogador
                _jogador.AddItemAoInventario(localizacao.QuestDisponivelAqui.RecompensaDeItem);

                // Marca quest como concluida
                _jogador.MarcaQuestComoConcluida(localizacao.QuestDisponivelAqui);
            }
        }
        private void JogadorNaoTemQuest(Localizacao localizacao)
        {
            // O jogador ainda não possui a quest

            // Apresenta quest
            rtbMessages.Text += $"Você recebeu a quest {localizacao.QuestDisponivelAqui.Nome}.{Environment.NewLine}";

            rtbMessages.Text += localizacao.QuestDisponivelAqui.Descricao + Environment.NewLine;

            rtbMessages.Text += $"Para completa-la, retorne com: {Environment.NewLine}";
            foreach (ItemDeQuest itemDeQuest in localizacao.QuestDisponivelAqui.ItemDeQuest)
            {
                if (itemDeQuest.Quantidade == 1)
                {
                    rtbMessages.Text += $"{itemDeQuest.Quantidade.ToString()} {itemDeQuest.Detalhes.Nome}{Environment.NewLine}";
                }
                else
                {
                    rtbMessages.Text += $"{itemDeQuest.Quantidade.ToString()} {itemDeQuest.Detalhes.NamePlural}{Environment.NewLine}";
                }
            }
            rtbMessages.Text += Environment.NewLine;

            // Adiciona quest as quests do jogador
            _jogador.Quests.Add(new PlayerQuest(localizacao.QuestDisponivelAqui));
        }
        private void ChecaPorMonstros(Localizacao localizacao)
        {
            // Does the location have a monster?
            if (localizacao.MonstroMorandoAqui != null)
            {
                rtbMessages.Text += "You see a " + localizacao.MonstroMorandoAqui.Nome + Environment.NewLine;

                // Make a new monster, using the values from the standard monster in the World.Monster list
                Monstro standardMonster = Mundo.MonstroPorID(localizacao.MonstroMorandoAqui.ID);

                _monstroAtual = new Monstro(standardMonster.ID, standardMonster.Nome, standardMonster.MaximumDamage,
                    standardMonster.RecompensaDeExperiencia, standardMonster.RecompensaDeOuro, standardMonster.HitPointsAtual, standardMonster.HitPointsMaximo);

                foreach (ItemDeLoot lootItem in standardMonster.tabelaDeLoot)
                {
                    _monstroAtual.tabelaDeLoot.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboPotions.Visible = true;
                btnUseWeapon.Visible = true;
                btnUsePotion.Visible = true;
            }
            else
            {
                _monstroAtual = null;

                cboWeapons.Visible = false;
                cboPotions.Visible = false;
                btnUseWeapon.Visible = false;
                btnUsePotion.Visible = false;
            }
        }
        private void AtualizaInventarioNaUI()
        {
            dgvInventario.RowHeadersVisible = false;

            dgvInventario.ColumnCount = 2;
            dgvInventario.Columns[0].Name = "Name";
            dgvInventario.Columns[0].Width = 197;
            dgvInventario.Columns[1].Name = "Quantity";

            dgvInventario.Rows.Clear();

            foreach (ItemNoInventario itemNoInventario in _jogador.Inventario)
            {
                if (itemNoInventario.Quantidade > 0)
                {
                    dgvInventario.Rows.Add(new[] { itemNoInventario.Detalhes.Nome, itemNoInventario.Quantidade.ToString() });
                }
            }
        }
        private void AtualizaQuestNaUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _jogador.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Detalhes.Nome, playerQuest.IsCompletado.ToString() });
            }
        }
        private void AtualizaArmaNaUI()
        {
            List<Arma> weapons = new List<Arma>();

            foreach (ItemNoInventario inventoryItem in _jogador.Inventario)
            {
                if (inventoryItem.Detalhes is Arma)
                {
                    if (inventoryItem.Quantidade > 0)
                    {
                        weapons.Add((Arma)inventoryItem.Detalhes);
                    }
                }
            }

            if (weapons.Count == 0)
            {
                // The player doesn't have any weapons, so hide the weapon combobox and "Use" button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";

                cboWeapons.SelectedIndex = 0;
            }
        }
        private void AtualizaPocaoNaUI()
        {
            List<PocaoDeCura> healingPotions = new List<PocaoDeCura>();

            foreach (ItemNoInventario inventoryItem in _jogador.Inventario)
            {
                if (inventoryItem.Detalhes is PocaoDeCura)
                {
                    if (inventoryItem.Quantidade > 0)
                    {
                        healingPotions.Add((PocaoDeCura)inventoryItem.Detalhes);
                    }
                }
            }

            if (healingPotions.Count == 0)
            {
                // The player doesn't have any potions, so hide the potion combobox and "Use" button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ID";

                cboPotions.SelectedIndex = 0;
            }
        }
        /* Fim do método MovePara */
        
        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            JogadorAtacaMonstro();

            // Checa se o monstro está morto
            if (_monstroAtual.HitPointsAtual <= 0)
            {
                MonstroEstaMorto();

                RecebeItemLoot();

                AtualizaInformacoesJogador();
            }
            else
            {
                MonstroAtacaJogador();
            }
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            // Recebe a poção selecionada no comboBox
            PocaoDeCura pocao = (PocaoDeCura)cboPotions.SelectedItem;

            CuraJogador(pocao);

            // Apresenta mensagem
            rtbMessages.Text += $"Você bebeu uma {pocao.Nome}{Environment.NewLine}";

            // Monstro recebe sua chance de atacar
            MonstroAtacaJogador();

            // Atualiza UI
            lblHitPoints.Text = _jogador.HitPointsAtual.ToString();
            AtualizaInventarioNaUI();
            AtualizaPocaoNaUI();
        }

        /* Começo btnUseWeapon */
        private void JogadorAtacaMonstro()
        {
            // Recebe a arma selecionada no comboBox
            Arma armaAtual = (Arma)cboWeapons.SelectedItem;

            // Determina o dano
            int danoAoMonstro = GeradorDeNumeroAleatorio.NumeroEntre(armaAtual.DanoMinimo, armaAtual.DanoMaximo);

            // Aplica o dano
            _monstroAtual.HitPointsAtual -= danoAoMonstro;

            // Mostra mensagem
            rtbMessages.Text += $"Você acertou {_monstroAtual.Nome} causando {danoAoMonstro.ToString()} de dano{Environment.NewLine}";
        }
        private void MonstroEstaMorto()
        {
            // Monstro está morto
            rtbMessages.Text += Environment.NewLine;
            rtbMessages.Text += $"Você derrotou {_monstroAtual.Nome}{Environment.NewLine}";

            // Recompensa jogador com experiência
            _jogador.PontosDeExperiencia += _monstroAtual.RecompensaDeExperiencia;
            rtbMessages.Text += $"Você recebeu {_monstroAtual.RecompensaDeExperiencia.ToString()} pontos de experiência.{Environment.NewLine}";

            // Recompensa jogador com Ouro
            _jogador.Ouro += _monstroAtual.RecompensaDeOuro;
            rtbMessages.Text += $"Você recebeu {_monstroAtual.RecompensaDeOuro.ToString()} de ouro.{Environment.NewLine}";
        }
        private void RecebeItemLoot()
        {
            // Recebe item aleatório da tabela de loot do monstro
            List<ItemNoInventario> lootedItems = new List<ItemNoInventario>();

            // Compara se o numerado gerado automáticamente é menor igual que a porcentagem de drop
            foreach (ItemDeLoot itemDeLoot in _monstroAtual.tabelaDeLoot)
            {
                if (GeradorDeNumeroAleatorio.NumeroEntre(1, 100) <= itemDeLoot.PorcentagemDeDrop)
                {
                    lootedItems.Add(new ItemNoInventario(itemDeLoot.Detalhes, 1));
                }
            }

            // Se nenhum item for selecionado, entregar o item default.
            if (lootedItems.Count == 0)
            {
                foreach (ItemDeLoot itemDeLoot in _monstroAtual.tabelaDeLoot)
                {
                    if (itemDeLoot.IsItemDefault)
                    {
                        lootedItems.Add(new ItemNoInventario(itemDeLoot.Detalhes, 1));
                    }
                }
            }

            // Adiciona o loot ao inventário do jogador
            foreach (ItemNoInventario itemNoInventario in lootedItems)
            {
                _jogador.AddItemAoInventario(itemNoInventario.Detalhes);

                if (itemNoInventario.Quantidade == 1)
                {
                    rtbMessages.Text += $"Você recebeu {itemNoInventario.Quantidade.ToString()} {itemNoInventario.Detalhes.Nome}{Environment.NewLine}";
                }
                else
                {
                    rtbMessages.Text += $"Você recebeu {itemNoInventario.Quantidade.ToString()}{itemNoInventario.Detalhes.NamePlural}{Environment.NewLine}";
                }
            }
        }
        private void AtualizaInformacoesJogador()
        {
            // Atualiza as informações do jogador
            lblHitPoints.Text = _jogador.HitPointsAtual.ToString();
            lblGold.Text = _jogador.Ouro.ToString();
            lblExperience.Text = _jogador.PontosDeExperiencia.ToString();
            lblLevel.Text = _jogador.Level.ToString();

            AtualizaInventarioNaUI();
            AtualizaArmaNaUI();
            AtualizaPocaoNaUI();

            rtbMessages.Text += Environment.NewLine;

            // Move jogador para a posição atual para cura-lo e spawnar um novo monstro
            MovePara(_jogador.LocalizacaoAtual);
        }
        /* Começo btnUsePotion */
        private void MonstroAtacaJogador()
        {
            // Determina quantidade de dano causado ao jogador
            int danoAoJogador = GeradorDeNumeroAleatorio.NumeroEntre(0, _monstroAtual.MaximumDamage);

            // Apresenta mensagem ao jogador
            rtbMessages.Text += $"{_monstroAtual.Nome} causou {danoAoJogador.ToString()} pontos de dano.{Environment.NewLine}";

            // Subtrai dano da vida do jogador
            _jogador.HitPointsAtual -= danoAoJogador;

            // Atualiza UI
            lblHitPoints.Text = _jogador.HitPointsAtual.ToString();

            // Se vida do jogador chegar a 0
            if (_jogador.HitPointsAtual <= 0)
            {
                // Mostra mensagem
                rtbMessages.Text += $"{_monstroAtual.Nome} te matou.{Environment.NewLine}";

                // Move jogador para sua casa
                MovePara(Mundo.LocalizacaoPorID(Mundo.LOCALIZACAO_ID_CASA));
            }
        }
        /* Fim btnUsePotion */
        /* Fim btnUseWeapon */
        /* Começo btnUsePotion */
        private void CuraJogador(PocaoDeCura pocao)
        {


            // Cura Jogador
            _jogador.HitPointsAtual = (_jogador.HitPointsAtual + pocao.QuantidadeDeCura);

            // HitPoint atual não pode exceder o máximo
            if (_jogador.HitPointsAtual > _jogador.HitPointsMaximo)
            {
                _jogador.HitPointsAtual = _jogador.HitPointsMaximo;
            }

            // Remove poção do inventário do jogador
            foreach (ItemNoInventario itemNoInventario in _jogador.Inventario)
            {
                if (itemNoInventario.Detalhes.ID == pocao.ID)
                {
                    itemNoInventario.Quantidade--;
                    break;
                }
            }
        }
        /* Começo btnUsePotion */
        /* Fim btnUsePotion */
    }
}
