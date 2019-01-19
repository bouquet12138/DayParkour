using UnityEngine;

namespace gameBase
{
	public class ParticleAutoDestruction : MonoBehaviour {

		private ParticleSystem[] mParticleSystems;
 
		void Start()
		{
			mParticleSystems = GetComponentsInChildren<ParticleSystem>();
		}
	
		void Update ()
		{
			bool allStopped = true;
 
			foreach (ParticleSystem ps in mParticleSystems)
			{
				if (!ps.isStopped)
				{
					allStopped = false;
				}
			}
 
			if (allStopped)
				GameObject.Destroy(gameObject);
		}
	
	}
}
