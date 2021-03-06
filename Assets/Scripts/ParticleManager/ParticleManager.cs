﻿using Buildings;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ParticleManager
{
    public class ParticleManager : MonoBehaviour
    {
        [Required]
        public Material cashMaterial;
        [Required]
        public Material projectMaterial;
        [Required]
        public ParticleSystem particleSystem;

        private ParticleSystemRenderer particleSystemRenderer;
        [Inject] private Container.Cloud cloud;
        private Company company;
        // Start is called before the first frame update
        void Awake()
        {
            company = cloud.Companies[0];
            particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();
            particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        }

        public void StartEffect(BuildingType buildingType,ParticleType particleType)
        {
            var position = particleSystem.gameObject.transform.position;
            position = Vector3.zero;
        
            position =
                company.GetOffice(buildingType).transform.position + Vector3.up * 30;
            particleSystem.gameObject.transform.position = position;
        
            switch (particleType)
            {
                case ParticleType.CASH:
                    particleSystemRenderer.material = cashMaterial;
                    break;
                case ParticleType.PROJECT:
                    particleSystemRenderer.material = projectMaterial;
                    break;
                default:
                    break;
            }
            particleSystem.Play();
        }
    }
}
