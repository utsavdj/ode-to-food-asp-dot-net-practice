using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
  public interface IResturantData
  {
	/*IEnumerable<Resturant> GetAll();*/
	IEnumerable<Resturant> GetResturantsByName(string name);
	Resturant GetById(int id);
	Resturant Update(Resturant updatedResturant);
	Resturant Add(Resturant newResturant);
	int Commit();
  }

  public class InMemoryResturantData : IResturantData
  {
	List<Resturant> resturants;

	public InMemoryResturantData()
	{
	  resturants = new List<Resturant>()
	  {
		new Resturant { Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine =  CusineType.Italian },
		new Resturant { Id = 2, Name = "Cinnamon Club", Location = "London", Cuisine =  CusineType.Italian },
		new Resturant { Id = 3, Name = "La Costa", Location = "California", Cuisine =  CusineType.Mexican }
	  };
	}

	/*public IEnumerable<Resturant> GetAll()
	{
	  return from r in resturants
			 orderby r.Name
			 select r;
	}*/

	public IEnumerable<Resturant> GetResturantsByName(string name = null)
	{
	  return from r in resturants
			 where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
			 orderby r.Name
			 select r;
	}

	public Resturant GetById(int id)
	{
	  return resturants.SingleOrDefault(r => r.Id == id);
	}

	public Resturant Update(Resturant updatedResturant)
	{
	  Resturant resturant = resturants.SingleOrDefault(r => r.Id == updatedResturant.Id);
	  if (resturant != null)
	  {
		resturant.Name = updatedResturant.Name;
		resturant.Location = updatedResturant.Location;
		resturant.Cuisine = updatedResturant.Cuisine;
	  }
	  return resturant;
	}

	public Resturant Add(Resturant newResturant)
	{
	  resturants.Add(newResturant);
	  newResturant.Id = resturants.Max(r => r.Id) + 1;
	  return newResturant;
	}

	public int Commit()
	{
	  return 0;
	}
  }
}
