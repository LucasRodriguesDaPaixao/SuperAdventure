using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monstro : LivingCreature
    {
        // Armazena valores de inimigos
        public int ID { get; set; }
        public string Nome { get; set; }
        public int RecompensaDeExperiencia { get; set; }
        public int RecompensaDeOuro { get; set; }
        public int MaximumDamage {get;set;}

        // Cria lista de objetos para loots
        public List<ItemDeLoot> tabelaDeLoot { get; set; }

        // Cria construtor Monster com base 
        public Monstro (int id, string nome, int danoMaximo, int recompensaDeExperiencia, int recompensaDeOuro, 
                        int hitPointsAtual,
                        int hitPointsMaximo)
                        : base (hitPointsMaximo, hitPointsAtual) // Redireciona dados para o construtor da classe mãe
        {
            ID = id;
            Nome = nome;
            RecompensaDeExperiencia = recompensaDeExperiencia;
            RecompensaDeOuro = recompensaDeOuro;
            MaximumDamage = danoMaximo;

            // Instancia uma lista vazia
            tabelaDeLoot = new List<ItemDeLoot>();
        }
    }
}
