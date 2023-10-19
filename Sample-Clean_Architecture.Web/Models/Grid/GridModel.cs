using System.ComponentModel.DataAnnotations;

namespace Sample_Clean_Architecture.Web.Models.Grid
{
    public class GridModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "{0} is required")]
        public string Name { set; get; }
        public string AuthorName { set; get; }
        public int Category_Id { set; get; }
        public int AccountGroup_Id { set; get; }
        public int Status { set; get; }
    }

}
