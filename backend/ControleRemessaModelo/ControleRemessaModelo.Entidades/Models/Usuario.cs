namespace ControleRemessaModelo.Entidades.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefone { get; set; }
    }
}
