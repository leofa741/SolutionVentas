using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
    {
    public class Familia : Componente
        {
        private List<Componente> _hijos = new List<Componente>();

        public override void Agregar(Componente c)
            {
            _hijos.Add(c);
            }

        public override void Remover(Componente c)
            {
            _hijos.Remove(c);
            }

        public override List<Componente> ObtenerHijos()
            {
            return _hijos;
            }
        }

    }
