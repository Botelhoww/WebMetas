using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WeBMetas.Models
{
    [Table("Metas")]
    public class CadastroMetasModel
    {
        [Column("Id_Meta")]
        public int Id{ get; set; }
        [Column("Nome_Meta")]
        public string NomeMeta { get; set; }
        [Column("Meta")]
        public string Meta { get; set; }
        [Column("Qtd_Mensal")]
        public string QtdGuardarPorMes { get; set; }
        [Column("Link_Imagem_Meta")]
        public string LinkImagemMeta { get; set; }
    }
}
