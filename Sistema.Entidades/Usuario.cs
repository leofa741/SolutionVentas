using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Entidades
    {
    public class Usuario
        {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public List<Componente> Permisos { get; set; } = new List<Componente>();
        }






    }
