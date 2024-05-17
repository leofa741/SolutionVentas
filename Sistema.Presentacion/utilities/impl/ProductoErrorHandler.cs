using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.Presentacion.utilities.impl
    {
    internal class ProductoErrorHandler : IErrorHandler

        {
        public void ShowErrorMessage(string msg)
            {
            MessageBox.Show(msg, "Error en Seccion Articulos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
