using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Resturants
{
  public class EditModel : PageModel
  {
	private readonly IResturantData resturantData;
	private readonly IHtmlHelper htmlHelper;

	[BindProperty]
	public Resturant Resturant { get; set; }
	public IEnumerable<SelectListItem> Cuisines { get; set; }
	public string PageTitle { get; set; }
	public string PageHeading { get; set; }

	public EditModel(IResturantData resturantData, IHtmlHelper htmlHelper)
	{
	  this.resturantData = resturantData;
	  this.htmlHelper = htmlHelper;
	}
	/*public IActionResult OnGet(int resturantId)*/
	public IActionResult OnGet(int? resturantId)
	{
	  Cuisines = htmlHelper.GetEnumSelectList<CusineType>();
	  if (resturantId.HasValue)
	  {
		Resturant = resturantData.GetById(resturantId.Value);
		PageTitle = "Edit " + Resturant.Name;
		PageHeading = "Editing " + Resturant.Name;
	  }
	  else
	  {
		Resturant = new Resturant();
		/*Resturant.Location =*/
		PageTitle = "Add Resturant";
		PageHeading = PageTitle;
	  }
	  if (Resturant == null)
	  {
		return RedirectToPage("./NotFound");
	  }
	  return Page();
	}

	public IActionResult OnPost()
	{
	  if (Resturant.Id > 0)
	  {
		PageTitle = "Edit " + Resturant.Name;
		PageHeading = "Editing " + Resturant.Name;
	  }
	  else
	  {
		PageTitle = "Add Resturant";
		PageHeading = PageTitle;
	  }

	  /*ModelState["Location"].AttemptedValue*/
	  if (!ModelState.IsValid)
	  {
		Cuisines = htmlHelper.GetEnumSelectList<CusineType>();
		return Page();
	  }

	  if (Resturant.Id > 0)
	  {
		resturantData.Update(Resturant);
		TempData["Message"] = "Successfully edited resturant!";
	  }
	  else
	  {
		resturantData.Add(Resturant);
		TempData["Message"] = "Successfully added resturant!";
	  }
	  resturantData.Commit();
	  return RedirectToPage("./Detail", new { resturantId = Resturant.Id });
	}
  }
}
