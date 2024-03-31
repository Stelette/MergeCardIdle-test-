using UnityEngine;

namespace Implementation.AssetsManagment
{
	public interface IAssetsProvider
	{
		public GameObject Instantiate(string path);
		public GameObject Instantiate(string path, Vector3 at);
	}
}

