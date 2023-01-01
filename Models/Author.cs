using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminBlog.Models;

public partial class Author
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Surname is required")]
    public string Surname { get; set; } = null!;
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; } = new List<Blog>();
}

/* before scaffolding
using  System;
using System.ComponentModel.DataAnnotations;

namespace AdminBlog.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
*/