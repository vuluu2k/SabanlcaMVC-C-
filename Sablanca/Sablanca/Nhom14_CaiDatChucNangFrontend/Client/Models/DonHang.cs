namespace Sablanca.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonHang")]
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            ChiTiet_DH = new HashSet<ChiTiet_DH>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }

        public int MaTK { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTenKH { get; set; }

        [Required]
        [StringLength(11)]
        public string DienThoaiKH { get; set; }

        [Required]
        [StringLength(200)]
        public string DiaChi { get; set; }

        public DateTime NgayDat { get; set; }

        [Required]
        [StringLength(100)]
        public string TinhTrangDH { get; set; }

        [Required]
        [StringLength(100)]
        public string HinhThucTT { get; set; }

        [StringLength(200)]
        public string GhiChu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_DH> ChiTiet_DH { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
