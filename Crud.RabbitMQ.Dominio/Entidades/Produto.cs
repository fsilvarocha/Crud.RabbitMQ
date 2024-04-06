namespace Crud.RabbitMQ.Dominio.Entidades
{
    public class Produto : BaseEntidade
    {
        public string Nome { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
