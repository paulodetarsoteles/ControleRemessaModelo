﻿namespace ControleRemessaModelo.Entidades.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string Role_Id { get; set; } = string.Empty;
    }
}
