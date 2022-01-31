using System;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Core
{
  public class Resturant
  {
	public int Id { get; set; }
	[Required, StringLength(80)]
	public String Name { get; set; }
	[Required]
	[StringLength(255)]
	public String Location { get; set; }
	public CusineType Cuisine { get; set; }
  }
}
