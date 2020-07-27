using System;
using System.Collections.Generic;
using System.Text;

namespace FlatCopy
{
    //Usado para saber que objetos internos son "interesantes de copiar"
    public interface IId
    {
        public int Id { get; set; }
    }
}
