namespace Sablanca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
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
        [DisplayName("Mã SP")]
        [Required(ErrorMessage = "Không được để trống!")]
        public int MaSP { get; set; }

        [DisplayName("Mã DM")]
        [Required(ErrorMessage = "Không được để trống!")]
        public int MaDM { get; set; }


        [DisplayName("Tên SP")]
        [Required(ErrorMessage = "Không được để trống!")]
        [StringLength(100)]
        public string TenSP { get; set; }
        [DisplayName("Giá bán")]
        [Required(ErrorMessage = "Không được để trống!")]
        public decimal GiaBan { get; set; }
        [DisplayName("Giá gốc")]
        [Required(ErrorMessage = "Không được để trống!")]
        public decimal GiaGoc { get; set; }

        [StringLength(20)]
        [DisplayName("Màu sắc")]
        [Required(ErrorMessage = "Không được để trống!")]
        public string MauSac { get; set; }

        [StringLength(100)]
        [DisplayName("Chất liệu")]
        [Required(ErrorMessage = "Không được để trống!")]
        public string ChatLieu { get; set; }

        [StringLength(100)]
        [DisplayName("Loại dây đeo")]
        public string LoaiDayDeo { get; set; }

        [StringLength(50)]
        [DisplayName("Kích thước")]
        [Required(ErrorMessage = "Không được để trống!")]

        public string KichThuoc { get; set; }
        [DisplayName("Số ngăn")]
        [Required(ErrorMessage = "Không được để trống!")]
        public int SoNgan { get; set; }

        [StringLength(50)]
        [DisplayName("Dòng")]
        [Required(ErrorMessage = "Không được để trống!")]
        public string Dong { get; set; }

        [Column(TypeName = "ntext")]
        [DisplayName("Mô tả SP")]
        public string MoTaSP { get; set; }

        [DisplayName("Ảnh SP")]
        
        [StringLength(150)]
        public string AnhSP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_DH> ChiTiet_DH { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
