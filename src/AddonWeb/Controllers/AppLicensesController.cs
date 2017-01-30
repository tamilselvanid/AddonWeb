using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AddonWeb.Data;
using addon.Models.Licensing;

namespace AddonWeb.Controllers
{
    [Produces("application/json")]
    [Route("api/AppLicenses")]
    public class AppLicensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppLicensesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/AppLicenses
        [HttpGet]
        public IEnumerable<AppLicenceMaster> GetAddonAppLicense()
        {
            return _context.AddonAppLicense;
        }

        // GET: api/AppLicenses/5
        [HttpGet("{id}")]
        public IEnumerable<AppLicViewModel> GetAddonAppLicense(string id)
        {
            

            var addonAppLicense =  (from p in _context.AddonAppLicense // .Includes("Addresses") here?
                         select new AppLicViewModel()
                         {
                             AppLicenseKey = p.AppLicenseKey, HardwareInfo = p.AppHardwares.Select(a => a.HardwareInfo)
                             
                        }).Where(m=>m.AppLicenseKey==id);


           // AppLicenceMaster addonAppLicense = await _context.AddonAppLicense.SingleOrDefaultAsync(m => m.AppLicenseKey == id);

            //if (addonAppLicense == null)
            //{
            //    return NotFound();
            //}

            return addonAppLicense;
        }

        // PUT: api/AppLicenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddonAppLicense([FromRoute] string id, [FromBody] AppLicenceMaster addonAppLicense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addonAppLicense.AppLicenseKey)
            {
                return BadRequest();
            }

            _context.Entry(addonAppLicense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddonAppLicenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AppLicenses
        [HttpPost]
        public async Task<IActionResult> PostAddonAppLicense([FromBody] AppLicenceViewModel addonAppLicense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppLicenceMaster licMaster = new AppLicenceMaster();
            licMaster.AppLicenseKey = addonAppLicense.AppLicenseKey;
            licMaster.AppId = addonAppLicense.AppId;
            licMaster.Verision = addonAppLicense.Verision;
            licMaster.CreatedDate = addonAppLicense.CreatedDate;
            licMaster.CurrentVerision = addonAppLicense.CurrentVerision;

            _context.AddonAppLicense.Add(licMaster);

            AppHardwareInfo licHardware = new AppHardwareInfo();
            //licHardware.AppHardwareInfoId = 1;
            licHardware.ActivatedDate = addonAppLicense.ActivatedDate;
            licHardware.HardwareInfo = addonAppLicense.HardwareInfo;
            licHardware.ExpiryDate = addonAppLicense.ExpiryDate;
            licHardware.AppLic = licMaster;
         
            _context.AppHardwareInfo.Add(licHardware);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AddonAppLicenseExists(addonAppLicense.AppLicenseKey))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAddonAppLicense", new { id = addonAppLicense.AppLicenseKey }, addonAppLicense);
        }

        // DELETE: api/AppLicenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddonAppLicense([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppLicenceMaster addonAppLicense = await _context.AddonAppLicense.SingleOrDefaultAsync(m => m.AppLicenseKey == id);
            if (addonAppLicense == null)
            {
                return NotFound();
            }

            _context.AddonAppLicense.Remove(addonAppLicense);
            await _context.SaveChangesAsync();

            return Ok(addonAppLicense);
        }

        private bool AddonAppLicenseExists(string id)
        {
            return _context.AddonAppLicense.Any(e => e.AppLicenseKey == id);
        }
    }
}