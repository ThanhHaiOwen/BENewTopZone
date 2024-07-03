using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BENewTopZone.Areas.Admin.Views.ViewModel
{
    public class ProListadmin
    {
        [Key]
        public int ProID { get; set; }
        public string ProName { get; set; }
        public string ProImage {  get; set; }
        public string NameDecription { get; set; }

        // Thêm thông tin từ bảng Category
        public int CatID { get; set; }
        public string NameCate { get; set; }

        // Thêm thông tin từ bảng ProDetail
        public int ProDeID { get; set; }
        public double Price { get; set; }
        public int RemainQuantity { get; set; }
        public Nullable<int> SoldQuantity { get; set; }
        public Nullable<int> ViewQuantity { get; set; }

        // Thêm thông tin từ bảng Color
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public string RGB { get; set; }

        
    }
}