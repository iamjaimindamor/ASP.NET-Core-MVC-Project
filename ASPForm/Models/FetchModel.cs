using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPForm.Models
{
    public class FetchModel
    {
        public int Id { get; set; }

        public List<SelectListItem> abha_wale{ get; set; }
    }
}
