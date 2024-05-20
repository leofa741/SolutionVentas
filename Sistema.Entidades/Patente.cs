using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
    {
    public class Patente : Componente
        {
        public override void Agregar(Componente c)
            {
            throw new NotImplementedException("Una patente no puede agregar componentes.");
            }

        public override void Remover(Componente c)
            {
            throw new NotImplementedException("Una patente no puede remover componentes.");
            }

        public override List<Componente> ObtenerHijos()
            {
            return new List<Componente>();
            }
        }

    }
