﻿using System.Collections.Generic;
using Enum;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
       #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] 
        private PlayerManager playerManager;
        
        [SerializeField] 
        private Animator animator;
        
        #endregion

        #region Private Variables
        
        private PlayerAnimationStates _currentAnimationState;

        private float _velocityX, _velocityZ;

        private float _acceleration, _decelaration;

        private Dictionary<WeaponTypes, PlayerAnimationStates> _animationStatesMap;

        #endregion

        #endregion
        private void Awake()
        {
            Init();
            DefineDictionary();
        }
        private void DefineDictionary()
        {
            _animationStatesMap = new Dictionary<WeaponTypes, PlayerAnimationStates>()
            {
                {WeaponTypes.PistolBullet, PlayerAnimationStates.Pistol},
                {WeaponTypes.RifleBullet, PlayerAnimationStates.Riffle},
                {WeaponTypes.PumpBullet, PlayerAnimationStates.ShotGun},
                {WeaponTypes.TurretBullet, PlayerAnimationStates.MiniGun},
            };
        }
        private void Init()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayTurretAnimation(bool onTurretHold)
        {
            animator.SetLayerWeight(2,onTurretHold ? 1 : 0);
            //animator.SetTrigger("TurretHold");
        }
        
        public void PlayAnimation(XZInputParams inputParams)
        { 
            if (playerManager.CurrentAreaType == AreaType.BattleOn)
            {
                animator.SetLayerWeight(1,1);
                animator.SetBool("IsBattleOn",true);
                ChangeAnimations(_animationStatesMap[playerManager.WeaponType]);
                animator.SetBool("Aimed",true);
                _velocityX = inputParams.XValue;
                _velocityZ = inputParams.ZValue;
                
                if (_velocityZ < 0.1f)                                        
                {
                    _velocityZ += Time.deltaTime * _acceleration;
                }
                if (_velocityX > -0.1f && Mathf.Abs(_velocityZ) <= 0.2f)      
                {
                    _velocityX -= Time.deltaTime * _acceleration;
                }
                if (_velocityX < 0.1f && Mathf.Abs(_velocityZ) <= 0.2f)        
                {
                    _velocityX += Time.deltaTime * _acceleration;
                }
                if (_velocityZ > 0.0f)                                         
                {
                    _velocityZ -= Time.deltaTime * _decelaration;
                }
                if (_velocityX < 0.0f)
                {
                    _velocityX += Time.deltaTime * _decelaration;
                }
                if (_velocityX > 0.0f)
                {
                    _velocityX -= Time.deltaTime * _decelaration;
                }
                if ( _velocityX!= 0.0f &&(_velocityX > -0.05f && _velocityX<0.05f))
                {
                    _velocityX = 0.0f;
                }
                
                animator.SetFloat("VelocityX",_velocityX);
                animator.SetFloat("VelocityZ",_velocityZ);

                if (new Vector2(inputParams.XValue,inputParams.ZValue).sqrMagnitude == 0)
                {
                    AimTarget(playerManager.EnemyTarget);
                }
            }
            else
            {
                animator.SetBool("Aimed",false);
                animator.SetLayerWeight(1,0);
                animator.SetBool("IsBattleOn",false);
                ChangeAnimations(new Vector2(inputParams.XValue,inputParams.ZValue).sqrMagnitude > 0
                    ? PlayerAnimationStates.Run
                    : PlayerAnimationStates.Idle);
            }
        }
        public void ChangeAnimations(PlayerAnimationStates animationStates)
        {
            if (animationStates == _currentAnimationState) return;
             animator.Play(animationStates.ToString());
            _currentAnimationState = animationStates;
            if (playerManager.CurrentAreaType != AreaType.BaseDefense) return;
            animator.SetBool("Aimed",false);
            animator.SetLayerWeight(1,0);
            animator.SetBool("IsBattleOn",false);
        } 
        public void AimTarget(bool hasTarget)
        {
            animator.SetBool("Aimed",hasTarget);
        }
    }
}