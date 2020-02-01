using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RegistroInscripcion.Entidades
{
    public class Inscripciones
    {
        [Key]
        public int IncripcionId { get; set; }
        public DateTime Fecha { get; set; }
        public int PersonaId { get; set; }
        public string Comentarios { get; set; }
        public decimal Monto { get; set; }
        public decimal Balance { get; set; }
        public decimal Deposito { get; set; }


        public Inscripciones()
        {
            IncripcionId = 0;
            Fecha = DateTime.Now;
            PersonaId = 0;
            Deposito = 0;
            Monto = 0;
            Balance = 0;
            Comentarios = string.Empty;
        }
    }
}
