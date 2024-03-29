//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TK1.Data.Bizz.Client.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PropertyAd
    {
        public PropertyAd()
        {
            this.PropertyAdDetails = new HashSet<PropertyAdDetail>();
            this.PropertyAdPics = new HashSet<PropertyAdPic>();
        }
    
        public int PropertyAdID { get; set; }
        public string PropertyAdTypeID { get; set; }
        public string CustomerCode { get; set; }
        public string PropertyAdStatusID { get; set; }
        public string CategoryName { get; set; }
        public string PropertyTypeName { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public float TotalArea { get; set; }
        public int TotalRooms { get; set; }
        public float InternalArea { get; set; }
        public float ExternalArea { get; set; }
        public float Value { get; set; }
        public Nullable<float> CityTaxes { get; set; }
        public Nullable<float> CondoTaxes { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public bool Featured { get; set; }
        public bool Visible { get; set; }
        public string FullDescription { get; set; }
        public string AreaDescription { get; set; }
        public string CondoDescription { get; set; }
        public string PicUrl { get; set; }
    
        public virtual ICollection<PropertyAdDetail> PropertyAdDetails { get; set; }
        public virtual ICollection<PropertyAdPic> PropertyAdPics { get; set; }
        public virtual PropertyReleaseAd PropertyReleaseAd { get; set; }
    }
}
