using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item
    {
        // Armazena valores de item
        public int ID { get; set; }
        public string Nome { get; set; }
        public string NamePlural { get; set; }

        // Construtor da classe base item
        public Item (int id, string nome, string nomePlural)
        {
            ID = id;
            Nome = nome;
            NamePlural = nomePlural;
        }
    }
}
