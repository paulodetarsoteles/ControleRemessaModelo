using ControleRemessaModelo.Entidades.Models;

namespace ControleRemessaModelo.Negocio.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public Role Role { get; set; } = new();
    }
}
