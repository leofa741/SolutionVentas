using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
    {
    public abstract class Componente
        {
        public string Nombre { get; set; }
        public abstract void Agregar(Componente c);
        public abstract void Remover(Componente c);
        public abstract List<Componente> ObtenerHijos();
        }

    }
