﻿using System;
using Data.ValueObject;
using Datas.ValueObject;
using Enum;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
       #region Self Variables
       
               #region Public Variables
               
               #endregion
       
               #region Serialized Variables

               [SerializeField]
               private new Rigidbody rigidbody;
               [SerializeField]
               private PlayerManager manager;
               
               #endregion
       
               #region Private Variables
               
               private PlayerMovementData _data;
       
               private Vector2 _inputVector;
       
               private bool _isReadyToMove;
               
               
               #endregion
       
               #endregion
               public void SetMovementData(PlayerMovementData movementData)
               {
                   _data = movementData;
               }
               public void UpdateInputValues(XZInputParams inputParams)
               {
                   _inputVector = new Vector2(inputParams.XValue,inputParams.ZValue);
                   EnableMovement(_inputVector.sqrMagnitude > 0);
               }
       
        
               public void LookAtTarget(Transform enemyTarget)
               {
                   if(enemyTarget == null) return;
                   transform.LookAt(new Vector3(enemyTarget.position.x, 0, enemyTarget.position.z), Vector3.up * 3f);
                       
               }

               private void LateUpdate()
               {
                   if (manager.EnemyTarget == null) 
                       return; 
                   LookAtTarget(manager.EnemyList[0].GetTransform());
               }

               private void EnableMovement(bool movementStatus)
               {
                   _isReadyToMove = movementStatus;
               }
               private void FixedUpdate()
               {
                   PlayerMove();
               }
               private void PlayerMove()
               {
                   if (_isReadyToMove&&!manager.IsPlayerDeath)
                   {
                       var velocity = rigidbody.velocity; 
                       velocity = new Vector3(-_inputVector.x,velocity.y, -_inputVector.y)*_data.Speed;
                       rigidbody.velocity = velocity;
                       if (!manager.EnemyTarget)
                       {
                           RotatePlayer();
                       }
                   }
                   else if(rigidbody.velocity != Vector3.zero)
                   {
                       rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
                   }
               }
               private void RotatePlayer()
               {
                   Vector3 movementDirection = new Vector3(-_inputVector.x, 0, -_inputVector.y);
                   if (movementDirection == Vector3.zero) return;
                   Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                   rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, toRotation,30);
               }
               public void DisableMovement(InputHandlers inputHandlers)
               {
                   if (inputHandlers != InputHandlers.Turret)
                   {
                       manager.SetTurretAnimation(true);
                       return;
                   }
                   rigidbody.velocity = Vector3.zero;
                   transform.rotation = new Quaternion(0, .5f, 0, 0);
               }

               public void PlayerDeath()
               {
                   manager.IsPlayerDeath = true;
               }
    }
}