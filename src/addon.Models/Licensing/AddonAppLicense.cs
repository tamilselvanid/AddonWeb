using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace addon.Models.Licensing
{
    public class AppLicenceViewModel
    {
        [Key]
        public string AppLicenseKey { get; set; }
        public string AppId { get; set; }
        public string Verision { get; set; }
        public string CurrentVerision { get; set; }
        public DateTime CreatedDate { get; set; }
      
        public string HardwareInfo { get; set; }
        public DateTime ActivatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    

    }
    public class AppLicenceMaster
    {
        [Key]
        public string AppLicenseKey { get; set; }
        public string AppId { get; set; }
        public string Verision { get; set; }
        public string CurrentVerision { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<AppHardwareInfo> AppHardwares { get; set; }


    }
    public class AppLicViewModel
    {
        public string AppLicenseKey { get; set; }
        public IEnumerable<String> HardwareInfo { get; set; }

    }
public class AppHardwareInfo
    {
        public int AppHardwareInfoId { get; set; }

        public string AppLicenseKey { get; set; }

        [ForeignKey("AppLicenseKey")]
        public virtual AppLicenceMaster AppLic { get; set; }

        public string HardwareInfo { get; set; }
        public DateTime ActivatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime LastLogged { get; set; }

    }
    public class AppStatusInfo
    {
        public int AppStatusInfoId { get; set; }
        public string AppLicenseKey { get; set; }
        public int MaxDevices { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
    public class AppStatusType
    {
        public int AppStatusTypeId  { get; set; }

        public string AppStatusName { get; set; }
    }
    public class AppMaster
    {
        public int AppMasterId { get; set; }
        public string AppName { get; set; }
    }

}
