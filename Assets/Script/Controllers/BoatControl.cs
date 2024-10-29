using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UIElements;

public class BoatControl : ClickAction
{
    Boat boatModel;
    IUserAction userAction;

    public void DealClick()
    {
        // 前置条件：船没有满员
        // 如果能上船，角色应到的位置就是船所在的位置
        // 如果不能上船，角色应到的位置就是原来的位置
        if (boatModel.roles[0] != null || boatModel.roles[1] != null)
        {
            userAction.MoveBoat();
        }
    }

    public BoatControl()
    {
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
    }
    public void CreateBoat(Vector3 position)
    {
        if (boatModel != null)
        {
            Object.DestroyImmediate(boatModel.boat);
        }
        boatModel = new Boat(position);
        boatModel.boat.GetComponent<Click>().setClickAction(this);
    }

    public Boat GetBoatModel()
    {
        return boatModel;
    }

    public Vector3 AddRole(Role roleModel)
    {
        int index = -1;
        if (boatModel.roles[0] == null) index = 0;
        else if (boatModel.roles[1] == null) index = 1;

        if (index == -1) return roleModel.role.transform.localPosition;

        boatModel.roles[index] = roleModel;
        roleModel.inBoat = true;
        roleModel.role.transform.parent = boatModel.boat.transform;
        if (roleModel.isPriest) boatModel.priestCount++;
        else boatModel.devilCount++;
        return Position.role_boat[index];
    }

    //将角色从船上移到岸上
    public void RemoveRole(Role roleModel)
    {
        for (int i = 0; i < 2; ++i)
        {
            if (boatModel.roles[i] == roleModel)
            {
                boatModel.roles[i] = null;
                if (roleModel.isPriest) boatModel.priestCount--;
                else boatModel.devilCount--;
                break;
            }
        }
    }


}
