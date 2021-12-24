namespace Sablanca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTiet_DH = new HashSet<ChiTiet_DH>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        public int MaDM { get; set; }

        [Required]
        [StringLength(100)]
        public string TenSP { get; set; }

        public decimal GiaBan { get; set; }

        public decimal GiaGoc { get; set; }

        [Required]
        [StringLength(20)]
        public string MauSac { get; set; }

        [Required]
        [StringLength(100)]
        public string ChatLieu { get; set; }

        [StringLength(100)]
        public string LoaiDayDeo { get; set; }

        [Required]
        [StringLength(50)]
        public string KichThuoc { get; set; }

        public int SoNgan { get; set; }

        [Required]
        [StringLength(50)]
        public string Dong { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTaSP { get; set; }

        [Required]
        [StringLength(150)]
        public string AnhSP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_DH> ChiTiet_DH { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
